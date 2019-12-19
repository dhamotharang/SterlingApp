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
    [RoutePrefix("api/memoglmap")]
    [UsesDisposableService]
    public class MemoGLMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MemoGLMapApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatememoglmaps")]
        public HttpResponseMessage UpdateMemoGLMap(HttpRequestMessage request, [FromBody]MemoGLMap memoglmapsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var memoglmaps = _MPRCoreService.UpdateMemoGLMap(memoglmapsModel);

                return request.CreateResponse<MemoGLMap>(HttpStatusCode.OK, memoglmaps);
            });
        }


        [HttpPost]
        [Route("deletememoglmaps")]
        public HttpResponseMessage DeleteMemoGLMap(HttpRequestMessage request, [FromBody]int memoglmapsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MemoGLMap memoglmaps = _MPRCoreService.GetMemoGLMap(memoglmapsId);

                if (memoglmaps != null)
                {
                    _MPRCoreService.DeleteMemoGLMap(memoglmapsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No memoglmaps found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getmemoglmaps/{memoglmapsId}")]
        public HttpResponseMessage GetMemoGLMap(HttpRequestMessage request, int memoglmapsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MemoGLMap memoglmaps = _MPRCoreService.GetMemoGLMap(memoglmapsId);

                // notice no need to create a seperate model object since MemoGLMap entity will do just fine
                response = request.CreateResponse<MemoGLMap>(HttpStatusCode.OK, memoglmaps);

                return response;
            });
        }


        [HttpGet]
        [Route("availablememoglmaps")]
        public HttpResponseMessage GetAvailableMemoGLMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MemoGLMapData[] memoglmaps = _MPRCoreService.GetAllMemoGLMaps();

                return request.CreateResponse<MemoGLMapData[]>(HttpStatusCode.OK, memoglmaps);
            });
        }
    }
}
