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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/borrowingschedule")]
    [UsesDisposableService]
    public class BorrowingScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BorrowingScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getborrowingschedule")]
        public HttpResponseMessage GetBorrowingSchedule(HttpRequestMessage request, string RefNo, DateTime? Date)
        {
            return GetHttpResponse(request, () =>
            {
                if (RefNo.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetBorrowingSchedulebyRefNo(RefNo, Date, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Borrowing_Daily_Schedule.zip");
                    return response;
                }
                else
                {
                    BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetBorrowingSchedulebyRefNo(RefNo, Date, null);

                    // notice no need to create a seperate model object since LoanSchedule entity will do just fine
                    return request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);
                }
            });
        }


        [HttpGet]
        [Route("getborrowingscheduledistinctrefno")]
        public HttpResponseMessage GetBorrowingScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetBorrowingScheduleDistinctRefNo();

                return request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);
            });
        }

        [HttpGet]
        [Route("availableborrowingschedule")]
        public HttpResponseMessage GetAvailableBorrowingSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingSchedule[] borrowingschedule = _IFRSDataService.GetAllBorrowingSchedules();

                return request.CreateResponse<BorrowingSchedule[]>(HttpStatusCode.OK, borrowingschedule);
            });
        }

        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<string> refNo = _IFRSDataService.GetDistinctBorrowingScheduleRefNos();

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
