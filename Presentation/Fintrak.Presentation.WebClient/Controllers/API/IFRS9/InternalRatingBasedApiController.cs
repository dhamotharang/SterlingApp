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
    [RoutePrefix("api/internalratingbased")]
    [UsesDisposableService]
    public class InternalRatingBasedApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InternalRatingBasedApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateinternalRatingBased")]
        public HttpResponseMessage UpdateInternalRatingBased(HttpRequestMessage request, [FromBody]InternalRatingBased internalRatingBasedModel)
        {
            return GetHttpResponse(request, () =>
            {
                var internalRatingBased = _IFRS9Service.UpdateInternalRatingBased(internalRatingBasedModel);

                return request.CreateResponse<InternalRatingBased>(HttpStatusCode.OK, internalRatingBased);
            });
        }

        [HttpPost]
        [Route("deleteinternalRatingBased")]
        public HttpResponseMessage DeleteInternalRatingBased(HttpRequestMessage request, [FromBody]int internalRatingBasedId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InternalRatingBased internalRatingBased = _IFRS9Service.GetInternalRatingBased(internalRatingBasedId);

                if (internalRatingBased != null)
                {
                    _IFRS9Service.DeleteInternalRatingBased(internalRatingBasedId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No internalRatingBased found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinternalRatingBased/{internalRatingBasedId}")]
        public HttpResponseMessage GetInternalRatingBased(HttpRequestMessage request, int internalRatingBasedId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InternalRatingBased internalRatingBased = _IFRS9Service.GetInternalRatingBased(internalRatingBasedId);

                // notice no need to create a seperate model object since InternalRatingBased entity will do just fine
                response = request.CreateResponse<InternalRatingBased>(HttpStatusCode.OK, internalRatingBased);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinternalRatingBaseds")]
        public HttpResponseMessage GetAvailableInternalRatingBaseds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InternalRatingBased[] internalRatingBaseds = _IFRS9Service.GetAllInternalRatingBaseds();

                return request.CreateResponse<InternalRatingBased[]>(HttpStatusCode.OK, internalRatingBaseds);
            });
        }
    }
}
