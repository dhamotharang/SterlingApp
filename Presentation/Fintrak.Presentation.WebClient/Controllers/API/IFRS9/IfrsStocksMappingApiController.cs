using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsstocksmapping")]
    [UsesDisposableService]
    public class IfrsStocksMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsStocksMappingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsStocksMapping")]
        public HttpResponseMessage UpdateIfrsStocksMapping(HttpRequestMessage request, [FromBody]IfrsStocksMapping ifrsStocksMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsStocksMapping = _IFRS9Service.UpdateIfrsStocksMapping(ifrsStocksMappingModel);

                return request.CreateResponse<IfrsStocksMapping>(HttpStatusCode.OK, ifrsStocksMapping);
            });
        }

        [HttpPost]
        [Route("deleteifrsStocksMapping")]
        public HttpResponseMessage DeleteIfrsStocksMapping(HttpRequestMessage request, [FromBody]int ifrsStocksMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsStocksMapping ifrsStocksMapping = _IFRS9Service.GetIfrsStocksMapping(ifrsStocksMappingId);

                if (ifrsStocksMapping != null)
                {
                    _IFRS9Service.DeleteIfrsStocksMapping(ifrsStocksMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsStocksMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsStocksMapping/{ifrsStocksMappingId}")]
        public HttpResponseMessage GetIfrsStocksMapping(HttpRequestMessage request,int ifrsStocksMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsStocksMapping ifrsStocksMapping = _IFRS9Service.GetIfrsStocksMapping(ifrsStocksMappingId);

                // notice no need to create a seperate model object since IfrsStocksMapping entity will do just fine
                response = request.CreateResponse<IfrsStocksMapping>(HttpStatusCode.OK, ifrsStocksMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrsStocksMappings")]
        public HttpResponseMessage GetAvailableIfrsStocksMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsStocksMappingData[] ifrsStocksMappings = _IFRS9Service.GetAllIfrsStocksMappings();

                return request.CreateResponse<IfrsStocksMappingData[]>(HttpStatusCode.OK, ifrsStocksMappings);
            });
        }
    }
}
