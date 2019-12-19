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
    [RoutePrefix("api/loanbucketdistribution")]
    [UsesDisposableService]
    public class LoanBucketDistributionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanBucketDistributionApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpGet]
        [Route("availabledistribution")]
        public HttpResponseMessage GetAvailableLoanBucketDistributions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanBucketDistribution[] loanBucketDistributions = _IFRS9Service.GetAllLoanBucketDistributions();

                return request.CreateResponse<LoanBucketDistribution[]>(HttpStatusCode.OK, loanBucketDistributions);
            });
        }

        [HttpPost]
        [Route("pddistribution")]
        public HttpResponseMessage PDDist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _IFRS9Service.PDDistribution();


                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpPost]
        [Route("pastduedaysdistribution")]
        public HttpResponseMessage PastDueDayDist(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _IFRS9Service.PastDueDayDistribution();


                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpGet]
        [Route("loanassessment/{bucket}")]
        public HttpResponseMessage GetLoanAssessments(HttpRequestMessage request,string bucket)
        {
            return GetHttpResponse(request, () =>
            {
                LoanBucketDistribution[] loanBucketDistributions = _IFRS9Service.GetLoanAssessments(bucket);

                return request.CreateResponse<LoanBucketDistribution[]>(HttpStatusCode.OK, loanBucketDistributions);
            });
        }
    }
}
