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
    [RoutePrefix("api/bondconsolidateddataothers")]
    [UsesDisposableService]
    public class BondConsolidatedDataOthersApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondConsolidatedDataOthersApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("availablebondconsolidateddataothers")]
        public HttpResponseMessage GetAllBondConsolidatedDataOthers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondConsolidatedDataOthers[] bondconsolidateddataothers = _IFRSDataService.GetAllBondConsolidatedDataOthers();

                return request.CreateResponse<BondConsolidatedDataOthers[]>(HttpStatusCode.OK, bondconsolidateddataothers);
            });
        }
    }
}
