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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/company")]
    [UsesDisposableService]
    public class CompanyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CompanyApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecompany")]
        public HttpResponseMessage UpdateCompany(HttpRequestMessage request, [FromBody]Company companyModel)
        {
            return GetHttpResponse(request, () =>
            {
                var company = _CoreService.UpdateCompany(companyModel);

                return request.CreateResponse<Company>(HttpStatusCode.OK, company);
            });
        }

        [HttpPost]
        [Route("deletecompany")]
        public HttpResponseMessage DeleteCompany(HttpRequestMessage request, [FromBody]int companyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Company company = _CoreService.GetCompany(companyId);

                if (company != null)
                {
                    _CoreService.DeleteCompany(companyId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No company found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcompany/{companyId}")]
        public HttpResponseMessage GetCompany(HttpRequestMessage request,int companyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Company company = _CoreService.GetCompany(companyId);

                // notice no need to create a seperate model object since Company entity will do just fine
                response = request.CreateResponse<Company>(HttpStatusCode.OK, company);

                return response;
            });
        }

        [HttpGet]
        [Route("getcompanywithchildrens/{companyId}")]
        public HttpResponseMessage GetCompanyWithChildrens(HttpRequestMessage request, int companyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var companyModel = new CompanyModel();

                Company company = _CoreService.GetCompany(companyId);
                //Branch[] branches = _CoreService.GetAllBranches().Where(c => c.CompanyId == companyId).ToArray();

                companyModel.Company = company;
                //companyModel.Branches = branches;

                // notice no need to create a seperate model object since Company entity will do just fine
                response = request.CreateResponse<CompanyModel>(HttpStatusCode.OK, companyModel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecompanies")]
        public HttpResponseMessage GetAvailableCompanies(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Company[] companies = _CoreService.GetAllCompanies();

                return request.CreateResponse<Company[]>(HttpStatusCode.OK, companies);
            });
        }
    }
}
