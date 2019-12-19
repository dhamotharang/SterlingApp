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
    [RoutePrefix("api/loanconsolidateddatafsdh")]
    [UsesDisposableService]
    public class LoanConsolidatedDataFSDHApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanConsolidatedDataFSDHApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("availableloanconsolidateddatafsdh")]
        public HttpResponseMessage GetAllLoanConsolidatedDataFSDH(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanConsolidatedDataFSDH[] loanconsolidateddata = _IFRSDataService.GetAllLoanConsolidatedDataFSDH();

                return request.CreateResponse<LoanConsolidatedDataFSDH[]>(HttpStatusCode.OK, loanconsolidateddata);
            });
        }
    }
}
