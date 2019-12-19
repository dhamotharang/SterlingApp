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
    [RoutePrefix("api/opexrawexpense")]
    [UsesDisposableService]
    public class OpexRawExpenseApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexRawExpenseApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexRawExpense")]
        public HttpResponseMessage UpdateOpexRawExpense(HttpRequestMessage request, [FromBody]OpexRawExpense opexRawExpenseModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexRawExpense = _MPROPEXService.UpdateOpexRawExpense(opexRawExpenseModel);

                return request.CreateResponse<OpexRawExpense>(HttpStatusCode.OK, opexRawExpense);
            });
        }

        [HttpPost]
        [Route("deleteopexRawExpense")]
        public HttpResponseMessage DeleteOpexRawExpense(HttpRequestMessage request, [FromBody]int opexRawExpenseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexRawExpense opexRawExpense = _MPROPEXService.GetOpexRawExpense(opexRawExpenseId);

                if (opexRawExpense != null)
                {
                    _MPROPEXService.DeleteOpexRawExpense(opexRawExpenseId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No opexRawExpense found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexRawExpense/{opexRawExpenseId}")]
        public HttpResponseMessage GetOpexRawExpense(HttpRequestMessage request, int opexRawExpenseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexRawExpense opexRawExpense = _MPROPEXService.GetOpexRawExpense(opexRawExpenseId);

                // notice no need to create a seperate model object since OpexRawExpense entity will do just fine
                response = request.CreateResponse<OpexRawExpense>(HttpStatusCode.OK, opexRawExpense);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexRawExpenses")]
        public HttpResponseMessage GetAvailableOpexRawExpenses(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexRawExpense[] opexRawExpenses = _MPROPEXService.GetAllOpexRawExpenses();

                return request.CreateResponse<OpexRawExpense[]>(HttpStatusCode.OK, opexRawExpenses);
            });
        }
    }
}
