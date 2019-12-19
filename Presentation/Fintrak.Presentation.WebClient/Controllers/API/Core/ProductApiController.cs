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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/product")]
    [UsesDisposableService]
    public class ProductApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateproduct")]
        public HttpResponseMessage UpdateProduct(HttpRequestMessage request, [FromBody]Product productModel)
        {
            return GetHttpResponse(request, () =>
            {
                var product = _CoreService.UpdateProduct(productModel);

                return request.CreateResponse<Product>(HttpStatusCode.OK, product);
            });
        }

        [HttpPost]
        [Route("deleteproduct")]
        public HttpResponseMessage DeleteProduct(HttpRequestMessage request, [FromBody]int productId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Product product = _CoreService.GetProduct(productId);

                if (product != null)
                {
                    _CoreService.DeleteProduct(productId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getproduct/{productId}")]
        public HttpResponseMessage GetProduct(HttpRequestMessage request,int productId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Product product = _CoreService.GetProduct(productId);

                // notice no need to create a seperate model object since Product entity will do just fine
                response = request.CreateResponse<Product>(HttpStatusCode.OK, product);

                return response;
            });
        }

        [HttpGet]
        [Route("getproductwithchildren/{productId}")]
        public HttpResponseMessage GetProductWithChildren(HttpRequestMessage request, int productId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var productModel = new ProductModel();

                productModel.Product = _CoreService.GetProduct(productId);
                productModel.ProductTypeMappings = _CoreService.GetProductTypeMappingByProduct(productModel.Product.Code);

                // notice no need to create a seperate model object since Product entity will do just fine
                response = request.CreateResponse<ProductModel>(HttpStatusCode.OK, productModel);

                return response;
            });
        }

        [HttpGet]
        [Route("availableproducts")]
        public HttpResponseMessage GetAvailableProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Product[] products = _CoreService.GetAllProducts();

                return request.CreateResponse<Product[]>(HttpStatusCode.OK, products);
            });
        }

        [HttpGet]
        [Route("getAllProducts")]
        public HttpResponseMessage GetAvailableProducts(HttpRequestMessage request, [FromUri] QueryOptions queryOptions)
        {
            return GetHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(queryOptions.SortField)) queryOptions.SortField = "Code";
                if (queryOptions.init)
                {
                    queryOptions.init = false;
                    queryOptions.CurrentPage = 1;
                    if (!string.IsNullOrEmpty(queryOptions.FilterField))
                    {
                        Double output;
                        Double.TryParse(queryOptions.FilterOption, out output);
                        if (queryOptions.FilterFieldType == "string")
                        {
                            queryOptions.TotalRecords = _CoreService.GetTotalRecordsCount("cor_product", queryOptions.FilterField, queryOptions.FilterOption, null);
                        }
                        else
                        {
                            queryOptions.TotalRecords = _CoreService.GetTotalRecordsCount("cor_product", queryOptions.FilterField, queryOptions.FilterOption, output);
                        }
                    }
                    else
                    {
                        queryOptions.TotalRecords = _CoreService.GetTotalRecordsCountProduct("cor_product", queryOptions.FilterOption);
                    } 
                    queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages((UInt64)queryOptions.TotalRecords, queryOptions.PageSize);
                }
                if (queryOptions.TotalPages > 0 && queryOptions.TotalPages < (UInt64)queryOptions.CurrentPage)
                    queryOptions.CurrentPage = (int)queryOptions.TotalPages;

                Product[] Product = _CoreService.GetAvailableProduct(queryOptions);

                queryOptions.DisplayedRows = Product.Count();

                ResultData<Product> ResultData = new ResultData<Product>(Product.ToList(), queryOptions);

                return request.CreateResponse<ResultData<Product>>(HttpStatusCode.OK, ResultData);
            });
        }
    }
}
