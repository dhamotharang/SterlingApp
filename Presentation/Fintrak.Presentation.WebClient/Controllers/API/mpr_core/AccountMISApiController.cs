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
    [RoutePrefix("api/accountmis")]
    [UsesDisposableService]
    public class AccountMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountMISApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateaccountmis")]
        public HttpResponseMessage UpdateAccountMIS(HttpRequestMessage request, [FromBody]AccountMIS teamaccountmisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teamaccountmis = _MPRCoreService.UpdateAccountMIS(teamaccountmisModel);

                return request.CreateResponse<AccountMIS>(HttpStatusCode.OK, teamaccountmis);
            });
        }

        [HttpPost]
        [Route("deleteaccountMIS")]
        public HttpResponseMessage DeleteAccountMIS(HttpRequestMessage request, [FromBody]int accountMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AccountMIS accountMIS = _MPRCoreService.GetAccountMIS(accountMISId);

                if (accountMIS != null)
                {
                    _MPRCoreService.DeleteAccountMIS(accountMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No accountMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getaccountMIS/{accountMISId}")]
        public HttpResponseMessage GetAccountMIS(HttpRequestMessage request, int accountMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AccountMIS accountMIS = _MPRCoreService.GetAccountMIS(accountMISId);

                // notice no need to create a seperate model object since AccountMIS entity will do just fine
                response = request.CreateResponse<AccountMIS>(HttpStatusCode.OK, accountMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availableaccountMISs")]
        public HttpResponseMessage GetAvailableAccountMISs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AccountMISData[] accountMISs = _MPRCoreService.GetAllAccountMISs();

                return request.CreateResponse<AccountMISData[]>(HttpStatusCode.OK, accountMISs);
            });
        }

        [HttpPost]
        [Route("deleteselectedlist/{selectedIds}")]
        public HttpResponseMessage DeleteLoanSetupIdList(string selectedIds)
        {
            _MPRCoreService.DeleteSelectedIds(selectedIds);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
