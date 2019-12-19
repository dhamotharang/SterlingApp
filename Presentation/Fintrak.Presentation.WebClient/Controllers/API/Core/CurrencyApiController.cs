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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/currency")]
    [UsesDisposableService]
    public class CurrencyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CurrencyApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecurrency")]
        public HttpResponseMessage UpdateCurrency(HttpRequestMessage request, [FromBody]Currency currencyModel)
        {
            return GetHttpResponse(request, () =>
            {
                var currency = _CoreService.UpdateCurrency(currencyModel);

                return request.CreateResponse<Currency>(HttpStatusCode.OK, currency);
            });
        }

        [HttpPost]
        [Route("deletecurrency")]
        public HttpResponseMessage DeleteCurrency(HttpRequestMessage request, [FromBody]int currencyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Currency currency = _CoreService.GetCurrency(currencyId);

                if (currency != null)
                {
                    _CoreService.DeleteCurrency(currencyId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No currency found under that ID.");

                return response;
            });
        }

        //[HttpGet]
        //[Route("getBasecurrency")]
        //public HttpResponseMessage GetOpenCurrency(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        Currency currency = _CoreService.GetBaseCurrency();

        //        // notice no need to create a seperate model object since Currency entity will do just fine
        //        response = request.CreateResponse<Currency>(HttpStatusCode.OK, currency);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("getcurrency/{currencyId}")]
        public HttpResponseMessage GetCurrency(HttpRequestMessage request, int currencyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Currency currency = _CoreService.GetCurrency(currencyId);

                // notice no need to create a seperate model object since Currency entity will do just fine
                response = request.CreateResponse<Currency>(HttpStatusCode.OK, currency);

                return response;
            });
        }

        [HttpGet]
        [Route("getcurrencywithchildrens/{currencyId}")]
        public HttpResponseMessage GetCurrencyWithPeriod(HttpRequestMessage request, int currencyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var currencyModel = new CurrencyModel();

                Currency currency = _CoreService.GetCurrency(currencyId);
                CurrencyRateData[] currencyRates = _CoreService.GetCurrencyRates(currencyId);

                currencyModel.Currency = currency;
                currencyModel.CurrencyRates = currencyRates;

                // notice no need to create a seperate model object since Currency entity will do just fine
                response = request.CreateResponse<CurrencyModel>(HttpStatusCode.OK, currencyModel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecurrencies")]
        public HttpResponseMessage GetAvailableCurrencys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Currency[] currencies = _CoreService.GetAllCurrencies();

                return request.CreateResponse<Currency[]>(HttpStatusCode.OK, currencies);
            });
        }


        [HttpGet]
        [Route("getbasecurrency")]
        public HttpResponseMessage GetBaseCurrency(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Currency[] currencies = _CoreService.GetBaseCurrency();

                return request.CreateResponse<Currency[]>(HttpStatusCode.OK, currencies);
            });
        }
    }
}
