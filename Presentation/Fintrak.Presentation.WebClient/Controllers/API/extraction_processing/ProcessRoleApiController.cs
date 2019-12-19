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
    [RoutePrefix("api/processrole")]
    [UsesDisposableService]
    public class ProcessRoleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ProcessRoleApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateprocessrole")]
        public HttpResponseMessage UpdateProcessRole(HttpRequestMessage request, [FromBody]ProcessRole processroleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var processrole = _ExtractionProcessService.UpdateProcessRole(processroleModel);

                return request.CreateResponse<ProcessRole>(HttpStatusCode.OK, processrole);
            });
        }

        [HttpPost]
        [Route("deleteprocessrole")]
        public HttpResponseMessage DeleteProcessRole(HttpRequestMessage request, [FromBody]int processroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ProcessRole processrole = _ExtractionProcessService.GetProcessRole(processroleId);

                if (processrole != null)
                {
                    _ExtractionProcessService.DeleteProcessRole(processroleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No processrole found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getprocessrole/{processroleId}")]
        public HttpResponseMessage GetProcessRole(HttpRequestMessage request,int processroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ProcessRole processrole = _ExtractionProcessService.GetProcessRole(processroleId);

                // notice no need to create a seperate model object since ProcessRole entity will do just fine
                response = request.CreateResponse<ProcessRole>(HttpStatusCode.OK, processrole);

                return response;
            });
        }

        [HttpGet]
        [Route("availableprocessroles")]
        public HttpResponseMessage GetAvailableProcessRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ProcessRoleData[] processroles = _ExtractionProcessService.GetProcessRoles();

                return request.CreateResponse<ProcessRoleData[]>(HttpStatusCode.OK, processroles);
            });
        }
    }
}
