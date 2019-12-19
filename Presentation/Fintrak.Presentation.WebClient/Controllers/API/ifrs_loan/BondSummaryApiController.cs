
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondsummary")]
    [UsesDisposableService]
    public class BondSummaryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondSummaryApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpGet]
        [Route("availablebondSummarys")]
        public HttpResponseMessage GetAllBondSummarys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondSummary[] bondSummarys = _LoanService.GetAllBondSummarys();

                return request.CreateResponse<BondSummary[]>(HttpStatusCode.OK, bondSummarys);
            });
        }
    }
}
