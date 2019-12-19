
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
    [RoutePrefix("api/loanCommitmentComputationResult")]
    [UsesDisposableService]
    public class LoanCommitmentComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanCommitmentComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateloanCommitmentComputationResult")]
        public HttpResponseMessage UpdateLoanCommitmentComputationResult(HttpRequestMessage request, [FromBody]LoanCommitmentComputationResult loanCommitmentComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanCommitmentComputationResult = _IFRS9Service.UpdateLoanCommitmentComputationResult(loanCommitmentComputationResultModel);

                return request.CreateResponse<LoanCommitmentComputationResult>(HttpStatusCode.OK, loanCommitmentComputationResult);
            });
        }

        [HttpPost]
        [Route("deleteloanCommitmentComputationResult")]
        public HttpResponseMessage DeleteLoanCommitmentComputationResult(HttpRequestMessage request, [FromBody]int loanCommitmentComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanCommitmentComputationResult loanCommitmentComputationResult = _IFRS9Service.GetLoanCommitmentComputationResult(loanCommitmentComputationResultId);

                if (loanCommitmentComputationResult != null)
                {
                    _IFRS9Service.DeleteLoanCommitmentComputationResult(loanCommitmentComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No loanCommitmentComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getloanCommitmentComputationResult/{loanCommitmentComputationResultId}")]
        public HttpResponseMessage GetLoanCommitmentComputationResult(HttpRequestMessage request, int loanCommitmentComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanCommitmentComputationResult loanCommitmentComputationResult = _IFRS9Service.GetLoanCommitmentComputationResult(loanCommitmentComputationResultId);

                // notice no need to create a seperate model object since LoanCommitmentComputationResult entity will do just fine
                response = request.CreateResponse<LoanCommitmentComputationResult>(HttpStatusCode.OK, loanCommitmentComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableloanCommitmentComputationResults")]
        public HttpResponseMessage GetAvailableLoanCommitmentComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanCommitmentComputationResult[] loanCommitmentComputationResults = _IFRS9Service.GetAllLoanCommitmentComputationResults();

                return request.CreateResponse<LoanCommitmentComputationResult[]>(HttpStatusCode.OK, loanCommitmentComputationResults);
            });
        }
    }
}
