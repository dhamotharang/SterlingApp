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
    [RoutePrefix("api/companymodule")]
    [UsesDisposableService]
    public class CompanyModuleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CompanyModuleApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecompanyModule")]
        public HttpResponseMessage UpdateCompanyModule(HttpRequestMessage request, [FromBody]CompanyModule companyModuleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var companyModule = _CoreService.UpdateCompanyModule(companyModuleModel);

                return request.CreateResponse<CompanyModule>(HttpStatusCode.OK, companyModule);
            });
        }

        [HttpPost]
        [Route("deletecompanymodule")]
        public HttpResponseMessage DeleteCompanyModule(HttpRequestMessage request, [FromBody]int companyModuleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CompanyModule companyModule = _CoreService.GetCompanyModule(companyModuleId);

                if (companyModule != null)
                {
                    _CoreService.DeleteCompanyModule(companyModuleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No companyModule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcompanyModule/{companyModuleId}")]
        public HttpResponseMessage GetCompanyModule(HttpRequestMessage request, int companyModuleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CompanyModule companyModule = _CoreService.GetCompanyModule(companyModuleId);

                // notice no need to create a seperate model object since CompanyModule entity will do just fine
                response = request.CreateResponse<CompanyModule>(HttpStatusCode.OK, companyModule);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecompanyModule")]
        public HttpResponseMessage GetAvailableCompanyModules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CompanyModuleData[] companyModule = _CoreService.GetCompanyModules();

                return request.CreateResponse<CompanyModuleData[]>(HttpStatusCode.OK, companyModule);
            });
        }
    }
}
