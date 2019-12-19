using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;
using CodeEntities = Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/threshold")]
    [UsesDisposableService]
    public class ThresholdApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ThresholdApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatethreshold")]
        public HttpResponseMessage UpdateThreshold(HttpRequestMessage request, [FromBody]SCDThreshold thresholdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var threshold = _ScorecardService.UpdateSCDThreshold(thresholdModel);

                return request.CreateResponse<SCDThreshold>(HttpStatusCode.OK, threshold);
            });
        }

        [HttpPost]
        [Route("deletethreshold")]
        public HttpResponseMessage DeleteThreshold(HttpRequestMessage request, [FromBody]int thresholdId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDThreshold threshold = _ScorecardService.GetSCDThreshold(thresholdId);

                if (threshold != null)
                {
                    _ScorecardService.DeleteSCDThreshold(thresholdId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No threshold found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getthreshold/{thresholdId}")]
        public HttpResponseMessage GetThreshold(HttpRequestMessage request, int thresholdId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDThreshold threshold = _ScorecardService.GetSCDThreshold(thresholdId);

                // notice no need to create a seperate model object since Threshold entity will do just fine
                response = request.CreateResponse<SCDThreshold>(HttpStatusCode.OK, threshold);

                return response;
            });
        }

        [HttpGet]
        [Route("availablethreshold")]
        public HttpResponseMessage GetAvailableThresholds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDThresholdData[] threshold = _ScorecardService.GetAllSCDThresholds();

                return request.CreateResponse<SCDThresholdData[]>(HttpStatusCode.OK, threshold);
            });
        }
    }
}
