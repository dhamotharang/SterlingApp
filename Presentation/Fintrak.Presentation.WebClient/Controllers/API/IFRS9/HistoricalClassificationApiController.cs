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
    [RoutePrefix("api/historicalclassification")]
    [UsesDisposableService]
    public class HistoricalClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HistoricalClassificationApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatehistoricalClassification")]
        public HttpResponseMessage UpdateHistoricalClassification(HttpRequestMessage request, [FromBody]HistoricalClassification historicalClassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var historicalClassification = _IFRS9Service.UpdateHistoricalClassification(historicalClassificationModel);

                return request.CreateResponse<HistoricalClassification>(HttpStatusCode.OK, historicalClassification);
            });
        }

        [HttpPost]
        [Route("deletehistoricalClassification")]
        public HttpResponseMessage DeleteHistoricalClassification(HttpRequestMessage request, [FromBody]int historicalClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HistoricalClassification historicalClassification = _IFRS9Service.GetHistoricalClassification(historicalClassificationId);

                if (historicalClassification != null)
                {
                    _IFRS9Service.DeleteHistoricalClassification(historicalClassificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No historicalClassification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gethistoricalClassification/{historicalClassificationId}")]
        public HttpResponseMessage GetHistoricalClassification(HttpRequestMessage request,int historicalClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HistoricalClassification historicalClassification = _IFRS9Service.GetHistoricalClassification(historicalClassificationId);

                // notice no need to create a seperate model object since HistoricalClassification entity will do just fine
                response = request.CreateResponse<HistoricalClassification>(HttpStatusCode.OK, historicalClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availablehistoricalClassifications")]
        public HttpResponseMessage GetAvailableHistoricalClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HistoricalClassification[] historicalClassifications = _IFRS9Service.GetAllHistoricalClassifications();

                return request.CreateResponse<HistoricalClassification[]>(HttpStatusCode.OK, historicalClassifications);
            });
        }
    }
}
