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
    [RoutePrefix("api/misreplacement")]
    [UsesDisposableService]
    public class MISReplacementApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MISReplacementApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatemisreplacement")]
        public HttpResponseMessage UpdateMISReplacement(HttpRequestMessage request, [FromBody]MISReplacement teammisreplacementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teammisreplacement = _MPRCoreService.UpdateMISReplacement(teammisreplacementModel);

                return request.CreateResponse<MISReplacement>(HttpStatusCode.OK, teammisreplacement);
            });
        }

        [HttpPost]
        [Route("deletemisReplacement")]
        public HttpResponseMessage DeleteMISReplacement(HttpRequestMessage request, [FromBody]int misReplacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MISReplacement misReplacement = _MPRCoreService.GetMISReplacement(misReplacementId);

                if (misReplacement != null)
                {
                    _MPRCoreService.DeleteMISReplacement(misReplacementId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No misReplacement found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmisReplacement/{misReplacementId}")]
        public HttpResponseMessage GetMISReplacement(HttpRequestMessage request, int misReplacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MISReplacement misReplacement = _MPRCoreService.GetMISReplacement(misReplacementId);

                // notice no need to create a seperate model object since MISReplacement entity will do just fine
                response = request.CreateResponse<MISReplacement>(HttpStatusCode.OK, misReplacement);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemisReplacements")]
        public HttpResponseMessage GetAvailableMISReplacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MISReplacement[] misReplacements = _MPRCoreService.GetAllMISReplacements();

                return request.CreateResponse<MISReplacement[]>(HttpStatusCode.OK, misReplacements);
            });
        }
    }
}
