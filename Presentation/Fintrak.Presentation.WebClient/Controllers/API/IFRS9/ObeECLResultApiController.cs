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
    [RoutePrefix("api/obeeclresult")]
    [UsesDisposableService]
    public class ObeECLResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ObeECLResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateobeeclresult")]
        public HttpResponseMessage UpdateObeECLResult(HttpRequestMessage request, [FromBody]ObeECLResult obeeclresultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var obeeclresult = _IFRS9Service.UpdateObeECLResult(obeeclresultModel);

                return request.CreateResponse<ObeECLResult>(HttpStatusCode.OK, obeeclresult);
            });
        }

        [HttpPost]
        [Route("deleteobeeclresult")]
        public HttpResponseMessage DeleteObeECLResult(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ObeECLResult obeeclresult = _IFRS9Service.GetObeECLResult(ID);

                if (obeeclresult != null)
                {
                    _IFRS9Service.DeleteObeECLResult(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ObeECLResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getobeeclresult/{ID}")]
        public HttpResponseMessage GetObeECLResult(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ObeECLResult obeeclresult = _IFRS9Service.GetObeECLResult(ID);

                // notice no need to create a seperate model object since ObeECLResult entity will do just fine
                response = request.CreateResponse<ObeECLResult>(HttpStatusCode.OK, obeeclresult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableobeeclresults/{defaultCount}")]
        public HttpResponseMessage GetAvailableObeECLResults(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    ObeECLResult[] bondseclcomputationresults = _IFRS9Service.GetObeECLResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "OBE_ECL_Summary.zip");
                    return response;
                }
                else
                {
                    ObeECLResult[] bondseclcomputationresults = _IFRS9Service.GetObeECLResults(defaultCount, null);
                    return request.CreateResponse<ObeECLResult[]>(HttpStatusCode.OK, bondseclcomputationresults);
                }
            });
        }



        [HttpGet]
        [Route("getObeECLResultbysearch/{searchParam}")]
        public HttpResponseMessage GetObeECLResultBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                ObeECLResult[] obeeclresults = _IFRS9Service.GetObeECLResultBySearch(searchParam);
                return request.CreateResponse<ObeECLResult[]>(HttpStatusCode.OK, obeeclresults.ToArray());
            });
        }





    }
}
