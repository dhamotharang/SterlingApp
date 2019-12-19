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
    [RoutePrefix("api/teammap")]
    [UsesDisposableService]
    public class TeamMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamMapApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateteamMap")]
        public HttpResponseMessage UpdateTeamMap(HttpRequestMessage request, [FromBody]SCDTeamMap teamMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamMap = _ScorecardService.UpdateSCDTeamMap(teamMapModel);

                return request.CreateResponse<SCDTeamMap>(HttpStatusCode.OK, teamMap);
            });
        }

        [HttpPost]
        [Route("deleteteamMap")]
        public HttpResponseMessage DeleteTeamMap(HttpRequestMessage request, [FromBody]int teamMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDTeamMap teamMap = _ScorecardService.GetSCDTeamMap(teamMapId);

                if (teamMap != null)
                {
                    _ScorecardService.DeleteSCDTeamMap(teamMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamMap/{teamMapId}")]
        public HttpResponseMessage GetTeamMap(HttpRequestMessage request, int teamMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDTeamMap teamMap = _ScorecardService.GetSCDTeamMap(teamMapId);

                // notice no need to create a seperate model object since TeamMap entity will do just fine
                response = request.CreateResponse<SCDTeamMap>(HttpStatusCode.OK, teamMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamMap")]
        public HttpResponseMessage GetAvailableTeamMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDTeamMapData[] teamMap = _ScorecardService.GetAllSCDTeamMaps();

                return request.CreateResponse<SCDTeamMapData[]>(HttpStatusCode.OK, teamMap);
            });
        }
    }
}
