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
    [RoutePrefix("api/foreigneadexchangerate")]
    [UsesDisposableService]
    public class ForeignEADExchangeRateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ForeignEADExchangeRateApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }
     

        [HttpGet]
        [Route("availableforeigneadexchangerates")]
        public HttpResponseMessage GetAllForeignEADExchangeRates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ForeignEADExchangeRate[] foreignEadExchangeRates = _IFRS9Service.GetAllForeignEADExchangeRates();

                return request.CreateResponse<ForeignEADExchangeRate[]>(HttpStatusCode.OK, foreignEadExchangeRates);
            });
        }


        [HttpPost]
        [Route("updateforeigneadexchangerate")]
        public HttpResponseMessage UpdateForeignEADExchangeRate(HttpRequestMessage request, [FromBody]ForeignEADExchangeRate foreigneadexchangerateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var foreigneadexchangerate = _IFRS9Service.UpdateForeignEADExchangeRate(foreigneadexchangerateModel);

                return request.CreateResponse<ForeignEADExchangeRate>(HttpStatusCode.OK, foreigneadexchangerate);
            });
        }

        [HttpPost]
        [Route("deleteforeigneadexchangerate")]
        public HttpResponseMessage DeleteForeignEADExchangeRate(HttpRequestMessage request, [FromBody]int foreigneadexchangerateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ForeignEADExchangeRate foreigneadexchangerate = _IFRS9Service.GetForeignEADExchangeRate(foreigneadexchangerateId);

                if (foreigneadexchangerate != null)
                {
                    _IFRS9Service.DeleteForeignEADExchangeRate(foreigneadexchangerateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Foreign EAD exchange rate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getforeigneadexchangerate/{foreigneadexchangerateId}")]
        public HttpResponseMessage GetForeignEADExchangeRate(HttpRequestMessage request,int foreigneadexchangerateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ForeignEADExchangeRate foreigneadexchangerate = _IFRS9Service.GetForeignEADExchangeRate(foreigneadexchangerateId);

                // notice no need to create a seperate model object since Sector entity will do just fine
                response = request.CreateResponse<ForeignEADExchangeRate>(HttpStatusCode.OK, foreigneadexchangerate);

                return response;
            });
        }
                
    }

  }
