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
    [RoutePrefix("api/ccfanalysisoverdraftstrlb")]
    [UsesDisposableService]
    public class CcfAnalysisOverDraftSTRLBApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CcfAnalysisOverDraftSTRLBApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateccfanalysisoverdraftstrlb")]
        public HttpResponseMessage UpdateCcfAnalysisOverDraftSTRLB(HttpRequestMessage request, [FromBody]CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlbModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ccfanalysisoverdraftstrlb = _IFRS9Service.UpdateCcfAnalysisOverDraftSTRLB(ccfanalysisoverdraftstrlbModel);

                return request.CreateResponse<CcfAnalysisOverDraftSTRLB>(HttpStatusCode.OK, ccfanalysisoverdraftstrlb);
            });
        }

        [HttpPost]
        [Route("deleteccfanalysisoverdraftstrlb")]
        public HttpResponseMessage DeleteCcfAnalysisOverDraftSTRLB(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlb = _IFRS9Service.GetCcfAnalysisOverDraftSTRLB(ID);

                if (ccfanalysisoverdraftstrlb != null)
                {
                    _IFRS9Service.DeleteCcfAnalysisOverDraftSTRLB(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CcfAnalysisOverDraftSTRLB found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getccfanalysisoverdraftstrlb/{ID}")]
        public HttpResponseMessage GetCcfAnalysisOverDraftSTRLB(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CcfAnalysisOverDraftSTRLB ccfanalysisoverdraftstrlb = _IFRS9Service.GetCcfAnalysisOverDraftSTRLB(ID);

                // notice no need to create a seperate model object since CcfAnalysisOverDraftSTRLB entity will do just fine
                response = request.CreateResponse<CcfAnalysisOverDraftSTRLB>(HttpStatusCode.OK, ccfanalysisoverdraftstrlb);

                return response;
            });
        }

        [HttpGet]
        [Route("availableccfanalysisoverdraftstrlbs/{defaultCount}")]
        public HttpResponseMessage GetAvailableCcfAnalysisOverDraftSTRLBs(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                CcfAnalysisOverDraftSTRLB[] ccfanalysisoverdraftstrlbs = _IFRS9Service.GetCcfAnalysisOverDraftSTRLBs(defaultCount);
                return request.CreateResponse<CcfAnalysisOverDraftSTRLB[]>(HttpStatusCode.OK, ccfanalysisoverdraftstrlbs);
            });
        }



        [HttpGet]
        [Route("getCcfAnalysisOverDraftSTRLBbysearch/{searchParam}")]
        public HttpResponseMessage GetCcfAnalysisOverDraftSTRLBBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CcfAnalysisOverDraftSTRLB[] ccfanalysisoverdraftstrlbs = _IFRS9Service.GetCcfAnalysisOverDraftSTRLBBySearch(searchParam);
                return request.CreateResponse<CcfAnalysisOverDraftSTRLB[]>(HttpStatusCode.OK, ccfanalysisoverdraftstrlbs.ToArray());
            });
        }





    }
}
