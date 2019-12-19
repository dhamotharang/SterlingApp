

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
    [RoutePrefix("api/lcbgeclcomputationresult")]
    [UsesDisposableService]
    public class LcBgEclComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public LcBgEclComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelcBgEclComputationResult")]
        public HttpResponseMessage UpdateLcBgEclComputationResult(HttpRequestMessage request, [FromBody]LcBgEclComputationResult lcBgEclComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lcBgEclComputationResult = _IFRS9Service.UpdateLcBgEclComputationResult(lcBgEclComputationResultModel);

                return request.CreateResponse<LcBgEclComputationResult>(HttpStatusCode.OK, lcBgEclComputationResult);
            });
        }

        [HttpPost]
        [Route("deletelcBgEclComputationResult")]
        public HttpResponseMessage DeleteLcBgEclComputationResult(HttpRequestMessage request, [FromBody]int lcBgEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LcBgEclComputationResult lcBgEclComputationResult = _IFRS9Service.GetLcBgEclComputationResult(lcBgEclComputationResultId);

                if (lcBgEclComputationResult != null)
                {
                    _IFRS9Service.DeleteLcBgEclComputationResult(lcBgEclComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No lcBgEclComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlcBgEclComputationResult/{lcBgEclComputationResultId}")]
        public HttpResponseMessage GetLcBgEclComputationResult(HttpRequestMessage request, int lcBgEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LcBgEclComputationResult lcBgEclComputationResult = _IFRS9Service.GetLcBgEclComputationResult(lcBgEclComputationResultId);

                // notice no need to create a seperate model object since LcBgEclComputationResult entity will do just fine
                response = request.CreateResponse<LcBgEclComputationResult>(HttpStatusCode.OK, lcBgEclComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelcBgEclComputationResults")]
        public HttpResponseMessage GetAvailableLcBgEclComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LcBgEclComputationResult[] lcBgEclComputationResults = _IFRS9Service.GetAllLcBgEclComputationResults();

                return request.CreateResponse<LcBgEclComputationResult[]>(HttpStatusCode.OK, lcBgEclComputationResults);
            });
        }
    }
}
