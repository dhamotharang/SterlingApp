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
    [RoutePrefix("api/neasharingratiofcy")]
    [UsesDisposableService]
    public class NEASharingRatioFcyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NEASharingRatioFcyApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateneasharingratiofcy")]
        public HttpResponseMessage UpdateNEASharingRatioFcy(HttpRequestMessage request, [FromBody]NEASharingRatioFcy nEASharingRatioFcyModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nEASharingRatioFcy = _MPROPEXService.UpdateNEASharingRatioFcy(nEASharingRatioFcyModel);

                return request.CreateResponse<NEASharingRatioFcy>(HttpStatusCode.OK, nEASharingRatioFcy);
            });
        }

        [HttpPost]
        [Route("deleteneasharingratiofcy")]
        public HttpResponseMessage DeleteNEASharingRatioFcy(HttpRequestMessage request, [FromBody]int nEASharingRatioFcyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NEASharingRatioFcy nEASharingRatioFcy = _MPROPEXService.GetNEASharingRatioFcy(nEASharingRatioFcyId);

                if (nEASharingRatioFcy != null)
                {
                    _MPROPEXService.DeleteNEASharingRatioFcy(nEASharingRatioFcyId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getneasharingratiofcy/{neasharingratiofcyId}")]
        public HttpResponseMessage GetNEASharingRatioFcy(HttpRequestMessage request, int nEASharingRatioFcyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NEASharingRatioFcy nEASharingRatioFcy = _MPROPEXService.GetNEASharingRatioFcy(nEASharingRatioFcyId);

                // notice no need to create a seperate model object since NEASharingRatioFcy entity will do just fine
                response = request.CreateResponse<NEASharingRatioFcy>(HttpStatusCode.OK, nEASharingRatioFcy);

                return response;
            });
        }

        [HttpGet]
        [Route("availableneasharingratiofcy")]
        public HttpResponseMessage GetAvailableNEASharingRatioFcy(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NEASharingRatioFcy[] nEASharingRatioFcy = _MPROPEXService.GetAllNEASharingRatioFcys();


                return request.CreateResponse<NEASharingRatioFcy[]>(HttpStatusCode.OK, nEASharingRatioFcy);
            });
        }
    }
}
