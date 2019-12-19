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
    [RoutePrefix("api/producttype")]
    [UsesDisposableService]
    public class ProductTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductTypeApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateproductType")]
        public HttpResponseMessage UpdateProductType(HttpRequestMessage request, [FromBody]ProductType productTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var productType = _CoreService.UpdateProductType(productTypeModel);

                return request.CreateResponse<ProductType>(HttpStatusCode.OK, productType);
            });
        }

        [HttpPost]
        [Route("deleteproductType")]
        public HttpResponseMessage DeleteProductType(HttpRequestMessage request, [FromBody]int productTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProductType productType = _CoreService.GetProductType(productTypeId);

                if (productType != null)
                {
                    _CoreService.DeleteProductType(productTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No productType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getproductType/{productTypeId}")]
        public HttpResponseMessage GetProductType(HttpRequestMessage request,int productTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductType productType = _CoreService.GetProductType(productTypeId);

                // notice no need to create a seperate model object since ProductType entity will do just fine
                response = request.CreateResponse<ProductType>(HttpStatusCode.OK, productType);

                return response;
            });
        }

        [HttpGet]
        [Route("availableproductTypes")]
        public HttpResponseMessage GetAvailableProductTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProductType[] productTypes = _CoreService.GetAllProductTypes();

                return request.CreateResponse<ProductType[]>(HttpStatusCode.OK, productTypes);
            });
        }
    }
}
