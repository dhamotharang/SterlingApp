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
    [RoutePrefix("api/postingglmapping")]
    [UsesDisposableService]
    public class PostingGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PostingGLMappingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatepostingglmapping")]
        public HttpResponseMessage UpdatePostingGLMapping(HttpRequestMessage request, [FromBody]PostingGLMapping postingglmappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var postingglmapping = _IFRS9Service.UpdatePostingGLMapping(postingglmappingModel);

                return request.CreateResponse<PostingGLMapping>(HttpStatusCode.OK, postingglmapping);
            });
        }

        [HttpPost]
        [Route("deletepostingglmapping")]
        public HttpResponseMessage DeletePostingGLMapping(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PostingGLMapping postingglmapping = _IFRS9Service.GetPostingGLMapping(ID);

                if (postingglmapping != null)
                {
                    _IFRS9Service.DeletePostingGLMapping(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No PostingGLMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getpostingglmapping/{ID}")]
        public HttpResponseMessage GetPostingGLMapping(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PostingGLMapping postingglmapping = _IFRS9Service.GetPostingGLMapping(ID);

                // notice no need to create a seperate model object since PostingGLMapping entity will do just fine
                response = request.CreateResponse<PostingGLMapping>(HttpStatusCode.OK, postingglmapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availablepostingglmappings")]
        public HttpResponseMessage GetAvailablePostingGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PostingGLMapping[] postingglmappings = _IFRS9Service.GetAllPostingGLMappings();

                return request.CreateResponse<PostingGLMapping[]>(HttpStatusCode.OK, postingglmappings);
            });
        }
    }
}
