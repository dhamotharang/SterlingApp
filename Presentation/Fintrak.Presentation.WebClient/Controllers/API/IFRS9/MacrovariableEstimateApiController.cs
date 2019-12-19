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
    [RoutePrefix("api/macrovariableestimate")]
    [UsesDisposableService]
    public class MacrovariableEstimateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MacrovariableEstimateApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemacrovariableestimate")]
        public HttpResponseMessage UpdateMacrovariableEstimate(HttpRequestMessage request, [FromBody]MacrovariableEstimate macrovariableEstimateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var macrovariableEstimate = _IFRS9Service.UpdateMacrovariableEstimate(macrovariableEstimateModel);

                return request.CreateResponse<MacrovariableEstimate>(HttpStatusCode.OK, macrovariableEstimate);
            });
        }

        [HttpPost]
        [Route("deletemacrovariableestimate")]
        public HttpResponseMessage DeleteMacrovariableEstimate(HttpRequestMessage request, [FromBody]int MacrovariableEstimate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MacrovariableEstimate macrovariableEstimate = _IFRS9Service.GetMacrovariableEstimate(MacrovariableEstimate_Id);

                if (macrovariableEstimate != null)
                {
                    _IFRS9Service.DeleteMacrovariableEstimate(MacrovariableEstimate_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Macrovariable Estimate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmacrovariableestimate/{MacrovariableEstimate_Id}")]
        public HttpResponseMessage GetMacrovariableEstimate(HttpRequestMessage request, int MacrovariableEstimate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MacrovariableEstimate macrovariableEstimate = _IFRS9Service.GetMacrovariableEstimate(MacrovariableEstimate_Id);

                // notice no need to create a seperate model object since MacrovariableEstimate entity will do just fine
                response = request.CreateResponse<MacrovariableEstimate>(HttpStatusCode.OK, macrovariableEstimate);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemacrovariableestimates")]
        public HttpResponseMessage GetAvailableMacrovariableEstimates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MacrovariableEstimate[] macrovariableEstimate = _IFRS9Service.GetAllMacrovariableEstimates();

                return request.CreateResponse<MacrovariableEstimate[]>(HttpStatusCode.OK, macrovariableEstimate);
            });
        }

        [HttpGet]
        [Route("getmacrovariableestimatebycategory/{category}")]
        public HttpResponseMessage GetMacrovariableEstimateByCategory(HttpRequestMessage request, string Category)
        {
            return GetHttpResponse(request, () =>
            {
                MacrovariableEstimate[] macrovariableEstimate = _IFRS9Service.GetMacrovariableEstimateByCategory(Category);

                return request.CreateResponse<MacrovariableEstimate[]>(HttpStatusCode.OK, macrovariableEstimate);
            });
        }
    }
}
