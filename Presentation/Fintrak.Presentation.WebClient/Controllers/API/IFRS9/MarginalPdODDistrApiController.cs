using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/MarginalPdODDistr")]
    [UsesDisposableService]

    public class MarginalPdODDistrApiController : ApiControllerBase
    {

        [ImportingConstructor]
        public MarginalPdODDistrApiController(IIFRS9Service ifrs9service)
        {
            _IFRS9Service = ifrs9service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateMarginalPdODDistr")]
        public HttpResponseMessage UpdatemarginalpdODdistr(HttpRequestMessage request, [FromBody]MarginalPdODDistr marginalpdODdistrModel)
        {
            return GetHttpResponse(request, () => {
                var marginalpdODdistr = _IFRS9Service.UpdateMarginalPdODDistr(marginalpdODdistrModel);
                return request.CreateResponse<MarginalPdODDistr>(HttpStatusCode.OK, marginalpdODdistr);
            });
        }


        [HttpPost]
        [Route("deleteMarginalPdODDistr")]
        public HttpResponseMessage DeleteMarginalPdODDistr(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                MarginalPdODDistr marginalpdODdistr = _IFRS9Service.GetMarginalPdODDistr(Id);
                if (marginalpdODdistr != null)
                {
                    _IFRS9Service.DeleteMarginalPdODDistr(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MarginalPdODDistr data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableMarginalPdODDistr")]
        public HttpResponseMessage GetAvailableMarginalPdODDistrs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () => {
                MarginalPdODDistr[] marginalpdODdistr = _IFRS9Service.GetAllMarginalPdODDistr().ToArray();
                return request.CreateResponse<MarginalPdODDistr[]>(HttpStatusCode.OK, marginalpdODdistr.ToArray());
            });
        }

        [HttpGet]
        [Route("getMarginalPdODDistr/{Id}")]
        public HttpResponseMessage GetMarginalPdODDistr(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                MarginalPdODDistr marginalpdODdistr = _IFRS9Service.GetMarginalPdODDistr(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<MarginalPdODDistr>(HttpStatusCode.OK, marginalpdODdistr);
                return response;
            });
        }


        [HttpGet]
        [Route("getMarginalPdODDistrbysearch/{searchParam}")]
        public HttpResponseMessage GetMarginalPdODDistrBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                MarginalPdODDistr[] marginalpdODdistr = _IFRS9Service.GetMarginalPdODDistrBySearch(searchParam);
                return request.CreateResponse<MarginalPdODDistr[]>(HttpStatusCode.OK, marginalpdODdistr.ToArray());
            });
        }


        [HttpGet]
        [Route("availableMarginalPdODDistr/{defaultCount}")]
        public HttpResponseMessage GetAvailableMarginalPdODDistr(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () => {
                MarginalPdODDistr[] marginalpdODdistr = _IFRS9Service.GetMarginalPdODDistrs(defaultCount).ToArray();
                return request.CreateResponse<MarginalPdODDistr[]>(HttpStatusCode.OK, marginalpdODdistr.ToArray());
            });
        }

    }
}
