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
    [RoutePrefix("api/kpiActualMap")]
    [UsesDisposableService]
    public class KPIActualMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KPIActualMapApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatekpiActualMap")]
        public HttpResponseMessage UpdateKPIActualMap(HttpRequestMessage request, [FromBody]SCDKPIActualMap kpiActualMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var kpiActualMap = _ScorecardService.UpdateSCDKPIActualMap(kpiActualMapModel);

                return request.CreateResponse<SCDKPIActualMap>(HttpStatusCode.OK, kpiActualMap);
            });
        }

        [HttpPost]
        [Route("deletekpiActualMap")]
        public HttpResponseMessage DeleteKPIActualMap(HttpRequestMessage request, [FromBody]int kpiActualMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDKPIActualMap kpiActualMap = _ScorecardService.GetSCDKPIActualMap(kpiActualMapId);

                if (kpiActualMap != null)
                {
                    _ScorecardService.DeleteSCDKPIActualMap(kpiActualMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No kpiActualMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getkpiActualMap/{kpiActualMapId}")]
        public HttpResponseMessage GetKPIActualMap(HttpRequestMessage request, int kpiActualMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDKPIActualMap kpiActualMap = _ScorecardService.GetSCDKPIActualMap(kpiActualMapId);

                // notice no need to create a seperate model object since KPIActualMap entity will do just fine
                response = request.CreateResponse<SCDKPIActualMap>(HttpStatusCode.OK, kpiActualMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availablekpiActualMap")]
        public HttpResponseMessage GetAvailableKPIActualMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDKPIActualMapData[] kpiActualMap = _ScorecardService.GetAllSCDKPIActualMaps();

                return request.CreateResponse<SCDKPIActualMapData[]>(HttpStatusCode.OK, kpiActualMap);
            });
        }
    }
}
