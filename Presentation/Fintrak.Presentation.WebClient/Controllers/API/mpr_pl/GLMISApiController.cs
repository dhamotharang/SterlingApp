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
    [RoutePrefix("api/glmis")]
    [UsesDisposableService]
    public class GLMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLMISApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateglmis")]
        public HttpResponseMessage UpdateGLMIS(HttpRequestMessage request, [FromBody]GLMIS glMISModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMIS = _MPRPLService.UpdateGLMIS(glMISModel);

                return request.CreateResponse<GLMIS>(HttpStatusCode.OK, glMIS);
            });
        }

        [HttpPost]
        [Route("deleteglMIS")]
        public HttpResponseMessage DeleteGLMIS(HttpRequestMessage request, [FromBody]int glMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLMIS glMIS = _MPRPLService.GetGLMIS(glMISId);

                if (glMIS != null)
                {
                    _MPRPLService.DeleteGLMIS(glMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglMIS/{glMISId}")]
        public HttpResponseMessage GetGLMIS(HttpRequestMessage request, int glMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMIS glMIS = _MPRPLService.GetGLMIS(glMISId);

                // notice no need to create a seperate model object since GLMIS entity will do just fine
                response = request.CreateResponse<GLMIS>(HttpStatusCode.OK, glMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMIS")]
        public HttpResponseMessage GetAvailableGLMIS(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLMISData[] glMIS = _MPRPLService.GetAllGLMISInfo();

                return request.CreateResponse<GLMISData[]>(HttpStatusCode.OK, glMIS);
            });
        }
    }
}
