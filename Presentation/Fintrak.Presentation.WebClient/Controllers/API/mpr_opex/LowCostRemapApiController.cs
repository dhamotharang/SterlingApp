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
    [RoutePrefix("api/lowcostremap")]
    [UsesDisposableService]
    public class LowCostRemapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LowCostRemapApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatelowcostremap")]
        public HttpResponseMessage UpdateLowCostRemap(HttpRequestMessage request, [FromBody]LowCostRemap lowCostRemapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lowCostRemap = _MPROPEXService.UpdateLowCostRemap(lowCostRemapModel);

                return request.CreateResponse<LowCostRemap>(HttpStatusCode.OK, lowCostRemap);
            });
        }

        [HttpPost]
        [Route("deletelowcostremap")]
        public HttpResponseMessage DeleteLowCostRemap(HttpRequestMessage request, [FromBody]int lowCostRemapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LowCostRemap lowCostRemap = _MPROPEXService.GetLowCostRemap(lowCostRemapId);

                if (lowCostRemap != null)
                {
                    _MPROPEXService.DeleteLowCostRemap(lowCostRemapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlowcostremap/{lowcostremapId}")]
        public HttpResponseMessage GetLowCostRemap(HttpRequestMessage request, int lowCostRemapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LowCostRemap lowCostRemap = _MPROPEXService.GetLowCostRemap(lowCostRemapId);

                // notice no need to create a seperate model object since LowCostRemap entity will do just fine
                response = request.CreateResponse<LowCostRemap>(HttpStatusCode.OK, lowCostRemap);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelowcostremap")]
        public HttpResponseMessage GetAvailableLowCostRemap(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LowCostRemap[] lowCostRemap = _MPROPEXService.GetAllLowCostRemaps();


                return request.CreateResponse<LowCostRemap[]>(HttpStatusCode.OK, lowCostRemap);
            });
        }
    }
}
