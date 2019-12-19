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
    [RoutePrefix("api/productmis")]
    [UsesDisposableService]
    public class ProductMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProductMISApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updateproductmis")]
        public HttpResponseMessage UpdateProductMIS(HttpRequestMessage request, [FromBody]ProductMIS productMISModel)
        {
            return GetHttpResponse(request, () =>
            {
                var productMIS = _MPRBSService.UpdateProductMIS(productMISModel);

                return request.CreateResponse<ProductMIS>(HttpStatusCode.OK, productMIS);
            });
        }

        [HttpPost]
        [Route("deleteproductMIS")]
        public HttpResponseMessage DeleteProductMIS(HttpRequestMessage request, [FromBody]int productMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProductMIS productMIS = _MPRBSService.GetProductMIS(productMISId);

                if (productMIS != null)
                {
                    _MPRBSService.DeleteProductMIS(productMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No productMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getproductMIS/{productMISId}")]
        public HttpResponseMessage GetProductMIS(HttpRequestMessage request, int productMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProductMIS productMIS = _MPRBSService.GetProductMIS(productMISId);

                // notice no need to create a seperate model object since ProductMIS entity will do just fine
                response = request.CreateResponse<ProductMIS>(HttpStatusCode.OK, productMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availableproductMISs")]
        public HttpResponseMessage GetAvailableProductMISs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProductMISData[] productMISs = _MPRBSService.GetAllProductMISs();

                return request.CreateResponse<ProductMISData[]>(HttpStatusCode.OK, productMISs);
            });
        }
    }
}
