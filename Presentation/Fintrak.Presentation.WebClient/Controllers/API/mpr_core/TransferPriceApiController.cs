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
    [RoutePrefix("api/transferprice")]
    [UsesDisposableService]
    public class TransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatetransferprice")]
        public HttpResponseMessage UpdateTransferPrice(HttpRequestMessage request, [FromBody]TransferPrice transferpriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var transferprice = _MPRCoreService.UpdateTransferPrice(transferpriceModel);

                return request.CreateResponse<TransferPrice>(HttpStatusCode.OK, transferprice);
            });
        }

        [HttpPost]
        [Route("deletetransferPrice")]
        public HttpResponseMessage DeleteTransferPrice(HttpRequestMessage request, [FromBody]int transferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                TransferPrice transferPrice = _MPRCoreService.GetTransferPrice(transferPriceId);

                if (transferPrice != null)
                {
                    _MPRCoreService.DeleteTransferPrice(transferPriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No transferPrice found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gettransferPrice/{transferPriceId}")]
        public HttpResponseMessage GetTransferPrice(HttpRequestMessage request, int transferPriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TransferPrice transferPrice = _MPRCoreService.GetTransferPrice(transferPriceId);

                // notice no need to create a seperate model object since TransferPrice entity will do just fine
                response = request.CreateResponse<TransferPrice>(HttpStatusCode.OK, transferPrice);

                return response;
            });
        }

        [HttpGet]
        [Route("availabletransferPrices")]
        public HttpResponseMessage GetAvailableTransferPrices(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TransferPriceData[] transferPrices = _MPRCoreService.GetAllTransferPrices();

                return request.CreateResponse<TransferPriceData[]>(HttpStatusCode.OK, transferPrices);
            });
        }
    }
}
