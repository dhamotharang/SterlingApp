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
    [RoutePrefix("api/teamclassificationtype")]
    [UsesDisposableService]
    public class TeamClassificationTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TeamClassificationTypeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateteamclassificationtype")]
        public HttpResponseMessage UpdateTeamClassificationType(HttpRequestMessage request, [FromBody]TeamClassificationType teamclassificationtypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamclassificationtype = _MPRCoreService.UpdateTeamClassificationType(teamclassificationtypeModel);

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
                TeamClassificationType teamClassificationType = _MPRCoreService.GetTeamClassificationType(teamClassificationTypeId);

                if (teamClassificationType != null)
                {
                    _MPRCoreService.DeleteTeamClassificationType(teamClassificationTypeId);

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

                TeamClassificationType teamClassificationType = _MPRCoreService.GetTeamClassificationType(teamClassificationTypeId);

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
                TeamClassificationType[] teamClassificationTypes = _MPRCoreService.GetAllTeamClassificationTypes();

                return request.CreateResponse<TeamClassificationType[]>(HttpStatusCode.OK, teamClassificationTypes);
            });
        }
    }
}
