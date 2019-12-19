
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
    [RoutePrefix("api/tBilleclcomputationresult")]
    [UsesDisposableService]
    public class TBillEclComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public TBillEclComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatetBillEclComputationResult")]
        public HttpResponseMessage UpdateTBillEclComputationResult(HttpRequestMessage request, [FromBody]TBillEclComputationResult tBillEclComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var tBillEclComputationResult = _IFRS9Service.UpdateTBillEclComputationResult(tBillEclComputationResultModel);

                return request.CreateResponse<TBillEclComputationResult>(HttpStatusCode.OK, tBillEclComputationResult);
            });
        }

        [HttpPost]
        [Route("deletetBillEclComputationResult")]
        public HttpResponseMessage DeleteTBillEclComputationResult(HttpRequestMessage request, [FromBody]int tBillEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TBillEclComputationResult tBillEclComputationResult = _IFRS9Service.GetTBillEclComputationResult(tBillEclComputationResultId);

                if (tBillEclComputationResult != null)
                {
                    _IFRS9Service.DeleteTBillEclComputationResult(tBillEclComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No tBillEclComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gettBillEclComputationResult/{tBillEclComputationResultId}")]
        public HttpResponseMessage GetTBillEclComputationResult(HttpRequestMessage request, int tBillEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TBillEclComputationResult tBillEclComputationResult = _IFRS9Service.GetTBillEclComputationResult(tBillEclComputationResultId);

                // notice no need to create a seperate model object since TBillEclComputationResult entity will do just fine
                response = request.CreateResponse<TBillEclComputationResult>(HttpStatusCode.OK, tBillEclComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availabletBillEclComputationResults/{type}")]
        public HttpResponseMessage GetAvailableTBillEclComputationResults(HttpRequestMessage request, int type)
        {
            return GetHttpResponse(request, () =>
            {
                TBillEclComputationResult[] tBillEclComputationResults = _IFRS9Service.GetAllTBillEclComputationResults().Where(x => x.Flag == type).ToArray();

                return request.CreateResponse<TBillEclComputationResult[]>(HttpStatusCode.OK, tBillEclComputationResults);
            });
        }
    }
}
