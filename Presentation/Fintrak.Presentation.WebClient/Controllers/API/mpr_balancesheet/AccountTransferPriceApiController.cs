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
    [RoutePrefix("api/accountTransferPrice")]
    [UsesDisposableService]
    public class AccountTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountTransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateaccounttransferprice")]
        public HttpResponseMessage UpdateAccountTransferPrice(HttpRequestMessage request, [FromBody]AccountTransferPrice accounttransferpriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var accounttransferprice = _MPRCoreService.UpdateAccountTransferPrice(accounttransferpriceModel);

                return request.CreateResponse<AccountTransferPrice>(HttpStatusCode.OK, accounttransferprice);
            });
        }

        [HttpPost]
        [Route("deleteaccounttransferprice")]
        public HttpResponseMessage DeleteAccountTransferPrice(HttpRequestMessage request, [FromBody]int accounttransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AccountTransferPrice accounttransferprice = _MPRCoreService.GetAccountTransferPrice(accounttransferpriceId);

                if (accounttransferprice != null)
                {
                    _MPRCoreService.DeleteAccountTransferPrice(accounttransferpriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Account Transfer Price found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getaccounttransferprice/{accounttransferpriceId}")]
        public HttpResponseMessage GetAccountTransferPrice(HttpRequestMessage request, int accounttransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AccountTransferPrice accounttransferprice = _MPRCoreService.GetAccountTransferPrice(accounttransferpriceId);

                // notice no need to create a seperate model object since AccountTransferPrice entity will do just fine
                response = request.CreateResponse<AccountTransferPrice>(HttpStatusCode.OK, accounttransferprice);

                return response;
            });
        }

        [HttpGet]
        [Route("accounttransferprice")]
        public HttpResponseMessage GetAvailableAccountTransferPrices(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AccountTransferPriceData[] accounttransferprice = _MPRCoreService.GetAllAccountTransferPrices();

                return request.CreateResponse<AccountTransferPriceData[]>(HttpStatusCode.OK, accounttransferprice);
            });
        }
    }
}
