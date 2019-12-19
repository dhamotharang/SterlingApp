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
    [RoutePrefix("api/liabilityrepaymentschedule")]
    [UsesDisposableService]
    public class LiabilityRepaymentScheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LiabilityRepaymentScheduleApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateliabilityrepaymentschedule")]
        public HttpResponseMessage UpdateLiabilityRepaymentSchedule(HttpRequestMessage request, [FromBody]LiabilityRepaymentSchedule liabilityrepaymentscheduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var liabilityrepaymentschedule = _IFRSDataService.UpdateLiabilityRepaymentSchedule(liabilityrepaymentscheduleModel);

                return request.CreateResponse<LiabilityRepaymentSchedule>(HttpStatusCode.OK, liabilityrepaymentschedule);
            });
        }

        [HttpPost]
        [Route("deleteliabilityrepaymentschedule")]
        public HttpResponseMessage Deleteliabilityrepaymentschedule(HttpRequestMessage request, [FromBody]int liabilityrepayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LiabilityRepaymentSchedule liabilityrepaymentschedule = _IFRSDataService.GetLiabilityRepaymentSchedule(liabilityrepayId);

                if (liabilityrepaymentschedule != null)
                {
                    _IFRSDataService.DeleteLiabilityRepaymentSchedule(liabilityrepayId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No liabilityrepaymentschedule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availableliabilityrepaymentschedule")]
        public HttpResponseMessage GetAvailableliabilityrepaymentschedules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LiabilityRepaymentSchedule[] liabilityrepaymentschedule = _IFRSDataService.GetAllLiabilityRepaymentSchedule().ToArray();

                return request.CreateResponse<LiabilityRepaymentSchedule[]>(HttpStatusCode.OK, liabilityrepaymentschedule.ToArray());
            });
        }

        [HttpGet]
        [Route("getliabilityrepaymentschedule/{liabilityrepayId}")]
        public HttpResponseMessage Getliabilityrepaymentschedule(HttpRequestMessage request, int liabilityrepayId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LiabilityRepaymentSchedule liabilityrepaymentschedule = _IFRSDataService.GetLiabilityRepaymentSchedule(liabilityrepayId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LiabilityRepaymentSchedule>(HttpStatusCode.OK, liabilityrepaymentschedule);

                return response;
            });
        }      
    }
}
