using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using CodeEntities = Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.SystemCore.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/errortracker")]
    [UsesDisposableService]
    public class ErrorTrackerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ErrorTrackerApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateerrortracker")]
        public HttpResponseMessage UpdateErrorTracker(HttpRequestMessage request, [FromBody]CodeEntities.ErrorTracker errorTrackerModel)
        {
            return GetHttpResponse(request, () =>
            {
                var errorTracker = _CoreService.UpdateErrorTracker(errorTrackerModel);

                return request.CreateResponse<CodeEntities.ErrorTracker>(HttpStatusCode.OK, errorTracker);
            });
        }

        [HttpPost]
        [Route("deleteerrortracker")]
        public HttpResponseMessage DeleteErrorTracker(HttpRequestMessage request, [FromBody]int errorTrackerId)
        {
            return GetHttpResponse(request, () =>
            {
                var errorTracker = new ErrorTracker();

                _CoreService.DeleteErrorTracker(errorTrackerId);

                return request.CreateResponse<CodeEntities.ErrorTracker>(HttpStatusCode.OK, errorTracker);
            });

        }

        [HttpGet]
        [Route("geterrortracker/{errorTrackerId}")]
        public HttpResponseMessage GetErrorTracker(HttpRequestMessage request, int errorTrackerId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CodeEntities.ErrorTracker errorTracker = _CoreService.GetErrorTracker(errorTrackerId);

                // notice no need to create a seperate model object since Client entity will do just fine
                response = request.CreateResponse<CodeEntities.ErrorTracker>(HttpStatusCode.OK, errorTracker);

                return response;
            });
        }

        [HttpGet]
        [Route("availableerrortracker")]
        public HttpResponseMessage GetAvailableErrorTrackers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CodeEntities.ErrorTracker[] errorTracker = _CoreService.GetAllErrorTrackers();

                return request.CreateResponse<CodeEntities.ErrorTracker[]>(HttpStatusCode.OK, errorTracker);
            });
        }

        [HttpPost]
        [Route("deleteallerrortracker")]
        public HttpResponseMessage DeleteAllErrorTracker(HttpRequestMessage request, [FromBody]int errorTrackerId)
        {
            return GetHttpResponse(request, () =>
            {
                var errorTracker = new ErrorTracker();

                _CoreService.DeleteAllErrorTracker();

                return request.CreateResponse<CodeEntities.ErrorTracker>(HttpStatusCode.OK, errorTracker);
            });
        }


    }
}
