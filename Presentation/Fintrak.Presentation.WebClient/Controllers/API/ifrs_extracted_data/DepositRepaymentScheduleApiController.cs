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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/depositrepaymentschedule")]
    [UsesDisposableService]
    public class DepositRepaymentScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DepositRepaymentScheduleApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updatedepositrepaymentschedule")]
        public HttpResponseMessage Updatedepositrepaymentschedule(HttpRequestMessage request, [FromBody]DepositRepaymentSchedule depositrepaymentscheduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var depositrepaymentschedule = _IFRSDataService.UpdateDepositRepaymentSchedule(depositrepaymentscheduleModel);

                return request.CreateResponse<DepositRepaymentSchedule>(HttpStatusCode.OK, depositrepaymentschedule);
            });
        }

        [HttpPost]
        [Route("deletedepositrepaymentschedule")]
        public HttpResponseMessage Deletedepositrepaymentschedule(HttpRequestMessage request, [FromBody]int depositrepaymentscheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                DepositRepaymentSchedule depositrepaymentschedule = _IFRSDataService.GetDepositRepaymentSchedule(depositrepaymentscheduleId);

                if (depositrepaymentschedule != null)
                {
                    _IFRSDataService.DeleteDepositRepaymentSchedule(depositrepaymentscheduleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No depositrepaymentschedule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availabledepositrepaymentschedule")]
        public HttpResponseMessage GetAvailabledepositrepaymentschedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DepositRepaymentSchedule[] depositrepaymentschedule = _IFRSDataService.GetAllDepositRepaymentSchedule().ToArray();

                return request.CreateResponse<DepositRepaymentSchedule[]>(HttpStatusCode.OK, depositrepaymentschedule.ToArray());
            });
        }

        [HttpGet]
        [Route("getdepositrepaymentschedule/{depositrepayId}")]
        public HttpResponseMessage Getdepositrepaymentschedule(HttpRequestMessage request, int depositrepayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DepositRepaymentSchedule depositrepaymentschedule = _IFRSDataService.GetDepositRepaymentSchedule(depositrepayId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<DepositRepaymentSchedule>(HttpStatusCode.OK, depositrepaymentschedule);

                return response;
            });
        }

        [HttpPost]
        [Route("getvariancedata")]
        public HttpResponseMessage RefNumberLists(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DepositRepaymentSchedule[] depositrepaymentschedule = _IFRSDataService.GetVarianceData();

                response = request.CreateResponse<DepositRepaymentSchedule[]>(HttpStatusCode.OK, depositrepaymentschedule);

                return response;
            });
        }
    }
}
