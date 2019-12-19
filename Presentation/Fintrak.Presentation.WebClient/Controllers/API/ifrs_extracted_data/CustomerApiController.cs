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
    [RoutePrefix("api/ifrscustomer")]
    [UsesDisposableService]
    public class ifrscustomerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ifrscustomerApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateifrscustomer")]
        public HttpResponseMessage Updateifrscustomer(HttpRequestMessage request, [FromBody]IfrsCustomer ifrscustomerModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrscustomer = _IFRSDataService.UpdateIfrsCustomer(ifrscustomerModel);

                return request.CreateResponse<IfrsCustomer>(HttpStatusCode.OK, ifrscustomer);
            });
        }

        [HttpPost]
        [Route("deleteifrscustomer")]
        public HttpResponseMessage Deleteifrscustomer(HttpRequestMessage request, [FromBody]int ifrscustomerId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCustomer ifrscustomer = _IFRSDataService.GetIfrsCustomer(ifrscustomerId);

                if (ifrscustomer != null)
                {
                    _IFRSDataService.DeleteIfrsCustomer(ifrscustomerId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrscustomer found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrscustomer")]
        public HttpResponseMessage GetAvailableifrscustomers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsCustomer[] ifrscustomer = _IFRSDataService.GetAllIfrsCustomer().ToArray();

                return request.CreateResponse<IfrsCustomer[]>(HttpStatusCode.OK, ifrscustomer.ToArray());
            });
        }

        [HttpGet]
        [Route("getifrscustomer/{ifrscustomerId}")]
        public HttpResponseMessage Getifrscustomer(HttpRequestMessage request, int ifrscustomerId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCustomer ifrscustomer = _IFRSDataService.GetIfrsCustomer(ifrscustomerId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsCustomer>(HttpStatusCode.OK, ifrscustomer);

                return response;
            });
        }

        [HttpGet]
        [Route("getcustomerinfobysearch/{searchParam}")]
        public HttpResponseMessage GetCustomerInfoBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsCustomer[] custInfo = _IFRSDataService.GetCustomerInfoBySearch(searchParam);

                return request.CreateResponse<IfrsCustomer[]>(HttpStatusCode.OK, custInfo.ToArray());
            });
        }
    }
}
