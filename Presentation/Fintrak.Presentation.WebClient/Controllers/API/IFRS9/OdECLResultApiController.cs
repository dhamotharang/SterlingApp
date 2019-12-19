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
    [RoutePrefix("api/odeclresult")]
    [UsesDisposableService]
    public class OdECLResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OdECLResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateodeclresult")]
        public HttpResponseMessage UpdateOdECLResult(HttpRequestMessage request, [FromBody]OdECLResult odeclresultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var odeclresult = _IFRS9Service.UpdateOdECLResult(odeclresultModel);

                return request.CreateResponse<OdECLResult>(HttpStatusCode.OK, odeclresult);
            });
        }

        [HttpPost]
        [Route("deleteodeclresult")]
        public HttpResponseMessage DeleteOdECLResult(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OdECLResult odeclresult = _IFRS9Service.GetOdECLResult(ID);

                if (odeclresult != null)
                {
                    _IFRS9Service.DeleteOdECLResult(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OdECLResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getodeclresult/{ID}")]
        public HttpResponseMessage GetOdECLResult(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OdECLResult odeclresult = _IFRS9Service.GetOdECLResult(ID);

                // notice no need to create a seperate model object since OdECLResult entity will do just fine
                response = request.CreateResponse<OdECLResult>(HttpStatusCode.OK, odeclresult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableodeclresults/{defaultCount}")]
        public HttpResponseMessage GetAvailableOdECLResults(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    OdECLResult[] bondseclcomputationresults = _IFRS9Service.GetOdECLResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Overdraft_ECL_Summary.zip");
                    return response;
                }
                else
                {
                    OdECLResult[] bondseclcomputationresults = _IFRS9Service.GetOdECLResults(defaultCount, null);
                    return request.CreateResponse<OdECLResult[]>(HttpStatusCode.OK, bondseclcomputationresults);
                }
            });
        }



        [HttpGet]
        [Route("getOdECLResultbysearch/{searchParam}")]
        public HttpResponseMessage GetOdECLResultBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                OdECLResult[] odeclresults = _IFRS9Service.GetOdECLResultBySearch(searchParam);
                return request.CreateResponse<OdECLResult[]>(HttpStatusCode.OK, odeclresults.ToArray());
            });
        }





    }
}
