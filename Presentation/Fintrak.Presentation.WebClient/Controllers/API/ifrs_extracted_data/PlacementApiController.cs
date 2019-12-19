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
    [RoutePrefix("api/placement")]
    [UsesDisposableService]
    public class PlacementApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PlacementApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateplacement")]
        public HttpResponseMessage UpdatePlacement(HttpRequestMessage request, [FromBody]Placement placementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var placement = _IFRSDataService.UpdatePlacement(placementModel);

                return request.CreateResponse<Placement>(HttpStatusCode.OK, placement);
            });
        }

        [HttpPost]
        [Route("deleteplacement")]
        public HttpResponseMessage DeletePlacement(HttpRequestMessage request, [FromBody]int Placement_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Placement placement = _IFRSDataService.GetPlacement(Placement_Id);

                if (placement != null)
                {
                    _IFRSDataService.DeletePlacement(Placement_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No placement found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getplacement/{placement_Id}")]
        public HttpResponseMessage GetPlacement(HttpRequestMessage request, int Placement_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Placement placement = _IFRSDataService.GetPlacement(Placement_Id);

                // notice no need to create a seperate model object since Placement entity will do just fine
                response = request.CreateResponse<Placement>(HttpStatusCode.OK, placement);

                return response;
            });
        }

        [HttpGet]
        [Route("availableplacements")]
        public HttpResponseMessage GetAvailablePlacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Placement[] placements = _IFRSDataService.GetAllPlacements();

                return request.CreateResponse<Placement[]>(HttpStatusCode.OK, placements);
            });
        }
    }
}
