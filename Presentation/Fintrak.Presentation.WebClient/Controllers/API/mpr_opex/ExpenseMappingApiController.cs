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
    [RoutePrefix("api/expensemapping")]
    [UsesDisposableService]
    public class ExpenseMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExpenseMappingApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateexpenseMapping")]
        public HttpResponseMessage UpdateExpenseMapping(HttpRequestMessage request, [FromBody]ExpenseMapping expenseMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var expenseMapping = _MPROPEXService.UpdateExpenseMapping(expenseMappingModel);

                return request.CreateResponse<ExpenseMapping>(HttpStatusCode.OK, expenseMapping);
            });
        }

        [HttpPost]
        [Route("deleteexpenseMapping")]
        public HttpResponseMessage DeleteExpenseMapping(HttpRequestMessage request, [FromBody]int expenseMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExpenseMapping expenseMapping = _MPROPEXService.GetExpenseMapping(expenseMappingId);

                if (expenseMapping != null)
                {
                    _MPROPEXService.DeleteExpenseMapping(expenseMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No expenseMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseMapping/{expenseMappingId}")]
        public HttpResponseMessage GetExpenseMapping(HttpRequestMessage request, int expenseMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseMapping expenseMapping = _MPROPEXService.GetExpenseMapping(expenseMappingId);

                // notice no need to create a seperate model object since ExpenseMapping entity will do just fine
                response = request.CreateResponse<ExpenseMapping>(HttpStatusCode.OK, expenseMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexpenseMappings")]
        public HttpResponseMessage GetAvailableExpenseMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExpenseMappingData[] expenseMappings = _MPROPEXService.GetAllExpenseMappings();

                return request.CreateResponse<ExpenseMappingData[]>(HttpStatusCode.OK, expenseMappings);
            });
        }
    }
}
