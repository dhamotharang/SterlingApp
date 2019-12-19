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
    [RoutePrefix("api/branch")]
    [UsesDisposableService]
    public class BranchApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BranchApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatebranch")]
        public HttpResponseMessage UpdateBranch(HttpRequestMessage request, [FromBody]Branch branchModel)
        {
            return GetHttpResponse(request, () =>
            {
                var branch = _CoreService.UpdateBranch(branchModel);

                return request.CreateResponse<Branch>(HttpStatusCode.OK, branch);
            });
        }

        [HttpPost]
        [Route("deletebranch")]
        public HttpResponseMessage DeleteBranch(HttpRequestMessage request, [FromBody]int branchId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Branch branch = _CoreService.GetBranch(branchId);

                if (branch != null)
                {
                    _CoreService.DeleteBranch(branchId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No branch found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbranch/{branchId}")]
        public HttpResponseMessage GetBranch(HttpRequestMessage request,int branchId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Branch branch = _CoreService.GetBranch(branchId);

                // notice no need to create a seperate model object since Branch entity will do just fine
                response = request.CreateResponse<Branch>(HttpStatusCode.OK, branch);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebranches")]
        public HttpResponseMessage GetAvailableBranches(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Branch[] branches = _CoreService.GetAllBranches();

                return request.CreateResponse<Branch[]>(HttpStatusCode.OK, branches);
            });
        }

        [HttpGet]
        [Route("getbranchebycompany/{companyId}")]
        public HttpResponseMessage GetBrancheByCompany(HttpRequestMessage request,int companyId)
        {
            return GetHttpResponse(request, () =>
            {
                Branch[] branches = _CoreService.GetAllBranches().Where(c=>c.CompanyId == companyId).ToArray();

                return request.CreateResponse<Branch[]>(HttpStatusCode.OK, branches);
            });
        }
    }
}
