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
    [RoutePrefix("api/kpientry")]
    [UsesDisposableService]
    public class KPIEntryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KPIEntryApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updatekpiEntry")]
        public HttpResponseMessage UpdateKPIEntry(HttpRequestMessage request, [FromBody]SCDKPIEntry kpiEntryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var kpiEntry = _ScorecardService.UpdateSCDKPIEntry(kpiEntryModel);

                return request.CreateResponse<SCDKPIEntry>(HttpStatusCode.OK, kpiEntry);
            });
        }

        [HttpPost]
        [Route("deletekpiEntry")]
        public HttpResponseMessage DeleteKPIEntry(HttpRequestMessage request, [FromBody]int kpiEntryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDKPIEntry kpiEntry = _ScorecardService.GetSCDKPIEntry(kpiEntryId);

                if (kpiEntry != null)
                {
                    _ScorecardService.DeleteSCDKPIEntry(kpiEntryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No kpiEntry found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getkpiEntry/{kpiEntryId}")]
        public HttpResponseMessage GetKPIEntry(HttpRequestMessage request, int kpiEntryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDKPIEntry kpiEntry = _ScorecardService.GetSCDKPIEntry(kpiEntryId);

                // notice no need to create a seperate model object since KPIEntry entity will do just fine
                response = request.CreateResponse<SCDKPIEntry>(HttpStatusCode.OK, kpiEntry);

                return response;
            });
        }

        [HttpGet]
        [Route("availablekpiEntry")]
        public HttpResponseMessage GetAvailableKPIEntrys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDKPIEntryData[] kpiEntry = _ScorecardService.GetAllSCDKPIEntrys();

                return request.CreateResponse<SCDKPIEntryData[]>(HttpStatusCode.OK, kpiEntry);
            });
        }
    }
}
