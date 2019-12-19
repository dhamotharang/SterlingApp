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
    [RoutePrefix("api/mprsetup")]
    [UsesDisposableService]
    public class MPRSetupApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRSetupApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatemprSetup")]
        public HttpResponseMessage UpdateSetup(HttpRequestMessage request, [FromBody]Setup mprSetupModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprSetup = _MPRCoreService.UpdateMPRSetup(mprSetupModel);

                return request.CreateResponse<Setup>(HttpStatusCode.OK, mprSetup);
            });
        }

        [HttpGet]
        [Route("getmprSetup")]
        public HttpResponseMessage GetFirstMPRSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Setup mprSetup = _MPRCoreService.GetFirstMPRSetup();

                // notice no need to create a seperate model object since MPRSetup entity will do just fine
                response = request.CreateResponse<Setup>(HttpStatusCode.OK, mprSetup);

                return response;
            });
        }

        [HttpGet]
        [Route("getmprFirstSetup")]
        public HttpResponseMessage GetFirstMPRSetups(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRSetupData[] mprSetup = _MPRCoreService.GetFirstMPRSetups();

                // notice no need to create a seperate model object since MPRSetup entity will do just fine
                response = request.CreateResponse<MPRSetupData[]>(HttpStatusCode.OK, mprSetup.ToArray());

                return response;
            });
        }
    }
}
