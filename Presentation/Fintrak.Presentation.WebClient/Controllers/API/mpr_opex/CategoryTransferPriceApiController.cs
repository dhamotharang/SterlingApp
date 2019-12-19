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
    [RoutePrefix("api/categorytransferprice")]
    [UsesDisposableService]
    public class CategoryTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CategoryTransferPriceApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatecategorytransferprice")]
        public HttpResponseMessage UpdateCategoryTransferPrice(HttpRequestMessage request, [FromBody]CategoryTransferPrice categoryTransferPriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var categoryTransferPrice = _MPROPEXService.UpdateCategoryTransferPrice(categoryTransferPriceModel);

                return request.CreateResponse<CategoryTransferPrice>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }

        [HttpPost]
        [Route("deletecategorytransferprice")]
        public HttpResponseMessage DeleteCategoryTransferPrice(HttpRequestMessage request, [FromBody]int categoryTransferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CategoryTransferPrice categoryTransferPrice = _MPROPEXService.GetCategoryTransferPrice(categoryTransferPriceId);

                if (categoryTransferPrice != null)
                {
                    _MPROPEXService.DeleteCategoryTransferPrice(categoryTransferPriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcategorytransferprice/{categorytransferpriceId}")]
        public HttpResponseMessage GetCategoryTransferPrice(HttpRequestMessage request, int categoryTransferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CategoryTransferPrice categoryTransferPrice = _MPROPEXService.GetCategoryTransferPrice(categoryTransferPriceId);

                // notice no need to create a seperate model object since CategoryTransferPrice entity will do just fine
                response = request.CreateResponse<CategoryTransferPrice>(HttpStatusCode.OK, categoryTransferPrice);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecategorytransferprice")]
        public HttpResponseMessage GetAvailableCategoryTransferPrice(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CategoryTransferPrice[] categoryTransferPrice = _MPROPEXService.GetAllCategoryTransferPrices();


                return request.CreateResponse<CategoryTransferPrice[]>(HttpStatusCode.OK, categoryTransferPrice);
            });
        }
    }
}
