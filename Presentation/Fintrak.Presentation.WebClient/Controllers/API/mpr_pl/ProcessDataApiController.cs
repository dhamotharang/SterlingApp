using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/processdata")]
    [UsesDisposableService]
    public class ProcessDataApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProcessDataApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateprocessData")]
        public HttpResponseMessage UpdateProcessData(HttpRequestMessage request, [FromBody]ProcessData processDataModel)
        {
            return GetHttpResponse(request, () =>
            {
                var processData = _MPRPLService.UpdateProcessData(processDataModel);

                return request.CreateResponse<ProcessData>(HttpStatusCode.OK, processData);
            });
        }

        [HttpPost]
        [Route("deleteprocessData")]
        public HttpResponseMessage DeleteProcessData(HttpRequestMessage request, [FromBody]int processDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProcessData processData = _MPRPLService.GetProcessData(processDataId);

                if (processData != null)
                {
                    _MPRPLService.DeleteProcessData(processDataId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No processData found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getprocessData/{processDataId}")]
        public HttpResponseMessage GetProcessData(HttpRequestMessage request, int processDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProcessData processData = _MPRPLService.GetProcessData(processDataId);

                // notice no need to create a seperate model object since ProcessData entity will do just fine
                response = request.CreateResponse<ProcessData>(HttpStatusCode.OK, processData);

                return response;
            });
        }

        [HttpGet]
        [Route("availableprocessData")]
        public HttpResponseMessage GetAvailableProcessData(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProcessData[] processData = _MPRPLService.GetAllProcessData();

                return request.CreateResponse<ProcessData[]>(HttpStatusCode.OK, processData);
            });
        }

    }
}