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
    [RoutePrefix("api/neasharingratio")]
    [UsesDisposableService]
    public class NEASharingRatioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NEASharingRatioApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateneasharingratio")]
        public HttpResponseMessage UpdateNEASharingRatio(HttpRequestMessage request, [FromBody]NEASharingRatio nEASharingRatioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nEASharingRatio = _MPROPEXService.UpdateNEASharingRatio(nEASharingRatioModel);

                return request.CreateResponse<NEASharingRatio>(HttpStatusCode.OK, nEASharingRatio);
            });
        }

        [HttpPost]
        [Route("deleteneasharingratio")]
        public HttpResponseMessage DeleteNEASharingRatio(HttpRequestMessage request, [FromBody]int nEASharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NEASharingRatio nEASharingRatio = _MPROPEXService.GetNEASharingRatio(nEASharingRatioId);

                if (nEASharingRatio != null)
                {
                    _MPROPEXService.DeleteNEASharingRatio(nEASharingRatioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getneasharingratio/{neasharingratioId}")]
        public HttpResponseMessage GetNEASharingRatio(HttpRequestMessage request, int nEASharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NEASharingRatio nEASharingRatio = _MPROPEXService.GetNEASharingRatio(nEASharingRatioId);

                // notice no need to create a seperate model object since NEASharingRatio entity will do just fine
                response = request.CreateResponse<NEASharingRatio>(HttpStatusCode.OK, nEASharingRatio);

                return response;
            });
        }

        [HttpGet]
        [Route("availableneasharingratio")]
        public HttpResponseMessage GetAvailableNEASharingRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NEASharingRatio[] nEASharingRatio = _MPROPEXService.GetAllNEASharingRatios();


                return request.CreateResponse<NEASharingRatio[]>(HttpStatusCode.OK, nEASharingRatio);
            });
        }
    }
}
