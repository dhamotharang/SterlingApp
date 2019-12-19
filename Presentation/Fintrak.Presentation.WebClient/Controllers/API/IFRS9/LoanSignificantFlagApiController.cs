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
    [RoutePrefix("api/LoanSignificantFlag")]
    [UsesDisposableService]
    public class LoanSignificantFlagApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanSignificantFlagApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices){
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateLoanSignificantFlag")]
        public HttpResponseMessage UpdateLoanSignificantFlag (HttpRequestMessage request, [FromBody]LoanSignificantFlag loansignificantflagModel){
            return GetHttpResponse(request, () => {
                var loansignificantflag = _IFRS9Service.UpdateLoanSignificantFlag(loansignificantflagModel);
                return request.CreateResponse<LoanSignificantFlag>(HttpStatusCode.OK, loansignificantflag);
            });
        }


        [HttpPost]
        [Route("deleteLoanSignificantFlag")]
        public HttpResponseMessage DeleteLoanSignificantFlag(HttpRequestMessage request, [FromBody]int Id){
            return GetHttpResponse(request, () =>  {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                LoanSignificantFlag loansignificantflag = _IFRS9Service.GetLoanSignificantFlag(Id);
                if (loansignificantflag != null){
                    _IFRS9Service.DeleteLoanSignificantFlag(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanSignificantFlagdata found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("getAvailableLoanSignificantFlag")]
        public HttpResponseMessage GetAvailableLoanSignificantFlags(HttpRequestMessage request){
            return GetHttpResponse(request, () => {
                LoanSignificantFlag[] loansignificantflags = _IFRS9Service.GetAllLoanSignificantFlag().ToArray();
                return request.CreateResponse<LoanSignificantFlag[]>(HttpStatusCode.OK, loansignificantflags.ToArray());
            });
        }

        [HttpGet]
        [Route("getLoanSignificantFlag/{Id}")]
        public HttpResponseMessage GetLoanSignificantFlag(HttpRequestMessage request, int Id){
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                LoanSignificantFlag loansignificantflag = _IFRS9Service.GetLoanSignificantFlag(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoanSignificantFlag>(HttpStatusCode.OK, loansignificantflag);
                return response;
            });
        }


        [HttpGet]
        [Route("getLoanSignificantFlagbysearch/{searchParam}")]
        public HttpResponseMessage GetLoanSignificantFlagBySearch(HttpRequestMessage request, string searchParam){
            return GetHttpResponse(request, () => {
                LoanSignificantFlag[] loansignificantflags = _IFRS9Service.GetLoanSignificantFlagBySearch(searchParam);
                return request.CreateResponse<LoanSignificantFlag[]>(HttpStatusCode.OK, loansignificantflags.ToArray());
            });
        }


        [HttpGet]
        [Route("availableLoanSignificantFlag/{defaultCount}")]
        public HttpResponseMessage GetAvailableLoanSignificantFlag(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                LoanSignificantFlag[] loansignificantflag = _IFRS9Service.GetLoanSignificantFlags(defaultCount).ToArray();
                return request.CreateResponse<LoanSignificantFlag[]>(HttpStatusCode.OK, loansignificantflag.ToArray());
            });
        }

    }
}
