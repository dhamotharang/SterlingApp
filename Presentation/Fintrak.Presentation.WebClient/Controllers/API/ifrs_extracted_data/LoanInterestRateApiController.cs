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
    [RoutePrefix("api/loaninterestrate")]
    [UsesDisposableService]
    public class LoanInterestRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanInterestRateApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateloaninterestrate")]
        public HttpResponseMessage UpdateLoanInterestRate(HttpRequestMessage request, [FromBody]LoanInterestRate loanInterestRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanInterestRate = _IFRSDataService.UpdateLoanInterestRate(loanInterestRateModel);

                return request.CreateResponse<LoanInterestRate>(HttpStatusCode.OK, loanInterestRate);
            });
        }

        [HttpPost]
        [Route("deleteloaninterestrate")]
        public HttpResponseMessage DeleteLoanInterestRate(HttpRequestMessage request, [FromBody]int LoanInterestRate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanInterestRate loanInterestRate = _IFRSDataService.GetLoanInterestRate(LoanInterestRate_Id);

                if (loanInterestRate != null)
                {
                    _IFRSDataService.DeleteLoanInterestRate(LoanInterestRate_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No loanInterestRate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getloaninterestrate/{loaninterestrate_id}")]
        public HttpResponseMessage GetLoanInterestRate(HttpRequestMessage request, int LoanInterestRate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanInterestRate loanInterestRate = _IFRSDataService.GetLoanInterestRate(LoanInterestRate_Id);

                // notice no need to create a seperate model object since LoanInterestRate entity will do just fine
                response = request.CreateResponse<LoanInterestRate>(HttpStatusCode.OK, loanInterestRate);

                return response;
            });
        }

        [HttpGet]
        [Route("availableloaninterestrates")]
        public HttpResponseMessage GetAvailableLoanInterestRates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanInterestRate[] loanInterestRates = _IFRSDataService.GetAllLoanInterestRates();

                return request.CreateResponse<LoanInterestRate[]>(HttpStatusCode.OK, loanInterestRates);
            });
        }
    }
}
