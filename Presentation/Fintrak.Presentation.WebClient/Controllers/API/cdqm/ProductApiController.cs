using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.CDQM.Contracts;
using Fintrak.Client.CDQM.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cdqmproduct")]
    [UsesDisposableService]
    public class CDQMProductApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMProductApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmproduct")]
        public HttpResponseMessage UpdateCDQMProduct(HttpRequestMessage request, [FromBody]CDQMProduct cdqmProductModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmProduct = _CDQMService.UpdateCDQMProduct(cdqmProductModel);

                return request.CreateResponse<CDQMProduct>(HttpStatusCode.OK, cdqmProduct);
            });
        }

        [HttpPost]
        [Route("deletecdqmProduct")]
        public HttpResponseMessage DeleteCDQMProduct(HttpRequestMessage request, [FromBody]int cdqmProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMProduct cdqmProduct = _CDQMService.GetCDQMProduct(cdqmProductId);

                if (cdqmProduct != null)
                {
                    _CDQMService.DeleteCDQMProduct(cdqmProductId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmProduct found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmProduct/{cdqmProductId}")]
        public HttpResponseMessage GetCDQMProduct(HttpRequestMessage request, int cdqmProductId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMProduct cdqmProduct = _CDQMService.GetCDQMProduct(cdqmProductId);

                // notice no need to create a seperate model object since CDQMProduct entity will do just fine
                response = request.CreateResponse<CDQMProduct>(HttpStatusCode.OK, cdqmProduct);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmProducts")]
        public HttpResponseMessage GetAvailableCDQMProducts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMProduct[] cdqmProducts = _CDQMService.GetAllCDQMProducts();

                return request.CreateResponse<CDQMProduct[]>(HttpStatusCode.OK, cdqmProducts);
            });
        }
    }
}
