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
    [RoutePrefix("api/ratiocaption")]
    [UsesDisposableService]
    public class RatioCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RatioCaptionApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateratiocaption")]
        public HttpResponseMessage UpdateRatioCaption(HttpRequestMessage request, [FromBody]RatioCaption ratiocaptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratiocaption = _FinstatService.UpdateRatioCaption(ratiocaptionModel);

                return request.CreateResponse<RatioCaption>(HttpStatusCode.OK, ratiocaption);
            });
        }

        [HttpPost]
        [Route("deleteratiocaption")]
        public HttpResponseMessage DeleteRatioCaption(HttpRequestMessage request, [FromBody]int RatioCaptionID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RatioCaption ratiocaption = _FinstatService.GetRatioCaption(RatioCaptionID);

                if (ratiocaption != null)
                {
                    _FinstatService.DeleteRatioCaption(RatioCaptionID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No RatioCaption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getratiocaption/{RatioCaptionID}")]
        public HttpResponseMessage GetRatioCaption(HttpRequestMessage request,int RatioCaptionID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RatioCaption ratiocaption = _FinstatService.GetRatioCaption(RatioCaptionID);

                // notice no need to create a seperate model object since RatioCaption entity will do just fine
                response = request.CreateResponse<RatioCaption>(HttpStatusCode.OK, ratiocaption);

                return response;
            });
        }

        [HttpGet]
        [Route("availableratiocaptions")]
        public HttpResponseMessage GetAvailableRatioCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RatioCaption[] ratiocaptions = _FinstatService.GetAllRatioCaptions();

                return request.CreateResponse<RatioCaption[]>(HttpStatusCode.OK, ratiocaptions);
            });
        }
    }
}
