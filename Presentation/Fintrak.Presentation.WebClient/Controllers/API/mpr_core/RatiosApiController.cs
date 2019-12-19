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
    [RoutePrefix("api/ratios")]
    [UsesDisposableService]
    public class RatiosApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RatiosApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateratios")]
        public HttpResponseMessage UpdateRatios(HttpRequestMessage request, [FromBody]Ratios ratiosModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratios = _MPRCoreService.UpdateRatios(ratiosModel);

                return request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);
            });
        }


        [HttpPost]
        [Route("deleteratios")]
        public HttpResponseMessage DeleteRatios(HttpRequestMessage request, [FromBody]int ratiosId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Ratios ratios = _MPRCoreService.GetRatios(ratiosId);

                if (ratios != null)
                {
                    _MPRCoreService.DeleteRatios(ratiosId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Ratios found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getratios/{ratiosId}")]
        public HttpResponseMessage GetRatios(HttpRequestMessage request, int ratiosId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Ratios ratios = _MPRCoreService.GetRatios(ratiosId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<Ratios>(HttpStatusCode.OK, ratios);

                return response;
            });
        }


        [HttpGet]
        [Route("availableratios")]
        public HttpResponseMessage GetAvailableRatios(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Ratios[] ratios = _MPRCoreService.GetAllRatios();

                return request.CreateResponse<Ratios[]>(HttpStatusCode.OK, ratios);
            });
        }
    }
}
