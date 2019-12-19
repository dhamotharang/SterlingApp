using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/activity")]
    [UsesDisposableService]
    public class ActivityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ActivityApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpGet]
        [Route("getactivity/{activityId}")]
        public HttpResponseMessage GetActivity(HttpRequestMessage request,string activityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LogEvent activity = _CoreService.GetLogById(activityId);

                // notice no need to create a seperate model object since Activity entity will do just fine
                response = request.CreateResponse<LogEvent>(HttpStatusCode.OK, activity);

                return response;
            });
        }

        [HttpGet]
        [Route("getactivities")]
        public HttpResponseMessage GetActivities(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LogEvent[] activities = _CoreService.GetLogByDateRangeAndType(DateTime.Parse("09/10/2015"), DateTime.Parse("09/28/2015"), "1");

                return request.CreateResponse<LogEvent[]>(HttpStatusCode.OK, activities);
            });
        }
    }
}
