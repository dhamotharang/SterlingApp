using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/process")]
    [UsesDisposableService]
    public class ProcessApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProcessApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateprocess")]
        public HttpResponseMessage UpdateProcess(HttpRequestMessage request, [FromBody]Process processModel)
        {
            return GetHttpResponse(request, () =>
            {
                var process = _ExtractionProcessService.UpdateProcess(processModel);

                return request.CreateResponse<Process>(HttpStatusCode.OK, process);
            });
        }

        [HttpPost]
        [Route("deleteprocess")]
        public HttpResponseMessage DeleteProcess(HttpRequestMessage request, [FromBody]int processId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Process process = _ExtractionProcessService.GetProcess(processId);

                if (process != null)
                {
                    _ExtractionProcessService.DeleteProcess(processId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No process found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getprocess/{processId}")]
        public HttpResponseMessage GetProcess(HttpRequestMessage request,int processId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Process process = _ExtractionProcessService.GetProcess(processId);

                // notice no need to create a seperate model object since Process entity will do just fine
                response = request.CreateResponse<Process>(HttpStatusCode.OK, process);

                return response;
            });
        }

        [HttpGet]
        [Route("getprocesswithchildren/{processId}")]
        public HttpResponseMessage GetProcessWithChildren(HttpRequestMessage request, int processId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var processModel = new ProcessModel();

                processModel.Process = _ExtractionProcessService.GetProcess(processId);
                processModel.ProcessRoles = _ExtractionProcessService.GetProcessRoleByProcess(processId);

                // notice no need to create a seperate model object since Process entity will do just fine
                response = request.CreateResponse<ProcessModel>(HttpStatusCode.OK, processModel);

                return response;
            });
        }

        [HttpGet]
        [Route("getprocesses")]
        public HttpResponseMessage GetAvailableProcesss(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProcessData[] processs = _ExtractionProcessService.GetProcesses();

                return request.CreateResponse<ProcessData[]>(HttpStatusCode.OK, processs.OrderBy(c=> c.Position).ToArray());
            });
        }
    }
}
