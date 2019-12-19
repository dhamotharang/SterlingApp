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
    [RoutePrefix("api/fixedassetsharingratio")]
    [UsesDisposableService]
    public class FixedAssetSharingRatioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FixedAssetSharingRatioApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatefixedassetsharingratio")]
        public HttpResponseMessage UpdateFixedAssetSharingRatio(HttpRequestMessage request, [FromBody]FixedAssetSharingRatio fixedAssetSharingRatioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fixedAssetSharingRatio = _MPROPEXService.UpdateFixedAssetSharingRatio(fixedAssetSharingRatioModel);

                return request.CreateResponse<FixedAssetSharingRatio>(HttpStatusCode.OK, fixedAssetSharingRatio);
            });
        }

        [HttpPost]
        [Route("deletefixedAssetSharingRatio")]
        public HttpResponseMessage DeleteFixedAssetSharingRatio(HttpRequestMessage request, [FromBody]int fixedAssetSharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FixedAssetSharingRatio fixedAssetSharingRatio = _MPROPEXService.GetFixedAssetSharingRatio(fixedAssetSharingRatioId);

                if (fixedAssetSharingRatio != null)
                {
                    _MPROPEXService.DeleteFixedAssetSharingRatio(fixedAssetSharingRatioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getfixedAssetSharingRatio/{fixedAssetSharingRatioId}")]
        public HttpResponseMessage GetFixedAssetSharingRatio(HttpRequestMessage request, int fixedAssetSharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FixedAssetSharingRatio fixedAssetSharingRatio = _MPROPEXService.GetFixedAssetSharingRatio(fixedAssetSharingRatioId);

                // notice no need to create a seperate model object since FixedAssetSharingRatio entity will do just fine
                response = request.CreateResponse<FixedAssetSharingRatio>(HttpStatusCode.OK, fixedAssetSharingRatio);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefixedAssetSharingRatio")]
        public HttpResponseMessage GetAvailableFixedAssetSharingRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FixedAssetSharingRatio[] fixedAssetSharingRatio = _MPROPEXService.GetAllFixedAssetSharingRatios();


                return request.CreateResponse<FixedAssetSharingRatio[]>(HttpStatusCode.OK, fixedAssetSharingRatio);
            });
        }
    }
}
