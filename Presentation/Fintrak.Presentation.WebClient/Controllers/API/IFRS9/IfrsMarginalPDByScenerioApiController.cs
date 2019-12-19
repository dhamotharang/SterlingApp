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
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsmarginalpdscenerio")]
    [UsesDisposableService]
    public class IfrsMarginalPDByScenerioApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsMarginalPDByScenerioApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

     

  

        [HttpGet]
        [Route("getifrsmarginalpdscenerio/{Id}")]
        public HttpResponseMessage GetIfrsMarginalPDByScenerio(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsMarginalPDByScenerio ifrsmarginalpdscenerio = _IFRS9Service.GetIfrsMarginalPDByScenerio(Id);

                // notice no need to create a seperate model object since IfrsMarginalPDByScenerior entity will do just fine
                response = request.CreateResponse<IfrsMarginalPDByScenerio>(HttpStatusCode.OK, ifrsmarginalpdscenerio);

                return response;
            });
        }

   
    [HttpGet]
    [Route("getallifrsmarginalpdscenerios/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsMarginalPDByScenerio(HttpRequestMessage request, int defaultCount)
    {
      return GetHttpResponse(request, () =>
      {
          if (defaultCount == 0)
          {
              string path = HostingEnvironment.MapPath("~");
              IfrsMarginalPDByScenerio[] ifrsmarginalpdscenerio = _IFRS9Service.GetAllIfrsMarginalPDByScenerio(defaultCount, path + "ExportedData\\").ToArray();
              var response = DownloadFileService.DownloadFile(path, "Marginal%20PD%20By%20Scenatio.zip");
              return response;
          }
          else
          {
              IfrsMarginalPDByScenerio[] ifrsmarginalpdscenerio = _IFRS9Service.GetAllIfrsMarginalPDByScenerio(defaultCount, null);

              return request.CreateResponse<IfrsMarginalPDByScenerio[]>(HttpStatusCode.OK, ifrsmarginalpdscenerio);
          }
      });
    }


  }
}
