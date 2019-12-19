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
    [RoutePrefix("api/creditriskrating")]
    [UsesDisposableService]
    public class CreditRiskRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CreditRiskRatingApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updatecreditriskrating")]
        public HttpResponseMessage UpdateCreditRiskRating(HttpRequestMessage request, [FromBody]CreditRiskRating creditRiskRatingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var creditRiskRating = _LoanService.UpdateCreditRiskRating(creditRiskRatingModel);

                return request.CreateResponse<CreditRiskRating>(HttpStatusCode.OK, creditRiskRating);
            });
        }

        [HttpPost]
        [Route("deletecreditRiskRating")]
        public HttpResponseMessage DeleteCreditRiskRating(HttpRequestMessage request, [FromBody]int creditRiskRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CreditRiskRating creditRiskRating = _LoanService.GetCreditRiskRating(creditRiskRatingId);

                if (creditRiskRating != null)
                {
                    _LoanService.DeleteCreditRiskRating(creditRiskRatingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcreditRiskRating/{creditRiskRatingId}")]
        public HttpResponseMessage GetCreditRiskRating(HttpRequestMessage request, int creditRiskRatingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CreditRiskRating creditRiskRating = _LoanService.GetCreditRiskRating(creditRiskRatingId);

                // notice no need to create a seperate model object since CreditRiskRating entity will do just fine
                response = request.CreateResponse<CreditRiskRating>(HttpStatusCode.OK, creditRiskRating);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecreditRiskRating")]
        public HttpResponseMessage GetAvailableCreditRiskRating(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CreditRiskRating[] creditRiskRating = _LoanService.GetAllCreditRiskRatings();


                return request.CreateResponse<CreditRiskRating[]>(HttpStatusCode.OK, creditRiskRating);
            });
        }

        [HttpGet]
        [Route("getRiskRatingCode")]
        public HttpResponseMessage GetAllRiskRatingCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] riskRatingCode = _LoanService.GetRiskRatingCode();
             
                var model = new List<KeyValueData>();

                foreach (var s in riskRatingCode)
                    model.Add(new KeyValueData() { Key = s, Value = s });

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());

               // return request.CreateResponse<string[]>(HttpStatusCode.OK, riskRatingCode);
            });
        }
    }
}
