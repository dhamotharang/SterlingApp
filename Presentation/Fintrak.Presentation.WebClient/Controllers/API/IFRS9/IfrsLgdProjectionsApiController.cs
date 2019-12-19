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
    [RoutePrefix("api/IfrsLgdProjections")]
    [UsesDisposableService]
    public class IfrsLgdProjectionsApiController : ApiControllerBase {
        [ImportingConstructor]
        public IfrsLgdProjectionsApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsLgdProjections")]
        public HttpResponseMessage UpdateIfrslgdprojections(HttpRequestMessage request, [FromBody]IfrsLgdProjections IfrslgdprojectionsModel) {
            return GetHttpResponse(request, () => {
                var Ifrslgdprojections = _IFRS9Service.UpdateIfrsLgdProjections(IfrslgdprojectionsModel);
                return request.CreateResponse<IfrsLgdProjections>(HttpStatusCode.OK, Ifrslgdprojections);
            });
        }


       /* [HttpPost]
        [Route("deleteIfrsLgdProjections")]
        public HttpResponseMessage DeleteIfrsLgdProjections(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                IfrsLgdProjections Ifrslgdprojections = _IFRS9Service.GetIfrsLgdProjections(Id);
                if (Ifrslgdprojections != null) {
                    _IFRS9Service.DeleteIfrsLgdProjections(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsLgdProjections data found under that ID.");
                return response;
            });
        } */


        [HttpGet]
        [Route("availableIfrsLgdProjections")]
        public HttpResponseMessage GetAvailableIfrsLgdProjections(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                IfrsLgdProjections[] Ifrslgdprojections = _IFRS9Service.GetAllIfrsLgdProjections().ToArray();
                return request.CreateResponse<IfrsLgdProjections[]>(HttpStatusCode.OK, Ifrslgdprojections.ToArray());
            });
        }

        /*[HttpGet]
        [Route("getIfrsLgdProjections/{Id}")]
        public HttpResponseMessage GetIfrsLgdProjections(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                IfrsLgdProjections Ifrslgdprojections = _IFRS9Service.GetIfrsLgdProjections(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsLgdProjections>(HttpStatusCode.OK, Ifrslgdprojections);
                return response;
            });
        } */


        [HttpGet]
        [Route("getIfrsLgdProjectionsbysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsLgdProjectionsBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                IfrsLgdProjections[] Ifrslgdprojections = _IFRS9Service.GetIfrsLgdProjectionsBySearch(searchParam);
                return request.CreateResponse<IfrsLgdProjections[]>(HttpStatusCode.OK, Ifrslgdprojections.ToArray());
            });
        }


        [HttpGet]
        [Route("availableIfrsLgdProjections/{defaultCount}")]
        public HttpResponseMessage GetAvailableIfrsLgdProjections(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    IfrsLgdProjections[] Ifrslgdprojections = _IFRS9Service.GetIfrsLgdProjections(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path,"LGDProjection");
                    return response;
                }
                else
                {
                    IfrsLgdProjections[] Ifrslgdprojections = _IFRS9Service.GetIfrsLgdProjections(defaultCount, null).ToArray();
                    return request.CreateResponse<IfrsLgdProjections[]>(HttpStatusCode.OK, Ifrslgdprojections.ToArray());
                }
            });
        }

    }
}
