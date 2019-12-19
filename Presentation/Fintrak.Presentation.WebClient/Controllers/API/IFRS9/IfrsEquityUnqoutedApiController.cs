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
    [RoutePrefix("api/ifrsequityunqouted")]
    [UsesDisposableService]
    public class IfrsEquityUnqoutedApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsEquityUnqoutedApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsEquityUnqouted")]
        public HttpResponseMessage UpdateIfrsEquityUnqouted(HttpRequestMessage request, [FromBody]IfrsEquityUnqouted ifrsEquityUnqoutedModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsEquityUnqouted = _IFRS9Service.UpdateIfrsEquityUnqouted(ifrsEquityUnqoutedModel);

                return request.CreateResponse<IfrsEquityUnqouted>(HttpStatusCode.OK, ifrsEquityUnqouted);
            });
        }

        [HttpPost]
        [Route("deleteifrsEquityUnqouted")]
        public HttpResponseMessage DeleteIfrsEquityUnqouted(HttpRequestMessage request, [FromBody]int ifrsEquityUnqoutedId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsEquityUnqouted ifrsEquityUnqouted = _IFRS9Service.GetIfrsEquityUnqouted(ifrsEquityUnqoutedId);

                if (ifrsEquityUnqouted != null)
                {
                    _IFRS9Service.DeleteIfrsEquityUnqouted(ifrsEquityUnqoutedId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsEquityUnqouted found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsEquityUnqouted/{ifrsEquityUnqoutedId}")]
        public HttpResponseMessage GetIfrsEquityUnqouted(HttpRequestMessage request,int ifrsEquityUnqoutedId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsEquityUnqouted ifrsEquityUnqouted = _IFRS9Service.GetIfrsEquityUnqouted(ifrsEquityUnqoutedId);

                // notice no need to create a seperate model object since IfrsEquityUnqouted entity will do just fine
                response = request.CreateResponse<IfrsEquityUnqouted>(HttpStatusCode.OK, ifrsEquityUnqouted);

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrsEquityUnqouteds")]
        public HttpResponseMessage GetAvailableIfrsEquityUnqouteds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsEquityUnqouted[] ifrsEquityUnqouteds = _IFRS9Service.GetAllIfrsEquityUnqouteds();

                return request.CreateResponse<IfrsEquityUnqouted[]>(HttpStatusCode.OK, ifrsEquityUnqouteds);
            });
        }
    }
}
