using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/currencyRate")]
    [UsesDisposableService]
    public class CurrencyRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CurrencyRateApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecurrencyRate")]
        public HttpResponseMessage UpdateCurrencyRate(HttpRequestMessage request, [FromBody]CurrencyRate currencyRateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var currencyRate = _CoreService.UpdateCurrencyRate(currencyRateModel);

                return request.CreateResponse<CurrencyRate>(HttpStatusCode.OK, currencyRate);
            });
        }

        [HttpPost]
        [Route("deletecurrencyRate")]
        public HttpResponseMessage DeleteCurrencyRate(HttpRequestMessage request, [FromBody]int currencyRateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CurrencyRate currencyRate = _CoreService.GetCurrencyRate(currencyRateId);

                if (currencyRate != null)
                {
                    _CoreService.DeleteCurrencyRate(currencyRateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No currencyRate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcurrencyRate/{currencyRateId}")]
        public HttpResponseMessage GetCurrencyRate(HttpRequestMessage request, int currencyRateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CurrencyRate currencyRate = _CoreService.GetCurrencyRate(currencyRateId);

                // notice no need to create a seperate model object since CurrencyRate entity will do just fine
                response = request.CreateResponse<CurrencyRate>(HttpStatusCode.OK, currencyRate);

                return response;
            });
        }

        [HttpGet]
        [Route("getcurrencyRates/{currencyId}")]
        public HttpResponseMessage GetAvailableCurrencyRates(HttpRequestMessage request,int currencyId)
        {
            return GetHttpResponse(request, () =>
            {
                CurrencyRateData[] currencyRates = _CoreService.GetCurrencyRates(currencyId);

                return request.CreateResponse<CurrencyRateData[]>(HttpStatusCode.OK, currencyRates);
            });
        }

        [HttpGet]
        [Route("getcurrencyRatebyDate/{curSymbol}")]
        public HttpResponseMessage GetCurrencyRateByDate(HttpRequestMessage request, string curSymbol)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CurrencyRate[] currencyRate = _CoreService.GetCurrencyRateByDate(curSymbol);

                // notice no need to create a seperate model object since CurrencyRate entity will do just fine
                response = request.CreateResponse<CurrencyRate[]>(HttpStatusCode.OK, currencyRate);

                return response;
            });
        }

    }
}
