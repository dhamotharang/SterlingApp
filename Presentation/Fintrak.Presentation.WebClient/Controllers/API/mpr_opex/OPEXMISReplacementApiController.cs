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
    [RoutePrefix("api/opexmisreplacement")]
    [UsesDisposableService]
    public class OpexMISReplacementApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexMISReplacementApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexmisreplacement")]
        public HttpResponseMessage UpdateOpexMISReplacement(HttpRequestMessage request, [FromBody]OpexMISReplacement opexmisreplacementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexmisreplacement = _MPROPEXService.UpdateOpexMISReplacement(opexmisreplacementModel);

                return request.CreateResponse<OpexMISReplacement>(HttpStatusCode.OK, opexmisreplacement);
            });
        }

        [HttpPost]
        [Route("deleteopexmisReplacement")]
        public HttpResponseMessage DeleteOpexMISReplacement(HttpRequestMessage request, [FromBody]int opexmisReplacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexMISReplacement opexmisReplacement = _MPROPEXService.GetOpexMISReplacement(opexmisReplacementId);

                if (opexmisReplacement != null)
                {
                    _MPROPEXService.DeleteOpexMISReplacement(opexmisReplacementId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No opexmisReplacement found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexmisReplacement/{opexmisReplacementId}")]
        public HttpResponseMessage GetOpexMISReplacement(HttpRequestMessage request, int opexmisReplacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexMISReplacement opexmisReplacement = _MPROPEXService.GetOpexMISReplacement(opexmisReplacementId);

                // notice no need to create a seperate model object since OpexMISReplacement entity will do just fine
                response = request.CreateResponse<OpexMISReplacement>(HttpStatusCode.OK, opexmisReplacement);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexmisReplacements")]
        public HttpResponseMessage GetAvailableOpexMISReplacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexMISReplacementData[] opexmisReplacements = _MPROPEXService.GetAllOpexMISReplacements();

                return request.CreateResponse<OpexMISReplacementData[]>(HttpStatusCode.OK, opexmisReplacements);
            });
        }
    }
}
