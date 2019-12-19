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
    [RoutePrefix("api/monthlydiscountfactor")]
    [UsesDisposableService]
    public class MonthlyDiscountFactorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MonthlyDiscountFactorApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemonthlydiscountfactor")]
        public HttpResponseMessage UpdateMonthlyDiscountFactor(HttpRequestMessage request, [FromBody]MonthlyDiscountFactor monthlydiscountfactorModel)
        {
            return GetHttpResponse(request, () =>
            {
                var monthlydiscountfactor = _IFRS9Service.UpdateMonthlyDiscountFactor(monthlydiscountfactorModel);

                return request.CreateResponse<MonthlyDiscountFactor>(HttpStatusCode.OK, monthlydiscountfactor);
            });
        }

        [HttpPost]
        [Route("deletemonthlydiscountfactor")]
        public HttpResponseMessage DeleteMonthlyDiscountFactor(HttpRequestMessage request, [FromBody]int MonthlyDiscountFactor_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MonthlyDiscountFactor monthlydiscountfactor = _IFRS9Service.GetMonthlyDiscountFactor(MonthlyDiscountFactor_Id);

                if (monthlydiscountfactor != null)
                {
                    _IFRS9Service.DeleteMonthlyDiscountFactor(MonthlyDiscountFactor_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No monthlydiscountfactor found under that Id.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactor/{id}")]
        public HttpResponseMessage GetMonthlyDiscountFactor(HttpRequestMessage request, int MonthlyDiscountFactor_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MonthlyDiscountFactor monthlydiscountfactor = _IFRS9Service.GetMonthlyDiscountFactor(MonthlyDiscountFactor_Id);

                // notice no need to create a seperate model object since MonthlyDiscountFactor entity will do just fine
                response = request.CreateResponse<MonthlyDiscountFactor>(HttpStatusCode.OK, monthlydiscountfactor);

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactors")]
        public HttpResponseMessage GetAvailableMonthlyDiscountFactors(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactor[] monthlydiscountfactors = _IFRS9Service.GetAllMonthlyDiscountFactors();

                return request.CreateResponse<MonthlyDiscountFactor[]>(HttpStatusCode.OK, monthlydiscountfactors);
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorbyrefno/{refno}")]
        public HttpResponseMessage GetMonthlyDiscountFactorByRefNo(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactor[] monthlydiscountfactors = _IFRS9Service.GetMonthlyDiscountFactorByRefNo(refNo);

                return request.CreateResponse<MonthlyDiscountFactor[]>(HttpStatusCode.OK, monthlydiscountfactors);
            });
        }
    }
}
