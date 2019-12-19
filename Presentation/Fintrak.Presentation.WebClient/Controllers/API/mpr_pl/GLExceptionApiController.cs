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
    [RoutePrefix("api/glexception")]
    [UsesDisposableService]
    public class GLExceptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLExceptionApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateglexception")]
        public HttpResponseMessage UpdateGLException(HttpRequestMessage request, [FromBody]GLException glExceptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glException = _MPRPLService.UpdateGLException(glExceptionModel);

                return request.CreateResponse<GLException>(HttpStatusCode.OK, glException);
            });
        }

        [HttpPost]
        [Route("deleteglException")]
        public HttpResponseMessage DeleteGLException(HttpRequestMessage request, [FromBody]int glExceptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLException glException = _MPRPLService.GetGLException(glExceptionId);

                if (glException != null)
                {
                    _MPRPLService.DeleteGLException(glExceptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glException found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglException/{glExceptionId}")]
        public HttpResponseMessage GetGLException(HttpRequestMessage request, int glExceptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLException glException = _MPRPLService.GetGLException(glExceptionId);

                // notice no need to create a seperate model object since GLException entity will do just fine
                response = request.CreateResponse<GLException>(HttpStatusCode.OK, glException);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglException")]
        public HttpResponseMessage GetAvailableGLException(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLExceptionData[] glException = _MPRPLService.GetAllGLExceptions();

                return request.CreateResponse<GLExceptionData[]>(HttpStatusCode.OK, glException);
            });
        }
    }
}
