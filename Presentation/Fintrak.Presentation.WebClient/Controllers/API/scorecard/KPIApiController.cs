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
    [RoutePrefix("api/scdkpi")]
    [UsesDisposableService]
    public class KPIApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KPIApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatekpi")]
        public HttpResponseMessage UpdateKPI(HttpRequestMessage request, [FromBody]SCDKPI kpiModel)
        {
            return GetHttpResponse(request, () =>
            {
                var kpi = _ScorecardService.UpdateSCDKPI(kpiModel);

                return request.CreateResponse<SCDKPI>(HttpStatusCode.OK, kpi);
            });
        }

        [HttpPost]
        [Route("deletekpi")]
        public HttpResponseMessage DeleteKPI(HttpRequestMessage request, [FromBody]int kpiId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDKPI kpi = _ScorecardService.GetSCDKPI(kpiId);

                if (kpi != null)
                {
                    _ScorecardService.DeleteSCDKPI(kpiId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No kpi found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getkpi/{kpiId}")]
        public HttpResponseMessage GetKPI(HttpRequestMessage request, int kpiId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDKPI kpi = _ScorecardService.GetSCDKPI(kpiId);

                // notice no need to create a seperate model object since KPI entity will do just fine
                response = request.CreateResponse<SCDKPI>(HttpStatusCode.OK, kpi);

                return response;
            });
        }

        [HttpGet]
        [Route("availablekpi")]
        public HttpResponseMessage GetAvailableKPIs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDKPIData[] kpi = _ScorecardService.GetAllSCDKPIs();

                return request.CreateResponse<SCDKPIData[]>(HttpStatusCode.OK, kpi);
            });
        }
    }
}
