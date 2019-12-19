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
    [RoutePrefix("api/ifrsmonthlyforwardpdmacrovar")]
    [UsesDisposableService]
    public class IfrsMonthlyForwardPDMacroVarApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsMonthlyForwardPDMacroVarApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

   
        [HttpPost]
        [Route("deleteifrsmonthlyforwardpdmacrovar")]
        public HttpResponseMessage DeleteIfrsMonthlyForwardPDMacroVar(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsMonthlyForwardPDMacroVar ifrsmonthlyforwardpdmacrovar = _IFRS9Service.GetIfrsMonthlyForwardPDMacroVar(Id);

                if (ifrsmonthlyforwardpdmacrovar != null)
                {
                    _IFRS9Service.DeleteIfrsMonthlyForwardPDMacroVar(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsmonthlyforwardpdmacrovar found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsmonthlyforwardpdmacrovar/{Id}")]
        public HttpResponseMessage GetIfrsMonthlyForwardPDMacroVar(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsMonthlyForwardPDMacroVar ifrsmonthlyforwardpdmacrovar = _IFRS9Service.GetIfrsMonthlyForwardPDMacroVar(Id);

                // notice no need to create a seperate model object since IfrsMonthlyForwardPDMacroVarr entity will do just fine
                response = request.CreateResponse<IfrsMonthlyForwardPDMacroVar>(HttpStatusCode.OK, ifrsmonthlyforwardpdmacrovar);

                return response;
            });
        }

   
    [HttpGet]
    [Route("getallifrsmonthlyforwardpdmacrovars/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsMonthlyForwardPDMacroVar(HttpRequestMessage request, int defaultCount)
    {
      return GetHttpResponse(request, () =>
      {
          if (defaultCount == 0)
          {
              string path = HostingEnvironment.MapPath("~");
              IfrsMonthlyForwardPDMacroVar[] ifrsmonthlyforwardpdmacrovar = _IFRS9Service.GetAllIfrsMonthlyForwardPDMacroVar(defaultCount, path + "ExportedData\\").ToArray();
              var response = DownloadFileService.DownloadFile(path, "Monthly%20Forward%20PD%20with%20Macros.zip");
              return response;
          }
          else
          {
              IfrsMonthlyForwardPDMacroVar[] ifrsmonthlyforwardpdmacrovar = _IFRS9Service.GetAllIfrsMonthlyForwardPDMacroVar(defaultCount, null);

              return request.CreateResponse<IfrsMonthlyForwardPDMacroVar[]>(HttpStatusCode.OK, ifrsmonthlyforwardpdmacrovar);
          }
      });
    }


  }
}
