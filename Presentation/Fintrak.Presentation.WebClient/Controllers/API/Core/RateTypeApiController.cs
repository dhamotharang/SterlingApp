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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ratetype")]
    [UsesDisposableService]
    public class RateTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RateTypeApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updaterateType")]
        public HttpResponseMessage UpdateRateType(HttpRequestMessage request, [FromBody]RateType rateTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var rateType = _CoreService.UpdateRateType(rateTypeModel);

                return request.CreateResponse<RateType>(HttpStatusCode.OK, rateType);
            });
        }

        [HttpPost]
        [Route("deleterateType")]
        public HttpResponseMessage DeleteRateType(HttpRequestMessage request, [FromBody]int rateTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RateType rateType = _CoreService.GetRateType(rateTypeId);

                if (rateType != null)
                {
                    _CoreService.DeleteRateType(rateTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No rateType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getrateType/{rateTypeId}")]
        public HttpResponseMessage GetRateType(HttpRequestMessage request,int rateTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RateType rateType = _CoreService.GetRateType(rateTypeId);

                // notice no need to create a seperate model object since RateType entity will do just fine
                response = request.CreateResponse<RateType>(HttpStatusCode.OK, rateType);

                return response;
            });
        }

        [HttpGet]
        [Route("availablerateTypes")]
        public HttpResponseMessage GetAvailableRateTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RateType[] rateTypes = _CoreService.GetAllRateTypes();

                return request.CreateResponse<RateType[]>(HttpStatusCode.OK, rateTypes);
            });
        }
    }
}
