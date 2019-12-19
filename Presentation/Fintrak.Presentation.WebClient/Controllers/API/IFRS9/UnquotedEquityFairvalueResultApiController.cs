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
    [RoutePrefix("api/unquotedequityfairvalueresult")]
    [UsesDisposableService]
    public class UnquotedEquityFairvalueResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UnquotedEquityFairvalueResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateunquotedEquityFairvalueResult")]
        public HttpResponseMessage UpdateUnquotedEquityFairvalueResult(HttpRequestMessage request, [FromBody]UnquotedEquityFairvalueResult unquotedEquityFairvalueResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var unquotedEquityFairvalueResult = _IFRS9Service.UpdateUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultModel);

                return request.CreateResponse<UnquotedEquityFairvalueResult>(HttpStatusCode.OK, unquotedEquityFairvalueResult);
            });
        }

        [HttpPost]
        [Route("deleteunquotedEquityFairvalueResult")]
        public HttpResponseMessage DeleteUnquotedEquityFairvalueResult(HttpRequestMessage request, [FromBody]int unquotedEquityFairvalueResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                UnquotedEquityFairvalueResult unquotedEquityFairvalueResult = _IFRS9Service.GetUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);

                if (unquotedEquityFairvalueResult != null)
                {
                    _IFRS9Service.DeleteUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No unquotedEquityFairvalueResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getunquotedEquityFairvalueResult/{unquotedEquityFairvalueResultId}")]
        public HttpResponseMessage GetUnquotedEquityFairvalueResult(HttpRequestMessage request,int unquotedEquityFairvalueResultId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UnquotedEquityFairvalueResult unquotedEquityFairvalueResult = _IFRS9Service.GetUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);

                // notice no need to create a seperate model object since UnquotedEquityFairvalueResult entity will do just fine
                response = request.CreateResponse<UnquotedEquityFairvalueResult>(HttpStatusCode.OK, unquotedEquityFairvalueResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableunquotedEquityFairvalueResults")]
        public HttpResponseMessage GetAvailableUnquotedEquityFairvalueResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UnquotedEquityFairvalueResult[] unquotedEquityFairvalueResults = _IFRS9Service.GetAllUnquotedEquityFairvalueResults();

                return request.CreateResponse<UnquotedEquityFairvalueResult[]>(HttpStatusCode.OK, unquotedEquityFairvalueResults);
            });
        }
    }
}
