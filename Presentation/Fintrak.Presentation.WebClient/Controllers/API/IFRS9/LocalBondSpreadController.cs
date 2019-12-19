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
    [RoutePrefix("api/localbondspread")]
    [UsesDisposableService]
    public class LocalBondSpreadController : ApiControllerBase
    {
        [ImportingConstructor]
        public LocalBondSpreadController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelocalBondSpread")]
        public HttpResponseMessage UpdateLocalBondSpread(HttpRequestMessage request, [FromBody]LocalBondSpread localBondSpreadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var localBondSpread = _IFRS9Service.UpdateLocalBondSpread(localBondSpreadModel);

                return request.CreateResponse<LocalBondSpread>(HttpStatusCode.OK, localBondSpread);
            });
        }

        [HttpPost]
        [Route("deletelocalBondSpread")]
        public HttpResponseMessage DeleteLocalBondSpread(HttpRequestMessage request, [FromBody]int localBondSpreadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LocalBondSpread localBondSpread = _IFRS9Service.GetLocalBondSpread(localBondSpreadId);

                if (localBondSpread != null)
                {
                    _IFRS9Service.DeleteLocalBondSpread(localBondSpreadId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No localBondSpread found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlocalBondSpread/{localBondSpreadId}")]
        public HttpResponseMessage GetLocalBondSpread(HttpRequestMessage request, int localBondSpreadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LocalBondSpread localBondSpread = _IFRS9Service.GetLocalBondSpread(localBondSpreadId);

                // notice no need to create a seperate model object since LocalBondSpread entity will do just fine
                response = request.CreateResponse<LocalBondSpread>(HttpStatusCode.OK, localBondSpread);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelocalBondSpreads")]
        public HttpResponseMessage GetAvailableLocalBondSpreads(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LocalBondSpread[] localBondSpreads = _IFRS9Service.GetAllLocalBondSpreads();

                return request.CreateResponse<LocalBondSpread[]>(HttpStatusCode.OK, localBondSpreads);
            });
        }
    }
}
