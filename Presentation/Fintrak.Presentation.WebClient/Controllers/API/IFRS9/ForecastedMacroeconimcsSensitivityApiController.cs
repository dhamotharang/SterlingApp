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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/forecastedmacroeconimcssensitivity")]
    [UsesDisposableService]
    public class ForecastedMacroeconimcsSensitivityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ForecastedMacroeconimcsSensitivityApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateforecastedMacroeconimcsSensitivity")]
        public HttpResponseMessage UpdateForecastedMacroeconimcsSensitivity(HttpRequestMessage request, [FromBody]ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivityModel)
        {
            return GetHttpResponse(request, () =>
            {
                var forecastedMacroeconimcsSensitivity = _IFRS9Service.UpdateForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityModel);

                return request.CreateResponse<ForecastedMacroeconimcsSensitivity>(HttpStatusCode.OK, forecastedMacroeconimcsSensitivity);
            });
        }

        [HttpPost]
        [Route("deleteforecastedMacroeconimcsSensitivity")]
        public HttpResponseMessage DeleteForecastedMacroeconimcsSensitivity(HttpRequestMessage request, [FromBody]int forecastedMacroeconimcsSensitivityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity = _IFRS9Service.GetForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);

                if (forecastedMacroeconimcsSensitivity != null)
                {
                    _IFRS9Service.DeleteForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No forecastedMacroeconimcsSensitivity found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getforecastedMacroeconimcsSensitivity/{forecastedMacroeconimcsSensitivityId}")]
        public HttpResponseMessage GetForecastedMacroeconimcsSensitivity(HttpRequestMessage request,int forecastedMacroeconimcsSensitivityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity = _IFRS9Service.GetForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);

                // notice no need to create a seperate model object since ForecastedMacroeconimcsSensitivity entity will do just fine
                response = request.CreateResponse<ForecastedMacroeconimcsSensitivity>(HttpStatusCode.OK, forecastedMacroeconimcsSensitivity);

                return response;
            });
        }

        [HttpGet]
        [Route("availableforecastedMacroeconimcsSensitivitys")]
        public HttpResponseMessage GetAvailableForecastedMacroeconimcsSensitivitys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ForecastedMacroeconimcsSensitivityData[] forecastedMacroeconimcsSensitivitys = _IFRS9Service.GetAllForecastedMacroeconimcsSensitivitys();

                return request.CreateResponse<ForecastedMacroeconimcsSensitivityData[]>(HttpStatusCode.OK, forecastedMacroeconimcsSensitivitys);
            });
        }


        [HttpPost]
        [Route("InsertSensitivityData")]
        public HttpResponseMessage InsertSensitivity(HttpRequestMessage request, [FromBody] InsertParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRS9Service.InsertSensitivityData(param.microeconomic, param.year, param.types, param.values);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpPost]
        [Route("Compute")]
        public HttpResponseMessage ComputeSensitivity(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRS9Service.ComputeSensitivity();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
