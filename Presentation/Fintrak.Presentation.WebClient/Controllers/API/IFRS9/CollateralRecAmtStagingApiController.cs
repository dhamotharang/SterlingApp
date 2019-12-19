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
    [RoutePrefix("api/collateralrecamtstaging")]
    [UsesDisposableService]
    public class CollateralRecAmtStagingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralRecAmtStagingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecollateralrecamtstaging")]
        public HttpResponseMessage UpdateCollateralRecAmtStaging(HttpRequestMessage request, [FromBody]CollateralRecAmtStaging collateralrecamtstagingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralrecamtstaging = _IFRS9Service.UpdateCollateralRecAmtStaging(collateralrecamtstagingModel);

                return request.CreateResponse<CollateralRecAmtStaging>(HttpStatusCode.OK, collateralrecamtstaging);
            });
        }

        [HttpPost]
        [Route("deletecollateralrecamtstaging")]
        public HttpResponseMessage DeleteCollateralRecAmtStaging(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralRecAmtStaging collateralrecamtstaging = _IFRS9Service.GetCollateralRecAmtStaging(ID);

                if (collateralrecamtstaging != null)
                {
                    _IFRS9Service.DeleteCollateralRecAmtStaging(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CollateralRecAmtStaging found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralrecamtstaging/{ID}")]
        public HttpResponseMessage GetCollateralRecAmtStaging(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralRecAmtStaging collateralrecamtstaging = _IFRS9Service.GetCollateralRecAmtStaging(ID);

                // notice no need to create a seperate model object since CollateralRecAmtStaging entity will do just fine
                response = request.CreateResponse<CollateralRecAmtStaging>(HttpStatusCode.OK, collateralrecamtstaging);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralrecamtstagings/{defaultCount}")]
        public HttpResponseMessage GetAvailableCollateralRecAmtStagings(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                CollateralRecAmtStaging[] collateralrecamtstagings = _IFRS9Service.GetCollateralRecAmtStagings(defaultCount);
                return request.CreateResponse<CollateralRecAmtStaging[]>(HttpStatusCode.OK, collateralrecamtstagings);
            });
        }



        [HttpGet]
        [Route("getCollateralRecAmtStagingbysearch/{searchParam}")]
        public HttpResponseMessage GetCollateralRecAmtStagingBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CollateralRecAmtStaging[] collateralrecamtstagings = _IFRS9Service.GetCollateralRecAmtStagingBySearch(searchParam);
                return request.CreateResponse<CollateralRecAmtStaging[]>(HttpStatusCode.OK, collateralrecamtstagings.ToArray());
            });
        }





    }
}
