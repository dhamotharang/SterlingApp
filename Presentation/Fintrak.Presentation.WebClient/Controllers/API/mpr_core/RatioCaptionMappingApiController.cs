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
    [RoutePrefix("api/ratiocaptionmapping")]
    [UsesDisposableService]
    public class RatioCaptionMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RatioCaptionMappingApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateratioCaptionMapping")]
        public HttpResponseMessage UpdateRatioCaptionMapping(HttpRequestMessage request, [FromBody]RatioCaptionMapping ratioCaptionMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratioCaptionMapping = _MPRCoreService.UpdateRatioCaptionMapping(ratioCaptionMappingModel);

                return request.CreateResponse<RatioCaptionMapping>(HttpStatusCode.OK, ratioCaptionMapping);
            });
        }


        [HttpPost]
        [Route("deleteratioCaptionMapping")]
        public HttpResponseMessage DeleteRatioCaptionMapping(HttpRequestMessage request, [FromBody]int ratioCaptionMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RatioCaptionMapping ratioCaptionMapping = _MPRCoreService.GetRatioCaptionMapping(ratioCaptionMappingId);

                if (ratioCaptionMapping != null)
                {
                    _MPRCoreService.DeleteRatioCaptionMapping(ratioCaptionMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No RatioCaptionMapping found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getratioCaptionMapping/{ratioCaptionMappingId}")]
        public HttpResponseMessage GetRatioCaptionMapping(HttpRequestMessage request, int ratioCaptionMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RatioCaptionMapping ratioCaptionMapping = _MPRCoreService.GetRatioCaptionMapping(ratioCaptionMappingId);

                // notice no need to create a seperate model object since RatioCaptionMapping entity will do just fine
                response = request.CreateResponse<RatioCaptionMapping>(HttpStatusCode.OK, ratioCaptionMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("availableratioCaptionMappings")]
        public HttpResponseMessage GetAvailableRatioCaptionMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RatioCaptionMapping[] ratioCaptionMappings = _MPRCoreService.GetAllRatioCaptionMappings();

                return request.CreateResponse<RatioCaptionMapping[]>(HttpStatusCode.OK, ratioCaptionMappings);
            });
        }
    }
}
