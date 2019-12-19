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
    [RoutePrefix("api/borrowingperiodicschedule")]
    [UsesDisposableService]
    public class BorrowingPeriodicScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BorrowingPeriodicScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getborrowingperiodicschedule/{refno}")]
        public HttpResponseMessage GetBorrowingPeriodicSchedule(HttpRequestMessage request, string RefNo)
        {
            return GetHttpResponse(request, () =>
            {
                if (RefNo.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetBorrowingPeriodicSchedulebyRefNo(RefNo, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Borrowing_Periodic_Schedule.zip");
                    return response;
                }
                else
                {
                    BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetBorrowingPeriodicSchedulebyRefNo(RefNo, null);

                    // notice no need to create a seperate model object since BorrowingPeriodicSchedule entity will do just fine
                    return request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);
                }
            });
        }


        [HttpGet]
        [Route("getborrowingperiodicscheduledistinctrefno")]
        public HttpResponseMessage GetBorrowingPeriodicScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetBorrowingPeriodicScheduleDistinctRefNo();

                return request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);
            });
        }

        [HttpGet]
        [Route("availableborrowingperiodicschedule")]
        public HttpResponseMessage GetAvailableBorrowingPeriodicSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BorrowingPeriodicSchedule[] borrowingperiodicschedule = _IFRSDataService.GetAllBorrowingPeriodicSchedules();

                return request.CreateResponse<BorrowingPeriodicSchedule[]>(HttpStatusCode.OK, borrowingperiodicschedule);
            });
        }

        [HttpPost]
        [Route("deleteborrowingperiodicschedule/{refno}")]
        public HttpResponseMessage DeleteBorrowingPeriodic(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;


                _IFRSDataService.DeleteBorrowingPeriodicSchedulebyRefNo(refNo);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNo = _IFRSDataService.GetDistinctBorrowingPeriodicScheduleRefNos();

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
