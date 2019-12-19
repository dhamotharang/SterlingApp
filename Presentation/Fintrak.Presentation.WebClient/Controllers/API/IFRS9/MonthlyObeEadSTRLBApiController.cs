using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/monthlyobeeadstrlb")]
    [UsesDisposableService]
    public class MonthlyObeEadSTRLBApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MonthlyObeEadSTRLBApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)  {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemonthlyobeeadstrlb")]
        public HttpResponseMessage UpdateMonthlyObeEadSTRLB(HttpRequestMessage request, [FromBody]MonthlyObeEadSTRLB monthlyobeeadstrlbModel)
        {
            return GetHttpResponse(request, () =>  {
                var monthlyobeeadstrlb = _IFRS9Service.UpdateMonthlyObeEadSTRLB(monthlyobeeadstrlbModel);
                return request.CreateResponse<MonthlyObeEadSTRLB>(HttpStatusCode.OK, monthlyobeeadstrlb);
            });
        }

        [HttpPost]
        [Route("deletemonthlyobeeadstrlb")]
        public HttpResponseMessage DeleteMonthlyObeEadSTRLB(HttpRequestMessage request, [FromBody]int ID) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                MonthlyObeEadSTRLB monthlyobeeadstrlb = _IFRS9Service.GetMonthlyObeEadSTRLB(ID);
                if (monthlyobeeadstrlb != null) {
                    _IFRS9Service.DeleteMonthlyObeEadSTRLB(ID);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MonthlyObeEadSTRLB found under that ID.");
                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlyobeeadstrlb/{ID}")]
        public HttpResponseMessage GetMonthlyObeEadSTRLB(HttpRequestMessage request,int ID) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                MonthlyObeEadSTRLB monthlyobeeadstrlb = _IFRS9Service.GetMonthlyObeEadSTRLB(ID);
                // notice no need to create a seperate model object since MonthlyObeEadSTRLB entity will do just fine
                response = request.CreateResponse<MonthlyObeEadSTRLB>(HttpStatusCode.OK, monthlyobeeadstrlb);
                return response;
            });
        }

        [HttpGet]
        [Route("availablemonthlyobeeadstrlbs/{defaultCount}")]
        public HttpResponseMessage GetAvailableMonthlyObeEadSTRLBs(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                MonthlyObeEadSTRLB[] monthlyobeeadstrlbs = _IFRS9Service.GetMonthlyObeEadSTRLBs(defaultCount);
                return request.CreateResponse<MonthlyObeEadSTRLB[]>(HttpStatusCode.OK, monthlyobeeadstrlbs);
            });
        }



        [HttpGet]
        [Route("getMonthlyObeEadSTRLBbysearch/{searchParam}")]
        public HttpResponseMessage GetMonthlyObeEadSTRLBBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                MonthlyObeEadSTRLB[] monthlyobeeadstrlbs = _IFRS9Service.GetMonthlyObeEadSTRLBBySearch(searchParam);
                return request.CreateResponse<MonthlyObeEadSTRLB[]>(HttpStatusCode.OK, monthlyobeeadstrlbs.ToArray());
            });
        }





    }
}
