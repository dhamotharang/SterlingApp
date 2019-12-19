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
    [RoutePrefix("api/equitystockcomputationresult")]
    [UsesDisposableService]
    public class EquityStockComputationResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public EquityStockComputationResultApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getequitystockcomputationresult/{classification}")]
        public HttpResponseMessage GetEquityStockComputationResult(HttpRequestMessage request, string classification)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EquityStockComputationResult[] equitystockcomputationresult = _IFRSDataService.GetEquityStockByClassification(classification);

                // notice no need to create a seperate model object since EquityStockComputationResult entity will do just fine
                response = request.CreateResponse<EquityStockComputationResult[]>(HttpStatusCode.OK, equitystockcomputationresult);

                return response;
            });
        }


        [HttpGet]
        [Route("availableequitystockcomputationresult")]
        public HttpResponseMessage GetAvailableEquityStockComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                EquityStockComputationResult[] equitystockcomputationresult = _IFRSDataService.GetAllEquityStocks();

                return request.CreateResponse<EquityStockComputationResult[]>(HttpStatusCode.OK, equitystockcomputationresult);
            });
        }
    }
}
