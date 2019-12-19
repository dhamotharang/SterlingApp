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
    [RoutePrefix("api/IfrsCustomerPD")]
    [UsesDisposableService]
    public class IfrsCustomerPDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsCustomerPDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsCustomerPD")]
        public HttpResponseMessage UpdateIfrsCustomerPD(HttpRequestMessage request, [FromBody]IfrsCustomerPD IfrsCustomerPDModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsCustomerPD = _IFRS9Service.UpdateIfrsCustomerPD(IfrsCustomerPDModel);

                return request.CreateResponse<IfrsCustomerPD>(HttpStatusCode.OK, IfrsCustomerPD);
            });
        }

        [HttpPost]
        [Route("deleteIfrsCustomerPD")]
        public HttpResponseMessage DeleteIfrsCustomerPD(HttpRequestMessage request, [FromBody]int CustomerPDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCustomerPD IfrsCustomerPD = _IFRS9Service.GetIfrsCustomerPD(CustomerPDId);

                if (IfrsCustomerPD != null)
                {
                    _IFRS9Service.DeleteIfrsCustomerPD(CustomerPDId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Customer found with that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsCustomerPD/{CustomerPDId}")]
        public HttpResponseMessage GetIfrsCustomerPD(HttpRequestMessage request, int CustomerPDId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCustomerPD IfrsCustomerPD = _IFRS9Service.GetIfrsCustomerPD(CustomerPDId);

                // notice no need to create a seperate model object since IfrsCustomerPD entity will do just fine
                response = request.CreateResponse<IfrsCustomerPD>(HttpStatusCode.OK, IfrsCustomerPD);

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsCustomerPDByRating/{Rating}")]
        public HttpResponseMessage GetIfrsCustomerPDByRating(HttpRequestMessage request, string rating)
        {
            rating = rating.Replace("p", "+");
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCustomerPD[] IfrsCustomerPD = _IFRS9Service.GetIfrsCustomerPDByRating(rating);

                // notice no need to create a seperate model object since IfrsCustomerPD entity will do just fine
                response = request.CreateResponse<IfrsCustomerPD[]>(HttpStatusCode.OK, IfrsCustomerPD);

                return response;
            });
        }

        [HttpGet]
        [Route("availableRatings")]
        public HttpResponseMessage GetAvailableIfrsCustomerPDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] IfrsCustomerPDs = _IFRS9Service.GetAllCustomerRatings();

                return request.CreateResponse<string[]>(HttpStatusCode.OK, IfrsCustomerPDs);
            });
        }
    }
}
