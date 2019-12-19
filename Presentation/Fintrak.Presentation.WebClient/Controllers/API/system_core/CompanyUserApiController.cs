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
    [RoutePrefix("api/companyuser")]
    [UsesDisposableService]
    public class CompanyUserApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CompanyUserApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecompanyUser")]
        public HttpResponseMessage UpdateCompanyUser(HttpRequestMessage request, [FromBody]CompanyUser companyUserModel)
        {
            return GetHttpResponse(request, () =>
            {
                var companyUser = _CoreService.UpdateCompanyUser(companyUserModel);

                return request.CreateResponse<CompanyUser>(HttpStatusCode.OK, companyUser);
            });
        }

        [HttpPost]
        [Route("deletecompanyUser")]
        public HttpResponseMessage DeleteCompanyUser(HttpRequestMessage request, [FromBody]int companyUserId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CompanyUser companyUser = _CoreService.GetCompanyUser(companyUserId);

                if (companyUser != null)
                {
                    _CoreService.DeleteCompanyUser(companyUserId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No companyUser found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcompanyUser/{companyUserId}")]
        public HttpResponseMessage GetCompanyUser(HttpRequestMessage request, int companyUserId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CompanyUser companyUser = _CoreService.GetCompanyUser(companyUserId);

                // notice no need to create a seperate model object since CompanyUser entity will do just fine
                response = request.CreateResponse<CompanyUser>(HttpStatusCode.OK, companyUser);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecompanyUser")]
        public HttpResponseMessage GetAvailableCompanyUsers(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CompanyUser[] companyUser = _CoreService.GetAllCompanyUsers();

                return request.CreateResponse<CompanyUser[]>(HttpStatusCode.OK, companyUser);
            });
        }
    }
}
