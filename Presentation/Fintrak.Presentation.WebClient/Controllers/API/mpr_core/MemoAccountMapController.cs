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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]   
    [RoutePrefix("api/memoaccountmap")]
    [UsesDisposableService]
    public class MemoAccountMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MemoAccountMapApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatememoaccountmap")]
        public HttpResponseMessage UpdateMemoAccountMap(HttpRequestMessage request, [FromBody]MemoAccountMap memoaccountmapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var memoaccountmap = _MPRCoreService.UpdateMemoAccountMap(memoaccountmapModel);

                return request.CreateResponse<MemoAccountMap>(HttpStatusCode.OK, memoaccountmap);
            });
        }

        [HttpPost]
        [Route("deletememoaccountmap")]
        public HttpResponseMessage DeleteMemoAccountMap(HttpRequestMessage request, [FromBody]int memoaccountmapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MemoAccountMap memoaccountmap = _MPRCoreService.GetMemoAccountMap(memoaccountmapId);

                if (memoaccountmap != null)
                {
                    _MPRCoreService.DeleteMemoAccountMap(memoaccountmapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No memoaccountmap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmemoaccountmap/{memoaccountmapId}")]
        public HttpResponseMessage GetMemoAccountMap(HttpRequestMessage request, int memoaccountmapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MemoAccountMap memoaccountmap = _MPRCoreService.GetMemoAccountMap(memoaccountmapId);

                // notice no need to create a seperate model object since MemoAccountMap entity will do just fine
                response = request.CreateResponse<MemoAccountMap>(HttpStatusCode.OK, memoaccountmap);

                return response;
            });
        }


        [HttpGet]
        [Route("availablememoaccountmaps")]
        public HttpResponseMessage GetAvailableMemoAccountMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MemoAccountMapData[] memoaccountmaps = _MPRCoreService.GetAllMemoAccountMaps();

                return request.CreateResponse<MemoAccountMapData[]>(HttpStatusCode.OK, memoaccountmaps);
            });
        }
    }
}
