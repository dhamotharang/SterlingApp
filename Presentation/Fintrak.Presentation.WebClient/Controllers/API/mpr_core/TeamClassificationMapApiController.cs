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
    [RoutePrefix("api/teamclassificationmap")]
    [UsesDisposableService]
    public class TeamClassificationMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamClassificationMapApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateteamclassificationmap")]
        public HttpResponseMessage UpdateTeamClassificationMap(HttpRequestMessage request, [FromBody]TeamClassificationMap teamteamclassificationmapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamteamclassificationmap = _MPRCoreService.UpdateTeamClassificationMap(teamteamclassificationmapModel);

                return request.CreateResponse<TeamClassificationMap>(HttpStatusCode.OK, teamteamclassificationmap);
            });
        }

        [HttpPost]
        [Route("deleteteamClassificationMap")]
        public HttpResponseMessage DeleteTeamClassificationMap(HttpRequestMessage request, [FromBody]int teamClassificationMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TeamClassificationMap teamClassificationMap = _MPRCoreService.GetTeamClassificationMap(teamClassificationMapId);

                if (teamClassificationMap != null)
                {
                    _MPRCoreService.DeleteTeamClassificationMap(teamClassificationMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No teamClassificationMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getteamClassificationMap/{teamClassificationMapId}")]
        public HttpResponseMessage GetTeamClassificationMap(HttpRequestMessage request, int teamClassificationMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TeamClassificationMap teamClassificationMap = _MPRCoreService.GetTeamClassificationMap(teamClassificationMapId);

                // notice no need to create a seperate model object since TeamClassificationMap entity will do just fine
                response = request.CreateResponse<TeamClassificationMap>(HttpStatusCode.OK, teamClassificationMap);

                return response;
            });
        }

        [HttpGet]
        [Route("getteamlassificationmaps/{misCode}/{definitionCode}")]
        public HttpResponseMessage GetAvailableTeamClassificationMaps(HttpRequestMessage request,string misCode,string definitioncode)
        {
            return GetHttpResponse(request, () =>
            {
                TeamClassificationMap[] teamClassificationMaps = _MPRCoreService.GetAllTeamClassificationMaps(misCode, definitioncode);

                return request.CreateResponse<TeamClassificationMap[]>(HttpStatusCode.OK, teamClassificationMaps);
            });
        }
    }
}
