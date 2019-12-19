using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/menurole")]
    [UsesDisposableService]
    public class MenuRoleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MenuRoleApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatemenurole")]
        public HttpResponseMessage UpdateMenuSetupRole(HttpRequestMessage request, [FromBody]MenuRole menuroleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var menurole = _CoreService.UpdateMenuRole(menuroleModel);

                return request.CreateResponse<MenuRole>(HttpStatusCode.OK, menurole);
            });
        }

        [HttpPost]
        [Route("deletemenurole")]
        public HttpResponseMessage DeleteMenuSetupRole(HttpRequestMessage request, [FromBody]int menuroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                MenuRole menurole = _CoreService.GetMenuRole(menuroleId);

                if (menurole != null)
                {
                    _CoreService.DeleteMenuRole(menuroleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No menurole found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmenurole/{menuroleId}")]
        public HttpResponseMessage GetMenuSetupRole(HttpRequestMessage request,int menuroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                MenuRole menurole = _CoreService.GetMenuRole(menuroleId);

                // notice no need to create a seperate model object since MenuSetupRole entity will do just fine
                response = request.CreateResponse<MenuRole>(HttpStatusCode.OK, menurole);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemenuroles")]
        public HttpResponseMessage GetAvailableMenuSetupRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                MenuRoleData[] menuroles = _CoreService.GetMenuRoles();

                return request.CreateResponse<MenuRoleData[]>(HttpStatusCode.OK, menuroles);
            });
        }
    }
}
