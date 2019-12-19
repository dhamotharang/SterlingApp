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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/gldefinition")]
    [UsesDisposableService]
    public class GLDefinitionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLDefinitionApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateglDefinition")]
        public HttpResponseMessage UpdateGLDefinition(HttpRequestMessage request, [FromBody]GLDefinition glDefinitionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glDefinition = _CoreService.UpdateGLDefinition(glDefinitionModel);

                return request.CreateResponse<GLDefinition>(HttpStatusCode.OK, glDefinition);
            });
        }


        [HttpPost]
        [Route("deleteglDefinition")]
        public HttpResponseMessage DeleteGLDefinition(HttpRequestMessage request, [FromBody]int glDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLDefinition glDefinition = _CoreService.GetGLDefinition(glDefinitionId);

                if (glDefinition != null)
                {
                    _CoreService.DeleteGLDefinition(glDefinitionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glDefinition found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getglDefinition/{glDefinitionId}")]
        public HttpResponseMessage GetGLDefinition(HttpRequestMessage request, int glDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLDefinition glDefinition = _CoreService.GetGLDefinition(glDefinitionId);

                // notice no need to create a seperate model object since GLDefinition entity will do just fine
                response = request.CreateResponse<GLDefinition>(HttpStatusCode.OK, glDefinition);

                return response;
            });
        }


        [HttpGet]
        [Route("availableglDefinitions")]
        public HttpResponseMessage GetAvailableGLDefinitions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLDefinition[] glDefinitions = _CoreService.GetAllGLDefinitions();

                return request.CreateResponse<GLDefinition[]>(HttpStatusCode.OK, glDefinitions);
            });
        }
    }
}
