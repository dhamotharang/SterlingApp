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
    [RoutePrefix("api/bondmarginalpddistribution")]
    [UsesDisposableService]
    public class BondMarginalPDDistributionController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondMarginalPDDistributionController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatebondMarginalPDDistribution")]
        public HttpResponseMessage UpdateBondMarginalPDDistribution(HttpRequestMessage request, [FromBody]BondMarginalPDDistribution bondMarginalPDDistributionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bondMarginalPDDistribution = _IFRS9Service.UpdateBondMarginalPDDistribution(bondMarginalPDDistributionModel);

                return request.CreateResponse<BondMarginalPDDistribution>(HttpStatusCode.OK, bondMarginalPDDistribution);
            });
        }

        [HttpPost]
        [Route("deletebondMarginalPDDistribution")]
        public HttpResponseMessage DeleteBondMarginalPDDistribution(HttpRequestMessage request, [FromBody]int bondMarginalPDDistributionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BondMarginalPDDistribution bondMarginalPDDistribution = _IFRS9Service.GetBondMarginalPDDistribution(bondMarginalPDDistributionId);

                if (bondMarginalPDDistribution != null)
                {
                    _IFRS9Service.DeleteBondMarginalPDDistribution(bondMarginalPDDistributionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bondMarginalPDDistribution found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbondMarginalPDDistribution/{bondMarginalPDDistributionId}")]
        public HttpResponseMessage GetBondMarginalPDDistribution(HttpRequestMessage request, int bondMarginalPDDistributionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondMarginalPDDistribution bondMarginalPDDistribution = _IFRS9Service.GetBondMarginalPDDistribution(bondMarginalPDDistributionId);

                // notice no need to create a seperate model object since BondMarginalPDDistribution entity will do just fine
                response = request.CreateResponse<BondMarginalPDDistribution>(HttpStatusCode.OK, bondMarginalPDDistribution);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebondMarginalPDDistributions")]
        public HttpResponseMessage GetAvailableBondMarginalPDDistributions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondMarginalPDDistribution[] bondMarginalPDDistributions = _IFRS9Service.GetAllBondMarginalPDDistributions();

                return request.CreateResponse<BondMarginalPDDistribution[]>(HttpStatusCode.OK, bondMarginalPDDistributions);
            });
        }
    }
}
