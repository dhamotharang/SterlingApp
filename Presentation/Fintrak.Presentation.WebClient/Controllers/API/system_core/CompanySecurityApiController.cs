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
    [RoutePrefix("api/companysecurity")]
    [UsesDisposableService]
    public class CompanySecurityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CompanySecurityApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatecompanysecurity")]
        public HttpResponseMessage UpdateCompanySecurity(HttpRequestMessage request, [FromBody]CompanySecurity companysecurityModel)
        {
            return GetHttpResponse(request, () =>
            {
                var companysecurity = _CoreService.UpdateCompanySecurity(companysecurityModel);

                return request.CreateResponse<CompanySecurity>(HttpStatusCode.OK, companysecurity);
            });
        }

        [HttpPost]
        [Route("deletecompanysecurity")]
        public HttpResponseMessage DeleteCompanySecurity(HttpRequestMessage request, [FromBody]int companysecurityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CompanySecurity companysecurity = _CoreService.GetCompanySecurity(companysecurityId);

                if (companysecurity != null)
                {
                    _CoreService.DeleteCompanySecurity(companysecurityId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No companysecurity found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcompanysecurity/{companysecurityId}")]
        public HttpResponseMessage GetCompanySecurity(HttpRequestMessage request, int companySecurityId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CompanySecurity companysecurity = _CoreService.GetCompanySecurity(companySecurityId);

                // notice no need to create a seperate model object since CompanySecurity entity will do just fine
                response = request.CreateResponse<CompanySecurity>(HttpStatusCode.OK, companysecurity);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecompanysecurity")]
        public HttpResponseMessage GetAvailableCompanySecuritys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CompanySecurity[] companysecurity = _CoreService.GetAllCompanySecuritys();

                return request.CreateResponse<CompanySecurity[]>(HttpStatusCode.OK, companysecurity);
            });
        }
    }
}
