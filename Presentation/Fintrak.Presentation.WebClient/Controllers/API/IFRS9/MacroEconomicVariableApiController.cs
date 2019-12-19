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
    [RoutePrefix("api/macroeconomicvariable")]
    [UsesDisposableService]
    public class MacroEconomicVariableApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroEconomicVariableApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroEconomicVariable")]
        public HttpResponseMessage UpdateMacroEconomicVariable(HttpRequestMessage request, [FromBody]MacroEconomicVariable macroEconomicVariableModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroEconomicVariable = _IFRS9Service.UpdateMacroEconomicVariable(macroEconomicVariableModel);

                return request.CreateResponse<MacroEconomicVariable>(HttpStatusCode.OK, macroEconomicVariable);
            });
        }

        [HttpPost]
        [Route("deletemacroEconomicVariable")]
        public HttpResponseMessage DeleteMacroEconomicVariable(HttpRequestMessage request, [FromBody]int macroEconomicVariableId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroEconomicVariable macroEconomicVariable = _IFRS9Service.GetMacroEconomicVariable(macroEconomicVariableId);

                if (macroEconomicVariable != null)
                {
                    _IFRS9Service.DeleteMacroEconomicVariable(macroEconomicVariableId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroEconomicVariable found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroEconomicVariable/{macroEconomicVariableId}")]
        public HttpResponseMessage GetMacroEconomicVariable(HttpRequestMessage request,int macroEconomicVariableId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroEconomicVariable macroEconomicVariable = _IFRS9Service.GetMacroEconomicVariable(macroEconomicVariableId);

                // notice no need to create a seperate model object since MacroEconomicVariable entity will do just fine
                response = request.CreateResponse<MacroEconomicVariable>(HttpStatusCode.OK, macroEconomicVariable);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroEconomicVariables")]
        public HttpResponseMessage GetAvailableMacroEconomicVariables(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroEconomicVariable[] macroEconomicVariables = _IFRS9Service.GetAllMacroEconomicVariables();

                return request.CreateResponse<MacroEconomicVariable[]>(HttpStatusCode.OK, macroEconomicVariables);
            });
        }
    }
}
