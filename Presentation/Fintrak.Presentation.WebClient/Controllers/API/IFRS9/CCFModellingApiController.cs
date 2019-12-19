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
    [RoutePrefix("api/ccfmodelling")]
    [UsesDisposableService]
    public class CCFModellingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CCFModellingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateccfmodelling")]
        public HttpResponseMessage UpdateCCFModelling(HttpRequestMessage request, [FromBody]CCFModelling ccfmodellingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ccfmodelling = _IFRS9Service.UpdateCCFModelling(ccfmodellingModel);

                return request.CreateResponse<CCFModelling>(HttpStatusCode.OK, ccfmodelling);
            });
        }

        [HttpPost]
        [Route("deleteccfmodelling")]
        public HttpResponseMessage DeleteCCFModelling(HttpRequestMessage request, [FromBody]int ccfmodellingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CCFModelling ccfmodelling = _IFRS9Service.GetCCFModelling(ccfmodellingId);

                if (ccfmodelling != null)
                {
                    _IFRS9Service.DeleteCCFModelling(ccfmodellingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ccfmodelling found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getccfmodelling/{ccfmodellingId}")]
        public HttpResponseMessage GetCCFModelling(HttpRequestMessage request,int ccfmodellingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CCFModelling ccfmodelling = _IFRS9Service.GetCCFModelling(ccfmodellingId);

                // notice no need to create a seperate model object since CCFModelling entity will do just fine
                response = request.CreateResponse<CCFModelling>(HttpStatusCode.OK, ccfmodelling);

                return response;
            });
        }

        [HttpGet]
        [Route("availableccfmodellings")]
        public HttpResponseMessage GetAvailableCCFModellings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CCFModelling[] ccfmodellings = _IFRS9Service.GetAllCCFModellings();

                return request.CreateResponse<CCFModelling[]>(HttpStatusCode.OK, ccfmodellings);
            });
        }
    }
}
