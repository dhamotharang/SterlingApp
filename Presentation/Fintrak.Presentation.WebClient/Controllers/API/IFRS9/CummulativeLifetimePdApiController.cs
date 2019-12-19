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
    [RoutePrefix("api/cummulativelifetimepd")]
    [UsesDisposableService]
    public class CummulativeLifetimePdApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CummulativeLifetimePdApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecummulativelifetimepd")]
        public HttpResponseMessage UpdateCummulativeLifetimePd(HttpRequestMessage request, [FromBody]CummulativeLifetimePd cummulativelifetimepdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cummulativelifetimepd = _IFRS9Service.UpdateCummulativeLifetimePd(cummulativelifetimepdModel);

                return request.CreateResponse<CummulativeLifetimePd>(HttpStatusCode.OK, cummulativelifetimepd);
            });
        }

        [HttpPost]
        [Route("deletecummulativelifetimepd")]
        public HttpResponseMessage DeleteCummulativeLifetimePd(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CummulativeLifetimePd cummulativelifetimepd = _IFRS9Service.GetCummulativeLifetimePd(ID);

                if (cummulativelifetimepd != null)
                {
                    _IFRS9Service.DeleteCummulativeLifetimePd(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CummulativeLifetimePd found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcummulativelifetimepd/{ID}")]
        public HttpResponseMessage GetCummulativeLifetimePd(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CummulativeLifetimePd cummulativelifetimepd = _IFRS9Service.GetCummulativeLifetimePd(ID);

                // notice no need to create a seperate model object since CummulativeLifetimePd entity will do just fine
                response = request.CreateResponse<CummulativeLifetimePd>(HttpStatusCode.OK, cummulativelifetimepd);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecummulativelifetimepds/{defaultCount}")]
        public HttpResponseMessage GetAvailableCummulativeLifetimePds(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    CummulativeLifetimePd[] cummulativelifetimepds = _IFRS9Service.GetCummulativeLifetimePds(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Cumulative%20Lifetime%20PDs.zip");
                    return response;
                }
                else
                {
                    CummulativeLifetimePd[] cummulativelifetimepds = _IFRS9Service.GetCummulativeLifetimePds(defaultCount, null);
                    return request.CreateResponse<CummulativeLifetimePd[]>(HttpStatusCode.OK, cummulativelifetimepds);
                }
            });
        }



        [HttpGet]
        [Route("getCummulativeLifetimePdbysearch/{searchParam}")]
        public HttpResponseMessage GetCummulativeLifetimePdBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CummulativeLifetimePd[] cummulativelifetimepds = _IFRS9Service.GetCummulativeLifetimePdBySearch(searchParam);
                return request.CreateResponse<CummulativeLifetimePd[]>(HttpStatusCode.OK, cummulativelifetimepds.ToArray());
            });
        }





    }
}
