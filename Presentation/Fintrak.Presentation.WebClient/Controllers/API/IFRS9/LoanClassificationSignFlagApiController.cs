using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loanclassificationSignFlag")]
    [UsesDisposableService]
    public class LoanClassificationSignFlagApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanClassificationSignFlagApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices){
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateLoanClassificationSignFlag")]
        public HttpResponseMessage UpdateLoanClassificationSignFlag (HttpRequestMessage request, [FromBody]LoanClassificationSICRSignFlag loansignificantflagModel){
            return GetHttpResponse(request, () => {
                int loanId = loansignificantflagModel.LoanClassificationId;
                KeyValueData[] grpClass = _IFRS9Service.GetSplitClassification(loanId);
                loansignificantflagModel.ProductType = grpClass[0].Key;
                loansignificantflagModel.SubType = grpClass[0].Value;
                                
                var loansignificantflag = _IFRS9Service.UpdateLoanClassificationSICRSignFlag(loansignificantflagModel);
                return request.CreateResponse<LoanClassificationSICRSignFlag>(HttpStatusCode.OK, loansignificantflag);
            });
        }


        [HttpPost]
        [Route("deleteLoanClassificationSignFlag")]
        public HttpResponseMessage DeleteLoanClassificationSignFlag(HttpRequestMessage request, [FromBody]int Id){
            return GetHttpResponse(request, () =>  {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                LoanClassificationSICRSignFlag loansignificantflag = _IFRS9Service.GetLoanClassificationSICRSignFlag(Id);
                if (loansignificantflag != null){
                    _IFRS9Service.DeleteLoanClassificationSICRSignFlag(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanClassificationSignFlagdata found under that ID.");
                return response;
            });
        }

        [HttpGet]
        [Route("getLoanClassificationSignFlag/{Id}")]
        public HttpResponseMessage GetLoanClassificationSignFlag(HttpRequestMessage request, int Id){
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                LoanClassificationSICRSignFlag loansignificantflag = _IFRS9Service.GetLoanClassificationSICRSignFlag(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoanClassificationSICRSignFlag>(HttpStatusCode.OK, loansignificantflag);
                return response;
            });
        }



        [HttpGet]
        [Route("availableLoanClassificationSignFlag/{defaultCount}")]
        public HttpResponseMessage GetAvailableLoanClassificationSignFlag(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                LoanClassificationSignificantFlagData[] loansignificantflag = _IFRS9Service.GetAllLoanClassificationSICRSignFlagData().ToArray();
                return request.CreateResponse<LoanClassificationSignificantFlagData[]>(HttpStatusCode.OK, loansignificantflag.ToArray());
            });
        }

        [HttpGet]
        [Route("getLoanClassSICRFlagByLoanClassId/{loanClassId}")]
        public HttpResponseMessage GetLoanClassSICRFlagByLoanClassId(HttpRequestMessage request, int loanClassId)
        {
            return GetHttpResponse(request, () => {
                LoanClassificationSignificantFlagData[] loansignificantflag = _IFRS9Service.GetAllLoanClassificationSICRSignFlagData().Where(c=>c.LoanClassificationId == loanClassId).ToArray();
                return request.CreateResponse<LoanClassificationSignificantFlagData[]>(HttpStatusCode.OK, loansignificantflag.ToArray());
            });
        }

        [HttpGet]
        [Route("getgroupedclassification")]
        public HttpResponseMessage GetGroupedClassification(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                KeyValueData[] grpClass = _IFRS9Service.GetGroupedClassification();

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, grpClass);

            });
        }


    }
}
