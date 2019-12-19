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
    [RoutePrefix("api/macroeconomicsnpl")]
    [UsesDisposableService]
    public class MacroEconomicsNPLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroEconomicsNPLApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroeconomicsnpl")]
        public HttpResponseMessage UpdateMacroEconomicsNPL(HttpRequestMessage request, [FromBody]MacroEconomicsNPL macroeconomicsnplModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroeconomicsnpl = _IFRS9Service.UpdateMacroEconomicsNPL(macroeconomicsnplModel);

                return request.CreateResponse<MacroEconomicsNPL>(HttpStatusCode.OK, macroeconomicsnpl);
            });
        }

        [HttpPost]
        [Route("deletemacroeconomicsnpl")]
        public HttpResponseMessage DeleteMacroEconomicsNPL(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroEconomicsNPL macroeconomicsnpl = _IFRS9Service.GetMacroEconomicsNPL(Id);

                if (macroeconomicsnpl != null)
                {
                    _IFRS9Service.DeleteMacroEconomicsNPL(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroeconomicsnpl found under that Id.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroeconomicsnpl/{id}")]
        public HttpResponseMessage GetMacroEconomicsNPL(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroEconomicsNPL macroeconomicsnpl = _IFRS9Service.GetMacroEconomicsNPL(Id);

                // notice no need to create a seperate model object since MacroEconomicsNPL entity will do just fine
                response = request.CreateResponse<MacroEconomicsNPL>(HttpStatusCode.OK, macroeconomicsnpl);

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroeconomicsnplbyscenario/{scenario}")]
        public HttpResponseMessage GetMacroEconomicsNPLByScenario(HttpRequestMessage request, string scenario)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroEconomicsNPL[] macroeconomicsnpl = _IFRS9Service.GetMacroEconomicsNPLByScenario(scenario);

                // notice no need to create a seperate model object since MacroEconomicsNPL entity will do just fine
                response = request.CreateResponse<MacroEconomicsNPL[]>(HttpStatusCode.OK, macroeconomicsnpl);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroeconomicsnpls")]
        public HttpResponseMessage GetAvailableMacroEconomicsNPLs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroEconomicsNPL[] macroeconomicsnpls = _IFRS9Service.GetAllMacroEconomicsNPLs();

                return request.CreateResponse<MacroEconomicsNPL[]>(HttpStatusCode.OK, macroeconomicsnpls);
            });
        }
    }
}
