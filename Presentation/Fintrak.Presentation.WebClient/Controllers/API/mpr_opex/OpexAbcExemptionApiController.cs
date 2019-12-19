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
    [RoutePrefix("api/opexabcexemption")]
    [UsesDisposableService]
    public class OpexAbcExemptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexAbcExemptionApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexabcexemption")]
        public HttpResponseMessage UpdateOpexAbcExemption(HttpRequestMessage request, [FromBody]OpexAbcExemption opexAbcExemptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexAbcExemption = _MPROPEXService.UpdateOpexAbcExemption(opexAbcExemptionModel);

                return request.CreateResponse<OpexAbcExemption>(HttpStatusCode.OK, opexAbcExemption);
            });
        }

        [HttpPost]
        [Route("deleteopexAbcExemption")]
        public HttpResponseMessage DeleteOpexAbcExemption(HttpRequestMessage request, [FromBody]int opexAbcExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexAbcExemption opexAbcExemption = _MPROPEXService.GetOpexAbcExemption(opexAbcExemptionId);

                if (opexAbcExemption != null)
                {
                    _MPROPEXService.DeleteOpexAbcExemption(opexAbcExemptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No opexAbcExemption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexAbcExemption/{opexAbcExemptionId}")]
        public HttpResponseMessage GetOpexAbcExemption(HttpRequestMessage request, int opexAbcExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexAbcExemption opexAbcExemption = _MPROPEXService.GetOpexAbcExemption(opexAbcExemptionId);

                // notice no need to create a seperate model object since OpexAbcExemption entity will do just fine
                response = request.CreateResponse<OpexAbcExemption>(HttpStatusCode.OK, opexAbcExemption);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexAbcExemption")]
        public HttpResponseMessage GetAvailableOpexAbcExemption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexAbcExemptionData[] opexAbcExemption = _MPROPEXService.GetAllOpexAbcExemptions();

                return request.CreateResponse<OpexAbcExemptionData[]>(HttpStatusCode.OK, opexAbcExemption);
            });
        }
    }
}
