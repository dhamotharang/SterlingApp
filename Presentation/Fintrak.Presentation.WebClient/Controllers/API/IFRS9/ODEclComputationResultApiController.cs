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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ODEclComputationResult")]
    [UsesDisposableService]
    public class ODEclComputationResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ODEclComputationResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateODEclComputationResult")]
        public HttpResponseMessage Updateodeclcomputationresult(HttpRequestMessage request, [FromBody]ODEclComputationResult odeclcomputationresultModel)
        {
            return GetHttpResponse(request, () => {
                var odeclcomputationresult = _IFRS9Service.UpdateODEclComputationResult(odeclcomputationresultModel);
                return request.CreateResponse<ODEclComputationResult>(HttpStatusCode.OK, odeclcomputationresult);
            });
        }


        [HttpPost]
        [Route("deleteODEclComputationResult")]
        public HttpResponseMessage DeleteODEclComputationResult(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                ODEclComputationResult odeclcomputationresult = _IFRS9Service.GetODEclComputationResult(Id);
                if (odeclcomputationresult != null)
                {
                    _IFRS9Service.DeleteODEclComputationResult(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ODEclComputationResult data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableODEclComputationResult")]
        public HttpResponseMessage GetAvailableODEclComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () => {
                ODEclComputationResult[] odeclcomputationresult = _IFRS9Service.GetAllODEclComputationResult().ToArray();
                return request.CreateResponse<ODEclComputationResult[]>(HttpStatusCode.OK, odeclcomputationresult.ToArray());
            });
        }

        [HttpGet]
        [Route("getODEclComputationResult/{Id}")]
        public HttpResponseMessage GetODEclComputationResult(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                ODEclComputationResult odeclcomputationresult = _IFRS9Service.GetODEclComputationResult(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<ODEclComputationResult>(HttpStatusCode.OK, odeclcomputationresult);
                return response;
            });
        }


        [HttpGet]
        [Route("getODEclComputationResultbysearch/{searchParam}")]
        public HttpResponseMessage GetODEclComputationResultBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                if (searchParam.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    ODEclComputationResult[] loanseclcomputationresult = _IFRS9Service.GetODEclComputationResultBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "OD_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    ODEclComputationResult[] odeclcomputationresult = _IFRS9Service.GetODEclComputationResultBySearch(searchParam, null);
                    return request.CreateResponse(HttpStatusCode.OK, odeclcomputationresult.ToArray());
                }
            });
        }


        [HttpGet]
        [Route("availableODEclComputationResult/{defaultCount}")]
        public HttpResponseMessage GetAvailableODEclComputationResult(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () => {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    ODEclComputationResult[] odeclcomputationresult = _IFRS9Service.GetODEclComputationResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "OD_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    ODEclComputationResult[] odeclcomputationresult = _IFRS9Service.GetODEclComputationResults(defaultCount, null).ToArray();
                    return request.CreateResponse<ODEclComputationResult[]>(HttpStatusCode.OK, odeclcomputationresult.ToArray());
                }
            });
        }

    }
}
