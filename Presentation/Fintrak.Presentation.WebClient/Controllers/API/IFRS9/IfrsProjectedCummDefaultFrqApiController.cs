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
    [RoutePrefix("api/ifrsprojectedcummdefault")]
    [UsesDisposableService]
    public class IfrsProjectedCummDefaultFrqApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsProjectedCummDefaultFrqApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

    
        [HttpPost]
        [Route("deleteifrsprojectedcummdefault")]
        public HttpResponseMessage DeleteIfrsProjectedCummDefaultFrq(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsProjectedCummDefaultFrq ifrsprojectedcummdefault = _IFRS9Service.GetIfrsProjectedCummDefaultFrq(Id);

                if (ifrsprojectedcummdefault != null)
                {
                    _IFRS9Service.DeleteIfrsProjectedCummDefaultFrq(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsprojectedcummdefault found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsprojectedcummdefault/{Id}")]
        public HttpResponseMessage GetIfrsProjectedCummDefaultFrq(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsProjectedCummDefaultFrq ifrsprojectedcummdefault = _IFRS9Service.GetIfrsProjectedCummDefaultFrq(Id);

                // notice no need to create a seperate model object since IfrsProjectedCummDefaultFrqr entity will do just fine
                response = request.CreateResponse<IfrsProjectedCummDefaultFrq>(HttpStatusCode.OK, ifrsprojectedcummdefault);

                return response;
            });
        }

   
    [HttpGet]
    [Route("getallifrsprojectedcummdefaults/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsProjectedCummDefaultFrq(HttpRequestMessage request, int defaultCount)
    {
      return GetHttpResponse(request, () =>
      {
          if (defaultCount == 0)
          {
              string path = HostingEnvironment.MapPath("~");
              IfrsProjectedCummDefaultFrq[] ifrsprojectedcummdefault = _IFRS9Service.GetAllIfrsProjectedCummDefaultFrq(defaultCount, path + "ExportedData\\").ToArray();
              var response = DownloadFileService.DownloadFile(path, "Projected%20Cumulative%20Defaulted%20Amount.zip");
              return response;
          }
          else
          {
              IfrsProjectedCummDefaultFrq[] ifrsprojectedcummdefault = _IFRS9Service.GetAllIfrsProjectedCummDefaultFrq(defaultCount, null);

              return request.CreateResponse<IfrsProjectedCummDefaultFrq[]>(HttpStatusCode.OK, ifrsprojectedcummdefault);
          }
      });
    }


  }
}
