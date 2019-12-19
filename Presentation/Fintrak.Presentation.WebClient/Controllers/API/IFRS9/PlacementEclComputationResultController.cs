
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
    [RoutePrefix("api/placementeclcomputationresult")]
    [UsesDisposableService]
    public class PlacementEclComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public PlacementEclComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateplacementEclComputationResult")]
        public HttpResponseMessage UpdatePlacementEclComputationResult(HttpRequestMessage request, [FromBody]PlacementEclComputationResult placementEclComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var placementEclComputationResult = _IFRS9Service.UpdatePlacementEclComputationResult(placementEclComputationResultModel);

                return request.CreateResponse<PlacementEclComputationResult>(HttpStatusCode.OK, placementEclComputationResult);
            });
        }

        [HttpPost]
        [Route("deleteplacementEclComputationResult")]
        public HttpResponseMessage DeletePlacementEclComputationResult(HttpRequestMessage request, [FromBody]int placementEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PlacementEclComputationResult placementEclComputationResult = _IFRS9Service.GetPlacementEclComputationResult(placementEclComputationResultId);

                if (placementEclComputationResult != null)
                {
                    _IFRS9Service.DeletePlacementEclComputationResult(placementEclComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No placementEclComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getplacementEclComputationResult/{placementEclComputationResultId}")]
        public HttpResponseMessage GetPlacementEclComputationResult(HttpRequestMessage request, int placementEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PlacementEclComputationResult placementEclComputationResult = _IFRS9Service.GetPlacementEclComputationResult(placementEclComputationResultId);

                // notice no need to create a seperate model object since PlacementEclComputationResult entity will do just fine
                response = request.CreateResponse<PlacementEclComputationResult>(HttpStatusCode.OK, placementEclComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableplacementEclComputationResults")]
        public HttpResponseMessage GetAvailablePlacementEclComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PlacementEclComputationResult[] placementEclComputationResults = _IFRS9Service.GetAllPlacementEclComputationResults();

                return request.CreateResponse<PlacementEclComputationResult[]>(HttpStatusCode.OK, placementEclComputationResults);
            });
        }
    }
}
