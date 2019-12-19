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
    [RoutePrefix("api/unmappedproduct")]
    [UsesDisposableService]
    public class UnMappedProductApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UnMappedProductApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("availableunmappedproduct")]
        public HttpResponseMessage GetAllUnMappedProduct(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UnMappedProduct[] unMappedProduct = _IFRSDataService.GetAllUnMappedProducts();

                return request.CreateResponse<UnMappedProduct[]>(HttpStatusCode.OK, unMappedProduct);
            });
        }
    }
}
