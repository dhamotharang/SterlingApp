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

namespace Fintrak.Presentation.WebClient.API {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/MarginalCCFStr")]
    [UsesDisposableService]
    public class MarginalCCFStrApiController : ApiControllerBase {
        [ImportingConstructor]
        public MarginalCCFStrApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateMarginalCCFStr")]
        public HttpResponseMessage Updatemarginalccfstr(HttpRequestMessage request, [FromBody]MarginalCCFStr marginalccfstrModel) {
            return GetHttpResponse(request, () => {
                var marginalccfstr = _IFRS9Service.UpdateMarginalCCFStr(marginalccfstrModel);
                return request.CreateResponse<MarginalCCFStr>(HttpStatusCode.OK, marginalccfstr);
            });
        }


        [HttpPost]
        [Route("deleteMarginalCCFStr")]
        public HttpResponseMessage DeleteloanPry(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                MarginalCCFStr marginalccfstr = _IFRS9Service.GetMarginalCCFStr(Id);
                if (marginalccfstr != null) {
                    _IFRS9Service.DeleteMarginalCCFStr(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MarginalCCFStrdata found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableMarginalCCFStr")]
        public HttpResponseMessage GetAvailableMarginalCCFStrs(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                MarginalCCFStr[] marginalccfstr = _IFRS9Service.GetAllMarginalCCFStr().ToArray();
                return request.CreateResponse<MarginalCCFStr[]>(HttpStatusCode.OK, marginalccfstr.ToArray());
            });
        }

        [HttpGet]
        [Route("getMarginalCCFStr/{Id}")]
        public HttpResponseMessage GetMarginalCCFStr(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                MarginalCCFStr marginalccfstr = _IFRS9Service.GetMarginalCCFStr(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<MarginalCCFStr>(HttpStatusCode.OK, marginalccfstr);
                return response;
            });
        }


        [HttpGet]
        [Route("getMarginalCCFStrbysearch/{searchParam}")]
        public HttpResponseMessage GetMarginalCCFStrBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                MarginalCCFStr[] marginalccfstr = _IFRS9Service.GetMarginalCCFStrBySearch(searchParam);
                return request.CreateResponse<MarginalCCFStr[]>(HttpStatusCode.OK, marginalccfstr.ToArray());
            });
        }


        [HttpGet]
        [Route("availableMarginalCCFStr/{defaultCount}")]
        public HttpResponseMessage GetAvailableMarginalCCFStr(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                MarginalCCFStr[] marginalccfstr = _IFRS9Service.GetMarginalCCFStrs(defaultCount).ToArray();
                return request.CreateResponse<MarginalCCFStr[]>(HttpStatusCode.OK, marginalccfstr.ToArray());
            });
        }

    }
}


