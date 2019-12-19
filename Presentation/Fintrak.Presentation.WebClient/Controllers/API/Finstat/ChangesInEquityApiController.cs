
//api/changesinequity/availablechangesinequitys

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/changesinequity")]
    [UsesDisposableService]
    public class ChangesInEquityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ChangesInEquityApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updatechangesinequity")]
        public HttpResponseMessage UpdateChangesInEquity(HttpRequestMessage request, [FromBody]ChangesInEquity changesInEquityModel)
        {
            return GetHttpResponse(request, () =>
            {
                var changesInEquity = _FinstatService.UpdateChangesInEquity(changesInEquityModel);

                return request.CreateResponse<ChangesInEquity>(HttpStatusCode.OK, changesInEquity);
            });
        }

        [HttpPost]
        [Route("deletechangesinequity")]
        public HttpResponseMessage DeleteChangesInEquity(HttpRequestMessage request, [FromBody]int ChangesInEquityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ChangesInEquity changesInEquity = _FinstatService.GetChangesInEquity(ChangesInEquityId);

                if (changesInEquity != null)
                {
                    _FinstatService.DeleteChangesInEquity(ChangesInEquityId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No changesInEquity found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getchangesinequity/{ChangesInEquityId}")]
        public HttpResponseMessage GetChangesInEquity(HttpRequestMessage request, int ChangesInEquityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ChangesInEquity changesInEquity = _FinstatService.GetChangesInEquity(ChangesInEquityId);

                // notice no need to create a seperate model object since ChangesInEquity entity will do just fine
                response = request.CreateResponse<ChangesInEquity>(HttpStatusCode.OK, changesInEquity);

                return response;
            });
        }

        [HttpGet]
        [Route("availablechangesinequitys")] 
        public HttpResponseMessage GetAvailableChangesInEquitys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ChangesInEquity[] changesInEquitys = _FinstatService.GetAllChangesInEquitys();

                return request.CreateResponse<ChangesInEquity[]>(HttpStatusCode.OK, changesInEquitys);
            });
        }

    }
}