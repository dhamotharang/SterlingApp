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
    [RoutePrefix("api/LGDComptResult")]
    [UsesDisposableService]
    public class LGDComptResultApiController : ApiControllerBase {
        [ImportingConstructor]
        public LGDComptResultApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateLGDComptResult")]
        public HttpResponseMessage Updatelgdcomptresult(HttpRequestMessage request, [FromBody]LGDComptResult lgdcomptresultModel) {
            return GetHttpResponse(request, () => {
                var lgdcomptresult = _IFRS9Service.UpdateLGDComptResult(lgdcomptresultModel);
                return request.CreateResponse<LGDComptResult>(HttpStatusCode.OK, lgdcomptresult);
            });
        }


        [HttpPost]
        [Route("deleteLGDComptResult")]
        public HttpResponseMessage DeleteLGDComptResult(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                LGDComptResult lgdcomptresult = _IFRS9Service.GetLGDComptResult(Id);
                if (lgdcomptresult != null) {
                    _IFRS9Service.DeleteLGDComptResult(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LGDComptResult data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableLGDComptResult")]
        public HttpResponseMessage GetAvailableLGDComptResults(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                LGDComptResult[] lgdcomptresult = _IFRS9Service.GetAllLGDComptResult().ToArray();
                return request.CreateResponse<LGDComptResult[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
            });
        }

        [HttpGet]
        [Route("getLGDComptResult/{Id}")]
        public HttpResponseMessage GetLGDComptResult(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                LGDComptResult lgdcomptresult = _IFRS9Service.GetLGDComptResult(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LGDComptResult>(HttpStatusCode.OK, lgdcomptresult);
                return response;
            });
        }


        [HttpGet]
        [Route("getLGDComptResultbysearch/{searchParam}")]
        public HttpResponseMessage GetLGDComptResultBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                LGDComptResult[] lgdcomptresult = _IFRS9Service.GetLGDComptResultBySearch(searchParam);
                return request.CreateResponse<LGDComptResult[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
            });
        }


        [HttpGet]
        [Route("availableLGDComptResult/{defaultCount}")]
        public HttpResponseMessage GetAvailableLGDComptResult(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    LGDComptResult[] lgdcomptresult = _IFRS9Service.GetLGDComptResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LGD%20Results%20-%20Loans%20&%20OD.zip");
                    return response;
                }
                else
                {
                    LGDComptResult[] lgdcomptresult = _IFRS9Service.GetLGDComptResults(defaultCount, null).ToArray();
                    return request.CreateResponse<LGDComptResult[]>(HttpStatusCode.OK, lgdcomptresult.ToArray());
                }
            });
        }

    }
}
