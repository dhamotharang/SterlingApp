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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/gltype")]
    [UsesDisposableService]
    public class GLTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLTypeApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateglType")]
        public HttpResponseMessage UpdateGLType(HttpRequestMessage request, [FromBody]GLType glTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glType = _FinstatService.UpdateGLType(glTypeModel);

                return request.CreateResponse<GLType>(HttpStatusCode.OK, glType);
            });
        }

        [HttpPost]
        [Route("deleteglType")]
        public HttpResponseMessage DeleteGLType(HttpRequestMessage request, [FromBody]int glTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLType glType = _FinstatService.GetGLType(glTypeId);

                if (glType != null)
                {
                    _FinstatService.DeleteGLType(glTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglType/{glTypeId}")]
        public HttpResponseMessage GetGLType(HttpRequestMessage request,int glTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLType glType = _FinstatService.GetGLType(glTypeId);

                // notice no need to create a seperate model object since GLType entity will do just fine
                response = request.CreateResponse<GLType>(HttpStatusCode.OK, glType);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglTypes")]
        public HttpResponseMessage GetAvailableGLTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLType[] glTypes = _FinstatService.GetAllGLTypes();

                return request.CreateResponse<GLType[]>(HttpStatusCode.OK, glTypes);
            });
        }
    }
}
