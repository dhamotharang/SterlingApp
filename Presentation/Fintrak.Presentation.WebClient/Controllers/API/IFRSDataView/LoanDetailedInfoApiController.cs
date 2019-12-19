using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using AutoMapper;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loandetailedinfo")]
    [UsesDisposableService]
    public class LoanDetailedInfoApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanDetailedInfoApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getloandetailedinfo")]
        public HttpResponseMessage GetAvailableLoanDetailedInfos(HttpRequestMessage request, string refno, DateTime? Date)
        {
            return GetHttpResponse(request, () =>
            {
                var loanpry = _IFRSDataService.GetPryLoanBySearch(refno);
                var loandetails = _IFRSDataService.GetAllLoanDetailsBySearch(refno);
                var collateraldetails = _IFRSDataService.GetCollateralDetailsBySearch(refno);
                var loancashflow = _IFRSDataService.GetCashflowBySearch(refno);
                var loanschedule = _IFRSDataService.GetScheduleRange(refno, Date, null);
                var loaneclresult = _IFRSDataService.GetLoanECLResultBySearch(refno);
                var loaneclcomputationresult = _IFRSDataService.GetLoansECLComputationResultBySearch(refno, null);
                var loanmonthlyead = _IFRSDataService.GetIfrsMonthlyEADBySearch(refno);

                LoanDetailedInfo loandetailedinfo = new LoanDetailedInfo();

                loandetailedinfo.loanpry = loanpry.Length > 0 ? loanpry[0] : new LoanPry();
                loandetailedinfo.loandetails = loandetails.Length > 0 ? loandetails[0] : new RawLoanDetails();
                loandetailedinfo.collateraldetails = collateraldetails.Length > 0 ? collateraldetails[0] : new CollateralDetails();
                loandetailedinfo.loancashflow = loancashflow;
                loandetailedinfo.loanschedule = loanschedule;
                loandetailedinfo.loanmonthlyead = loanmonthlyead;
                loandetailedinfo.loaneclresult = loaneclresult.Length > 0 ? loaneclresult[0] : new LoanECLResult();
                loandetailedinfo.loaneclcomputationresult = loaneclcomputationresult;

                return request.CreateResponse(HttpStatusCode.OK, loandetailedinfo);
            });
        }


        [HttpGet]
        [Route("getrefnos/{count}")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request, int count)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<string> refNo = _IFRSDataService.GetDistinctLoanDetailsRefNos(count);

                var dropDown = refNo.Select(e => new
                {
                    name = e,
                    icon = "",
                    maker = "",
                    ticked = false
                });

                return request.CreateResponse(HttpStatusCode.OK, dropDown);

            });
        }

    }
}
