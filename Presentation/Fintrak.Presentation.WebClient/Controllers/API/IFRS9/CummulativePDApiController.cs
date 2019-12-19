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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cummulativepd")]
    [UsesDisposableService]
    public class CummulativePDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CummulativePDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecummulativepd")]
        public HttpResponseMessage UpdateCummulativePD(HttpRequestMessage request, [FromBody]CummulativePD cummulativepdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cummulativepd = _IFRS9Service.UpdateCummulativePD(cummulativepdModel);

                return request.CreateResponse<CummulativePD>(HttpStatusCode.OK, cummulativepd);
            });
        }

        [HttpPost]
        [Route("deletecummulativepd")]
        public HttpResponseMessage DeleteCummulativePD(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CummulativePD cummulativepd = _IFRS9Service.GetCummulativePD(ID);

                if (cummulativepd != null)
                {
                    _IFRS9Service.DeleteCummulativePD(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CummulativePD found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcummulativepd/{ID}")]
        public HttpResponseMessage GetCummulativePD(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CummulativePD cummulativepd = _IFRS9Service.GetCummulativePD(ID);

                // notice no need to create a seperate model object since CummulativePD entity will do just fine
                response = request.CreateResponse<CummulativePD>(HttpStatusCode.OK, cummulativepd);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecummulativepds/{defaultCount}")]
        public HttpResponseMessage GetAvailableCummulativePDs(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    CummulativePD[] cummulativepds = _IFRS9Service.GetCummulativePDs(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Cumulative%20PDs.zip");
                    return response;
                }
                else
                {
                    CummulativePD[] cummulativepds = _IFRS9Service.GetCummulativePDs(defaultCount, null);
                    return request.CreateResponse<CummulativePD[]>(HttpStatusCode.OK, cummulativepds);
                }
            });
        }



        [HttpGet]
        [Route("getCummulativePDbysearch/{searchParam}")]
        public HttpResponseMessage GetCummulativePDBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CummulativePD[] cummulativepds = _IFRS9Service.GetCummulativePDBySearch(searchParam);
                return request.CreateResponse<CummulativePD[]>(HttpStatusCode.OK, cummulativepds.ToArray());
            });
        }





    }
}
