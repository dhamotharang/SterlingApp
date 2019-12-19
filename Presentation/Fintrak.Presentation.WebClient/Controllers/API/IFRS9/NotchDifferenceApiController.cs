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
    [RoutePrefix("api/notchdifference")]
    [UsesDisposableService]
    public class NotchDifferenceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NotchDifferenceApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatenotchDifference")]
        public HttpResponseMessage UpdateNotchDifference(HttpRequestMessage request, [FromBody]NotchDifference notchDifferenceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var notchDifference = _IFRS9Service.UpdateNotchDifference(notchDifferenceModel);

                return request.CreateResponse<NotchDifference>(HttpStatusCode.OK, notchDifference);
            });
        }

        [HttpPost]
        [Route("deletenotchDifference")]
        public HttpResponseMessage DeleteNotchDifference(HttpRequestMessage request, [FromBody]int notchDifferenceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NotchDifference notchDifference = _IFRS9Service.GetNotchDifference(notchDifferenceId);

                if (notchDifference != null)
                {
                    _IFRS9Service.DeleteNotchDifference(notchDifferenceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No notchDifference found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getnotchDifference/{notchDifferenceId}")]
        public HttpResponseMessage GetNotchDifference(HttpRequestMessage request,int notchDifferenceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NotchDifference notchDifference = _IFRS9Service.GetNotchDifference(notchDifferenceId);

                // notice no need to create a seperate model object since NotchDifference entity will do just fine
                response = request.CreateResponse<NotchDifference>(HttpStatusCode.OK, notchDifference);

                return response;
            });
        }

        [HttpGet]
        [Route("availablenotchDifferences")]
        public HttpResponseMessage GetAvailableNotchDifferences(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NotchDifference[] notchDifferences = _IFRS9Service.GetAllNotchDifferences();

                return request.CreateResponse<NotchDifference[]>(HttpStatusCode.OK, notchDifferences);
            });
        }
    }
}
