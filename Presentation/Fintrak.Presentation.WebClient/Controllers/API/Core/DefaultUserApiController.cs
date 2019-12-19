using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/defaultuser")]
    [UsesDisposableService]
    public class DefaultUserApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DefaultUserApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatedefaultuser")]
        public HttpResponseMessage UpdateDefaultUser(HttpRequestMessage request, [FromBody]DefaultUser defaultUserModel)
        {
            return GetHttpResponse(request, () =>
            {
                var defaultUser = _CoreService.UpdateDefaultUser(defaultUserModel);

                return request.CreateResponse<DefaultUser>(HttpStatusCode.OK, defaultUser);
            });
        }

        [HttpPost]
        [Route("deletedefaultuser")]
        public HttpResponseMessage DeleteDefaultUser(HttpRequestMessage request, [FromBody]int defaultUserId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                DefaultUser defaultUser = _CoreService.GetDefaultUser(defaultUserId);

                if (defaultUser != null)
                {
                    _CoreService.DeleteDefaultUser(defaultUserId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No defaultUser found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getdefaultUser/{defaultuserId}")]
        public HttpResponseMessage GetDefaultUser(HttpRequestMessage request,int defaultUserId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DefaultUser defaultUser = _CoreService.GetDefaultUser(defaultUserId);

                // notice no need to create a seperate model object since DefaultUser entity will do just fine
                response = request.CreateResponse<DefaultUser>(HttpStatusCode.OK, defaultUser);

                return response;
            });
        }

        [HttpGet]
        [Route("availabledefaultusers")]
        public HttpResponseMessage GetAvailableDefaultUsers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DefaultUserData[] defaultusers = _CoreService.GetAllDefaultUsers();

                return request.CreateResponse<DefaultUserData[]>(HttpStatusCode.OK, defaultusers);
            });
        }

        [HttpGet]
        [Route("getdefaultUserebysolution/{solutionId}")]
        public HttpResponseMessage GetDefaultUsereByCompany(HttpRequestMessage request, int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                DefaultUserData[] defaultUseres = _CoreService.GetAllDefaultUsers().Where(c => c.SolutionId == solutionId).ToArray();

                return request.CreateResponse<DefaultUserData[]>(HttpStatusCode.OK, defaultUseres);
            });
        }
    }
}
