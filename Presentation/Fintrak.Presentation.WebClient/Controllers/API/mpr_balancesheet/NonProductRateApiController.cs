using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/nonproductrate")]
    [UsesDisposableService]
    public class NonProductRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NonProductRateApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatenonproductrate")]
        public HttpResponseMessage UpdateNonProductRate(HttpRequestMessage request, [FromBody]NonProductRate nonProductRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nonProductRate = _MPRBSService.UpdateNonProductRate(nonProductRateModel);

                return request.CreateResponse<NonProductRate>(HttpStatusCode.OK, nonProductRate);
            });
        }

        [HttpPost]
        [Route("deletenonProductRate")]
        public HttpResponseMessage DeleteNonProductRate(HttpRequestMessage request, [FromBody]int nonProductRateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NonProductRate nonProductRate = _MPRBSService.GetNonProductRate(nonProductRateId);

                if (nonProductRate != null)
                {
                    _MPRBSService.DeleteNonProductRate(nonProductRateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No nonProductRate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getnonProductRate/{nonProductRateId}")]
        public HttpResponseMessage GetNonProductRate(HttpRequestMessage request, int nonProductRateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NonProductRate nonProductRate = _MPRBSService.GetNonProductRate(nonProductRateId);

                // notice no need to create a seperate model object since NonProductRate entity will do just fine
                response = request.CreateResponse<NonProductRate>(HttpStatusCode.OK, nonProductRate);

                return response;
            });
        }

        [HttpGet]
        [Route("availablenonProductRates")]
        public HttpResponseMessage GetAvailableNonProductRates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NonProductRate[] nonProductRates = _MPRBSService.GetAllNonProductRates();

                return request.CreateResponse<NonProductRate[]>(HttpStatusCode.OK, nonProductRates);
            });
        }
    }
}
