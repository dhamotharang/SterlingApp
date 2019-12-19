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
    [RoutePrefix("api/captionmapping")]
    [UsesDisposableService]
    public class CaptionMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CaptionMappingApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatecaptionMapping")]
        public HttpResponseMessage UpdateCaptionMapping(HttpRequestMessage request, [FromBody]CaptionMapping captionMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var captionMapping = _MPRCoreService.UpdateCaptionMapping(captionMappingModel);

                return request.CreateResponse<CaptionMapping>(HttpStatusCode.OK, captionMapping);
            });
        }


        [HttpPost]
        [Route("deletecaptionMapping")]
        public HttpResponseMessage DeleteCaptionMapping(HttpRequestMessage request, [FromBody]int captionMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CaptionMapping captionMapping = _MPRCoreService.GetCaptionMapping(captionMappingId);

                if (captionMapping != null)
                {
                    _MPRCoreService.DeleteCaptionMapping(captionMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CaptionMapping found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getcaptionMapping/{captionMappingId}")]
        public HttpResponseMessage GetCaptionMapping(HttpRequestMessage request, int captionMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CaptionMapping captionMapping = _MPRCoreService.GetCaptionMapping(captionMappingId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<CaptionMapping>(HttpStatusCode.OK, captionMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("availablecaptionMappings")]
        public HttpResponseMessage GetAvailableCaptionMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CaptionMapping[] captionMappings = _MPRCoreService.GetAllCaptionMappings();

                return request.CreateResponse<CaptionMapping[]>(HttpStatusCode.OK, captionMappings);
            });
        }
    }
}
