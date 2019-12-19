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
    [RoutePrefix("api/collateralinformation")]
    [UsesDisposableService]
    public class CollateralInformationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralInformationApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatecollateralinformation")]
        public HttpResponseMessage UpdateCollateralInformation(HttpRequestMessage request, [FromBody]CollateralInformation collateralInformationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var collateralInformation = _LoanService.UpdateCollateralInformation(collateralInformationModel);

                return request.CreateResponse<CollateralInformation>(HttpStatusCode.OK, collateralInformation);
            });
        }

        [HttpPost]
        [Route("deletecollateralinformation")]
        public HttpResponseMessage DeleteCollateralInformation(HttpRequestMessage request, [FromBody]int collateralInformationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralInformation collateralInformation = _LoanService.GetCollateralInformation(collateralInformationId);

                if (collateralInformation != null)
                {
                    _LoanService.DeleteCollateralInformation(collateralInformationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No collateralinformation found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcollateralinformation/{collateralInformationId}")]
        public HttpResponseMessage GetCollateralInformation(HttpRequestMessage request,int collateralInformationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralInformation collateralInformation = _LoanService.GetCollateralInformation(collateralInformationId);

                // notice no need to create a seperate model object since CollateralInformation entity will do just fine
                response = request.CreateResponse<CollateralInformation>(HttpStatusCode.OK, collateralInformation);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecollateralinformations")]
        public HttpResponseMessage GetAvailableCollateralInformations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralInformationData[] collateralInformations = _LoanService.GetAllCollateralInformations();

                return request.CreateResponse<CollateralInformationData[]>(HttpStatusCode.OK, collateralInformations);
            });
        }
    }
}
