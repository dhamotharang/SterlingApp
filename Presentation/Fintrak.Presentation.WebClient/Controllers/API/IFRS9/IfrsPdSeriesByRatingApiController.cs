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
    [RoutePrefix("api/IfrsPdSeriesByRating")]
    [UsesDisposableService]
    public class IfrsPdSeriesByRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsPdSeriesByRatingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsPdSeriesByRating")]
        public HttpResponseMessage UpdateIfrsPdSeriesByRating(HttpRequestMessage request, [FromBody]IfrsPdSeriesByRating IfrsPdSeriesByRatingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsPdSeriesByRating = _IFRS9Service.UpdateIfrsPdSeriesByRating(IfrsPdSeriesByRatingModel);

                return request.CreateResponse<IfrsPdSeriesByRating>(HttpStatusCode.OK, IfrsPdSeriesByRating);
            });
        }

        [HttpPost]
        [Route("deleteIfrsPdSeriesByRating")]
        public HttpResponseMessage DeleteIfrsPdSeriesByRating(HttpRequestMessage request, [FromBody]int IfrsPdSeriesByRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsPdSeriesByRating IfrsPdSeriesByRating = _IFRS9Service.GetIfrsPdSeriesBySno(IfrsPdSeriesByRatingId);

                if (IfrsPdSeriesByRating != null)
                {
                    _IFRS9Service.DeleteIfrsPdSeriesByRating(IfrsPdSeriesByRatingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsPdSeriesByRating found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsPdSeriesBySno/{IfrsPdSeriesByRatingId}")]
        public HttpResponseMessage GetIfrsPdSeriesBySno(HttpRequestMessage request, int IfrsPdSeriesByRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsPdSeriesByRating IfrsPdSeriesByRating = _IFRS9Service.GetIfrsPdSeriesBySno(IfrsPdSeriesByRatingId);

                // notice no need to create a seperate model object since IfrsPdSeriesByRating entity will do just fine
                response = request.CreateResponse<IfrsPdSeriesByRating>(HttpStatusCode.OK, IfrsPdSeriesByRating);

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsPdSeriesByRating/{Code}")]
        public HttpResponseMessage GetIfrsPdSeriesByRating(HttpRequestMessage request, string code)
        {
            code = code.Replace("p", "+");
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsPdSeriesByRating[] IfrsPdSeriesByRating = _IFRS9Service.GetIfrsPdSeriesByRating(code);

                // notice no need to create a seperate model object since IfrsPdSeriesByRating entity will do just fine
                response = request.CreateResponse<IfrsPdSeriesByRating[]>(HttpStatusCode.OK, IfrsPdSeriesByRating);

                return response;
            });
        }

        [HttpGet]
        [Route("availableRatings")]
        public HttpResponseMessage GetAvailableIfrsPdSeriesByRatings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] IfrsPdSeriesByRatings = _IFRS9Service.GetAllRatings();

                return request.CreateResponse<string[]>(HttpStatusCode.OK, IfrsPdSeriesByRatings);
            });
        }
    }
}
