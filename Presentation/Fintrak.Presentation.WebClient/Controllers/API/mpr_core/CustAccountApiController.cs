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
    [RoutePrefix("api/custaccount")]
    [UsesDisposableService]
    public class CustAccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CustAccountApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpGet]
        [Route("getcustaccount/{searchType}/{searchValue}/{number}")]
        public HttpResponseMessage GetCustAccounts(HttpRequestMessage request, string searchType, string searchValue, int number)
        {
            return GetHttpResponse(request, () =>
            {
                CustAccount[] custaaccount = _MPRCoreService.GetCustAccounts(searchType, searchValue, number);

                return request.CreateResponse<CustAccount[]>(HttpStatusCode.OK, custaaccount);
            });
        }

        [HttpGet]
        [Route("availablecustaccount")]
        public HttpResponseMessage GetAvailableCustAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CustAccount[] custaaccount = _MPRCoreService.GetAllCustAccounts();

                return request.CreateResponse<CustAccount[]>(HttpStatusCode.OK, custaaccount);
            });
        }
    }
}
