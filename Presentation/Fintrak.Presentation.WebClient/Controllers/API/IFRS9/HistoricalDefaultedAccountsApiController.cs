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
    [RoutePrefix("api/historicaldefaultedaccounts")]
    [UsesDisposableService]
    public class HistoricalDefaultedAccountsController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalDefaultedAccountsController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatehistoricaldefaultedaccount")]
        public HttpResponseMessage UpdateHistoricalDefaultedAccount(HttpRequestMessage request, [FromBody]HistoricalDefaultedAccounts historicalDefaultedAccountsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicalDefaultedAccount = _IFRS9Service.UpdateHistoricalDefaultedAccount(historicalDefaultedAccountsModel);

                return request.CreateResponse<HistoricalDefaultedAccounts>(HttpStatusCode.OK, historicalDefaultedAccount);
            });
        }

        [HttpPost]
        [Route("deletehistoricaldefaultedaccount")]
        public HttpResponseMessage DeleteHistoricalDefaultedAccount(HttpRequestMessage request, [FromBody]int DefaultedAccountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalDefaultedAccounts historicalDefaultedAccounts = _IFRS9Service.GetHistoricalDefaultedAccount(DefaultedAccountId);

                if (historicalDefaultedAccounts != null)
                {
                    _IFRS9Service.DeleteHistoricalDefaultedAccount(DefaultedAccountId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No historicalDefaultedAccount found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricaldefaultedaccount/{DefaultedAccountId}")]
        public HttpResponseMessage GetHistoricalDefaultedAccounts(HttpRequestMessage request, int DefaultedAccountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalDefaultedAccounts historicalDefaultedAccount = _IFRS9Service.GetHistoricalDefaultedAccount(DefaultedAccountId);

                // notice no need to create a seperate model object since HistoricalDefaultedAccounts entity will do just fine
                response = request.CreateResponse<HistoricalDefaultedAccounts>(HttpStatusCode.OK, historicalDefaultedAccount);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricaldefaultedaccounts")]
        public HttpResponseMessage GetAvailableHistoricalDefaultedAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HistoricalDefaultedAccounts[] historicalDefaultedAccounts = _IFRS9Service.GetAllHistoricalDefaultedAccounts();

                return request.CreateResponse<HistoricalDefaultedAccounts[]>(HttpStatusCode.OK, historicalDefaultedAccounts);
            });
        }
    }
}
