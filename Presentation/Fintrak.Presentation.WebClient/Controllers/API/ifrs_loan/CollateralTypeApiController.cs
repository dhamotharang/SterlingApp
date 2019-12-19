using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/collateralType")]
    [UsesDisposableService]
    public class CollateralTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralTypeApiController(IIFRSLoanService coreService)
        {
            _LoanService = coreService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatecollateralType")]
        public HttpResponseMessage UpdateCollateralType(HttpRequestMessage request, [FromBody]CollateralType collateralTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralType = _LoanService.UpdateCollateralType(collateralTypeModel);

                return request.CreateResponse<CollateralType>(HttpStatusCode.OK, collateralType);
            });
        }

        [HttpPost]
        [Route("deletecollateralType")]
        public HttpResponseMessage DeleteCollateralType(HttpRequestMessage request, [FromBody]int collateralTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralType collateralType = _LoanService.GetCollateralType(collateralTypeId);

                if (collateralType != null)
                {
                    _LoanService.DeleteCollateralType(collateralTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No collateralType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralType/{collateralTypeId}")]
        public HttpResponseMessage GetCollateralType(HttpRequestMessage request, int collateralTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralType collateralType = _LoanService.GetCollateralType(collateralTypeId);

                // notice no need to create a seperate model object since CollateralType entity will do just fine
                response = request.CreateResponse<CollateralType>(HttpStatusCode.OK, collateralType);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralTypes")]
        public HttpResponseMessage GetAvailableCollateralTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralTypeData[] collateralTypes = _LoanService.GetAllCollateralTypes();

                return request.CreateResponse<CollateralTypeData[]>(HttpStatusCode.OK, collateralTypes);
            });
        }

        [HttpGet]
        [Route("getcollateralTypebyCode/{categoryCode}")]
        public HttpResponseMessage GetCollateralCategory(HttpRequestMessage request, string categoryCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralTypeData[] collateralTypes = _LoanService.GetCollateralTypeByCategory(categoryCode);

                // notice no need to create a seperate model object since CollateralCategory entity will do just fine
                response = request.CreateResponse<CollateralTypeData[]>(HttpStatusCode.OK, collateralTypes);

                return response;
            });
        }

    }
}
