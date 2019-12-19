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
    [RoutePrefix("api/memounit")]
    [UsesDisposableService]
    public class MemoUnitApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MemoUnitApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatememounit")]
        public HttpResponseMessage UpdateMemoUnit(HttpRequestMessage request, [FromBody]MemoUnits memounitModel)
        {
            return GetHttpResponse(request, () =>
            {
                var memounit = _MPRCoreService.UpdateMemoUnits(memounitModel);

                return request.CreateResponse<MemoUnits>(HttpStatusCode.OK, memounit);
            });
        }

        [HttpPost]
        [Route("deletememounit")]
        public HttpResponseMessage DeleteMemoUnit(HttpRequestMessage request, [FromBody]int memounitId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MemoUnits memounit = _MPRCoreService.GetMemoUnits(memounitId);

                if (memounit != null)
                {
                    _MPRCoreService.DeleteMemoUnits(memounitId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No memounit found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmemounit/{memounitId}")]
        public HttpResponseMessage GetMemoUnit(HttpRequestMessage request, int memounitId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MemoUnits memounit = _MPRCoreService.GetMemoUnits(memounitId);

                // notice no need to create a seperate model object since MemoUnit entity will do just fine
                response = request.CreateResponse<MemoUnits>(HttpStatusCode.OK, memounit);

                return response;
            });
        }


        [HttpGet]
        [Route("availablememounits")]
        public HttpResponseMessage GetAvailableMemoUnits(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MemoUnits[] memounits = _MPRCoreService.GetAllMemoUnits();

                return request.CreateResponse<MemoUnits[]>(HttpStatusCode.OK, memounits);
            });
        }
    }
}
