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
    [RoutePrefix("api/tbillscomputationresult")]
    [UsesDisposableService]
    public class TBillsComputationResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TBillsComputationResultApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("gettbillscomputationresult/{classification}")]
        public HttpResponseMessage GetTBillsComputationResult(HttpRequestMessage request, string classification)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                TBillsComputationResult[] tbillscomputationresult = _IFRSDataService.GetTBillsByClassification(classification);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<TBillsComputationResult[]>(HttpStatusCode.OK, tbillscomputationresult);

                return response;
            });
        }


        [HttpGet]
        [Route("availabletbillscomputationresult")]
        public HttpResponseMessage GetAvailableTBillsComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                TBillsComputationResult[] tbillscomputationresult = _IFRSDataService.GetAllTBillsComputationResults();

                return request.CreateResponse<TBillsComputationResult[]>(HttpStatusCode.OK, tbillscomputationresult);
            });
        }
    }
}
