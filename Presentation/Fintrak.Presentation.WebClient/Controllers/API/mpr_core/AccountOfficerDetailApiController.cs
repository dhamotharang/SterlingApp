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
    [RoutePrefix("api/accountofficerdetail")]
    [UsesDisposableService]
    public class AccountOfficerDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountOfficerDetailApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updateaccountofficerdetail")]
        public HttpResponseMessage UpdateAccountOfficerDetail(HttpRequestMessage request, [FromBody]AccountOfficerDetail accountofficerdetailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var accountofficerdetail = _MPRCoreService.UpdateAccountOfficerDetail(accountofficerdetailModel);

                return request.CreateResponse<AccountOfficerDetail>(HttpStatusCode.OK, accountofficerdetail);
            });
        }

        [HttpPost]
        [Route("deleteaccountOfficerDetail")]
        public HttpResponseMessage DeleteAccountOfficerDetail(HttpRequestMessage request, [FromBody]int accountOfficerDetailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AccountOfficerDetail accountOfficerDetail = _MPRCoreService.GetAccountOfficerDetail(accountOfficerDetailId);

                if (accountOfficerDetail != null)
                {
                    _MPRCoreService.DeleteAccountOfficerDetail(accountOfficerDetailId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No accountOfficerDetail found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getaccountOfficerDetail/{accountOfficerDetailId}")]
        public HttpResponseMessage GetAccountOfficerDetail(HttpRequestMessage request, int accountOfficerDetailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AccountOfficerDetail accountOfficerDetail = _MPRCoreService.GetAccountOfficerDetail(accountOfficerDetailId);

                // notice no need to create a seperate model object since AccountOfficerDetail entity will do just fine
                response = request.CreateResponse<AccountOfficerDetail>(HttpStatusCode.OK, accountOfficerDetail);

                return response;
            });
        }

        [HttpGet]
        [Route("availableaccountOfficerDetails")]
        public HttpResponseMessage GetAvailableAccountOfficerDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AccountOfficerDetail[] accountOfficerDetails = _MPRCoreService.GetAllAccountOfficerDetails();

                return request.CreateResponse<AccountOfficerDetail[]>(HttpStatusCode.OK, accountOfficerDetails);
            });
        }
    }
}
