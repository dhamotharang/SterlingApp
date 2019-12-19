using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/lgdcomputationresultplacement")]
    [UsesDisposableService]
    public class LgdComputationResultPlacementController : ApiControllerBase
    {
        [ImportingConstructor]
        public LgdComputationResultPlacementController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelgdComputationResultPlacement")]
        public HttpResponseMessage UpdateLgdComputationResultPlacement(HttpRequestMessage request, [FromBody]LgdComputationResultPlacement lgdComputationResultPlacementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lgdComputationResultPlacement = _IFRS9Service.UpdateLgdComputationResultPlacement(lgdComputationResultPlacementModel);

                return request.CreateResponse<LgdComputationResultPlacement>(HttpStatusCode.OK, lgdComputationResultPlacement);
            });
        }

        [HttpPost]
        [Route("deletelgdComputationResultPlacement")]
        public HttpResponseMessage DeleteLgdComputationResultPlacement(HttpRequestMessage request, [FromBody]int lgdComputationResultPlacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LgdComputationResultPlacement lgdComputationResultPlacement = _IFRS9Service.GetLgdComputationResultPlacement(lgdComputationResultPlacementId);

                if (lgdComputationResultPlacement != null)
                {
                    _IFRS9Service.DeleteLgdComputationResultPlacement(lgdComputationResultPlacementId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No lgdComputationResultPlacement found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlgdComputationResultPlacement/{lgdComputationResultPlacementId}")]
        public HttpResponseMessage GetLgdComputationResultPlacement(HttpRequestMessage request, int lgdComputationResultPlacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LgdComputationResultPlacement lgdComputationResultPlacement = _IFRS9Service.GetLgdComputationResultPlacement(lgdComputationResultPlacementId);

                // notice no need to create a seperate model object since LgdComputationResultPlacement entity will do just fine
                response = request.CreateResponse<LgdComputationResultPlacement>(HttpStatusCode.OK, lgdComputationResultPlacement);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelgdComputationResultPlacements")]
        public HttpResponseMessage GetAvailableLgdComputationResultPlacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LgdComputationResultPlacement[] lgdComputationResultPlacements = _IFRS9Service.GetAllLgdComputationResultPlacements();

                return request.CreateResponse<LgdComputationResultPlacement[]>(HttpStatusCode.OK, lgdComputationResultPlacements);
            });
        }
    }
}
