using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/general")]
    [UsesDisposableService]
    public class GeneralApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GeneralApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updategeneral")]
        public HttpResponseMessage UpdateGeneral(HttpRequestMessage request, [FromBody]General generalModel)
        {
            return GetHttpResponse(request, () =>
            {
                var general = _CoreService.UpdateGeneral(generalModel);

                return request.CreateResponse<General>(HttpStatusCode.OK, general);
            });
        }

        [HttpPost]
        [Route("deletegeneral")]
        public HttpResponseMessage DeleteGeneral(HttpRequestMessage request, [FromBody]int generalId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                General general = _CoreService.GetGeneral(generalId);

                if (general != null)
                {
                    _CoreService.DeleteGeneral(generalId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No general found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getgeneral")]
        public HttpResponseMessage GetGeneral(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                General general = _CoreService.GetFirstGeneral();

                // notice no need to create a seperate model object since General entity will do just fine
                response = request.CreateResponse<General>(HttpStatusCode.OK, general);

                return response;
            });
        }
    }
}
