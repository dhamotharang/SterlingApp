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
    [RoutePrefix("api/impairmentinvestment")]
    [UsesDisposableService]
    public class ImpairmentInvestmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ImpairmentInvestmentApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateimpairmentInvestment")]
        public HttpResponseMessage UpdateImpairmentInvestment(HttpRequestMessage request, [FromBody]ImpairmentInvestment impairmentInvestmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var impairmentInvestment = _IFRS9Service.UpdateImpairmentInvestment(impairmentInvestmentModel);

                return request.CreateResponse<ImpairmentInvestment>(HttpStatusCode.OK, impairmentInvestment);
            });
        }

        [HttpPost]
        [Route("deleteimpairmentInvestment")]
        public HttpResponseMessage DeleteImpairmentInvestment(HttpRequestMessage request, [FromBody]int Investment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ImpairmentInvestment impairmentInvestment = _IFRS9Service.GetImpairmentInvestment(Investment_Id);

                if (impairmentInvestment != null)
                {
                    _IFRS9Service.DeleteImpairmentInvestment(Investment_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Investment Impairment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getimpairmentInvestment/{Investment_Id}")]
        public HttpResponseMessage GetImpairmentInvestment(HttpRequestMessage request, int Investment_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentInvestment impairmentInvestment = _IFRS9Service.GetImpairmentInvestment(Investment_Id);

                // notice no need to create a seperate model object since ImpairmentInvestment entity will do just fine
                response = request.CreateResponse<ImpairmentInvestment>(HttpStatusCode.OK, impairmentInvestment);

                return response;
            });
        }

        [HttpGet]
        [Route("availableimpairmentInvestments")]
        public HttpResponseMessage GetAvailableImpairmentInvestments(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ImpairmentInvestment[] impairmentInvestments = _IFRS9Service.GetAllImpairmentInvestments();

                return request.CreateResponse<ImpairmentInvestment[]>(HttpStatusCode.OK, impairmentInvestments);
            });
        }
    }
}
