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
    [RoutePrefix("api/assumption")]
    [UsesDisposableService]
    public class AssumptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AssumptionApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateassumption")]
        public HttpResponseMessage UpdateAssumption(HttpRequestMessage request, [FromBody]Assumption assumptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var assumption = _IFRS9Service.UpdateAssumption(assumptionModel);

                return request.CreateResponse<Assumption>(HttpStatusCode.OK, assumption);
            });
        }

        [HttpPost]
        [Route("deleteassumption")]
        public HttpResponseMessage DeleteAssumption(HttpRequestMessage request, [FromBody]int InstrumentID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Assumption assumption = _IFRS9Service.GetAssumption(InstrumentID);

                if (assumption != null)
                {
                    _IFRS9Service.DeleteAssumption(InstrumentID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Assumption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getassumption/{InstrumentID}")]
        public HttpResponseMessage GetAssumption(HttpRequestMessage request,int InstrumentID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Assumption assumption = _IFRS9Service.GetAssumption(InstrumentID);

                // notice no need to create a seperate model object since Assumption entity will do just fine
                response = request.CreateResponse<Assumption>(HttpStatusCode.OK, assumption);

                return response;
            });
        }

        [HttpGet]
        [Route("availableassumptions")]
        public HttpResponseMessage GetAvailableAssumptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Assumption[] assumptions = _IFRS9Service.GetAllAssumptions();

                return request.CreateResponse<Assumption[]>(HttpStatusCode.OK, assumptions);
            });
        }
    }
}
