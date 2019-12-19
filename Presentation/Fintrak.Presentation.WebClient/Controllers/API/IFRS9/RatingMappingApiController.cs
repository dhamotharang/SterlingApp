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
    [RoutePrefix("api/ratingmapping")]
    [UsesDisposableService]
    public class RatingMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RatingMappingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateratingMapping")]
        public HttpResponseMessage UpdateRatingMapping(HttpRequestMessage request, [FromBody]RatingMapping ratingMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ratingMapping = _IFRS9Service.UpdateRatingMapping(ratingMappingModel);

                return request.CreateResponse<RatingMapping>(HttpStatusCode.OK, ratingMapping);
            });
        }

        [HttpPost]
        [Route("deleteratingMapping")]
        public HttpResponseMessage DeleteRatingMapping(HttpRequestMessage request, [FromBody]int ratingMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RatingMapping ratingMapping = _IFRS9Service.GetRatingMapping(ratingMappingId);

                if (ratingMapping != null)
                {
                    _IFRS9Service.DeleteRatingMapping(ratingMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ratingMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getratingMapping/{ratingMappingId}")]
        public HttpResponseMessage GetRatingMapping(HttpRequestMessage request,int ratingMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RatingMapping ratingMapping = _IFRS9Service.GetRatingMapping(ratingMappingId);

                // notice no need to create a seperate model object since RatingMapping entity will do just fine
                response = request.CreateResponse<RatingMapping>(HttpStatusCode.OK, ratingMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableratingMappings")]
        public HttpResponseMessage GetAvailableRatingMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RatingMappingData[] ratingMappings = _IFRS9Service.GetRatingMappings();

                return request.CreateResponse<RatingMappingData[]>(HttpStatusCode.OK, ratingMappings);
            });
        }
    }
}
