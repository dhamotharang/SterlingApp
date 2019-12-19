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
    [RoutePrefix("api/eurobondspread")]
    [UsesDisposableService]
    public class EuroBondSpreadController : ApiControllerBase
    {
        [ImportingConstructor]
        public EuroBondSpreadController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateeuroBondSpread")]
        public HttpResponseMessage UpdateEuroBondSpread(HttpRequestMessage request, [FromBody]EuroBondSpread euroBondSpreadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var euroBondSpread = _IFRS9Service.UpdateEuroBondSpread(euroBondSpreadModel);

                return request.CreateResponse<EuroBondSpread>(HttpStatusCode.OK, euroBondSpread);
            });
        }

        [HttpPost]
        [Route("deleteeuroBondSpread")]
        public HttpResponseMessage DeleteEuroBondSpread(HttpRequestMessage request, [FromBody]int euroBondSpreadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                EuroBondSpread euroBondSpread = _IFRS9Service.GetEuroBondSpread(euroBondSpreadId);

                if (euroBondSpread != null)
                {
                    _IFRS9Service.DeleteEuroBondSpread(euroBondSpreadId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No euroBondSpread found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("geteuroBondSpread/{euroBondSpreadId}")]
        public HttpResponseMessage GetEuroBondSpread(HttpRequestMessage request,int euroBondSpreadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EuroBondSpread euroBondSpread = _IFRS9Service.GetEuroBondSpread(euroBondSpreadId);

                // notice no need to create a seperate model object since EuroBondSpread entity will do just fine
                response = request.CreateResponse<EuroBondSpread>(HttpStatusCode.OK, euroBondSpread);

                return response;
            });
        }

        [HttpGet]
        [Route("availableeuroBondSpreads")]
        public HttpResponseMessage GetAvailableEuroBondSpreads(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                EuroBondSpread[] euroBondSpreads = _IFRS9Service.GetAllEuroBondSpreads();

                return request.CreateResponse<EuroBondSpread[]>(HttpStatusCode.OK, euroBondSpreads);
            });
        }
    }
}
