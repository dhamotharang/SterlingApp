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
    [RoutePrefix("api/userrole")]
    [UsesDisposableService]
    public class UserRoleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UserRoleApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateuserrole")]
        public HttpResponseMessage UpdateUserSetupRole(HttpRequestMessage request, [FromBody]UserRole userroleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var userrole = _CoreService.UpdateUserRole(userroleModel);

                return request.CreateResponse<UserRole>(HttpStatusCode.OK, userrole);
            });
        }

        [HttpPost]
        [Route("deleteuserrole")]
        public HttpResponseMessage DeleteUserSetupRole(HttpRequestMessage request, [FromBody]int userroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                UserRole userrole = _CoreService.GetUserRole(userroleId);

                if (userrole != null)
                {
                    _CoreService.DeleteUserRole(userroleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No userrole found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getuserrole/{userroleId}")]
        public HttpResponseMessage GetUserSetupRole(HttpRequestMessage request,int userroleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                UserRole userrole = _CoreService.GetUserRole(userroleId);

                // notice no need to create a seperate model object since UserSetupRole entity will do just fine
                response = request.CreateResponse<UserRole>(HttpStatusCode.OK, userrole);

                return response;
            });
        }

        [HttpGet]
        [Route("availableuserroles")]
        public HttpResponseMessage GetAvailableUserSetupRoles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UserRole[] userroles = _CoreService.GetAllUserRoles();

                return request.CreateResponse<UserRole[]>(HttpStatusCode.OK, userroles);
            });
        }

        [HttpGet]
        [Route("getuserrolebylogin")]
        public HttpResponseMessage GetUserRoleByLogin(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UserRoleData[] userroles = _CoreService.GetUserRoleByLoginID(User.Identity.Name);

                return request.CreateResponse<UserRoleData[]>(HttpStatusCode.OK, userroles);
            });
        }
    }
}
