using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/fairvaluebasismap")]
    [UsesDisposableService]
    public class FairValueBasisMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FairValueBasisMapApiController(IFinancialInstrumentService fiService)
        {
            _FIService = fiService;
        }

        IFinancialInstrumentService _FIService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FIService);
        }

        [HttpPost]
        [Route("updatefairvaluebasismap")]
        public HttpResponseMessage UpdateFairValueBasisMap(HttpRequestMessage request, [FromBody]FairValueBasisMap fairValueBasisMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fairValueBasisMap = _FIService.UpdateFairValueBasisMap(fairValueBasisMapModel);

                return request.CreateResponse<FairValueBasisMap>(HttpStatusCode.OK, fairValueBasisMap);
            });
        }

        [HttpPost]
        [Route("deletefairvaluebasismap")]
        public HttpResponseMessage DeleteFairValueBasisMap(HttpRequestMessage request, [FromBody]int fairValueBasisMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FairValueBasisMap fairValueBasisMap = _FIService.GetFairValueBasisMap(fairValueBasisMapId);

                if (fairValueBasisMap != null)
                {
                    _FIService.DeleteFairValueBasisMap(fairValueBasisMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No fairvaluebasismap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getfairvaluebasismap/{fairValueBasisMapId}")]
        public HttpResponseMessage GetFairValueBasisMap(HttpRequestMessage request, int fairValueBasisMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FairValueBasisMap fairValueBasisMap = _FIService.GetFairValueBasisMap(fairValueBasisMapId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FairValueBasisMap>(HttpStatusCode.OK, fairValueBasisMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefairvaluebasismaps")]
        public HttpResponseMessage GetAvailableFairValueBasisMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FairValueBasisMapData[] fairValueBasisMaps = _FIService.GetAllFairValueBasisMaps();

                return request.CreateResponse<FairValueBasisMapData[]>(HttpStatusCode.OK, fairValueBasisMaps);
            });
        }
    }
}
