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
    [RoutePrefix("api/producttypemapping")]
    [UsesDisposableService]
    public class ProductTypeMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductTypeMappingApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateproductTypeMapping")]
        public HttpResponseMessage UpdateProductTypeMapping(HttpRequestMessage request, [FromBody]ProductTypeMapping productTypeMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var productTypeMapping = _CoreService.UpdateProductTypeMapping(productTypeMappingModel);

                return request.CreateResponse<ProductTypeMapping>(HttpStatusCode.OK, productTypeMapping);
            });
        }

        [HttpPost]
        [Route("deleteproductTypeMapping")]
        public HttpResponseMessage DeleteProductTypeMapping(HttpRequestMessage request, [FromBody]int productTypeMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProductTypeMapping productTypeMapping = _CoreService.GetProductTypeMapping(productTypeMappingId);

                if (productTypeMapping != null)
                {
                    _CoreService.DeleteProductTypeMapping(productTypeMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No productTypeMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getproductTypeMapping/{productTypeMappingId}")]
        public HttpResponseMessage GetProductTypeMapping(HttpRequestMessage request,int productTypeMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductTypeMapping productTypeMapping = _CoreService.GetProductTypeMapping(productTypeMappingId);

                // notice no need to create a seperate model object since ProductTypeMapping entity will do just fine
                response = request.CreateResponse<ProductTypeMapping>(HttpStatusCode.OK, productTypeMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("getproducttypemappingbyproduct/{productId}")]
        public HttpResponseMessage GetAvailableProductTypeMappings(HttpRequestMessage request,int productId)
        {
            return GetHttpResponse(request, () =>
            {
                ProductTypeMappingData[] productTypeMappings = _CoreService.GetProductTypeMappingByProduct("");

                return request.CreateResponse<ProductTypeMappingData[]>(HttpStatusCode.OK, productTypeMappings);
            });
        }
    }
}
