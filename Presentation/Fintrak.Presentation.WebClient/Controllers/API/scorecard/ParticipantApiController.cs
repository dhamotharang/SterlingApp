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
    [RoutePrefix("api/participant")]
    [UsesDisposableService]
    public class ParticipantApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ParticipantApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateparticipant")]
        public HttpResponseMessage UpdateParticipant(HttpRequestMessage request, [FromBody]SCDParticipant participantModel)
        {
            return GetHttpResponse(request, () =>
            {
                var participant = _ScorecardService.UpdateSCDParticipant(participantModel);

                return request.CreateResponse<SCDParticipant>(HttpStatusCode.OK, participant);
            });
        }

        [HttpPost]
        [Route("deleteparticipant")]
        public HttpResponseMessage DeleteParticipant(HttpRequestMessage request, [FromBody]int participantId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDParticipant participant = _ScorecardService.GetSCDParticipant(participantId);

                if (participant != null)
                {
                    _ScorecardService.DeleteSCDParticipant(participantId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No participant found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getparticipant/{participantId}")]
        public HttpResponseMessage GetParticipant(HttpRequestMessage request, int participantId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDParticipant participant = _ScorecardService.GetSCDParticipant(participantId);

                // notice no need to create a seperate model object since Participant entity will do just fine
                response = request.CreateResponse<SCDParticipant>(HttpStatusCode.OK, participant);

                return response;
            });
        }

        [HttpGet]
        [Route("availableparticipant")]
        public HttpResponseMessage GetAvailableParticipants(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDParticipantData[] participant = _ScorecardService.GetAllSCDParticipants();

                return request.CreateResponse<SCDParticipantData[]>(HttpStatusCode.OK, participant);
            });
        }
    }
}
