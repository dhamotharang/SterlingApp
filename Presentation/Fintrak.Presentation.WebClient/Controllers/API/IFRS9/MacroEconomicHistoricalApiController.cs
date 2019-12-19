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
    [RoutePrefix("api/macroeconomichistorical")]
    [UsesDisposableService]
    public class MacroEconomicHistoricalApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroEconomicHistoricalApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroEconomicHistorical")]
        public HttpResponseMessage UpdateMacroEconomicHistorical(HttpRequestMessage request, [FromBody]MacroEconomicHistorical macroEconomicHistoricalModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroEconomicHistorical = _IFRS9Service.UpdateMacroEconomicHistorical(macroEconomicHistoricalModel);

                return request.CreateResponse<MacroEconomicHistorical>(HttpStatusCode.OK, macroEconomicHistorical);
            });
        }

        [HttpPost]
        [Route("deletemacroEconomicHistorical")]
        public HttpResponseMessage DeleteMacroEconomicHistorical(HttpRequestMessage request, [FromBody]int macroEconomicHistoricalId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroEconomicHistorical macroEconomicHistorical = _IFRS9Service.GetMacroEconomicHistorical(macroEconomicHistoricalId);

                if (macroEconomicHistorical != null)
                {
                    _IFRS9Service.DeleteMacroEconomicHistorical(macroEconomicHistoricalId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroEconomicHistorical found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroEconomicHistorical/{macroEconomicHistoricalId}")]
        public HttpResponseMessage GetMacroEconomicHistorical(HttpRequestMessage request,int macroEconomicHistoricalId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroEconomicHistorical macroEconomicHistorical = _IFRS9Service.GetMacroEconomicHistorical(macroEconomicHistoricalId);

                // notice no need to create a seperate model object since MacroEconomicHistorical entity will do just fine
                response = request.CreateResponse<MacroEconomicHistorical>(HttpStatusCode.OK, macroEconomicHistorical);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroEconomicHistoricals")]
        public HttpResponseMessage GetAvailableMacroEconomicHistoricals(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroEconomicHistoricalData[] macroEconomicHistoricals = _IFRS9Service.GetAllMacroEconomicHistoricals();

                return request.CreateResponse<MacroEconomicHistoricalData[]>(HttpStatusCode.OK, macroEconomicHistoricals);
            });
        }
    }
}
