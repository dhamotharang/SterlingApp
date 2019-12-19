using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
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
        public HttpResponseMessage UpdateIfrsCustomer(HttpRequestMessage request, [FromBody]IfrsCustomer IfrsCustomerModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsCustomer = _IFRSDataService.UpdateIfrsCustomer(IfrsCustomerModel);

                return request.CreateResponse<IfrsCustomer>(HttpStatusCode.OK, ifrsCustomer);
            });
        }


        [HttpPost]
        [Route("deleteifrscustomer")]
        public HttpResponseMessage DeleteIfrsCustomer(HttpRequestMessage request, [FromBody]int ifrsCustomerId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCustomer ifrscustomer = _IFRSDataService.GetIfrsCustomer(ifrsCustomerId);

                if (ifrscustomer != null)
                {
                    _IFRSDataService.DeleteIfrsCustomer(ifrsCustomerId); 

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Bonds found under that ID.");

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
        [Route("getcustomer/{ifrsCustomerId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int ifrsCustomerId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCustomer ifrscustomer = _IFRSDataService.GetIfrsCustomer(ifrsCustomerId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsCustomer>(HttpStatusCode.OK, ifrscustomer);

                return response;
            });
        }

        [HttpGet]
        [Route("getifrscustomer/{rating}")]
        public HttpResponseMessage GetifrsCustomer(HttpRequestMessage request, string rating)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCustomer[] ifrscustomer = _IFRSDataService.GetIfrsCustomerByRating(rating);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IfrsCustomer[]>(HttpStatusCode.OK, ifrscustomer.ToArray());

                return response;
            });
        }

      
    }
}
