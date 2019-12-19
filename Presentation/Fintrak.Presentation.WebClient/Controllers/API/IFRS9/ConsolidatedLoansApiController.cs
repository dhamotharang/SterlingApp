using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/consolidatedloans")]
    [UsesDisposableService]
    public class ConsolidatedLoansApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ConsolidatedLoansApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateconsolidatedloans")]
        public HttpResponseMessage UpdateConsolidatedLoans(HttpRequestMessage request, [FromBody]ConsolidatedLoans consolidatedloansModel)
        {
            return GetHttpResponse(request, () =>
            {
                var consolidatedloans = _IFRS9Service.UpdateConsolidatedLoans(consolidatedloansModel);

                return request.CreateResponse<ConsolidatedLoans>(HttpStatusCode.OK, consolidatedloans);
            });
        }

        [HttpPost]
        [Route("deleteconsolidatedloans")]
        public HttpResponseMessage DeleteConsolidatedLoans(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ConsolidatedLoans consolidatedloans = _IFRS9Service.GetConsolidatedLoans(ID);

                if (consolidatedloans != null)
                {
                    _IFRS9Service.DeleteConsolidatedLoans(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ConsolidatedLoans found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getconsolidatedloans/{ID}")]
        public HttpResponseMessage GetConsolidatedLoans(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ConsolidatedLoans consolidatedloans = _IFRS9Service.GetConsolidatedLoans(ID);

                // notice no need to create a seperate model object since ConsolidatedLoans entity will do just fine
                response = request.CreateResponse<ConsolidatedLoans>(HttpStatusCode.OK, consolidatedloans);

                return response;
            });
        }

        [HttpGet]
        [Route("availableconsolidatedloanss/{defaultCount}")]
        public HttpResponseMessage GetAvailableConsolidatedLoanss(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                ConsolidatedLoans[] consolidatedloanss = _IFRS9Service.GetConsolidatedLoanss(defaultCount);
                return request.CreateResponse<ConsolidatedLoans[]>(HttpStatusCode.OK, consolidatedloanss);
            });
        }



        [HttpGet]
        [Route("getConsolidatedLoansbysearch/{searchParam}")]
        public HttpResponseMessage GetConsolidatedLoansBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                ConsolidatedLoans[] consolidatedloanss = _IFRS9Service.GetConsolidatedLoansBySearch(searchParam);
                return request.CreateResponse<ConsolidatedLoans[]>(HttpStatusCode.OK, consolidatedloanss.ToArray());
            });
        }





    }
}
