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
    [RoutePrefix("api/placementcomputationresult")]
    [UsesDisposableService]
    public class PlacementComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public PlacementComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateplacementComputationResult")]
        public HttpResponseMessage UpdatePlacementComputationResult(HttpRequestMessage request, [FromBody]PlacementComputationResult placementComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var placementComputationResult = _IFRS9Service.UpdatePlacementComputationResult(placementComputationResultModel);

                return request.CreateResponse<PlacementComputationResult>(HttpStatusCode.OK, placementComputationResult);
            });
        }

        [HttpPost]
        [Route("deleteplacementComputationResult")]
        public HttpResponseMessage DeletePlacementComputationResult(HttpRequestMessage request, [FromBody]int placementComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PlacementComputationResult placementComputationResult = _IFRS9Service.GetPlacementComputationResult(placementComputationResultId);

                if (placementComputationResult != null)
                {
                    _IFRS9Service.DeletePlacementComputationResult(placementComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No placementComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getplacementComputationResult/{placementComputationResultId}")]
        public HttpResponseMessage GetPlacementComputationResult(HttpRequestMessage request, int placementComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PlacementComputationResult placementComputationResult = _IFRS9Service.GetPlacementComputationResult(placementComputationResultId);

                // notice no need to create a seperate model object since PlacementComputationResult entity will do just fine
                response = request.CreateResponse<PlacementComputationResult>(HttpStatusCode.OK, placementComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableplacementComputationResults")]
        public HttpResponseMessage GetAvailablePlacementComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PlacementComputationResult[] placementComputationResults = _IFRS9Service.GetAllPlacementComputationResults();

                return request.CreateResponse<PlacementComputationResult[]>(HttpStatusCode.OK, placementComputationResults);
            });
        }
    }
}
