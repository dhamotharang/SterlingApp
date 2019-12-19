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
    [RoutePrefix("api/transactiondetail")]
    [UsesDisposableService]
    public class TransactionDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TransactionDetailApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpGet]
        [Route("gettransactiondetail")]
        public HttpResponseMessage GetGetTransactionDetail(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TransactionDetail[] transactionDetails = _FinstatService.GetAllTransactionDetails();

                return request.CreateResponse<TransactionDetail[]>(HttpStatusCode.OK, transactionDetails);
            });
        }
    }
}
