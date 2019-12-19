using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Client.Budget.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/policy")]
    [UsesDisposableService]
    public class PolicyApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PolicyApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatepolicy")]
        public HttpResponseMessage UpdatePolicy(HttpRequestMessage request, [FromBody]Policy policyModel)
        {
            return GetHttpResponse(request, () =>
            {
                var policy = _CoreService.UpdatePolicy(policyModel);

                return request.CreateResponse<Policy>(HttpStatusCode.OK, policy);
            });
        }

        [HttpPost]
        [Route("deletepolicy")]
        public HttpResponseMessage DeletePolicy(HttpRequestMessage request, [FromBody]int policyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Policy policy = _CoreService.GetPolicy(policyId);

                if (policy != null)
                {
                    _CoreService.DeletePolicy(policyId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No policy found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getpolicy/{policyId}")]
        public HttpResponseMessage GetPolicy(HttpRequestMessage request, int policyId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Policy policy = _CoreService.GetPolicy(policyId);

                // notice no need to create a seperate model object since Policy entity will do just fine
                response = request.CreateResponse<Policy>(HttpStatusCode.OK, policy);

                return response;
            });
        }

        [HttpGet]
        [Route("availablepolicies")]
        public HttpResponseMessage GetAvailablePolicys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PolicyData[] policys = _CoreService.GetAllPolicy();

                return request.CreateResponse<PolicyData[]>(HttpStatusCode.OK, policys);
            });
        }
    }
}
