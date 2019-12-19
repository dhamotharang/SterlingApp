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
    [RoutePrefix("api/loanspreadsensitivity")]
    [UsesDisposableService]
    public class LoanSpreadSensitivityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanSpreadSensitivityApiController(IIFRS9Service ifrs9Service)
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
        public HttpResponseMessage GetAvailableLoanSpreadSensitivitys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSpreadSensitivity[] loanSpreadSensitivitys = _IFRS9Service.GetAllLoanSpreadSensitivitys();

                return request.CreateResponse<LoanSpreadSensitivity[]>(HttpStatusCode.OK, loanSpreadSensitivitys);
            });
        }

        //[HttpGet]
        //[Route("loanassessment/{bucket}")]
        //public HttpResponseMessage GetLoanAssessments(HttpRequestMessage request,string bucket)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        LoanSpreadSensitivity[] loanSpreadSensitivitys = _IFRS9Service.GetLoanAssessments(bucket);

        //        return request.CreateResponse<LoanSpreadSensitivity[]>(HttpStatusCode.OK, loanSpreadSensitivitys);
        //    });
        //}
    }
}
