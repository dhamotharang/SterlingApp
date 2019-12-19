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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondperiodicschedule")]
    [UsesDisposableService]
    public class BondPeriodicScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondPeriodicScheduleApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getbondperiodicschedule/{refno}")]
        public HttpResponseMessage GetBondPeriodicSchedule(HttpRequestMessage request, string RefNo)
        {
            return GetHttpResponse(request, () =>
            {
                if (RefNo.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetBondPeriodicSchedulebyRefNo(RefNo, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Bond_Periodic_Schedule.zip");
                    return response;
                }
                else
                {
                    BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetBondPeriodicSchedulebyRefNo(RefNo, null);

                    // notice no need to create a seperate model object since BondPeriodicSchedule entity will do just fine
                    return request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);
                }
            });
        }


        [HttpGet]
        [Route("getbondperiodicscheduledistinctrefno")]
        public HttpResponseMessage GetBondPeriodicScheduleResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetBondPeriodicScheduleDistinctRefNo();

                return request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);
            });
        }

        [HttpGet]
        [Route("availablebondperiodicschedule")]
        public HttpResponseMessage GetAvailableBondPeriodicSchedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondPeriodicSchedule[] bondperiodicschedule = _IFRSDataService.GetAllBondPeriodicSchedules();

                return request.CreateResponse<BondPeriodicSchedule[]>(HttpStatusCode.OK, bondperiodicschedule);
            });
        }

        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetDistinctRefNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNo = _IFRSDataService.GetDistinctBondPeriodicScheduleRefNos().ToArray();

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
