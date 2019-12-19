using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/integralfee")]
    [UsesDisposableService]
    public class IntegralFeeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IntegralFeeApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateintegralfee")]
        public HttpResponseMessage UpdateIntegralFee(HttpRequestMessage request, [FromBody]IntegralFee integralFeeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var integralFee = _IFRSDataService.UpdateIntegralFee(integralFeeModel);

                return request.CreateResponse<IntegralFee>(HttpStatusCode.OK, integralFee);
            });
        }

        [HttpPost]
        [Route("deleteintegralfee")]
        public HttpResponseMessage DeleteIntegralFee(HttpRequestMessage request, [FromBody]int integralFeeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IntegralFee integralFee = _IFRSDataService.GetIntegralFee(integralFeeId);

                if (integralFee != null)
                {
                    _IFRSDataService.DeleteIntegralFee(integralFeeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IntegralFee found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availableintegralfee")]
        public HttpResponseMessage GetAvailableIntegralFees(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IntegralFee[] integralFee = _IFRSDataService.GetAllIntegralFee().ToArray();

                return request.CreateResponse<IntegralFee[]>(HttpStatusCode.OK, integralFee.ToArray());
            });
        }

        [HttpGet]
        [Route("getintegralfee/{integralFeeId}")]
        public HttpResponseMessage GetIntegralFee(HttpRequestMessage request, int integralFeeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IntegralFee integralFee = _IFRSDataService.GetIntegralFee(integralFeeId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IntegralFee>(HttpStatusCode.OK, integralFee);

                return response;
            });
        }      
    }
}
