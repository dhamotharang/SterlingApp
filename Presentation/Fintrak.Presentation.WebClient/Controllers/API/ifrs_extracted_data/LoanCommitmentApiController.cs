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
    [RoutePrefix("api/loanCommitment")]
    [UsesDisposableService]
    public class LoanCommitmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanCommitmentApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateloanCommitment")]
        public HttpResponseMessage UpdateLoanCommitment(HttpRequestMessage request, [FromBody]LoanCommitment loanCommitmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanCommitment = _IFRSDataService.UpdateLoanCommitment(loanCommitmentModel);

                return request.CreateResponse<LoanCommitment>(HttpStatusCode.OK, loanCommitment);
            });
        }

        [HttpPost]
        [Route("deleteloanCommitment")]
        public HttpResponseMessage DeleteLoanCommitment(HttpRequestMessage request, [FromBody]int LoanCommitmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanCommitment loanCommitment = _IFRSDataService.GetLoanCommitment(LoanCommitmentId);

                if (loanCommitment != null)
                {
                    _IFRSDataService.DeleteLoanCommitment(LoanCommitmentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No loanCommitment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getloanCommitment/{loanCommitmentId}")]
        public HttpResponseMessage GetLoanCommitment(HttpRequestMessage request, int LoanCommitmentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanCommitment loanCommitment = _IFRSDataService.GetLoanCommitment(LoanCommitmentId);

                // notice no need to create a seperate model object since LoanCommitment entity will do just fine
                response = request.CreateResponse<LoanCommitment>(HttpStatusCode.OK, loanCommitment);

                return response;
            });
        }

        [HttpGet]
        [Route("availableloanCommitments")]
        public HttpResponseMessage GetAvailableLoanCommitments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanCommitment[] loanCommitments = _IFRSDataService.GetAllLoanCommitments();

                return request.CreateResponse<LoanCommitment[]>(HttpStatusCode.OK, loanCommitments);
            });
        }
    }
}
