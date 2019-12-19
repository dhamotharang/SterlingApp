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
    [RoutePrefix("api/markovmatrix")]
    [UsesDisposableService]
    public class MarkovMatrixApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MarkovMatrixApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatemarkovMatrix")]
        public HttpResponseMessage UpdateMarkovMatrix(HttpRequestMessage request, [FromBody]MarkovMatrix markovMatrixModel)
        {
            return GetHttpResponse(request, () =>
            {
                var markovMatrix = _IFRS9Service.UpdateMarkovMatrix(markovMatrixModel);

                return request.CreateResponse<MarkovMatrix>(HttpStatusCode.OK, markovMatrix);
            });
        }

        [HttpPost]
        [Route("deletemarkovMatrix")]
        public HttpResponseMessage DeleteMarkovMatrix(HttpRequestMessage request, [FromBody]int markovMatrixId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MarkovMatrix markovMatrix = _IFRS9Service.GetMarkovMatrix(markovMatrixId);

                if (markovMatrix != null)
                {
                    _IFRS9Service.DeleteMarkovMatrix(markovMatrixId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No markovMatrix found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmarkovMatrix/{markovMatrixId}")]
        public HttpResponseMessage GetMarkovMatrix(HttpRequestMessage request,int markovMatrixId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MarkovMatrix markovMatrix = _IFRS9Service.GetMarkovMatrix(markovMatrixId);

                // notice no need to create a seperate model object since MarkovMatrix entity will do just fine
                response = request.CreateResponse<MarkovMatrix>(HttpStatusCode.OK, markovMatrix);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemarkovMatrixs")]
        public HttpResponseMessage GetAvailableMarkovMatrixs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MarkovMatrixData[] markovMatrixs = _IFRS9Service.GetMarkovMatrixs();

                return request.CreateResponse<MarkovMatrixData[]>(HttpStatusCode.OK, markovMatrixs);
            });
        }
    }
}
