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
    [RoutePrefix("api/watchlistedloan")]
    [UsesDisposableService]
    public class WatchListedLoanApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public WatchListedLoanApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatewatchlistedloan")]
        public HttpResponseMessage UpdateWatchListedLoan(HttpRequestMessage request, [FromBody]WatchListedLoan watchListedLoanModel)
        {
            return GetHttpResponse(request, () =>
            {
                var watchListedLoan = _LoanService.UpdateWatchListedLoan(watchListedLoanModel);

                return request.CreateResponse<WatchListedLoan>(HttpStatusCode.OK, watchListedLoan);
            });
        }

        [HttpPost]
        [Route("deletewatchlistedloan")]
        public HttpResponseMessage DeleteWatchListedLoan(HttpRequestMessage request, [FromBody]int watchListedLoanId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                WatchListedLoan watchListedLoan = _LoanService.GetWatchListedLoan(watchListedLoanId);

                if (watchListedLoan != null)
                {
                    _LoanService.DeleteWatchListedLoan(watchListedLoanId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No watchlistedloan found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getwatchlistedloan/{watchListedLoanId}")]
        public HttpResponseMessage GetWatchListedLoan(HttpRequestMessage request,int watchListedLoanId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                WatchListedLoan watchListedLoan = _LoanService.GetWatchListedLoan(watchListedLoanId);

                // notice no need to create a seperate model object since WatchListedLoan entity will do just fine
                response = request.CreateResponse<WatchListedLoan>(HttpStatusCode.OK, watchListedLoan);

                return response;
            });
        }

        [HttpGet]
        [Route("availablewatchlistedloans")]
        public HttpResponseMessage GetAvailableWatchListedLoans(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                WatchListedLoan[] watchListedLoans = _LoanService.GetAllWatchListedLoans();

                return request.CreateResponse<WatchListedLoan[]>(HttpStatusCode.OK, watchListedLoans);
            });
        }
    }
}
