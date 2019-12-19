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
    [RoutePrefix("api/forecastedmacroeconimcsscenario")]
    [UsesDisposableService]
    public class ForecastedMacroeconimcsScenarioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ForecastedMacroeconimcsScenarioApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateforecastedMacroeconimcsScenario")]
        public HttpResponseMessage UpdateForecastedMacroeconimcsScenario(HttpRequestMessage request, [FromBody]ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenarioModel)
        {
            return GetHttpResponse(request, () =>
            {
                var forecastedMacroeconimcsScenario = _IFRS9Service.UpdateForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioModel);

                return request.CreateResponse<ForecastedMacroeconimcsScenario>(HttpStatusCode.OK, forecastedMacroeconimcsScenario);
            });
        }

        [HttpPost]
        [Route("deleteforecastedMacroeconimcsScenario")]
        public HttpResponseMessage DeleteForecastedMacroeconimcsScenario(HttpRequestMessage request, [FromBody]int forecastedMacroeconimcsScenarioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario = _IFRS9Service.GetForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);

                if (forecastedMacroeconimcsScenario != null)
                {
                    _IFRS9Service.DeleteForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No forecastedMacroeconimcsScenario found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getforecastedMacroeconimcsScenario/{forecastedMacroeconimcsScenarioId}")]
        public HttpResponseMessage GetForecastedMacroeconimcsScenario(HttpRequestMessage request,int forecastedMacroeconimcsScenarioId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario = _IFRS9Service.GetForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);

                // notice no need to create a seperate model object since ForecastedMacroeconimcsScenario entity will do just fine
                response = request.CreateResponse<ForecastedMacroeconimcsScenario>(HttpStatusCode.OK, forecastedMacroeconimcsScenario);

                return response;
            });
        }

        [HttpGet]
        [Route("availableforecastedMacroeconimcsScenarios")]
        public HttpResponseMessage GetAvailableForecastedMacroeconimcsScenarios(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ForecastedMacroeconimcsScenarioData[] forecastedMacroeconimcsScenarios = _IFRS9Service.GetAllForecastedMacroeconimcsScenarios();

                return request.CreateResponse<ForecastedMacroeconimcsScenarioData[]>(HttpStatusCode.OK, forecastedMacroeconimcsScenarios);
            });
        }


        [HttpPost]
        [Route("InsertScenarioData")]
        public HttpResponseMessage InsertScenario(HttpRequestMessage request, [FromBody] InsertScenarioParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRS9Service.InsertScenarioData(param.sector, param.microeconomic, param.year, param.types, param.values);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpPost]
        [Route("Compute")]
        public HttpResponseMessage ComputeScenario(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRS9Service.ComputeScenario();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
