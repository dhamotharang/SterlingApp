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
    [RoutePrefix("api/externalrating")]
    [UsesDisposableService]
    public class ExternalRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExternalRatingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateexternalRating")]
        public HttpResponseMessage UpdateExternalRating(HttpRequestMessage request, [FromBody]ExternalRating externalRatingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var externalRating = _IFRS9Service.UpdateExternalRating(externalRatingModel);

                return request.CreateResponse<ExternalRating>(HttpStatusCode.OK, externalRating);
            });
        }

        [HttpPost]
        [Route("deleteexternalRating")]
        public HttpResponseMessage DeleteExternalRating(HttpRequestMessage request, [FromBody]int externalRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExternalRating externalRating = _IFRS9Service.GetExternalRating(externalRatingId);

                if (externalRating != null)
                {
                    _IFRS9Service.DeleteExternalRating(externalRatingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No externalRating found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexternalRating/{externalRatingId}")]
        public HttpResponseMessage GetExternalRating(HttpRequestMessage request,int externalRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExternalRating externalRating = _IFRS9Service.GetExternalRating(externalRatingId);

                // notice no need to create a seperate model object since ExternalRating entity will do just fine
                response = request.CreateResponse<ExternalRating>(HttpStatusCode.OK, externalRating);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexternalRatings")]
        public HttpResponseMessage GetAvailableExternalRatings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExternalRating[] externalRatings = _IFRS9Service.GetAllExternalRatings();

                return request.CreateResponse<ExternalRating[]>(HttpStatusCode.OK, externalRatings);
            });
        }
    }
}
