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
    [RoutePrefix("api/collateralgrowthrate")]
    [UsesDisposableService]
    public class CollateralGrowthRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralGrowthRateApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecollateralgrowthrate")]
        public HttpResponseMessage UpdateCollateralGrowthRate(HttpRequestMessage request, [FromBody]CollateralGrowthRate collateralgrowthrateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralgrowthrate = _IFRS9Service.UpdateCollateralGrowthRate(collateralgrowthrateModel);

                return request.CreateResponse<CollateralGrowthRate>(HttpStatusCode.OK, collateralgrowthrate);
            });
        }

        [HttpPost]
        [Route("deletecollateralgrowthrate")]
        public HttpResponseMessage DeleteCollateralGrowthRate(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralGrowthRate collateralgrowthrate = _IFRS9Service.GetCollateralGrowthRate(ID);

                if (collateralgrowthrate != null)
                {
                    _IFRS9Service.DeleteCollateralGrowthRate(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CollateralGrowthRate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralgrowthrate/{ID}")]
        public HttpResponseMessage GetCollateralGrowthRate(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralGrowthRate collateralgrowthrate = _IFRS9Service.GetCollateralGrowthRate(ID);

                // notice no need to create a seperate model object since CollateralGrowthRate entity will do just fine
                response = request.CreateResponse<CollateralGrowthRate>(HttpStatusCode.OK, collateralgrowthrate);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralgrowthrates/{defaultCount}")]
        public HttpResponseMessage GetAvailableCollateralGrowthRates(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                CollateralGrowthRate[] collateralgrowthrates = _IFRS9Service.GetCollateralGrowthRates(defaultCount);
                return request.CreateResponse<CollateralGrowthRate[]>(HttpStatusCode.OK, collateralgrowthrates);
            });
        }



        [HttpGet]
        [Route("getCollateralGrowthRatebysearch/{searchParam}")]
        public HttpResponseMessage GetCollateralGrowthRateBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CollateralGrowthRate[] collateralgrowthrates = _IFRS9Service.GetCollateralGrowthRateBySearch(searchParam);
                return request.CreateResponse<CollateralGrowthRate[]>(HttpStatusCode.OK, collateralgrowthrates.ToArray());
            });
        }





    }
}
