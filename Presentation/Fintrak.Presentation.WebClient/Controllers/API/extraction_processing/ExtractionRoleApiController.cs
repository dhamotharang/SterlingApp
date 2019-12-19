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
    [RoutePrefix("api/extractionrole")]
    [UsesDisposableService]
    public class ExtractionRoleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExtractionRoleApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateextractionrole")]
        public HttpResponseMessage UpdateExtractionRole(HttpRequestMessage request, [FromBody]ExtractionRole extractionroleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var extractionrole = _ExtractionProcessService.UpdateExtractionRole(extractionroleModel);

                return request.CreateResponse<ExtractionRole>(HttpStatusCode.OK, extractionrole);
            });
        }

        [HttpPost]
        [Route("deleteextractionrole")]
        public HttpResponseMessage DeleteExtractionRole(HttpRequestMessage request, [FromBody]int extractionroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExtractionRole extractionrole = _ExtractionProcessService.GetExtractionRole(extractionroleId);

                if (extractionrole != null)
                {
                    _ExtractionProcessService.DeleteExtractionRole(extractionroleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No extractionrole found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getextractionrole/{extractionroleId}")]
        public HttpResponseMessage GetExtractionRole(HttpRequestMessage request,int extractionroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExtractionRole extractionrole = _ExtractionProcessService.GetExtractionRole(extractionroleId);

                // notice no need to create a seperate model object since ExtractionRole entity will do just fine
                response = request.CreateResponse<ExtractionRole>(HttpStatusCode.OK, extractionrole);

                return response;
            });
        }

        [HttpGet]
        [Route("availableextractionroles")]
        public HttpResponseMessage GetAvailableExtractionRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExtractionRoleData[] extractionroles = _ExtractionProcessService.GetExtractionRoles();

                return request.CreateResponse<ExtractionRoleData[]>(HttpStatusCode.OK, extractionroles);
            });
        }
    }
}
