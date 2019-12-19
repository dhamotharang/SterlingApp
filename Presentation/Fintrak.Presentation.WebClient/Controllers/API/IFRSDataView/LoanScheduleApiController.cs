using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;
using AutoMapper;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loanschedule")]
    [UsesDisposableService]
    public class LoanScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;
        private IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true).CreateMapper();

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getloanschedule/{refno}")]
        public HttpResponseMessage GetLoanSchedule(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanSchedule[] loanschedule = _IFRSDataService.GetLoanSchedulebyRefNo(refno);

                // notice no need to create a seperate model object since LoanSchedule entity will do just fine
                response = request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);

                return response;
            });
        }


        [HttpGet]
        [Route("getloanscheduledistinctrefno")]
        public HttpResponseMessage GetLoanScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MultiSelectDropDown[] loanschedule = _IFRSDataService.GetLoanScheduleDistinctRefNo();

                return request.CreateResponse(HttpStatusCode.OK, loanschedule.ToArray());
            });
        }

        [HttpGet]
        [Route("availableloanschedule")]
        public HttpResponseMessage GetAvailableLoanSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSchedule[] loanschedule = _IFRSDataService.GetAllLoanSchedules();

                return request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);
            });
        }

        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<string> refNo = _IFRSDataService.GetDistinctLoanScheduleRefNos();

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


        [HttpGet]
        [Route("getschedulerange")]
        public HttpResponseMessage GetScheduleRange(HttpRequestMessage request, string RefNo, DateTime? Date)
        {
            return GetHttpResponse(request, () =>
            {
                if (RefNo.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    LoanSchedule[] loanschedule = _IFRSDataService.GetScheduleRange(RefNo, Date, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Loan_Daily_Schedule.zip");
                    return response;
                }
                else
                {
                    LoanSchedule[] loanschedule = _IFRSDataService.GetScheduleRange(RefNo, Date, null);

                    // notice no need to create a seperate model object since LoanSchedule entity will do just fine
                    return request.CreateResponse<LoanSchedule[]>(HttpStatusCode.OK, loanschedule);
                }
            });
        }
    }
}
