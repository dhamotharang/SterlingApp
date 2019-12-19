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
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ledgerdetailsummary")]
    [UsesDisposableService]
    public class LedgerDetailSummaryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LedgerDetailSummaryApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("availableledgerdetailsummarys")]
        public HttpResponseMessage GetAllLedgerDetailSummarys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LedgerDetailSummary[] ledgerDetailSummarys = _FinstatService.GetAllLedgerDetailSummarys();

                return request.CreateResponse<LedgerDetailSummary[]>(HttpStatusCode.OK, ledgerDetailSummarys);
            });
        }
    }
}
