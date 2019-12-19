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
    [RoutePrefix("api/ratiodetail")]
    [UsesDisposableService]
    public class RatioDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RatioDetailApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateratiodetail")]
        public HttpResponseMessage UpdateRatioDetail(HttpRequestMessage request, [FromBody]RatioDetail ratiodetailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratiodetail = _FinstatService.UpdateRatioDetail(ratiodetailModel);

                return request.CreateResponse<RatioDetail>(HttpStatusCode.OK, ratiodetail);
            });
        }

        [HttpPost]
        [Route("deleteratiodetail")]
        public HttpResponseMessage DeleteRatioDetail(HttpRequestMessage request, [FromBody]int RatioID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RatioDetail ratiodetail = _FinstatService.GetRatioDetail(RatioID);

                if (ratiodetail != null)
                {
                    _FinstatService.DeleteRatioDetail(RatioID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No RatioDetail found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getratiodetail/{RatioID}")]
        public HttpResponseMessage GetRatioDetail(HttpRequestMessage request,int RatioID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RatioDetail ratiodetail = _FinstatService.GetRatioDetail(RatioID);

                // notice no need to create a seperate model object since RatioDetail entity will do just fine
                response = request.CreateResponse<RatioDetail>(HttpStatusCode.OK, ratiodetail);

                return response;
            });
        }

        [HttpGet]
        [Route("availableratiodetails")]
        public HttpResponseMessage GetAvailableRatioDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RatioDetail[] ratiodetails = _FinstatService.GetAllRatioDetails();

                return request.CreateResponse<RatioDetail[]>(HttpStatusCode.OK, ratiodetails);
            });
        }
    }
}
