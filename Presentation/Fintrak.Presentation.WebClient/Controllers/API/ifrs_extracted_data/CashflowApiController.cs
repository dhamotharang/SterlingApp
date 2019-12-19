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
    [RoutePrefix("api/Cashflow")]
    [UsesDisposableService]
    public class CashflowApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CashflowApiController(IExtractedDataService ifrsDataService){
            _IFRSDataService = ifrsDataService;
        }
        IExtractedDataService _IFRSDataService;
        protected override void RegisterServices(List<IServiceContract> disposableServices){
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateCashflow")]
        public HttpResponseMessage Updatecashflow(HttpRequestMessage request, [FromBody]Cashflow cashflowModel){
            return GetHttpResponse(request, () => {
                var cashflow = _IFRSDataService.UpdateCashflow(cashflowModel);
                return request.CreateResponse<Cashflow>(HttpStatusCode.OK, cashflow);
            });
        }


        [HttpPost]
        [Route("deleteCashflow")]
        public HttpResponseMessage DeleteloanPry(HttpRequestMessage request, [FromBody]int cashflowId){
            return GetHttpResponse(request, () =>  {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                Cashflow cashflow = _IFRSDataService.GetCashflow(cashflowId);
                if (cashflow != null){
                    _IFRSDataService.DeleteLoanPry(cashflowId);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Cashflowdata found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableCashflow")]
        public HttpResponseMessage GetAvailableCashflows(HttpRequestMessage request){
            return GetHttpResponse(request, () => {
                Cashflow[] cashflow = _IFRSDataService.GetAllCashflow().ToArray();
                return request.CreateResponse<Cashflow[]>(HttpStatusCode.OK, cashflow.ToArray());
            });
        }

        [HttpGet]
        [Route("getCashflow/{cashflowId}")]
        public HttpResponseMessage GetLoanPry(HttpRequestMessage request, int cashflowId){
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                Cashflow cashflow = _IFRSDataService.GetCashflow(cashflowId);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<Cashflow>(HttpStatusCode.OK, cashflow);
                return response;
            });
        }


        [HttpGet]
        [Route("getCashflowbysearch/{searchParam}")]
        public HttpResponseMessage GetPryLoanBySearch(HttpRequestMessage request, string searchParam){
            return GetHttpResponse(request, () => {
                Cashflow[] cashflow = _IFRSDataService.GetCashflowBySearch(searchParam);
                return request.CreateResponse<Cashflow[]>(HttpStatusCode.OK, cashflow.ToArray());
            });
        }


        [HttpGet]
        [Route("availableCashflow/{defaultCount}")]
        public HttpResponseMessage GetAvailableCashflow(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                Cashflow[] cashflow = _IFRSDataService.GetCashflows(defaultCount).ToArray();
                return request.CreateResponse<Cashflow[]>(HttpStatusCode.OK, cashflow.ToArray());
            });
        }

    }
}
