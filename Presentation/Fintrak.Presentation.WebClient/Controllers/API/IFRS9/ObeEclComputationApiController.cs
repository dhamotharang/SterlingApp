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
    [RoutePrefix("api/ObeEclComputation")]
    [UsesDisposableService]
    public class ObeEclComputationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ObeEclComputationApiController(IIFRS9Service ifrs9service)
        {
            _IFRS9Service = ifrs9service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateObeEclComputation")]
        public HttpResponseMessage Updateobeeclcomputation(HttpRequestMessage request, [FromBody]ObeEclComputation obeeclcomputationModel)
        {
            return GetHttpResponse(request, () => {
                var obeeclcomputation = _IFRS9Service.UpdateObeEclComputation(obeeclcomputationModel);
                return request.CreateResponse<ObeEclComputation>(HttpStatusCode.OK, obeeclcomputation);
            });
        }


        [HttpPost]
        [Route("deleteObeEclComputation")]
        public HttpResponseMessage DeleteObeEclComputation(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                ObeEclComputation obeeclcomputation = _IFRS9Service.GetObeEclComputation(Id);
                if (obeeclcomputation != null)
                {
                    _IFRS9Service.DeleteObeEclComputation(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ObeEclComputation data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableObeEclComputation")]
        public HttpResponseMessage GetAvailableObeEclComputations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () => {
                ObeEclComputation[] obeeclcomputation = _IFRS9Service.GetAllObeEclComputation().ToArray();
                return request.CreateResponse<ObeEclComputation[]>(HttpStatusCode.OK, obeeclcomputation.ToArray());
            });
        }

        [HttpGet]
        [Route("getObeEclComputation/{Id}")]
        public HttpResponseMessage GetObeEclComputation(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                ObeEclComputation obeeclcomputation = _IFRS9Service.GetObeEclComputation(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<ObeEclComputation>(HttpStatusCode.OK, obeeclcomputation);
                return response;
            });
        }


        [HttpGet]
        [Route("getObeEclComputationbysearch/{searchParam}")]
        public HttpResponseMessage GetObeEclComputationBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                if (searchParam.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    ObeEclComputation[] loanseclcomputationresult = _IFRS9Service.GetObeEclComputationBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "OBE_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    ObeEclComputation[] obeeclcomputation = _IFRS9Service.GetObeEclComputationBySearch(searchParam, null);
                    return request.CreateResponse(HttpStatusCode.OK, obeeclcomputation.ToArray());
                }
            });
        }


        [HttpGet]
        [Route("availableObeEclComputation/{defaultCount}")]
        public HttpResponseMessage GetAvailableObeEclComputation(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () => {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    ObeEclComputation[] obeeclcomputation = _IFRS9Service.GetObeEclComputations(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "OBE_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    ObeEclComputation[] obeeclcomputation = _IFRS9Service.GetObeEclComputations(defaultCount, null).ToArray();
                    return request.CreateResponse<ObeEclComputation[]>(HttpStatusCode.OK, obeeclcomputation.ToArray());
                }
            });
        }

    }
}
