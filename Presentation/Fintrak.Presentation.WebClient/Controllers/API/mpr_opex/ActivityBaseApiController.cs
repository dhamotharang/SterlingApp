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
    [RoutePrefix("api/activitybase")]
    [UsesDisposableService]
    public class ActivityBaseApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ActivityBaseApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateactivitybase")]
        public HttpResponseMessage UpdateActivityBase(HttpRequestMessage request, [FromBody]ActivityBase activityBaseModel)
        {
            return GetHttpResponse(request, () =>
            {
                var activityBase = _MPROPEXService.UpdateActivityBase(activityBaseModel);

                return request.CreateResponse<ActivityBase>(HttpStatusCode.OK, activityBase);
            });
        }

        [HttpPost]
        [Route("deleteactivityBase")]
        public HttpResponseMessage DeleteActivityBase(HttpRequestMessage request, [FromBody]int activityBaseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ActivityBase activityBase = _MPROPEXService.GetActivityBase(activityBaseId);

                if (activityBase != null)
                {
                    _MPROPEXService.DeleteActivityBase(activityBaseId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getactivityBase/{activityBaseId}")]
        public HttpResponseMessage GetActivityBase(HttpRequestMessage request, int activityBaseId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ActivityBase activityBase = _MPROPEXService.GetActivityBase(activityBaseId);

                // notice no need to create a seperate model object since ActivityBase entity will do just fine
                response = request.CreateResponse<ActivityBase>(HttpStatusCode.OK, activityBase);

                return response;
            });
        }

        [HttpGet]
        [Route("availableactivityBase")]
        public HttpResponseMessage GetAvailableActivityBase(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ActivityBase[] activityBase = _MPROPEXService.GetAllActivityBases();


                return request.CreateResponse<ActivityBase[]>(HttpStatusCode.OK, activityBase);
            });
        }
    }
}
