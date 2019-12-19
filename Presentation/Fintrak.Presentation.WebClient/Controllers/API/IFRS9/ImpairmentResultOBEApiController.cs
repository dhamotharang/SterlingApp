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
    [RoutePrefix("api/impairmentresultobe")]
    [UsesDisposableService]
    public class ImpairmentResultOBEApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ImpairmentResultOBEApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateimpairmentResultOBE")]
        public HttpResponseMessage UpdateImpairmentResultOBE(HttpRequestMessage request, [FromBody]ImpairmentResultOBE impairmentResultOBEModel)
        {
            return GetHttpResponse(request, () =>
            {
                var impairmentResultOBE = _IFRS9Service.UpdateImpairmentResultOBE(impairmentResultOBEModel);

                return request.CreateResponse<ImpairmentResultOBE>(HttpStatusCode.OK, impairmentResultOBE);
            });
        }

        [HttpPost]
        [Route("deleteimpairmentResultOBE")]
        public HttpResponseMessage DeleteImpairmentResultOBE(HttpRequestMessage request, [FromBody]int Impairment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ImpairmentResultOBE impairmentResultOBE = _IFRS9Service.GetImpairmentResultOBE(Impairment_Id);

                if (impairmentResultOBE != null)
                {
                    _IFRS9Service.DeleteImpairmentResultOBE(Impairment_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OBE Impairment Result found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getimpairmentResultOBE/{Impairment_Id}")]
        public HttpResponseMessage GetImpairmentResultOBE(HttpRequestMessage request, int Impairment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentResultOBE impairmentResultOBE = _IFRS9Service.GetImpairmentResultOBE(Impairment_Id);

                // notice no need to create a seperate model object since ImpairmentResultOBE entity will do just fine
                response = request.CreateResponse<ImpairmentResultOBE>(HttpStatusCode.OK, impairmentResultOBE);

                return response;
            });
        }

        [HttpGet]
        [Route("availableimpairmentResultOBEs")]
        public HttpResponseMessage GetAvailableImpairmentResultOBEs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ImpairmentResultOBE[] impairmentResultOBEs = _IFRS9Service.GetAllImpairmentResultOBEs();

                return request.CreateResponse<ImpairmentResultOBE[]>(HttpStatusCode.OK, impairmentResultOBEs);
            });
        }

        [HttpGet]
        [Route("getimpairmentResultOBEsBySearch/{SearchParam}")]
        public HttpResponseMessage GetImpairmentResultOBEBySearch(HttpRequestMessage request, string SearchParam)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentResultOBE[] impairmentResultOBEs = _IFRS9Service.GetImpairmentResultOBEBySearch(SearchParam);

                // notice no need to create a seperate model object since ImpairmentResultOBE entity will do just fine
                response = request.CreateResponse<ImpairmentResultOBE[]>(HttpStatusCode.OK, impairmentResultOBEs);

                return response;
            });
        }
    }
}
