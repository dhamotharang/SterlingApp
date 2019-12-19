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
    [RoutePrefix("api/expenseglmapping")]
    [UsesDisposableService]
    public class ExpenseGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExpenseGLMappingApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateexpenseGLMapping")]
        public HttpResponseMessage UpdateExpenseGLMapping(HttpRequestMessage request, [FromBody]ExpenseGLMapping expenseGLMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var expenseGLMapping = _MPROPEXService.UpdateExpenseGLMapping(expenseGLMappingModel);

                return request.CreateResponse<ExpenseGLMapping>(HttpStatusCode.OK, expenseGLMapping);
            });
        }

        [HttpPost]
        [Route("deleteexpenseGLMapping")]
        public HttpResponseMessage DeleteExpenseGLMapping(HttpRequestMessage request, [FromBody]int expenseGLId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExpenseGLMapping expenseGLMapping = _MPROPEXService.GetExpenseGLMapping(expenseGLId);

                if (expenseGLMapping != null)
                {
                    _MPROPEXService.DeleteExpenseGLMapping(expenseGLId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No expenseGLMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseGLMapping/{expenseGLId}")]
        public HttpResponseMessage GetExpenseGLMapping(HttpRequestMessage request, int expenseGLId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseGLMapping expenseGLMapping = _MPROPEXService.GetExpenseGLMapping(expenseGLId);

                // notice no need to create a seperate model object since ExpenseGLMapping entity will do just fine
                response = request.CreateResponse<ExpenseGLMapping>(HttpStatusCode.OK, expenseGLMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexpenseGLMappings")]
        public HttpResponseMessage GetAvailableExpenseGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExpenseGLMappingData[] expenseGLMappings = _MPROPEXService.GetAllExpenseGLMappings();

                return request.CreateResponse<ExpenseGLMappingData[]>(HttpStatusCode.OK, expenseGLMappings);
            });
        }
    }
}
