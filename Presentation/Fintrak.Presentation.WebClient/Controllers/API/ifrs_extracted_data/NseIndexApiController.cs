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
    [RoutePrefix("api/nseindex")]
    [UsesDisposableService]
    public class NseIndexApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NseIndexApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updatenseindex")]
        public HttpResponseMessage UpdateNseIndex(HttpRequestMessage request, [FromBody]NseIndex nseIndexModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nseIndex = _IFRSDataService.UpdateNseIndex(nseIndexModel);

                return request.CreateResponse<NseIndex>(HttpStatusCode.OK, nseIndex);
            });
        }

        [HttpPost]
        [Route("deletenseindex")]
        public HttpResponseMessage DeleteNseIndex(HttpRequestMessage request, [FromBody]int NseIndexId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NseIndex nseIndex = _IFRSDataService.GetNseIndex(NseIndexId);

                if (nseIndex != null)
                {
                    _IFRSDataService.DeleteNseIndex(NseIndexId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No nseIndex found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getnseindex/{nseIndexId}")]
        public HttpResponseMessage GetNseIndex(HttpRequestMessage request, int NseIndexId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NseIndex nseIndex = _IFRSDataService.GetNseIndex(NseIndexId);

                // notice no need to create a seperate model object since NseIndex entity will do just fine
                response = request.CreateResponse<NseIndex>(HttpStatusCode.OK, nseIndex);

                return response;
            });
        }

        [HttpGet]
        [Route("availablenseindexs")]
        public HttpResponseMessage GetAvailableNseIndexs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NseIndex[] nseIndexs = _IFRSDataService.GetAllNseIndexs();

                return request.CreateResponse<NseIndex[]>(HttpStatusCode.OK, nseIndexs);
            });
        }

        [HttpGet]
        [Route("availableprobabilityweights")]
        public HttpResponseMessage GetAvailableProbabilityWeights(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProbabilityWeight[] probabilityWeight = _IFRSDataService.GetAllProbabilityWeights();

                return request.CreateResponse<ProbabilityWeight[]>(HttpStatusCode.OK, probabilityWeight);
            });
        }

        [HttpGet]
        [Route("computeprobabilityweight/{lOC}")]
        public HttpResponseMessage ComputeProbabilityWeight(HttpRequestMessage request, double lOC)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSDataService.ComputeProbabilityWeight(lOC);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
