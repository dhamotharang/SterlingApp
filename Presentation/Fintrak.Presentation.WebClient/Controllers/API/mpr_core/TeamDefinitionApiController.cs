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
    [RoutePrefix("api/teamdefinition")]
    [UsesDisposableService]
    public class TeamDefinitionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamDefinitionApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateteamdefinition")]
        public HttpResponseMessage UpdateTeamDefinition(HttpRequestMessage request, [FromBody]TeamDefinition teamdefinitionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamdefinition = _MPRCoreService.UpdateTeamDefinition(teamdefinitionModel);

                return request.CreateResponse<TeamDefinition>(HttpStatusCode.OK, teamdefinition);
            });
        }

        [HttpPost]
        [Route("deleteteamDefinition")]
        public HttpResponseMessage DeleteTeamDefinition(HttpRequestMessage request, [FromBody]int teamDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamDefinition teamDefinition = _MPRCoreService.GetTeamDefinition(teamDefinitionId);

                if (teamDefinition != null)
                {
                    _MPRCoreService.DeleteTeamDefinition(teamDefinitionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamDefinition found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamDefinition/{teamDefinitionId}")]
        public HttpResponseMessage GetTeamDefinition(HttpRequestMessage request, int teamDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamDefinition teamDefinition = _MPRCoreService.GetTeamDefinition(teamDefinitionId);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<TeamDefinition>(HttpStatusCode.OK, teamDefinition);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamDefinitionByCode/{definitionCode}")]
        public HttpResponseMessage GetTeamDefinitionByCode(HttpRequestMessage request, string definitionCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamDefinition teamDefinition = _MPRCoreService.GetTeamDefinitionByCode(definitionCode);

                // notice no need to create a seperate model object since TeamDefinition entity will do just fine
                response = request.CreateResponse<TeamDefinition>(HttpStatusCode.OK, teamDefinition);

                return response;
            });
        }

        [HttpGet]
        [Route("availableteamDefinitions")]
        public HttpResponseMessage GetAvailableTeamDefinitions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<TeamDefinition> teamDefinitions = _MPRCoreService.GetAllTeamDefinitions();

                return request.CreateResponse<IEnumerable<TeamDefinition>>(HttpStatusCode.OK, teamDefinitions);
            });
        }
    }
}
