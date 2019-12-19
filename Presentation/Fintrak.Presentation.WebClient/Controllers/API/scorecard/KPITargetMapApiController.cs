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
    [RoutePrefix("api/kpiTargetMap")]
    [UsesDisposableService]
    public class KPITargetMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KPITargetMapApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatekpiTargetMap")]
        public HttpResponseMessage UpdateKPITargetMap(HttpRequestMessage request, [FromBody]SCDKPITargetMap kpiTargetMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var kpiTargetMap = _ScorecardService.UpdateSCDKPITargetMap(kpiTargetMapModel);

                return request.CreateResponse<SCDKPITargetMap>(HttpStatusCode.OK, kpiTargetMap);
            });
        }

        [HttpPost]
        [Route("deletekpiTargetMap")]
        public HttpResponseMessage DeleteKPITargetMap(HttpRequestMessage request, [FromBody]int kpiTargetMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDKPITargetMap kpiTargetMap = _ScorecardService.GetSCDKPITargetMap(kpiTargetMapId);

                if (kpiTargetMap != null)
                {
                    _ScorecardService.DeleteSCDKPITargetMap(kpiTargetMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No kpiTargetMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getkpiTargetMap/{kpiTargetMapId}")]
        public HttpResponseMessage GetKPITargetMap(HttpRequestMessage request, int kpiTargetMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDKPITargetMap kpiTargetMap = _ScorecardService.GetSCDKPITargetMap(kpiTargetMapId);

                // notice no need to create a seperate model object since KPITargetMap entity will do just fine
                response = request.CreateResponse<SCDKPITargetMap>(HttpStatusCode.OK, kpiTargetMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availablekpiTargetMap")]
        public HttpResponseMessage GetAvailableKPITargetMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDKPITargetMapData[] kpiTargetMap = _ScorecardService.GetAllSCDKPITargetMaps();

                return request.CreateResponse<SCDKPITargetMapData[]>(HttpStatusCode.OK, kpiTargetMap);
            });
        }
    }
}
