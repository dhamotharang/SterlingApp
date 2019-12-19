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
    [RoutePrefix("api/lgdcomputationresult")]
    [UsesDisposableService]
    public class LgdComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public LgdComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelgdComputationResult")]
        public HttpResponseMessage UpdateLgdComputationResult(HttpRequestMessage request, [FromBody]LgdComputationResult lgdComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lgdComputationResult = _IFRS9Service.UpdateLgdComputationResult(lgdComputationResultModel);

                return request.CreateResponse<LgdComputationResult>(HttpStatusCode.OK, lgdComputationResult);
            });
        }

        [HttpPost]
        [Route("deletelgdComputationResult")]
        public HttpResponseMessage DeleteLgdComputationResult(HttpRequestMessage request, [FromBody]int lgdComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LgdComputationResult lgdComputationResult = _IFRS9Service.GetLgdComputationResult(lgdComputationResultId);

                if (lgdComputationResult != null)
                {
                    _IFRS9Service.DeleteLgdComputationResult(lgdComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No lgdComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlgdComputationResult/{lgdComputationResultId}")]
        public HttpResponseMessage GetLgdComputationResult(HttpRequestMessage request, int lgdComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LgdComputationResult lgdComputationResult = _IFRS9Service.GetLgdComputationResult(lgdComputationResultId);

                // notice no need to create a seperate model object since LgdComputationResult entity will do just fine
                response = request.CreateResponse<LgdComputationResult>(HttpStatusCode.OK, lgdComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelgdComputationResults")]
        public HttpResponseMessage GetAvailableLgdComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LgdComputationResult[] lgdComputationResults = _IFRS9Service.GetAllLgdComputationResults();

                return request.CreateResponse<LgdComputationResult[]>(HttpStatusCode.OK, lgdComputationResults);
            });
        }
    }
}
