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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Extensions;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/team")]
    [UsesDisposableService]
    public class TeamApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateteam")]
        public HttpResponseMessage UpdateTeam(HttpRequestMessage request, [FromBody]Team teamModel)
        {
            return GetHttpResponse(request, () =>
            {
                var team = _MPRCoreService.UpdateTeam(teamModel);

                return request.CreateResponse<Team>(HttpStatusCode.OK, team);
            });
        }

        [HttpPost]
        [Route("deleteteam")]
        public HttpResponseMessage DeleteTeam(HttpRequestMessage request, [FromBody]int teamId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Team team = _MPRCoreService.GetTeam(teamId);

                if (team != null)
                {
                    _MPRCoreService.DeleteTeam(teamId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No team found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteam/{teamId}")]
        public HttpResponseMessage GetTeam(HttpRequestMessage request, int teamId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Team team = _MPRCoreService.GetTeam(teamId);

                // notice no need to create a seperate model object since Team entity will do just fine
                response = request.CreateResponse<Team>(HttpStatusCode.OK, team);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamwithchildren/{teamId}")]
        public HttpResponseMessage GetTeamWithChildren(HttpRequestMessage request, int teamId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var teamModel = new TeamModel();

                teamModel.Team = _MPRCoreService.GetTeam(teamId);
                teamModel.Definition = _MPRCoreService.GetTeamDefinitionByCode(teamModel.Team.DefinitionCode);
                teamModel.Classifications = _MPRCoreService.GetAllTeamClassificationMaps(teamModel.Team.Code, teamModel.Team.DefinitionCode);

                // notice no need to create a seperate model object since Team entity will do just fine
                response = request.CreateResponse<TeamModel>(HttpStatusCode.OK, teamModel);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteams")]
        public HttpResponseMessage GetAvailableTeams(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TeamData[] teams = _MPRCoreService.GetTeams();

                return request.CreateResponse<TeamData[]>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getteambyparentdefinition/{definitionCode}/{misCode}")]
        public HttpResponseMessage GetTeamByParentDefinition(HttpRequestMessage request, string definitionCode, string misCode)
        {
            return GetHttpResponse(request, () =>
            {
                TeamData[] teams = _MPRCoreService.GetTeams();

                var definitions = _MPRCoreService.GetAllTeamDefinitions();
                var maxPosition = definitions.Max(c => c.Position);

                var definition = definitions.Where(c => c.Code == definitionCode).FirstOrDefault();
                var teamDefinition = definitions.Where(c => c.Position == 2).FirstOrDefault();

                IEnumerable<TeamDeepModel> nodes = teams.RecursiveJoin(element => element.Code, element => element.ParentCode,
                (TeamData element, int index, int depth, IEnumerable<TeamDeepModel> children) =>
                {
                    return new TeamDeepModel()
                    {
                        TeamId = element.TeamId,
                        Code = element.Code,
                        Name = element.Name,
                        ParentCode = element.ParentCode,
                        ParentName = element.ParentName,
                        DefinitionCode = element.DefinitionCode,
                        Children = children,
                        Depth = maxPosition - depth
                    };
                });

                var parent = GetParentTeam(nodes, definitionCode, misCode);
                var selectedTeams = new List<TeamDeepModel>();
                if (parent != null)
                {
                    selectedTeams = GetSelectedTeams(parent.Children, teamDefinition);
                }

                return request.CreateResponse<TeamDeepModel[]>(HttpStatusCode.OK, selectedTeams.ToArray());
            });
        }

        [HttpGet]
        [Route("getparentteams/{definitionCode}")]
        public HttpResponseMessage GetAvailableTeams(HttpRequestMessage request, string definitionCode)
        {
            return GetHttpResponse(request, () =>
            {

                Team[] teams = _MPRCoreService.GetParentTeams(definitionCode);

                return request.CreateResponse<Team[]>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getteambylevel/{level}")]
        public HttpResponseMessage GetTeamByLevel(HttpRequestMessage request, int level)
        {
            return GetHttpResponse(request, () =>
            {

                Team[] teams = _MPRCoreService.GetTeamByLevel(level);

                return request.CreateResponse<Team[]>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getteambydefinition/{definitionCode}")]
        public HttpResponseMessage GetTeamByDefinition(HttpRequestMessage request, string definitionCode)
        {
            return GetHttpResponse(request, () =>
            {

                IEnumerable<Team> teams = _MPRCoreService.GetTeamByDefinition(definitionCode);

                return request.CreateResponse<IEnumerable<Team>>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getteamsbysearch/{searchvalue}")]
        public HttpResponseMessage GetTeamsBySearch(HttpRequestMessage request, string searchValue)
        {
            return GetHttpResponse(request, () =>
            {

                TeamData[] teams = _MPRCoreService.GetTeamsBySearch(searchValue);

                return request.CreateResponse<TeamData[]>(HttpStatusCode.OK, teams);
            });
        }

        private TeamDeepModel GetParentTeam(IEnumerable<TeamDeepModel> items, string definitionCode, string misCode)
        {
            TeamDeepModel node = null;

            foreach (var item in items)
            {
                if (item.DefinitionCode == definitionCode && item.Code == misCode)
                {
                    node = item;
                }
                else
                    node = GetParentTeam(item.Children, definitionCode, misCode);

                if (node != null)
                    return node;

            }

            return node;
        }

        private List<TeamDeepModel> GetSelectedTeams(IEnumerable<TeamDeepModel> items, TeamDefinition teamDefinition)
        {
            var nodes = new List<TeamDeepModel>();

            foreach (var item in items)
            {
                if (item.Depth == teamDefinition.Position)
                {
                    nodes.Add(item);
                }
                else
                {
                    nodes.AddRange(GetSelectedTeams(item.Children, teamDefinition));
                }
            }

            return nodes;
        }
    }
}
