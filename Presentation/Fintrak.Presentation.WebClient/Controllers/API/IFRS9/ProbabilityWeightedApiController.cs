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
    [RoutePrefix("api/probabilityweighted")]
    [UsesDisposableService]
    public class ProbabilityWeightedApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProbabilityWeightedApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateprobabilityweighted")]
        public HttpResponseMessage UpdateProbabilityWeighted(HttpRequestMessage request, [FromBody]ProbabilityWeighted probabilityWeightedModel)
        {
            return GetHttpResponse(request, () =>
            {
                var probabilityWeighted = _IFRS9Service.UpdateProbabilityWeighted(probabilityWeightedModel);

                return request.CreateResponse<ProbabilityWeighted>(HttpStatusCode.OK, probabilityWeighted);
            });
        }

        [HttpPost]
        [Route("deleteprobabilityweighted")]
        public HttpResponseMessage DeleteProbabilityWeighted(HttpRequestMessage request, [FromBody]int ProbabilityWeighted_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProbabilityWeighted probabilityWeighted = _IFRS9Service.GetProbabilityWeighted(ProbabilityWeighted_Id);

                if (probabilityWeighted != null)
                {
                    _IFRS9Service.DeleteProbabilityWeighted(ProbabilityWeighted_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Probability Weighting found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getprobabilityweighted/{ProbabilityWeighted_Id}")]
        public HttpResponseMessage GetProbabilityWeighted(HttpRequestMessage request, int ProbabilityWeighted_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProbabilityWeighted probabilityWeighted = _IFRS9Service.GetProbabilityWeighted(ProbabilityWeighted_Id);

                // notice no need to create a seperate model object since ProbabilityWeighted entity will do just fine
                response = request.CreateResponse<ProbabilityWeighted>(HttpStatusCode.OK, probabilityWeighted);

                return response;
            });
        }

        [HttpGet]
        [Route("availableprobabilityweighteds")]
        public HttpResponseMessage GetAvailableProbabilityWeighteds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProbabilityWeighted[] probabilityWeighted = _IFRS9Service.GetAllProbabilityWeighteds();

                return request.CreateResponse<ProbabilityWeighted[]>(HttpStatusCode.OK, probabilityWeighted);
            });
        }

        [HttpGet]
        [Route("getprobabilityweightedbyinstrumenttype/{instrumentType}")]
        public HttpResponseMessage GetAvailableProbabilityWeighteds(HttpRequestMessage request, string InstrumentType)
        {
            return GetHttpResponse(request, () =>
            {
                ProbabilityWeighted[] probabilityWeighted = _IFRS9Service.GetProbabilityWeightedByInstrumentType(InstrumentType);

                return request.CreateResponse<ProbabilityWeighted[]>(HttpStatusCode.OK, probabilityWeighted);
            });
        }
    }
}
