using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/scheduletype")]
    [UsesDisposableService]
    public class ScheduleTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ScheduleTypeApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatescheduletype")]
        public HttpResponseMessage UpdateScheduleType(HttpRequestMessage request, [FromBody]ScheduleType scheduleTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var scheduleType = _LoanService.UpdateScheduleType(scheduleTypeModel);

                return request.CreateResponse<ScheduleType>(HttpStatusCode.OK, scheduleType);
            });
        }

        [HttpPost]
        [Route("deletescheduletype")]
        public HttpResponseMessage DeleteScheduleType(HttpRequestMessage request, [FromBody]int scheduleTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ScheduleType scheduleType = _LoanService.GetScheduleType(scheduleTypeId);

                if (scheduleType != null)
                {
                    _LoanService.DeleteScheduleType(scheduleTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No scheduletype found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getscheduletype/{scheduleTypeId}")]
        public HttpResponseMessage GetScheduleType(HttpRequestMessage request,int scheduleTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ScheduleType scheduleType = _LoanService.GetScheduleType(scheduleTypeId);

                // notice no need to create a seperate model object since ScheduleType entity will do just fine
                response = request.CreateResponse<ScheduleType>(HttpStatusCode.OK, scheduleType);

                return response;
            });
        }

        [HttpGet]
        [Route("availablescheduletypes")]
        public HttpResponseMessage GetAvailableScheduleTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ScheduleType[] scheduleTypes = _LoanService.GetAllScheduleTypes();

                return request.CreateResponse<ScheduleType[]>(HttpStatusCode.OK, scheduleTypes);
            });
        }
    }
}
