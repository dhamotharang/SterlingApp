
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
    [RoutePrefix("api/eclcomputationresult")]
    [UsesDisposableService]
    public class EclComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public EclComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateeclComputationResult")]
        public HttpResponseMessage UpdateEclComputationResult(HttpRequestMessage request, [FromBody]EclComputationResult eclComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var eclComputationResult = _IFRS9Service.UpdateEclComputationResult(eclComputationResultModel);

                return request.CreateResponse<EclComputationResult>(HttpStatusCode.OK, eclComputationResult);
            });
        }

        [HttpPost]
        [Route("deleteeclComputationResult")]
        public HttpResponseMessage DeleteEclComputationResult(HttpRequestMessage request, [FromBody]int eclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                EclComputationResult eclComputationResult = _IFRS9Service.GetEclComputationResult(eclComputationResultId);

                if (eclComputationResult != null)
                {
                    _IFRS9Service.DeleteEclComputationResult(eclComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No eclComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("geteclComputationResult/{eclComputationResultId}")]
        public HttpResponseMessage GetEclComputationResult(HttpRequestMessage request, int eclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EclComputationResult eclComputationResult = _IFRS9Service.GetEclComputationResult(eclComputationResultId);

                // notice no need to create a seperate model object since EclComputationResult entity will do just fine
                response = request.CreateResponse<EclComputationResult>(HttpStatusCode.OK, eclComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableeclComputationResults")]
        public HttpResponseMessage GetAvailableEclComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                EclComputationResult[] eclComputationResults = _IFRS9Service.GetAllEclComputationResults();

                return request.CreateResponse<EclComputationResult[]>(HttpStatusCode.OK, eclComputationResults);
            });
        }

        [HttpGet]
        [Route("availableeclComputationResultsByStage/{stage}")]
        public HttpResponseMessage GetAvailableEclComputationResultsByStage(HttpRequestMessage request, int stage)
        {
            return GetHttpResponse(request, () =>
            {
                EclComputationResult[] eclComputationResults = _IFRS9Service.GetAllEclComputationResultsByStage(stage);

                return request.CreateResponse<EclComputationResult[]>(HttpStatusCode.OK, eclComputationResults);
            });
        }
    }
}
