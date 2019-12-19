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
    [RoutePrefix("api/impairmentcorporate")]
    [UsesDisposableService]
    public class ImpairmentCorporateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ImpairmentCorporateApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateimpairmentCorporate")]
        public HttpResponseMessage UpdateImpairmentCorporate(HttpRequestMessage request, [FromBody]ImpairmentCorporate impairmentCorporateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var impairmentCorporate = _IFRS9Service.UpdateImpairmentCorporate(impairmentCorporateModel);

                return request.CreateResponse<ImpairmentCorporate>(HttpStatusCode.OK, impairmentCorporate);
            });
        }

        [HttpPost]
        [Route("deleteimpairmentCorporate")]
        public HttpResponseMessage DeleteImpairmentCorporate(HttpRequestMessage request, [FromBody]int Corporate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ImpairmentCorporate impairmentCorporate = _IFRS9Service.GetImpairmentCorporate(Corporate_Id);

                if (impairmentCorporate != null)
                {
                    _IFRS9Service.DeleteImpairmentCorporate(Corporate_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Corporate Impairment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getimpairmentCorporate/{Corporate_Id}")]
        public HttpResponseMessage GetImpairmentCorporate(HttpRequestMessage request, int Corporate_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentCorporate impairmentCorporate = _IFRS9Service.GetImpairmentCorporate(Corporate_Id);

                // notice no need to create a seperate model object since ImpairmentCorporate entity will do just fine
                response = request.CreateResponse<ImpairmentCorporate>(HttpStatusCode.OK, impairmentCorporate);

                return response;
            });
        }

        [HttpGet]
        [Route("availableimpairmentCorporates")]
        public HttpResponseMessage GetAvailableImpairmentCorporates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ImpairmentCorporate[] impairmentCorporates = _IFRS9Service.GetAllImpairmentCorporates();

                return request.CreateResponse<ImpairmentCorporate[]>(HttpStatusCode.OK, impairmentCorporates);
            });
        }
    }
}
