using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/scdconfiguration")]
    [UsesDisposableService]
    public class SCDConfigurationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SCDConfigurationApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateconfiguration")]
        public HttpResponseMessage UpdateSCDConfiguration(HttpRequestMessage request, [FromBody]SCDConfiguration configurationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var configuration = _ScorecardService.UpdateSCDConfiguration(configurationModel);

                return request.CreateResponse<SCDConfiguration>(HttpStatusCode.OK, configuration);
            });
        }

        [HttpGet]
        [Route("getconfiguration")]
        public HttpResponseMessage GetSCDConfiguration(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDConfiguration configuration = _ScorecardService.GetFirstSetup();

                // notice no need to create a seperate model object since SCDConfiguration entity will do just fine
                response = request.CreateResponse<SCDConfiguration>(HttpStatusCode.OK, configuration);

                return response;
            });
        }
    }
}
