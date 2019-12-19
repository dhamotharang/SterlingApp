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
    [RoutePrefix("api/scdteamclassification")]
    [UsesDisposableService]
    public class SCDTeamClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SCDTeamClassificationApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateteamClassification")]
        public HttpResponseMessage UpdateTeamClassification(HttpRequestMessage request, [FromBody]SCDTeamClassification teamClassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamClassification = _ScorecardService.UpdateSCDTeamClassification(teamClassificationModel);

                return request.CreateResponse<SCDTeamClassification>(HttpStatusCode.OK, teamClassification);
            });
        }

        [HttpPost]
        [Route("deleteteamClassification")]
        public HttpResponseMessage DeleteTeamClassification(HttpRequestMessage request, [FromBody]int teamClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDTeamClassification teamClassification = _ScorecardService.GetSCDTeamClassification(teamClassificationId);

                if (teamClassification != null)
                {
                    _ScorecardService.DeleteSCDTeamClassification(teamClassificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamClassification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamClassification/{teamClassificationId}")]
        public HttpResponseMessage GetTeamClassification(HttpRequestMessage request, int teamClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDTeamClassification teamClassification = _ScorecardService.GetSCDTeamClassification(teamClassificationId);

                // notice no need to create a seperate model object since SCDTeamClassification entity will do just fine
                response = request.CreateResponse<SCDTeamClassification>(HttpStatusCode.OK, teamClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamClassification")]
        public HttpResponseMessage GetAvailableTeamClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDTeamClassification[] teamClassification = _ScorecardService.GetAllSCDTeamClassifications();

                return request.CreateResponse<SCDTeamClassification[]>(HttpStatusCode.OK, teamClassification);
            });
        }
    }
}
