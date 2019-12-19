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
    [RoutePrefix("api/tbillconsolidateddata")]
    [UsesDisposableService]
    public class TbillConsolidatedDataApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TbillConsolidatedDataApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("availabletbillconsolidateddata")]
        public HttpResponseMessage GetAllTbillConsolidatedData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TbillConsolidatedData[] tbillconsolidateddata = _IFRSDataService.GetAllTbillConsolidatedData();

                return request.CreateResponse<TbillConsolidatedData[]>(HttpStatusCode.OK, tbillconsolidateddata);
            });
        }
    }
}
