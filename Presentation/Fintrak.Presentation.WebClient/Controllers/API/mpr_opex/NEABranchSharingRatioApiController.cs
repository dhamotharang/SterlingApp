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
    [RoutePrefix("api/neabranchsharingratio")]
    [UsesDisposableService]
    public class NEABranchSharingRatioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NEABranchSharingRatioApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateneabranchsharingratio")]
        public HttpResponseMessage UpdateNEABranchSharingRatio(HttpRequestMessage request, [FromBody]NEABranchSharingRatio nEABranchSharingRatioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nEABranchSharingRatio = _MPROPEXService.UpdateNEABranchSharingRatio(nEABranchSharingRatioModel);

                return request.CreateResponse<NEABranchSharingRatio>(HttpStatusCode.OK, nEABranchSharingRatio);
            });
        }

        [HttpPost]
        [Route("deleteneabranchsharingratio")]
        public HttpResponseMessage DeleteNEABranchSharingRatio(HttpRequestMessage request, [FromBody]int nEABranchSharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NEABranchSharingRatio nEABranchSharingRatio = _MPROPEXService.GetNEABranchSharingRatio(nEABranchSharingRatioId);

                if (nEABranchSharingRatio != null)
                {
                    _MPROPEXService.DeleteNEABranchSharingRatio(nEABranchSharingRatioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getneabranchsharingratio/{neabranchsharingratioId}")]
        public HttpResponseMessage GetNEABranchSharingRatio(HttpRequestMessage request, int nEABranchSharingRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NEABranchSharingRatio nEABranchSharingRatio = _MPROPEXService.GetNEABranchSharingRatio(nEABranchSharingRatioId);

                // notice no need to create a seperate model object since NEABranchSharingRatio entity will do just fine
                response = request.CreateResponse<NEABranchSharingRatio>(HttpStatusCode.OK, nEABranchSharingRatio);

                return response;
            });
        }

        [HttpGet]
        [Route("availableneabranchsharingratio")]
        public HttpResponseMessage GetAvailableNEABranchSharingRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NEABranchSharingRatio[] nEABranchSharingRatio = _MPROPEXService.GetAllNEABranchSharingRatios();


                return request.CreateResponse<NEABranchSharingRatio[]>(HttpStatusCode.OK, nEABranchSharingRatio);
            });
        }
    }
}
