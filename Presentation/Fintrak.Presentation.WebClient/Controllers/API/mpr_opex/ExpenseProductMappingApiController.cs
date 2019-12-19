using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/expenseproductmapping")]
    [UsesDisposableService]
    public class ExpenseProductMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExpenseProductMappingApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateexpenseProductMapping")]
        public HttpResponseMessage UpdateExpenseProductMapping(HttpRequestMessage request, [FromBody]ExpenseProductMapping expenseProductMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var expenseProductMapping = _MPROPEXService.UpdateExpenseProductMapping(expenseProductMappingModel);

                return request.CreateResponse<ExpenseProductMapping>(HttpStatusCode.OK, expenseProductMapping);
            });
        }

        [HttpPost]
        [Route("deleteexpenseProductMapping")]
        public HttpResponseMessage DeleteExpenseProductMapping(HttpRequestMessage request, [FromBody]int expenseProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExpenseProductMapping expenseProductMapping = _MPROPEXService.GetExpenseProductMapping(expenseProductId);

                if (expenseProductMapping != null)
                {
                    _MPROPEXService.DeleteExpenseProductMapping(expenseProductId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No expenseProductMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseProductMapping/{expenseProductId}")]
        public HttpResponseMessage GetExpenseProductMapping(HttpRequestMessage request, int expenseProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseProductMapping expenseProductMapping = _MPROPEXService.GetExpenseProductMapping(expenseProductId);

                // notice no need to create a seperate model object since ExpenseProductMapping entity will do just fine
                response = request.CreateResponse<ExpenseProductMapping>(HttpStatusCode.OK, expenseProductMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexpenseProductMappings")]
        public HttpResponseMessage GetAvailableExpenseProductMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExpenseProductMappingData[] expenseProductMappings = _MPROPEXService.GetAllExpenseProductMappings();

                return request.CreateResponse<ExpenseProductMappingData[]>(HttpStatusCode.OK, expenseProductMappings);
            });
        }
    }
}
