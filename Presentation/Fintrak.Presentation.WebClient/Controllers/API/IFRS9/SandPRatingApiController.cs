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
    [RoutePrefix("api/sandprating")]
    [UsesDisposableService]
    public class SandPRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SandPRatingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesandprating")]
        public HttpResponseMessage UpdateSandPRating(HttpRequestMessage request, [FromBody]SandPRating sandpratingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sandprating = _IFRS9Service.UpdateSandPRating(sandpratingModel);

                return request.CreateResponse<SandPRating>(HttpStatusCode.OK, sandprating);
            });
        }

        [HttpPost]
        [Route("deletesandprating")]
        public HttpResponseMessage DeleteSandPRating(HttpRequestMessage request, [FromBody]int SandPRating_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SandPRating sandprating = _IFRS9Service.GetSandPRating(SandPRating_Id);

                if (sandprating != null)
                {
                    _IFRS9Service.DeleteSandPRating(SandPRating_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No SandPRating found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsandprating/{SandPRating_Id}")]
        public HttpResponseMessage GetSandPRating(HttpRequestMessage request,int SandPRating_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SandPRating sandprating = _IFRS9Service.GetSandPRating(SandPRating_Id);

                // notice no need to create a seperate model object since SandPRating entity will do just fine
                response = request.CreateResponse<SandPRating>(HttpStatusCode.OK, sandprating);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesandpratings")]
        public HttpResponseMessage GetAvailableSandPRatings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SandPRating[] sandpratings = _IFRS9Service.GetAllSandPRatings();

                return request.CreateResponse<SandPRating[]>(HttpStatusCode.OK, sandpratings);
            });
        }
    }
}
