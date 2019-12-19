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
    [RoutePrefix("api/incomecentralvaultaccounts")]
    [UsesDisposableService]
    public class IncomeCentralVaultAccountsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCentralVaultAccountsApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateincomecentralvaultaccounts")]
        public HttpResponseMessage UpdateIncomeCentralVaultAccounts(HttpRequestMessage request, [FromBody]IncomeCentralVaultAccounts incomeCentralVaultAccountsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCentralVaultAccounts = _MPROPEXService.UpdateIncomeCentralVaultAccounts(incomeCentralVaultAccountsModel);

                return request.CreateResponse<IncomeCentralVaultAccounts>(HttpStatusCode.OK, incomeCentralVaultAccounts);
            });
        }

        [HttpPost]
        [Route("deleteincomecentralvaultaccounts")]
        public HttpResponseMessage DeleteIncomeCentralVaultAccounts(HttpRequestMessage request, [FromBody]int incomeCentralVaultAccountsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCentralVaultAccounts incomeCentralVaultAccounts = _MPROPEXService.GetIncomeCentralVaultAccounts(incomeCentralVaultAccountsId);

                if (incomeCentralVaultAccounts != null)
                {
                    _MPROPEXService.DeleteIncomeCentralVaultAccounts(incomeCentralVaultAccountsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomecentralvaultaccounts/{incomecentralvaultaccountsId}")]
        public HttpResponseMessage GetIncomeCentralVaultAccounts(HttpRequestMessage request, int incomeCentralVaultAccountsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCentralVaultAccounts incomeCentralVaultAccounts = _MPROPEXService.GetIncomeCentralVaultAccounts(incomeCentralVaultAccountsId);

                // notice no need to create a seperate model object since IncomeCentralVaultAccounts entity will do just fine
                response = request.CreateResponse<IncomeCentralVaultAccounts>(HttpStatusCode.OK, incomeCentralVaultAccounts);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecentralvaultaccounts")]
        public HttpResponseMessage GetAvailableIncomeCentralVaultAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCentralVaultAccounts[] incomeCentralVaultAccounts = _MPROPEXService.GetAllIncomeCentralVaultAccounts();


                return request.CreateResponse<IncomeCentralVaultAccounts[]>(HttpStatusCode.OK, incomeCentralVaultAccounts);
            });
        }
    }
}
