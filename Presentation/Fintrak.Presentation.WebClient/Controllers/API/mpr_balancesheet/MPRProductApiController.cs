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
using Fintrak.Shared.Basic.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/mprproduct")]
    [UsesDisposableService]
    public class MPRProductApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MPRProductApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatemprproduct")]
        public HttpResponseMessage UpdateMPRProduct(HttpRequestMessage request, [FromBody]MPRProduct mprProductModel)
        {
            return GetHttpResponse(request, () =>
            {
                var mprProduct = _MPRBSService.UpdateMPRProduct(mprProductModel);

                return request.CreateResponse<MPRProduct>(HttpStatusCode.OK, mprProduct);
            });
        }

        [HttpPost]
        [Route("deletemprProduct")]
        public HttpResponseMessage DeleteMPRProduct(HttpRequestMessage request, [FromBody]int mprProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MPRProduct mprProduct = _MPRBSService.GetMPRProduct(mprProductId);

                if (mprProduct != null)
                {
                    _MPRBSService.DeleteMPRProduct(mprProductId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No mprProduct found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmprProduct/{mprProductId}")]
        public HttpResponseMessage GetMPRProduct(HttpRequestMessage request, int mprProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MPRProduct mprProduct = _MPRBSService.GetMPRProduct(mprProductId);

                // notice no need to create a seperate model object since MPRProduct entity will do just fine
                response = request.CreateResponse<MPRProduct>(HttpStatusCode.OK, mprProduct);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemprProducts")]
        public HttpResponseMessage GetAvailableMPRProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRProductData[] mprProducts = _MPRBSService.GetAllMPRProducts();

                return request.CreateResponse<MPRProductData[]>(HttpStatusCode.OK, mprProducts);
            });
        }

        [HttpGet]
        [Route("availablemprProducts/{productCode}")]
        public HttpResponseMessage GetAvailableMPRProducts(HttpRequestMessage request, string productCode)
        {
            return GetHttpResponse(request, () =>
            {
                MPRProductData[] mprProducts = _MPRBSService.GetAllMPRProductsByProductCode(productCode);

                return request.CreateResponse<MPRProductData[]>(HttpStatusCode.OK, mprProducts);
            });
        }

        [HttpGet]
        [Route("getrealproducts")]
        public HttpResponseMessage GetRealMPRProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRProductData[] mprProducts = _MPRBSService.GetMPRProductByNotional(false);

                return request.CreateResponse<MPRProductData[]>(HttpStatusCode.OK, mprProducts);
            });
        }

        [HttpGet]
        [Route("getnonproducts")]
        public HttpResponseMessage GetNonMPRProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MPRProductData[] mprProducts = _MPRBSService.GetMPRProductByNotional(true);

                return request.CreateResponse<MPRProductData[]>(HttpStatusCode.OK, mprProducts);
            });
        }

        [HttpGet]
        [Route("getunmappedproduct")]
        public HttpResponseMessage GetUnMappedGL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                KeyValueData[] gls = _MPRBSService.GetUnMappedProducts();

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, gls);
            });
        }
    }
}
