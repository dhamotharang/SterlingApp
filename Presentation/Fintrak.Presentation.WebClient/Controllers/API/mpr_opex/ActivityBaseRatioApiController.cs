using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/activitybaseratio")]
    [UsesDisposableService]
    public class ActivityBaseRatioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ActivityBaseRatioApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateactivitybaseratio")]
        public HttpResponseMessage UpdateActivityBaseRatio(HttpRequestMessage request, [FromBody]ActivityBaseRatio activityBaseRatioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var activityBaseRatio = _MPROPEXService.UpdateActivityBaseRatio(activityBaseRatioModel);

                return request.CreateResponse<ActivityBaseRatio>(HttpStatusCode.OK, activityBaseRatio);
            });
        }

        [HttpPost]
        [Route("deleteactivityBaseRatio")]
        public HttpResponseMessage DeleteActivityBaseRatio(HttpRequestMessage request, [FromBody]int activityBaseRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ActivityBaseRatio activityBaseRatio = _MPROPEXService.GetActivityBaseRatio(activityBaseRatioId);

                if (activityBaseRatio != null)
                {
                    _MPROPEXService.DeleteActivityBaseRatio(activityBaseRatioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getactivityBaseRatio/{activityBaseRatioId}")]
        public HttpResponseMessage GetActivityBaseRatio(HttpRequestMessage request, int activityBaseRatioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ActivityBaseRatio activityBaseRatio = _MPROPEXService.GetActivityBaseRatio(activityBaseRatioId);

                // notice no need to create a seperate model object since ActivityBaseRatio entity will do just fine
                response = request.CreateResponse<ActivityBaseRatio>(HttpStatusCode.OK, activityBaseRatio);

                return response;
            });
        }

        [HttpGet]
        [Route("availableactivityBaseRatio")]
        public HttpResponseMessage GetAvailableActivityBaseRatio(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ActivityBaseRatio[] activityBaseRatio = _MPROPEXService.GetAllActivityBaseRatios();


                return request.CreateResponse<ActivityBaseRatio[]>(HttpStatusCode.OK, activityBaseRatio);
            });
        }
    }
}
