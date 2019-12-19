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
    [RoutePrefix("api/uploadrole")]
    [UsesDisposableService]
    public class UploadRoleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UploadRoleApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateuploadrole")]
        public HttpResponseMessage UpdateUploadRole(HttpRequestMessage request, [FromBody]UploadRole uploadRoleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var uploadRole = _ExtractionProcessService.UpdateUploadRole(uploadRoleModel);

                return request.CreateResponse<UploadRole>(HttpStatusCode.OK, uploadRole);
            });
        }

        [HttpPost]
        [Route("deleteuploadrole")]
        public HttpResponseMessage DeleteUploadRole(HttpRequestMessage request, [FromBody]int uploadRoleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                UploadRole uploadRole = _ExtractionProcessService.GetUploadRole(uploadRoleId);

                if (uploadRole != null)
                {
                    _ExtractionProcessService.DeleteUploadRole(uploadRoleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No uploadrole found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getuploadrole/{uploadroleId}")]
        public HttpResponseMessage GetUploadRole(HttpRequestMessage request,int uploadRoleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UploadRole uploadRole = _ExtractionProcessService.GetUploadRole(uploadRoleId);

                // notice no need to create a seperate model object since UploadRole entity will do just fine
                response = request.CreateResponse<UploadRole>(HttpStatusCode.OK, uploadRole);

                return response;
            });
        }

        [HttpGet]
        [Route("availableuploadroles")]
        public HttpResponseMessage GetAvailableUploadRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UploadRoleData[] uploadRoles = _ExtractionProcessService.GetUploadRoles();

                return request.CreateResponse<UploadRoleData[]>(HttpStatusCode.OK, uploadRoles);
            });
        }
    }
}
