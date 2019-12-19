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
    [RoutePrefix("api/abcratio")]
    [UsesDisposableService]
    public class AbcRatioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AbcRatioApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateabcratio")]
        public HttpResponseMessage UpdateAbcRatio(HttpRequestMessage request, [FromBody]AbcRatio abcRatioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var abcRatio = _MPRCoreService.UpdateAbcRatio(abcRatioModel);

                return request.CreateResponse<AbcRatio>(HttpStatusCode.OK, abcRatio);
            });
        }


        [HttpPost]
        [Route("deleteabcratio")]
        public HttpResponseMessage DeleteAbcRatio(HttpRequestMessage request, [FromBody]int abcRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AbcRatio abcRatio = _MPRCoreService.GetAbcRatio(abcRatioId);

                if (abcRatio != null)
                {
                    _MPRCoreService.DeleteAbcRatio(abcRatioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No AbcRatio found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getabcratio/{abcRatioId}")]
        public HttpResponseMessage GetAbcRatio(HttpRequestMessage request, int abcRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AbcRatio abcRatio = _MPRCoreService.GetAbcRatio(abcRatioId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<AbcRatio>(HttpStatusCode.OK, abcRatio);

                return response;
            });
        }


        [HttpGet]
        [Route("availableabcratio")]
        public HttpResponseMessage GetAvailableAbcRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AbcRatio[] abcRatio = _MPRCoreService.GetAllAbcRatio();

                return request.CreateResponse<AbcRatio[]>(HttpStatusCode.OK, abcRatio);
            });
        }
    }
}
