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
    [RoutePrefix("api/marginalpddistribution")]
    [UsesDisposableService]
    public class MarginalPDDistributionController : ApiControllerBase
    {
        [ImportingConstructor]
        public MarginalPDDistributionController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemarginalPDDistribution")]
        public HttpResponseMessage UpdateMarginalPDDistribution(HttpRequestMessage request, [FromBody]MarginalPDDistribution marginalPDDistributionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var marginalPDDistribution = _IFRS9Service.UpdateMarginalPDDistribution(marginalPDDistributionModel);

                return request.CreateResponse<MarginalPDDistribution>(HttpStatusCode.OK, marginalPDDistribution);
            });
        }

        [HttpPost]
        [Route("deletemarginalPDDistribution")]
        public HttpResponseMessage DeleteMarginalPDDistribution(HttpRequestMessage request, [FromBody]int marginalPDDistributionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MarginalPDDistribution marginalPDDistribution = _IFRS9Service.GetMarginalPDDistribution(marginalPDDistributionId);

                if (marginalPDDistribution != null)
                {
                    _IFRS9Service.DeleteMarginalPDDistribution(marginalPDDistributionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No marginalPDDistribution found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmarginalPDDistribution/{marginalPDDistributionId}")]
        public HttpResponseMessage GetMarginalPDDistribution(HttpRequestMessage request, int marginalPDDistributionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MarginalPDDistribution marginalPDDistribution = _IFRS9Service.GetMarginalPDDistribution(marginalPDDistributionId);

                // notice no need to create a seperate model object since MarginalPDDistribution entity will do just fine
                response = request.CreateResponse<MarginalPDDistribution>(HttpStatusCode.OK, marginalPDDistribution);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemarginalPDDistributions")]
        public HttpResponseMessage GetAvailableMarginalPDDistributions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MarginalPDDistribution[] marginalPDDistributions = _IFRS9Service.GetAllMarginalPDDistributions();

                return request.CreateResponse<MarginalPDDistribution[]>(HttpStatusCode.OK, marginalPDDistributions);
            });
        }
    }
}
