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
    [RoutePrefix("api/macrovariablerecoveryrates")]
    [UsesDisposableService]
    public class MacroVarRecoveryRatesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacroVarRecoveryRatesApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateMacroVarRecoveryRates")]
        public HttpResponseMessage UpdateMacroVarRecoveryRates(HttpRequestMessage request, [FromBody]MacroVarRecoveryRates MacroVarRecoveryRatesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var MacroVarRecoveryRates = _IFRS9Service.UpdateMacroVarRecoveryRates(MacroVarRecoveryRatesModel);

                return request.CreateResponse<MacroVarRecoveryRates>(HttpStatusCode.OK, MacroVarRecoveryRates);
            });
        }

        [HttpPost]
        [Route("deleteMacroVarRecoveryRates")]
        public HttpResponseMessage DeleteMacroVarRecoveryRates(HttpRequestMessage request, [FromBody]int RecoveryRatesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacroVarRecoveryRates MacroVarRecoveryRates = _IFRS9Service.GetMacroVarRecoveryRatesById(RecoveryRatesId);

                if (MacroVarRecoveryRates != null)
                {
                    _IFRS9Service.DeleteMacroVarRecoveryRates(RecoveryRatesId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MacroVarRecoveryRates found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getMacroVarRecoveryRatesById/{RecoveryRatesId}")]
        public HttpResponseMessage GetMacroVarRecoveryRatesById(HttpRequestMessage request, int RecoveryRatesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacroVarRecoveryRates MacroVarRecoveryRates = _IFRS9Service.GetMacroVarRecoveryRatesById(RecoveryRatesId);

                // notice no need to create a seperate model object since MacroVarRecoveryRates entity will do just fine
                response = request.CreateResponse<MacroVarRecoveryRates>(HttpStatusCode.OK, MacroVarRecoveryRates);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllMacroVarRecoveryRates")]
        public HttpResponseMessage GetAvailableMacroVarRecoveryRatess(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacroVarRecoveryRates[] MacroVarRecoveryRatess = _IFRS9Service.GetAllMacroVarRecoveryRates();

                return request.CreateResponse<MacroVarRecoveryRates[]>(HttpStatusCode.OK, MacroVarRecoveryRatess);
            });
        }
    }
}
