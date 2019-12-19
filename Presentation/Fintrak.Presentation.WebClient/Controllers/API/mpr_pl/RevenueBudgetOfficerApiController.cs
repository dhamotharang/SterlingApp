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
    [RoutePrefix("api/revenuebudgetofficer")]
    [UsesDisposableService]
    public class RevenueBudgetOfficerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RevenueBudgetOfficerApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updaterevenuebudgetofficer")]
        public HttpResponseMessage UpdateRevenueBudgetOfficer(HttpRequestMessage request, [FromBody]RevenueBudgetOfficer revenueBudgetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var revenueBudget = _MPRPLService.UpdateRevenueBudgetOfficer(revenueBudgetModel);

                return request.CreateResponse<RevenueBudgetOfficer>(HttpStatusCode.OK, revenueBudget);
            });
        }

        [HttpPost]
        [Route("deleterevenuebudgetofficer")]
        public HttpResponseMessage DeleteRevenueBudgetOfficer(HttpRequestMessage request, [FromBody]int revenueBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RevenueBudgetOfficer revenueBudget = _MPRPLService.GetRevenueBudgetOfficer(revenueBudgetId);

                if (revenueBudget != null)
                {
                    _MPRPLService.DeleteRevenueBudgetOfficer(revenueBudgetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No revenueBudget found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getrevenuebudgetofficer/{revenueBudgetId}")]
        public HttpResponseMessage GetRevenueBudgetOfficer(HttpRequestMessage request, int revenueBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenueBudgetOfficer revenueBudget = _MPRPLService.GetRevenueBudgetOfficer(revenueBudgetId);

                // notice no need to create a seperate model object since RevenueBudgetOfficer entity will do just fine
                response = request.CreateResponse<RevenueBudgetOfficer>(HttpStatusCode.OK, revenueBudget);

                return response;
            });
        }

        [HttpGet]
        [Route("getrevenuebudgetofficers/{year}")]
        public HttpResponseMessage GetRevenueBudgetOfficers(HttpRequestMessage request,string year)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueBudgetOfficer[] revenueBudgets = _MPRPLService.GetAllRevenueBudgetOfficers(year);

                return request.CreateResponse<RevenueBudgetOfficer[]>(HttpStatusCode.OK, revenueBudgets);
            });
        }

        [HttpGet]
        [Route("getrevenuebudgetofficerssearch/{searchValue}")]
        public HttpResponseMessage GetRevenueBudgetOfficerSearch(HttpRequestMessage request, string searchValue)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueBudgetOfficer[] revenueBudgets = _MPRPLService.GetRevenueBudgetOfficers(searchValue);

                return request.CreateResponse<RevenueBudgetOfficer[]>(HttpStatusCode.OK, revenueBudgets);
            });
        }

    }
}
