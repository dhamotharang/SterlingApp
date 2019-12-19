using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/IfrsLgdScenarioByInstrument")]
    [UsesDisposableService]
    public class IfrsLgdScenarioByInstrumentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsLgdScenarioByInstrumentApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsLgdScenarioByInstrument")]
        public HttpResponseMessage UpdateIfrsLgdScenarioByInstrument(HttpRequestMessage request, [FromBody]IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrumentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsLgdScenarioByInstrument = _IFRS9Service.UpdateIfrsLgdScenarioByInstrument(IfrsLgdScenarioByInstrumentModel);

                return request.CreateResponse<IfrsLgdScenarioByInstrument>(HttpStatusCode.OK, IfrsLgdScenarioByInstrument);
            });
        }

        [HttpPost]
        [Route("deleteIfrsLgdScenarioByInstrument")]
        public HttpResponseMessage DeleteIfrsLgdScenarioByInstrument(HttpRequestMessage request, [FromBody]int InstrumentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrument = _IFRS9Service.GetIfrsLgdScenarioByInstrumentId(InstrumentId);

                if (IfrsLgdScenarioByInstrument != null)
                {
                    _IFRS9Service.DeleteIfrsLgdScenarioByInstrument(InstrumentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsLgdScenarioByInstrument found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsLgdScenarioByInstrumentId/{InstrumentId}")]
        public HttpResponseMessage GetIfrsLgdScenarioByInstrumentId(HttpRequestMessage request, int InstrumentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsLgdScenarioByInstrument IfrsLgdScenarioByInstrument = _IFRS9Service.GetIfrsLgdScenarioByInstrumentId(InstrumentId);

                // notice no need to create a seperate model object since IfrsLgdScenarioByInstrument entity will do just fine
                response = request.CreateResponse<IfrsLgdScenarioByInstrument>(HttpStatusCode.OK, IfrsLgdScenarioByInstrument);

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsLgdScenarioByInstrument/{type}")]
        public HttpResponseMessage GetIfrsLgdScenarioByInstrument(HttpRequestMessage request, string type)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsLgdScenarioByInstrument[] IfrsLgdScenarioByInstrument = _IFRS9Service.GetIfrsLgdScenarioByInstrument(type);

                // notice no need to create a seperate model object since IfrsLgdScenarioByInstrument entity will do just fine
                response = request.CreateResponse<IfrsLgdScenarioByInstrument[]>(HttpStatusCode.OK, IfrsLgdScenarioByInstrument);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsLgdScenarioByInstruments")]
        public HttpResponseMessage GetAvailableIfrsLgdScenarioByInstruments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsLgdScenarioByInstrument[] IfrsLgdScenarioByInstruments = _IFRS9Service.GetAllIfrsLgdScenarioByInstruments();

                return request.CreateResponse<IfrsLgdScenarioByInstrument[]>(HttpStatusCode.OK, IfrsLgdScenarioByInstruments);
            });
        }
    }
}
