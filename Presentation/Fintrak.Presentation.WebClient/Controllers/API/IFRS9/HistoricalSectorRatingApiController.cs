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
    [RoutePrefix("api/historicalsectorrating")]
    [UsesDisposableService]
    public class HistoricalSectorRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalSectorRatingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatehistoricalSectorRating")]
        public HttpResponseMessage UpdateHistoricalSectorRating(HttpRequestMessage request, [FromBody]HistoricalSectorRating historicalSectorRatingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicalSectorRating = _IFRS9Service.UpdateHistoricalSectorRating(historicalSectorRatingModel);

                return request.CreateResponse<HistoricalSectorRating>(HttpStatusCode.OK, historicalSectorRating);
            });
        }

        [HttpPost]
        [Route("deletehistoricalSectorRating")]
        public HttpResponseMessage DeleteHistoricalSectorRating(HttpRequestMessage request, [FromBody]int historicalSectorRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalSectorRating historicalSectorRating = _IFRS9Service.GetHistoricalSectorRating(historicalSectorRatingId);

                if (historicalSectorRating != null)
                {
                    _IFRS9Service.DeleteHistoricalSectorRating(historicalSectorRatingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No historicalSectorRating found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricalSectorRating/{historicalSectorRatingId}")]
        public HttpResponseMessage GetHistoricalSectorRating(HttpRequestMessage request,int historicalSectorRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalSectorRating historicalSectorRating = _IFRS9Service.GetHistoricalSectorRating(historicalSectorRatingId);

                // notice no need to create a seperate model object since HistoricalSectorRating entity will do just fine
                response = request.CreateResponse<HistoricalSectorRating>(HttpStatusCode.OK, historicalSectorRating);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricalSectorRatings")]
        public HttpResponseMessage GetAvailableHistoricalSectorRatings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HistoricalSectorRating[] historicalSectorRatings = _IFRS9Service.GetAllHistoricalSectorRatings();

                return request.CreateResponse<HistoricalSectorRating[]>(HttpStatusCode.OK, historicalSectorRatings);
            });
        }
    }
}
