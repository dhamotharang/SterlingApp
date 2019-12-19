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
    [RoutePrefix("api/macroeconomicsvariablescenario")]
    [UsesDisposableService]
    public class MacroeconomicsVariableScenarioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroeconomicsVariableScenarioApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroeconomicsvariablescenario")]
        public HttpResponseMessage UpdateMacroeconomicsVariableScenario(HttpRequestMessage request, [FromBody]MacroeconomicsVariableScenario macroeconomicsvariablescenarioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroeconomicsvariablescenario = _IFRS9Service.UpdateMacroeconomicsVariableScenario(macroeconomicsvariablescenarioModel);

                return request.CreateResponse<MacroeconomicsVariableScenario>(HttpStatusCode.OK, macroeconomicsvariablescenario);
            });
        }

        [HttpPost]
        [Route("deletemacroeconomicsvariablescenario")]
        public HttpResponseMessage DeleteMacroeconomicsVariableScenario(HttpRequestMessage request, [FromBody]int MacroeconomicsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroeconomicsVariableScenario macroeconomicsVariableScenario = _IFRS9Service.GetMacroeconomicsVariableScenario(MacroeconomicsId);

                if (macroeconomicsVariableScenario != null)
                {
                    _IFRS9Service.DeleteMacroeconomicsVariableScenario(MacroeconomicsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MacroeconomicsVariableScenario found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroeconomicsvariablescenario/{MacroeconomicsId}")]
        public HttpResponseMessage GetMacroeconomicsVariableScenario(HttpRequestMessage request,int MacroeconomicsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroeconomicsVariableScenario macroeconomicsvariablescenario = _IFRS9Service.GetMacroeconomicsVariableScenario(MacroeconomicsId);

                // notice no need to create a seperate model object since MacroeconomicsVariableScenario entity will do just fine
                response = request.CreateResponse<MacroeconomicsVariableScenario>(HttpStatusCode.OK, macroeconomicsvariablescenario);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroeconomicsvariablescenarios")]
        public HttpResponseMessage GetAvailableMacroeconomicsVariableScenarios(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroeconomicsVariableScenario[] macroeconomicsvariablescenarios = _IFRS9Service.GetAllMacroeconomicsVariableScenarios();

                return request.CreateResponse<MacroeconomicsVariableScenario[]>(HttpStatusCode.OK, macroeconomicsvariablescenarios);
            });
        }

        [HttpGet]
        [Route("availablemacroeconomicsbyflag/{flag}")]
        public HttpResponseMessage GetAvailableMacroeconomicsByFlag(HttpRequestMessage request, int flag)
        {
            return GetHttpResponse(request, () =>
            {
                MacroeconomicsVariableScenario[] macroeconomicsvariablescenarios = _IFRS9Service.GetMacroeconomicsVariableScenariosByFlag(flag);

                return request.CreateResponse<MacroeconomicsVariableScenario[]>(HttpStatusCode.OK, macroeconomicsvariablescenarios);
            });
        }
    }
}
