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
    [RoutePrefix("api/IfrsLoansInfo")]
    [UsesDisposableService]
    public class IfrsLoansInfoApiController : ApiControllerBase {
        [ImportingConstructor]
        public IfrsLoansInfoApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsLoansInfo")]
        public HttpResponseMessage UpdateIfrsloansinfo(HttpRequestMessage request, [FromBody]IfrsLoansInfo IfrsloansinfoModel) {
            return GetHttpResponse(request, () => {
                var Ifrsloansinfo = _IFRS9Service.UpdateIfrsLoansInfo(IfrsloansinfoModel);
                return request.CreateResponse<IfrsLoansInfo>(HttpStatusCode.OK, Ifrsloansinfo);
            });
        }


        [HttpPost]
        [Route("deleteIfrsLoansInfo")]
        public HttpResponseMessage DeleteIfrsLoansInfo(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                IfrsLoansInfo Ifrsloansinfo = _IFRS9Service.GetIfrsLoansInfo(Id);
                if (Ifrsloansinfo != null) {
                    _IFRS9Service.DeleteIfrsLoansInfo(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsLoansInfo data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableIfrsLoansInfo")]
        public HttpResponseMessage GetAvailableIfrsLoansInfos(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                IfrsLoansInfo[] Ifrsloansinfo = _IFRS9Service.GetAllIfrsLoansInfo().ToArray();
                return request.CreateResponse<IfrsLoansInfo[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
            });
        }

        [HttpGet]
        [Route("getIfrsLoansInfo/{Id}")]
        public HttpResponseMessage GetIfrsLoansInfo(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                IfrsLoansInfo Ifrsloansinfo = _IFRS9Service.GetIfrsLoansInfo(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsLoansInfo>(HttpStatusCode.OK, Ifrsloansinfo);
                return response;
            });
        }


        [HttpGet]
        [Route("getIfrsLoansInfobysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsLoansInfoBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                IfrsLoansInfo[] Ifrsloansinfo = _IFRS9Service.GetIfrsLoansInfoBySearch(searchParam);
                return request.CreateResponse<IfrsLoansInfo[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
            });
        }


        [HttpGet]
        [Route("availableIfrsLoansInfo/{defaultCount}")]
        public HttpResponseMessage GetAvailableIfrsLoansInfo(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    IfrsLoansInfo[] Ifrsloansinfo = _IFRS9Service.GetIfrsLoansInfos(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LoansInfo");
                    return response;
                }
                else
                {
                    IfrsLoansInfo[] Ifrsloansinfo = _IFRS9Service.GetIfrsLoansInfos(defaultCount, null).ToArray();
                    return request.CreateResponse<IfrsLoansInfo[]>(HttpStatusCode.OK, Ifrsloansinfo.ToArray());
                }
            });
        }

    }
}
