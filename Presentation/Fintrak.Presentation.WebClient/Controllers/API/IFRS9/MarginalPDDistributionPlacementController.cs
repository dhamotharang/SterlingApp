
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
    [RoutePrefix("api/marginalpddistributionplacement")]
    [UsesDisposableService]
    public class MarginalPDDistributionPlacementController : ApiControllerBase
    {
        [ImportingConstructor]
        public MarginalPDDistributionPlacementController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemarginalPDDistributionPlacement")]
        public HttpResponseMessage UpdateMarginalPDDistributionPlacement(HttpRequestMessage request, [FromBody]MarginalPDDistributionPlacement marginalPDDistributionPlacementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var marginalPDDistributionPlacement = _IFRS9Service.UpdateMarginalPDDistributionPlacement(marginalPDDistributionPlacementModel);

                return request.CreateResponse<MarginalPDDistributionPlacement>(HttpStatusCode.OK, marginalPDDistributionPlacement);
            });
        }

        [HttpPost]
        [Route("deletemarginalPDDistributionPlacement")]
        public HttpResponseMessage DeleteMarginalPDDistributionPlacement(HttpRequestMessage request, [FromBody]int marginalPDDistributionPlacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MarginalPDDistributionPlacement marginalPDDistributionPlacement = _IFRS9Service.GetMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);

                if (marginalPDDistributionPlacement != null)
                {
                    _IFRS9Service.DeleteMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No marginalPDDistributionPlacement found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmarginalPDDistributionPlacement/{marginalPDDistributionPlacementId}")]
        public HttpResponseMessage GetMarginalPDDistributionPlacement(HttpRequestMessage request, int marginalPDDistributionPlacementId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MarginalPDDistributionPlacement marginalPDDistributionPlacement = _IFRS9Service.GetMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);

                // notice no need to create a seperate model object since MarginalPDDistributionPlacement entity will do just fine
                response = request.CreateResponse<MarginalPDDistributionPlacement>(HttpStatusCode.OK, marginalPDDistributionPlacement);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemarginalPDDistributionPlacements")]
        public HttpResponseMessage GetAvailableMarginalPDDistributionPlacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MarginalPDDistributionPlacement[] marginalPDDistributionPlacements = _IFRS9Service.GetAllMarginalPDDistributionPlacements();

                return request.CreateResponse<MarginalPDDistributionPlacement[]>(HttpStatusCode.OK, marginalPDDistributionPlacements);
            });
        }
    }
}
