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
    [RoutePrefix("api/plincomereport")]
    [UsesDisposableService]
    public class PLIncomeReportApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PLIncomeReportApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }


        [HttpGet]
        [Route("getplincomereport/{searchType}/{searchValue}/{number}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchType, string searchValue, int number)
        {
            return GetHttpResponse(request, () =>
            {
                PLIncomeReport[] plincomereport = _MPRPLService.GetPLIncomeReports(searchType, searchValue, number);

                return request.CreateResponse<PLIncomeReport[]>(HttpStatusCode.OK, plincomereport);
            });
        }


        [HttpGet]
        [Route("availableplincomereport")]
        public HttpResponseMessage GetAvailablePLIncomeReports(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLIncomeReport[] plincomereport = _MPRPLService.GetAllPLIncomeReports();

                return request.CreateResponse<PLIncomeReport[]>(HttpStatusCode.OK, plincomereport);
            });
        }
    }
}
