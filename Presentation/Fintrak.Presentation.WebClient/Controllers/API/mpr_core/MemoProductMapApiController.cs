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
    [RoutePrefix("api/memoproductmap")]
    [UsesDisposableService]
    public class MemoProductMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MemoProductMapApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatememoproductmaps")]
        public HttpResponseMessage UpdateMemoProductMap(HttpRequestMessage request, [FromBody]MemoProductMap memoproductmapsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var memoproductmaps = _MPRCoreService.UpdateMemoProductMap(memoproductmapsModel);

                return request.CreateResponse<MemoProductMap>(HttpStatusCode.OK, memoproductmaps);
            });
        }


        [HttpPost]
        [Route("deletememoproductmaps")]
        public HttpResponseMessage DeleteMemoProductMap(HttpRequestMessage request, [FromBody]int memoproductmapsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MemoProductMap memoproductmaps = _MPRCoreService.GetMemoProductMap(memoproductmapsId);

                if (memoproductmaps != null)
                {
                    _MPRCoreService.DeleteMemoProductMap(memoproductmapsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No memo product maps found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getmemoproductmaps/{memoproductmapsId}")]
        public HttpResponseMessage GetMemoProductMap(HttpRequestMessage request, int memoproductmapsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MemoProductMap memoproductmaps = _MPRCoreService.GetMemoProductMap(memoproductmapsId);

                // notice no need to create a seperate model object since MemoProductMap entity will do just fine
                response = request.CreateResponse<MemoProductMap>(HttpStatusCode.OK, memoproductmaps);

                return response;
            });
        }


        [HttpGet]
        [Route("availablememoproductmaps")]
        public HttpResponseMessage GetAvailableMemoProductMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MemoProductMapData[] memoproductmaps = _MPRCoreService.GetAllMemoProductMaps();

                return request.CreateResponse<MemoProductMapData[]>(HttpStatusCode.OK, memoproductmaps);
            });
        }
    }
}
