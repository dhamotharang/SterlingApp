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
    [RoutePrefix("api/lgdinputfactor")]
    [UsesDisposableService]
    public class LgdInputFactorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LgdInputFactorApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelgdinputfactor")]
        public HttpResponseMessage UpdateLgdInputFactor(HttpRequestMessage request, [FromBody]LgdInputFactor lgdinputfactorModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lgdinputfactor = _IFRS9Service.UpdateLgdInputFactor(lgdinputfactorModel);

                return request.CreateResponse<LgdInputFactor>(HttpStatusCode.OK, lgdinputfactor);
            });
        }

        [HttpPost]
        [Route("deletelgdinputfactor")]
        public HttpResponseMessage DeleteLgdInputFactor(HttpRequestMessage request, [FromBody]int LgdInputFactorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LgdInputFactor lgdinputfactor = _IFRS9Service.GetLgdInputFactor(LgdInputFactorId);

                if (lgdinputfactor != null)
                {
                    _IFRS9Service.DeleteLgdInputFactor(LgdInputFactorId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LgdInputFactor found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlgdinputfactor/{LgdInputFactorId}")]
        public HttpResponseMessage GetLgdInputFactor(HttpRequestMessage request,int LgdInputFactorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LgdInputFactor lgdinputfactor = _IFRS9Service.GetLgdInputFactor(LgdInputFactorId);

                // notice no need to create a seperate model object since LgdInputFactor entity will do just fine
                response = request.CreateResponse<LgdInputFactor>(HttpStatusCode.OK, lgdinputfactor);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelgdinputfactors")]
        public HttpResponseMessage GetAvailableLgdInputFactors(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LgdInputFactor[] lgdinputfactors = _IFRS9Service.GetAllLgdInputFactors();

                return request.CreateResponse<LgdInputFactor[]>(HttpStatusCode.OK, lgdinputfactors);
            });
        }
    }
}
