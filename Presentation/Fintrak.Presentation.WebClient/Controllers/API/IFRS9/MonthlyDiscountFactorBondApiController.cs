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
    [RoutePrefix("api/monthlydiscountfactorbond")]
    [UsesDisposableService]
    public class MonthlyDiscountFactorBondApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MonthlyDiscountFactorBondApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemonthlydiscountfactorbond")]
        public HttpResponseMessage UpdateMonthlyDiscountFactorBond(HttpRequestMessage request, [FromBody]MonthlyDiscountFactorBond monthlydiscountfactorbondModel)
        {
            return GetHttpResponse(request, () =>
            {
                var monthlydiscountfactorbond = _IFRS9Service.UpdateMonthlyDiscountFactorBond(monthlydiscountfactorbondModel);

                return request.CreateResponse<MonthlyDiscountFactorBond>(HttpStatusCode.OK, monthlydiscountfactorbond);
            });
        }

        [HttpPost]
        [Route("deletemonthlydiscountfactorbond")]
        public HttpResponseMessage DeleteMonthlyDiscountFactorBond(HttpRequestMessage request, [FromBody]int MonthlyDiscountFactorBond_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MonthlyDiscountFactorBond monthlydiscountfactorbond = _IFRS9Service.GetMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);

                if (monthlydiscountfactorbond != null)
                {
                    _IFRS9Service.DeleteMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No monthlydiscountfactorbond found under that Id.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorbond/{id}")]
        public HttpResponseMessage GetMonthlyDiscountFactorBond(HttpRequestMessage request, int MonthlyDiscountFactorBond_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MonthlyDiscountFactorBond monthlydiscountfactorbond = _IFRS9Service.GetMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);

                // notice no need to create a seperate model object since MonthlyDiscountFactorBond entity will do just fine
                response = request.CreateResponse<MonthlyDiscountFactorBond>(HttpStatusCode.OK, monthlydiscountfactorbond);

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorbonds")]
        public HttpResponseMessage GetAvailableMonthlyDiscountFactorBonds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactorBond[] monthlydiscountfactorbonds = _IFRS9Service.GetAllMonthlyDiscountFactorBonds();

                return request.CreateResponse<MonthlyDiscountFactorBond[]>(HttpStatusCode.OK, monthlydiscountfactorbonds);
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorbondbyrefno/{refno}")]
        public HttpResponseMessage GetMonthlyDiscountFactorBondByRefNo(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactorBond[] monthlydiscountfactorbonds = _IFRS9Service.GetMonthlyDiscountFactorBondByRefNo(refNo);

                return request.CreateResponse<MonthlyDiscountFactorBond[]>(HttpStatusCode.OK, monthlydiscountfactorbonds);
            });
        }
    }
}
