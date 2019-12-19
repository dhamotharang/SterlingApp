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
    [RoutePrefix("api/ifrsbondlgd")]
    [UsesDisposableService]
    public class IfrsBondLGDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsBondLGDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsbondlgd")]
        public HttpResponseMessage UpdateIfrsBondLGD(HttpRequestMessage request, [FromBody]IfrsBondLGD ifrsbondlgdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsbondlgd = _IFRS9Service.UpdateIfrsBondLGD(ifrsbondlgdModel);

                return request.CreateResponse<IfrsBondLGD>(HttpStatusCode.OK, ifrsbondlgd);
            });
        }

        [HttpPost]
        [Route("deleteifrsbondlgd")]
        public HttpResponseMessage DeleteIfrsBondLGD(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
               IfrsBondLGD ifrsbondlgd = _IFRS9Service.GetIfrsBondLGD(Id);

                if (ifrsbondlgd != null)
                {
                    _IFRS9Service.DeleteIfrsBondLGD(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsbondlgd found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsbondlgd/{Id}")]
        public HttpResponseMessage GetIfrsBondLGD(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

               IfrsBondLGD ifrsbondlgd = _IFRS9Service.GetIfrsBondLGD(Id);

                // notice no need to create a seperate model object since IfrsBondLGDr entity will do just fine
                response = request.CreateResponse<IfrsBondLGD>(HttpStatusCode.OK, ifrsbondlgd);

                return response;
            });
        }



    [Route("getRecordByRefNo/{RefNo}")]
    public HttpResponseMessage GetRecordByRefNo(HttpRequestMessage request, string RefNo)
    {
      return GetHttpResponse(request, () =>
      {
        HttpResponseMessage response = null;

        IfrsBondLGD[] ifrsbondlgd = _IFRS9Service.GetRecordByRefNo(RefNo);

        // notice no need to create a seperate model object since IfrsBondLGDr entity will do just fine
        response = request.CreateResponse<IfrsBondLGD[]>(HttpStatusCode.OK, ifrsbondlgd);

        return response;
      });
    }



    [HttpGet]
    [Route("getallifrsbondlgd/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsBondLGD(HttpRequestMessage request,int defaultCount)
    {
      return GetHttpResponse(request, () =>
      {
          if (defaultCount == 0)
          {
              string path = HostingEnvironment.MapPath("~");
              IfrsBondLGD[] ifrsbondlgd = _IFRS9Service.GetAllIfrsBondLGD(defaultCount, path + "ExportedData\\").ToArray();
              var response = DownloadFileService.DownloadFile(path, "LGD%20Results%20-%20Bonds.zip");
              return response;
          }
          else
          {
              IfrsBondLGD[] ifrsbondlgd = _IFRS9Service.GetAllIfrsBondLGD(defaultCount, null);

              return request.CreateResponse<IfrsBondLGD[]>(HttpStatusCode.OK, ifrsbondlgd);
          }
      });
    }


  }
}
