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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/impairmentoverride")]
    [UsesDisposableService]
    public class ImpairmentOverrideApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ImpairmentOverrideApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updateimpairmentoverride")]
        public HttpResponseMessage UpdateImpairmentOverride(HttpRequestMessage request, [FromBody]ImpairmentOverride impairmentOverrideModel)
        {
            return GetHttpResponse(request, () =>
            {
                var impairmentOverride = _LoanService.UpdateImpairmentOverride(impairmentOverrideModel);

                return request.CreateResponse<ImpairmentOverride>(HttpStatusCode.OK, impairmentOverride);
            });
        }

        [HttpPost]
        [Route("deleteimpairmentoverride")]
        public HttpResponseMessage DeleteImpairmentOverride(HttpRequestMessage request, [FromBody]int impairmentOverrideId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ImpairmentOverride impairmentOverride = _LoanService.GetImpairmentOverride(impairmentOverrideId);

                if (impairmentOverride != null)
                {
                    _LoanService.DeleteImpairmentOverride(impairmentOverrideId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No impairmentoverride found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getimpairmentoverride/{impairmentOverrideId}")]
        public HttpResponseMessage GetImpairmentOverride(HttpRequestMessage request,int impairmentOverrideId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ImpairmentOverride impairmentOverride = _LoanService.GetImpairmentOverride(impairmentOverrideId);

                // notice no need to create a seperate model object since ImpairmentOverride entity will do just fine
                response = request.CreateResponse<ImpairmentOverride>(HttpStatusCode.OK, impairmentOverride);

                return response;
            });
        }

        [HttpGet]
        [Route("availableimpairmentoverrides")]
        public HttpResponseMessage GetAvailableImpairmentOverrides(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ImpairmentOverrideData[] impairmentOverrides = _LoanService.GetAllImpairmentOverrides();

                return request.CreateResponse<ImpairmentOverrideData[]>(HttpStatusCode.OK, impairmentOverrides);
            });
        }
    }
}
