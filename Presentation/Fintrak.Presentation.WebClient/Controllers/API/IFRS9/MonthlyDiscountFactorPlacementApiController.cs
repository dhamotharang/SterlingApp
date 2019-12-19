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
    [RoutePrefix("api/monthlydiscountfactorplacement")]
    [UsesDisposableService]
    public class MonthlyDiscountFactorPlacementApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MonthlyDiscountFactorPlacementApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemonthlydiscountfactorplacement")]
        public HttpResponseMessage UpdateMonthlyDiscountFactorPlacement(HttpRequestMessage request, [FromBody]MonthlyDiscountFactorPlacement monthlydiscountfactorplacementModel)
        {
            return GetHttpResponse(request, () =>
            {
                var monthlydiscountfactorplacement = _IFRS9Service.UpdateMonthlyDiscountFactorPlacement(monthlydiscountfactorplacementModel);

                return request.CreateResponse<MonthlyDiscountFactorPlacement>(HttpStatusCode.OK, monthlydiscountfactorplacement);
            });
        }

        [HttpPost]
        [Route("deletemonthlydiscountfactorplacement")]
        public HttpResponseMessage DeleteMonthlyDiscountFactorPlacement(HttpRequestMessage request, [FromBody]int MonthlyDiscountFactorPlacement_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MonthlyDiscountFactorPlacement monthlydiscountfactorplacement = _IFRS9Service.GetMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);

                if (monthlydiscountfactorplacement != null)
                {
                    _IFRS9Service.DeleteMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No monthlydiscountfactorplacement found under that Id.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorplacement/{id}")]
        public HttpResponseMessage GetMonthlyDiscountFactorPlacement(HttpRequestMessage request, int MonthlyDiscountFactorPlacement_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MonthlyDiscountFactorPlacement monthlydiscountfactorplacement = _IFRS9Service.GetMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);

                // notice no need to create a seperate model object since MonthlyDiscountFactorPlacement entity will do just fine
                response = request.CreateResponse<MonthlyDiscountFactorPlacement>(HttpStatusCode.OK, monthlydiscountfactorplacement);

                return response;
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorplacements")]
        public HttpResponseMessage GetAvailableMonthlyDiscountFactorPlacements(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactorPlacement[] monthlydiscountfactorplacements = _IFRS9Service.GetAllMonthlyDiscountFactorPlacements();

                return request.CreateResponse<MonthlyDiscountFactorPlacement[]>(HttpStatusCode.OK, monthlydiscountfactorplacements);
            });
        }

        [HttpGet]
        [Route("getmonthlydiscountfactorplacementbyrefno/{refno}")]
        public HttpResponseMessage GetMonthlyDiscountFactorPlacementByRefNo(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                MonthlyDiscountFactorPlacement[] monthlydiscountfactorplacements = _IFRS9Service.GetMonthlyDiscountFactorPlacementByRefNo(refNo);

                return request.CreateResponse<MonthlyDiscountFactorPlacement[]>(HttpStatusCode.OK, monthlydiscountfactorplacements);
            });
        }
    }
}
