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
    [RoutePrefix("api/macroeconomic")]
    [UsesDisposableService]
    public class MacroEconomicApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroEconomicApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacroEconomic")]
        public HttpResponseMessage UpdateMacroEconomic(HttpRequestMessage request, [FromBody]MacroEconomic macroEconomicModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macroEconomic = _IFRS9Service.UpdateMacroEconomic(macroEconomicModel);

                return request.CreateResponse<MacroEconomic>(HttpStatusCode.OK, macroEconomic);
            });
        }

        [HttpPost]
        [Route("deletemacroEconomic")]
        public HttpResponseMessage DeleteMacroEconomic(HttpRequestMessage request, [FromBody]int macroEconomicId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroEconomic macroEconomic = _IFRS9Service.GetMacroEconomic(macroEconomicId);

                if (macroEconomic != null)
                {
                    _IFRS9Service.DeleteMacroEconomic(macroEconomicId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No macroEconomic found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacroEconomic/{macroEconomicId}")]
        public HttpResponseMessage GetMacroEconomic(HttpRequestMessage request,int macroEconomicId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroEconomic macroEconomic = _IFRS9Service.GetMacroEconomic(macroEconomicId);

                // notice no need to create a seperate model object since MacroEconomic entity will do just fine
                response = request.CreateResponse<MacroEconomic>(HttpStatusCode.OK, macroEconomic);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacroEconomics")]
        public HttpResponseMessage GetAvailableMacroEconomics(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroEconomic[] macroEconomics = _IFRS9Service.GetAllMacroEconomics();

                return request.CreateResponse<MacroEconomic[]>(HttpStatusCode.OK, macroEconomics);
            });
        }
    }
}
