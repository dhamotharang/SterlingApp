using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/collateralrealizationperiod")]
    [UsesDisposableService]
    public class CollateralRealizationPeriodApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralRealizationPeriodApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatecollateralrealizationperiod")]
        public HttpResponseMessage UpdateCollateralRealizationPeriod(HttpRequestMessage request, [FromBody]CollateralRealizationPeriod collateralRealizationPeriodModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralRealizationPeriod = _LoanService.UpdateCollateralRealizationPeriod(collateralRealizationPeriodModel);

                return request.CreateResponse<CollateralRealizationPeriod>(HttpStatusCode.OK, collateralRealizationPeriod);
            });
        }

        [HttpPost]
        [Route("deletecollateralrealizationperiod")]
        public HttpResponseMessage DeleteCollateralRealizationPeriod(HttpRequestMessage request, [FromBody]int collateralRealizationPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralRealizationPeriod collateralRealizationPeriod = _LoanService.GetCollateralRealizationPeriod(collateralRealizationPeriodId);

                if (collateralRealizationPeriod != null)
                {
                    _LoanService.DeleteCollateralRealizationPeriod(collateralRealizationPeriodId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No collateralrealizationperiod found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralrealizationperiod/{collateralRealizationPeriodId}")]
        public HttpResponseMessage GetCollateralRealizationPeriod(HttpRequestMessage request,int collateralRealizationPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralRealizationPeriod collateralRealizationPeriod = _LoanService.GetCollateralRealizationPeriod(collateralRealizationPeriodId);

                // notice no need to create a seperate model object since CollateralRealizationPeriod entity will do just fine
                response = request.CreateResponse<CollateralRealizationPeriod>(HttpStatusCode.OK, collateralRealizationPeriod);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralrealizationperiods")]
        public HttpResponseMessage GetAvailableCollateralRealizationPeriods(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralRealizationPeriodData[] collateralRealizationPeriods = _LoanService.GetAllCollateralRealizationPeriods();

                return request.CreateResponse<CollateralRealizationPeriodData[]>(HttpStatusCode.OK, collateralRealizationPeriods);
            });
        }
    }
}
