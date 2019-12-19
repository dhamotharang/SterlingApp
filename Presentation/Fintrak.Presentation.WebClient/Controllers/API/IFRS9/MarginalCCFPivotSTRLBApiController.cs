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
    [RoutePrefix("api/marginalccfpivotstrlb")]
    [UsesDisposableService]
    public class MarginalCCFPivotSTRLBApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MarginalCCFPivotSTRLBApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemarginalccfpivotstrlb")]
        public HttpResponseMessage UpdateMarginalCCFPivotSTRLB(HttpRequestMessage request, [FromBody]MarginalCCFPivotSTRLB marginalccfpivotstrlbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var marginalccfpivotstrlb = _IFRS9Service.UpdateMarginalCCFPivotSTRLB(marginalccfpivotstrlbModel);

                return request.CreateResponse<MarginalCCFPivotSTRLB>(HttpStatusCode.OK, marginalccfpivotstrlb);
            });
        }

        [HttpPost]
        [Route("deletemarginalccfpivotstrlb")]
        public HttpResponseMessage DeleteMarginalCCFPivotSTRLB(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MarginalCCFPivotSTRLB marginalccfpivotstrlb = _IFRS9Service.GetMarginalCCFPivotSTRLB(ID);

                if (marginalccfpivotstrlb != null)
                {
                    _IFRS9Service.DeleteMarginalCCFPivotSTRLB(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MarginalCCFPivotSTRLB found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmarginalccfpivotstrlb/{ID}")]
        public HttpResponseMessage GetMarginalCCFPivotSTRLB(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MarginalCCFPivotSTRLB marginalccfpivotstrlb = _IFRS9Service.GetMarginalCCFPivotSTRLB(ID);

                // notice no need to create a seperate model object since MarginalCCFPivotSTRLB entity will do just fine
                response = request.CreateResponse<MarginalCCFPivotSTRLB>(HttpStatusCode.OK, marginalccfpivotstrlb);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemarginalccfpivotstrlbs/{defaultCount}")]
        public HttpResponseMessage GetAvailableMarginalCCFPivotSTRLBs(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                MarginalCCFPivotSTRLB[] marginalccfpivotstrlbs = _IFRS9Service.GetMarginalCCFPivotSTRLBs(defaultCount);
                return request.CreateResponse<MarginalCCFPivotSTRLB[]>(HttpStatusCode.OK, marginalccfpivotstrlbs);
            });
        }



        [HttpGet]
        [Route("getMarginalCCFPivotSTRLBbysearch/{searchParam}")]
        public HttpResponseMessage GetMarginalCCFPivotSTRLBBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                MarginalCCFPivotSTRLB[] marginalccfpivotstrlbs = _IFRS9Service.GetMarginalCCFPivotSTRLBBySearch(searchParam);
                return request.CreateResponse<MarginalCCFPivotSTRLB[]>(HttpStatusCode.OK, marginalccfpivotstrlbs.ToArray());
            });
        }





    }
}
