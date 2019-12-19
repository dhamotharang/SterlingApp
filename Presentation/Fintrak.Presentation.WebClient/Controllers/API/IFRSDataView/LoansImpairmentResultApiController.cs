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
    [RoutePrefix("api/loansimpairmentresult")]
    [UsesDisposableService]
    public class LoansImpairmentResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoansImpairmentResultApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("availableloansimpairmentresult")]
        public HttpResponseMessage GetAvailableLoansImpairmentResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoansImpairmentResult[] loansimpairmentresult = _IFRSDataService.GetAllLoansImpairmentResults();

                return request.CreateResponse<LoansImpairmentResult[]>(HttpStatusCode.OK, loansimpairmentresult);
            });
        }
    }
}
