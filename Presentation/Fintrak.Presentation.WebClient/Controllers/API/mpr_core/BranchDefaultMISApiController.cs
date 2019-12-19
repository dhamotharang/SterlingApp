using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/branchdefaultmis")]
    [UsesDisposableService]
    public class BranchDefaultMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BranchDefaultMISApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatebranchdefaultmis")]
        public HttpResponseMessage UpdateBranchDefaultMIS(HttpRequestMessage request, [FromBody]BranchDefaultMIS teambranchdefaultmisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teambranchdefaultmis = _MPRCoreService.UpdateBranchDefaultMIS(teambranchdefaultmisModel);

                return request.CreateResponse<BranchDefaultMIS>(HttpStatusCode.OK, teambranchdefaultmis);
            });
        }

        [HttpPost]
        [Route("deletebranchDefaultMIS")]
        public HttpResponseMessage DeleteBranchDefaultMIS(HttpRequestMessage request, [FromBody]int branchDefaultMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BranchDefaultMIS branchDefaultMIS = _MPRCoreService.GetBranchDefaultMIS(branchDefaultMISId);

                if (branchDefaultMIS != null)
                {
                    _MPRCoreService.DeleteBranchDefaultMIS(branchDefaultMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No branchDefaultMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbranchDefaultMIS/{branchDefaultMISId}")]
        public HttpResponseMessage GetBranchDefaultMIS(HttpRequestMessage request, int branchDefaultMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BranchDefaultMIS branchDefaultMIS = _MPRCoreService.GetBranchDefaultMIS(branchDefaultMISId);

                // notice no need to create a seperate model object since BranchDefaultMIS entity will do just fine
                response = request.CreateResponse<BranchDefaultMIS>(HttpStatusCode.OK, branchDefaultMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebranchDefaultMISs")]
        public HttpResponseMessage GetAvailableBranchDefaultMISs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BranchDefaultMIS[] branchDefaultMISs = _MPRCoreService.GetAllBranchDefaultMISs();

                return request.CreateResponse<BranchDefaultMIS[]>(HttpStatusCode.OK, branchDefaultMISs);
            });
        }
    }
}
