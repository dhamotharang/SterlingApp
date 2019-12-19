using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/revenuebudget")]
    [UsesDisposableService]
    public class RevenueBudgetApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RevenueBudgetApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updaterevenuebudget")]
        public HttpResponseMessage UpdateRevenueBudget(HttpRequestMessage request, [FromBody]RevenueBudget revenueBudgetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var revenueBudget = _MPRPLService.UpdateRevenueBudget(revenueBudgetModel);

                return request.CreateResponse<RevenueBudget>(HttpStatusCode.OK, revenueBudget);
            });
        }

        [HttpPost]
        [Route("deleterevenueBudget")]
        public HttpResponseMessage DeleteRevenueBudget(HttpRequestMessage request, [FromBody]int revenueBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RevenueBudget revenueBudget = _MPRPLService.GetRevenueBudget(revenueBudgetId);

                if (revenueBudget != null)
                {
                    _MPRPLService.DeleteRevenueBudget(revenueBudgetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No revenueBudget found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getrevenueBudget/{revenueBudgetId}")]
        public HttpResponseMessage GetRevenueBudget(HttpRequestMessage request, int revenueBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenueBudget revenueBudget = _MPRPLService.GetRevenueBudget(revenueBudgetId);

                // notice no need to create a seperate model object since RevenueBudget entity will do just fine
                response = request.CreateResponse<RevenueBudget>(HttpStatusCode.OK, revenueBudget);

                return response;
            });
        }

        [HttpGet]
        [Route("getrevenuebudgets/{year}")]
        public HttpResponseMessage GetRevenueBudgets(HttpRequestMessage request,string year)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueBudget[] revenueBudgets = _MPRPLService.GetAllRevenueBudgets(year);

                return request.CreateResponse<RevenueBudget[]>(HttpStatusCode.OK, revenueBudgets);
            });
        }

        [HttpGet]
        [Route("getrevenuebudgetsearch/{searchValue}")]
        public HttpResponseMessage GetRevenueBudgetSearch(HttpRequestMessage request, string searchValue)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueBudget[] revenueBudgets = _MPRPLService.GetRevenueBudgets(searchValue);

                return request.CreateResponse<RevenueBudget[]>(HttpStatusCode.OK, revenueBudgets);
            });
        }
    }
}
