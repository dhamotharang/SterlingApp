
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
    [RoutePrefix("api/bondeclcomputationresult")]
    [UsesDisposableService]
    public class BondEclComputationResultController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondEclComputationResultController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatebondEclComputationResult")]
        public HttpResponseMessage UpdateBondEclComputationResult(HttpRequestMessage request, [FromBody]BondEclComputationResult bondEclComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bondEclComputationResult = _IFRS9Service.UpdateBondEclComputationResult(bondEclComputationResultModel);

                return request.CreateResponse<BondEclComputationResult>(HttpStatusCode.OK, bondEclComputationResult);
            });
        }

        [HttpPost]
        [Route("deletebondEclComputationResult")]
        public HttpResponseMessage DeleteBondEclComputationResult(HttpRequestMessage request, [FromBody]int bondEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BondEclComputationResult bondEclComputationResult = _IFRS9Service.GetBondEclComputationResult(bondEclComputationResultId);

                if (bondEclComputationResult != null)
                {
                    _IFRS9Service.DeleteBondEclComputationResult(bondEclComputationResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bondEclComputationResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbondEclComputationResult/{bondEclComputationResultId}")]
        public HttpResponseMessage GetBondEclComputationResult(HttpRequestMessage request, int bondEclComputationResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondEclComputationResult bondEclComputationResult = _IFRS9Service.GetBondEclComputationResult(bondEclComputationResultId);

                // notice no need to create a seperate model object since BondEclComputationResult entity will do just fine
                response = request.CreateResponse<BondEclComputationResult>(HttpStatusCode.OK, bondEclComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebondEclComputationResults")]
        public HttpResponseMessage GetAvailableBondEclComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondEclComputationResult[] bondEclComputationResults = _IFRS9Service.GetAllBondEclComputationResults();

                return request.CreateResponse<BondEclComputationResult[]>(HttpStatusCode.OK, bondEclComputationResults);
            });
        }
    }
}
