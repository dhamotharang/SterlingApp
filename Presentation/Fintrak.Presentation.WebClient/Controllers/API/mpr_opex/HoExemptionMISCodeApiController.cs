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
    [RoutePrefix("api/hoexemptionmiscode")]
    [UsesDisposableService]
    public class HoExemptionMISCodeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HoExemptionMISCodeApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatehoexemptionmiscode")]
        public HttpResponseMessage UpdateHoExemptionMISCode(HttpRequestMessage request, [FromBody]HoExemptionMISCode hoExemptionMISCodeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var hoExemptionMISCode = _MPROPEXService.UpdateHoExemptionMISCode(hoExemptionMISCodeModel);

                return request.CreateResponse<HoExemptionMISCode>(HttpStatusCode.OK, hoExemptionMISCode);
            });
        }

        [HttpPost]
        [Route("deletehoexemptionmiscode")]
        public HttpResponseMessage DeleteHoExemptionMISCode(HttpRequestMessage request, [FromBody]int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HoExemptionMISCode hoExemptionMISCode = _MPROPEXService.GetHoExemptionMISCode(id);

                if (hoExemptionMISCode != null)
                {
                    _MPROPEXService.DeleteHoExemptionMISCode(id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethoexemptionmiscode/{id}")]
        public HttpResponseMessage GetHoExemptionMISCode(HttpRequestMessage request, int id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HoExemptionMISCode hoExemptionMISCode = _MPROPEXService.GetHoExemptionMISCode(id);

                // notice no need to create a seperate model object since HoExemptionMISCode entity will do just fine
                response = request.CreateResponse<HoExemptionMISCode>(HttpStatusCode.OK, hoExemptionMISCode);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehoexemptionmiscode")]
        public HttpResponseMessage GetAvailableHoExemptionMISCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HoExemptionMISCode[] hoExemptionMISCode = _MPROPEXService.GetAllHoExemptionMISCodes();


                return request.CreateResponse<HoExemptionMISCode[]>(HttpStatusCode.OK, hoExemptionMISCode);
            });
        }
    }
}
