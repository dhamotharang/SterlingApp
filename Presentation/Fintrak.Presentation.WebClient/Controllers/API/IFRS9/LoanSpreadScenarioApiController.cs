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
    [RoutePrefix("api/loanspreadscenario")]
    [UsesDisposableService]
    public class LoanSpreadScenarioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanSpreadScenarioApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpGet]
        [Route("availablescenario")]
        public HttpResponseMessage GetAvailableLoanSpreadScenarios(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSpreadScenario[] loanSpreadScenarios = _IFRS9Service.GetAllLoanSpreadScenarios();

                return request.CreateResponse<LoanSpreadScenario[]>(HttpStatusCode.OK, loanSpreadScenarios);
            });
        }

    }
}
