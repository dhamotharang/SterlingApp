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
    [RoutePrefix("api/policylevel")]
    [UsesDisposableService]
    public class PolicyLevelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PolicyLevelApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatepolicyLevel")]
        public HttpResponseMessage UpdatePolicyLevel(HttpRequestMessage request, [FromBody]PolicyLevel policyLevelModel)
        {
            return GetHttpResponse(request, () =>
            {
                var policyLevel = _CoreService.UpdatePolicyLevel(policyLevelModel);

                return request.CreateResponse<PolicyLevel>(HttpStatusCode.OK, policyLevel);
            });
        }

        [HttpPost]
        [Route("deletepolicyLevel")]
        public HttpResponseMessage DeletePolicyLevel(HttpRequestMessage request, [FromBody]int policyLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PolicyLevel policyLevel = _CoreService.GetPolicyLevel(policyLevelId);

                if (policyLevel != null)
                {
                    _CoreService.DeletePolicyLevel(policyLevelId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No policyLevel found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getpolicyLevel/{policyLevelId}")]
        public HttpResponseMessage GetPolicyLevel(HttpRequestMessage request, int policyLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PolicyLevel policyLevel = _CoreService.GetPolicyLevel(policyLevelId);

                // notice no need to create a seperate model object since PolicyLevel entity will do just fine
                response = request.CreateResponse<PolicyLevel>(HttpStatusCode.OK, policyLevel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablepolicyLevels")]
        public HttpResponseMessage GetAvailablePolicyLevels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PolicyLevelData[] policyLevels = _CoreService.GetPolicyLevels(string.Empty, string.Empty);

                return request.CreateResponse<PolicyLevelData[]>(HttpStatusCode.OK, policyLevels);
            });
        }
    }
}
