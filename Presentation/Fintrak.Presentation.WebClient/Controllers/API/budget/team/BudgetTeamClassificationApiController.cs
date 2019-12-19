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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API.Budget
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/budget/teamclassification")]
    [UsesDisposableService]
    public class BudgetTeamClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BudgetTeamClassificationApiController(ITeamService teamService)
        {
            _TeamService = teamService;
        }

        ITeamService _TeamService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_TeamService);
        }

        [HttpPost]
        [Route("updateteamclassification")]
        public HttpResponseMessage UpdateTeamClassification(HttpRequestMessage request, [FromBody]TeamClassification teamteamclassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamteamclassification = _TeamService.UpdateTeamClassification(teamteamclassificationModel);

                return request.CreateResponse<TeamClassification>(HttpStatusCode.OK, teamteamclassification);
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
                TeamClassification teamClassification = _TeamService.GetTeamClassification(teamClassificationId);

                if (teamClassification != null)
                {
                    _TeamService.DeleteTeamClassification(teamClassificationId);

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

                TeamClassification teamClassification = _TeamService.GetTeamClassification(teamClassificationId);

                // notice no need to create a seperate model object since TeamClassification entity will do just fine
                response = request.CreateResponse<TeamClassification>(HttpStatusCode.OK, teamClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamClassifications")]
        public HttpResponseMessage GetAvailableTeamClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassification[] teamClassifications = _TeamService.GetAllTeamClassifications();

                return request.CreateResponse<TeamClassification[]>(HttpStatusCode.OK, teamClassifications);
            });
        }
       
    }
}
