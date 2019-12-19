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
    [RoutePrefix("api/impairmentresultretail")]
    [UsesDisposableService]
    public class ImpairmentResultRetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ImpairmentResultRetailApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateimpairmentResultRetail")]
        public HttpResponseMessage UpdateImpairmentResultRetail(HttpRequestMessage request, [FromBody]ImpairmentResultRetail impairmentResultRetailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var impairmentResultRetail = _IFRS9Service.UpdateImpairmentResultRetail(impairmentResultRetailModel);

                return request.CreateResponse<ImpairmentResultRetail>(HttpStatusCode.OK, impairmentResultRetail);
            });
        }

        [HttpPost]
        [Route("deleteimpairmentResultRetail")]
        public HttpResponseMessage DeleteImpairmentResultRetail(HttpRequestMessage request, [FromBody]int Impairment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ImpairmentResultRetail impairmentResultRetail = _IFRS9Service.GetImpairmentResultRetail(Impairment_Id);

                if (impairmentResultRetail != null)
                {
                    _IFRS9Service.DeleteImpairmentResultRetail(Impairment_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OBE Impairment Result found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getimpairmentResultRetail/{Impairment_Id}")]
        public HttpResponseMessage GetImpairmentResultRetail(HttpRequestMessage request, int Impairment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentResultRetail impairmentResultRetail = _IFRS9Service.GetImpairmentResultRetail(Impairment_Id);

                // notice no need to create a seperate model object since ImpairmentResultRetail entity will do just fine
                response = request.CreateResponse<ImpairmentResultRetail>(HttpStatusCode.OK, impairmentResultRetail);

                return response;
            });
        }

        [HttpGet]
        [Route("availableimpairmentResultRetails")]
        public HttpResponseMessage GetAvailableImpairmentResultRetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ImpairmentResultRetail[] impairmentResultRetails = _IFRS9Service.GetAllImpairmentResultRetails();

                return request.CreateResponse<ImpairmentResultRetail[]>(HttpStatusCode.OK, impairmentResultRetails);
            });
        }

        [HttpGet]
        [Route("getimpairmentResultRetailsBySearch/{SearchParam}")]
        public HttpResponseMessage GetImpairmentResultRetailBySearch(HttpRequestMessage request, string SearchParam)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentResultRetail[] impairmentResultRetails = _IFRS9Service.GetImpairmentResultRetailBySearch(SearchParam);

                // notice no need to create a seperate model object since ImpairmentResultRetail entity will do just fine
                response = request.CreateResponse<ImpairmentResultRetail[]>(HttpStatusCode.OK, impairmentResultRetails);

                return response;
            });
        }
    }
}
