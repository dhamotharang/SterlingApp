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
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/glaarchive")]
    [UsesDisposableService]
    public class GLAArchiveApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLAArchiveApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("gladjustmentarchivesbyrundate/{rundate}")]
        public HttpResponseMessage GetGLAArchivesByRundate(HttpRequestMessage request, DateTime rundate)
        {
            return GetHttpResponse(request, () =>
            {
                GLAArchive[] glaarchives = _FinstatService.GetGLAArchivesByRundate(rundate);

                return request.CreateResponse<GLAArchive[]>(HttpStatusCode.OK, glaarchives);
            });
        }
    }
}
