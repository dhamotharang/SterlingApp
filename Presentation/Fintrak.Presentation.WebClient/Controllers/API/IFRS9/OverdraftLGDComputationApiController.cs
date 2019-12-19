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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/overdraftlgdcomputation")]
    [UsesDisposableService]
    public class OverdraftLGDComputationApiController : ApiControllerBase {
        [ImportingConstructor]
        public OverdraftLGDComputationApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateOverdraftLGDComputation")]
        public HttpResponseMessage Updatelgdcomptresult(HttpRequestMessage request, [FromBody]OverdraftLGDComputation lgdcomptresultModel) {
            return GetHttpResponse(request, () => {
                var lgdcomptresult = _IFRS9Service.UpdateOverdraftLGDComputation(lgdcomptresultModel);
                return request.CreateResponse<OverdraftLGDComputation>(HttpStatusCode.OK, lgdcomptresult);
            });
        }


        [HttpPost]
        [Route("deleteOverdraftLGDComputation")]
        public HttpResponseMessage DeleteOverdraftLGDComputation(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                OverdraftLGDComputation lgdcomptresult = _IFRS9Service.GetOverdraftLGDComputation(Id);
                if (lgdcomptresult != null) {
                    _IFRS9Service.DeleteOverdraftLGDComputation(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OverdraftLGDComputation data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableOverdraftLGDComputation")]
        public HttpResponseMessage GetAvailableOverdraftLGDComputations(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                OverdraftLGDComputation[] lgdcomptresult = _IFRS9Service.GetAllOverdraftLGDComputation().ToArray();
                return request.CreateResponse<OverdraftLGDComputation[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
            });
        }

        [HttpGet]
        [Route("getOverdraftLGDComputation/{Id}")]
        public HttpResponseMessage GetOverdraftLGDComputation(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                OverdraftLGDComputation lgdcomptresult = _IFRS9Service.GetOverdraftLGDComputation(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<OverdraftLGDComputation>(HttpStatusCode.OK, lgdcomptresult);
                return response;
            });
        }


        [HttpGet]
        [Route("getOverdraftLGDComputationbysearch/{searchParam}")]
        public HttpResponseMessage GetOverdraftLGDComputationBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                OverdraftLGDComputation[] lgdcomptresult = _IFRS9Service.GetOverdraftLGDComputationBySearch(searchParam);
                return request.CreateResponse<OverdraftLGDComputation[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
            });
        }


        [HttpGet]
        [Route("availableOverdraftLGDComputation/{defaultCount}")]
        public HttpResponseMessage GetAvailableOverdraftLGDComputation(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    OverdraftLGDComputation[] lgdcomptresult = _IFRS9Service.GetOverdraftLGDComputations(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LGD%20Result%20-%20Overdrafts.zip");
                    return response;
                }
                else
                {
                    OverdraftLGDComputation[] lgdcomptresult = _IFRS9Service.GetOverdraftLGDComputations(defaultCount, null).ToArray();
                    return request.CreateResponse<OverdraftLGDComputation[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
                }
            });
        }

    }
}
