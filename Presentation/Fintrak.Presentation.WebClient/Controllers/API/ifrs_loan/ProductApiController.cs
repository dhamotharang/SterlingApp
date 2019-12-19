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
    [RoutePrefix("api/ifrsproduct")]
    [UsesDisposableService]
    public class IFRSProductApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRSProductApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updateproduct")]
        public HttpResponseMessage UpdateIFRSProduct(HttpRequestMessage request, [FromBody]IFRSProduct productModel)
        {
            return GetHttpResponse(request, () =>
            {
                var product = _LoanService.UpdateIFRSProduct(productModel);

                return request.CreateResponse<IFRSProduct>(HttpStatusCode.OK, product);
            });
        }

        [HttpPost]
        [Route("deleteproduct")]
        public HttpResponseMessage DeleteIFRSProduct(HttpRequestMessage request, [FromBody]int productId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSProduct product = _LoanService.GetIFRSProduct(productId);

                if (product != null)
                {
                    _LoanService.DeleteIFRSProduct(productId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getproduct/{productId}")]
        public HttpResponseMessage GetIFRSProduct(HttpRequestMessage request,int productId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSProduct product = _LoanService.GetIFRSProduct(productId);

                // notice no need to create a seperate model object since IFRSProduct entity will do just fine
                response = request.CreateResponse<IFRSProduct>(HttpStatusCode.OK, product);

                return response;
            });
        }

        [HttpGet]
        [Route("availableproducts")]
        public HttpResponseMessage GetAvailableIFRSProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSProductData[] products = _LoanService.GetAllIFRSProducts();

                return request.CreateResponse<IFRSProductData[]>(HttpStatusCode.OK, products);
            });
        }
        [HttpGet]
        [Route("getloanproduct/{producttypeId}")]
        public HttpResponseMessage GetProduct(HttpRequestMessage request, int producttypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSProduct product = _LoanService.GetIFRSProduct(4);

                // notice no need to create a seperate model object since IFRSProduct entity will do just fine
                response = request.CreateResponse<IFRSProduct>(HttpStatusCode.OK, product);

                return response;
            });
        }

    }
}
