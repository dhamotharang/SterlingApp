using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mprglmapping")]
    [UsesDisposableService]
    public class MPRGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRGLMappingApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updatemprglMapping")]
        public HttpResponseMessage UpdateMPRGLMapping(HttpRequestMessage request, [FromBody]MPRGLMapping mprglMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprglMapping = _MPRPLService.UpdateMPRGLMapping(mprglMappingModel);

                return request.CreateResponse<MPRGLMapping>(HttpStatusCode.OK, mprglMapping);
            });
        }

        [HttpPost]
        [Route("deletemprglMapping")]
        public HttpResponseMessage DeleteMPRGLMapping(HttpRequestMessage request, [FromBody]int mprglMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MPRGLMapping mprglMapping = _MPRPLService.GetMPRGLMapping(mprglMappingId);

                if (mprglMapping != null)
                {
                    _MPRPLService.DeleteMPRGLMapping(mprglMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No mprglMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmprglMapping/{mprglMappingId}")]
        public HttpResponseMessage GetMPRGLMapping(HttpRequestMessage request,int mprglMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRGLMapping mprglMapping = _MPRPLService.GetMPRGLMapping(mprglMappingId);

                // notice no need to create a seperate model object since MPRGLMapping entity will do just fine
                response = request.CreateResponse<MPRGLMapping>(HttpStatusCode.OK, mprglMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemprglMappings")]
        public HttpResponseMessage GetAvailableMPRGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRGLMappingData[] mprglMappings = _MPRPLService.GetAllMPRGLMappings();

                return request.CreateResponse<MPRGLMappingData[]>(HttpStatusCode.OK, mprglMappings);
            });
        }

        [HttpGet]
        [Route("getunmappedgl")]
        public HttpResponseMessage GetUnMappedGL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                KeyValueData[] gls = _MPRPLService.GetUnMappedPLGLs();

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, gls);
            });
        }

    }
}
