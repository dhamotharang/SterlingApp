using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;

namespace Fintrak.Presentation.WebClient.API.Budget
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/budget/teamclassificationtype")]
    [UsesDisposableService]
    public class TeamClassificationTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamClassificationTypeApiController(ITeamService teamService)
        {
            _TeamService = teamService;
        }

        ITeamService _TeamService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_TeamService);
        }

        [HttpPost]
        [Route("updateteamclassificationtype")]
        public HttpResponseMessage UpdateTeamClassificationType(HttpRequestMessage request, [FromBody]TeamClassificationType teamclassificationtypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamclassificationtype = _TeamService.UpdateTeamClassificationType(teamclassificationtypeModel);

                return request.CreateResponse<TeamClassificationType>(HttpStatusCode.OK, teamclassificationtype);
            });
        }

        [HttpPost]
        [Route("deleteteamClassificationType")]
        public HttpResponseMessage DeleteTeamClassificationType(HttpRequestMessage request, [FromBody]int teamClassificationTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamClassificationType teamClassificationType = _TeamService.GetTeamClassificationType(teamClassificationTypeId);

                if (teamClassificationType != null)
                {
                    _TeamService.DeleteTeamClassificationType(teamClassificationTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamClassificationType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamClassificationType/{teamClassificationTypeId}")]
        public HttpResponseMessage GetTeamClassificationType(HttpRequestMessage request, int teamClassificationTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamClassificationType teamClassificationType = _TeamService.GetTeamClassificationType(teamClassificationTypeId);

                // notice no need to create a seperate model object since TeamClassificationType entity will do just fine
                response = request.CreateResponse<TeamClassificationType>(HttpStatusCode.OK, teamClassificationType);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamClassificationTypes")]
        public HttpResponseMessage GetAvailableTeamClassificationTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassificationType[] teamClassificationTypes = _TeamService.GetAllTeamClassificationTypes();

                return request.CreateResponse<TeamClassificationType[]>(HttpStatusCode.OK, teamClassificationTypes);
            });
        }
    }
}
