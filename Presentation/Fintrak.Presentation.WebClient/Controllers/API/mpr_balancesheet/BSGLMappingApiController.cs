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
    [RoutePrefix("api/bsglmapping")]
    [UsesDisposableService]
    public class BSGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BSGLMappingApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebsglmapping")]
        public HttpResponseMessage UpdateBSGLMapping(HttpRequestMessage request, [FromBody]BSGLMapping bsGLMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bsGLMapping = _MPRBSService.UpdateBSGLMapping(bsGLMappingModel);

                return request.CreateResponse<BSGLMapping>(HttpStatusCode.OK, bsGLMapping);
            });
        }

        [HttpPost]
        [Route("deletebsGLMapping")]
        public HttpResponseMessage DeleteBSGLMapping(HttpRequestMessage request, [FromBody]int bsGLMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BSGLMapping bsGLMapping = _MPRBSService.GetBSGLMapping(bsGLMappingId);

                if (bsGLMapping != null)
                {
                    _MPRBSService.DeleteBSGLMapping(bsGLMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bsGLMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbsGLMapping/{bsGLMappingId}")]
        public HttpResponseMessage GetBSGLMapping(HttpRequestMessage request, int bsGLMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSGLMapping bsGLMapping = _MPRBSService.GetBSGLMapping(bsGLMappingId);

                // notice no need to create a seperate model object since BSGLMapping entity will do just fine
                response = request.CreateResponse<BSGLMapping>(HttpStatusCode.OK, bsGLMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebsglmappings")]
        public HttpResponseMessage GetAvailableBSGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSGLMappingData[] bsGLMappings = _MPRBSService.GetAllBSGLMappings();

                return request.CreateResponse<BSGLMappingData[]>(HttpStatusCode.OK, bsGLMappings);
            });
        }
    }
}
