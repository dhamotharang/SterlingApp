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
    [RoutePrefix("api/IfrsPDProjection")]
    [UsesDisposableService]
    public class IfrsPDProjectionApiController : ApiControllerBase {
        [ImportingConstructor]
        public IfrsPDProjectionApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsPDProjection")]
        public HttpResponseMessage Updateifrspdprojection(HttpRequestMessage request, [FromBody]IfrsPDProjection ifrspdprojectionModel) {
            return GetHttpResponse(request, () => {
                var ifrspdprojection = _IFRS9Service.UpdateIfrsPDProjection(ifrspdprojectionModel);
                return request.CreateResponse<IfrsPDProjection>(HttpStatusCode.OK, ifrspdprojection);
            });
        }


        [HttpPost]
        [Route("deleteIfrsPDProjection")]
        public HttpResponseMessage DeleteIfrsPDProjection(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                IfrsPDProjection ifrspdprojection = _IFRS9Service.GetIfrsPDProjection(Id);
                if (ifrspdprojection != null) {
                    _IFRS9Service.DeleteIfrsPDProjection(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsPDProjection data found under that ID.");
                return response;
            });
        }





       /* [HttpGet]
        [Route("getAllSectorsForCCF")]
        public HttpResponseMessage GetAllSectorsForCCF(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Sector[] IfrsSectorCCF = _IFRS9Service.GetAllSectorsForCCF();

                // notice no need to create a seperate model object since IfrsSectorCCF entity will do just fine
                response = request.CreateResponse<Sector[]>(HttpStatusCode.OK, IfrsSectorCCF);

                return response;
            });
        }
        */


        [HttpGet]
        [Route("availableIfrsPDProjection")]
        public HttpResponseMessage GetAvailableIfrsPDProjections(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                IfrsPDProjection[] ifrspdprojection = _IFRS9Service.GetAllIfrsPDProjection().ToArray();
                return request.CreateResponse<IfrsPDProjection[]>(HttpStatusCode.OK, ifrspdprojection.ToArray());
            });
        }

        [HttpGet]
        [Route("getIfrsPDProjection/{Id}")]
        public HttpResponseMessage GetIfrsPDProjection(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                IfrsPDProjection ifrspdprojection = _IFRS9Service.GetIfrsPDProjection(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IfrsPDProjection>(HttpStatusCode.OK, ifrspdprojection);
                return response;
            });
        }


        [HttpGet]
        [Route("getIfrsPDProjectionbysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsPDProjectionBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                IfrsPDProjection[] ifrspdprojection = _IFRS9Service.GetIfrsPDProjectionBySearch(searchParam);
                return request.CreateResponse<IfrsPDProjection[]>(HttpStatusCode.OK, ifrspdprojection.ToArray());
            });
        }


        [HttpGet]
        [Route("availableIfrsPDProjection/{defaultCount}")]
        public HttpResponseMessage GetAvailableIfrsPDProjection(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    IfrsPDProjection[] ifrspdprojection = _IFRS9Service.GetIfrsPDProjections(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "PDProjection");
                    return response;
                }
                else
                {
                    IfrsPDProjection[] ifrspdprojection = _IFRS9Service.GetIfrsPDProjections(defaultCount, null).ToArray();
                    return request.CreateResponse<IfrsPDProjection[]>(HttpStatusCode.OK, ifrspdprojection.ToArray());
                }
            });
        }

    }
}
