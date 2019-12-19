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
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/staffcost")]
    [UsesDisposableService]
    public class StaffCostApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public StaffCostApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatestaffCost")]
        public HttpResponseMessage UpdateStaffCost(HttpRequestMessage request, [FromBody]StaffCost staffCostModel)
        {
            return GetHttpResponse(request, () =>
            {
                var staffCost = _MPROPEXService.UpdateStaffCost(staffCostModel);

                return request.CreateResponse<StaffCost>(HttpStatusCode.OK, staffCost);
            });
        }

        [HttpPost]
        [Route("deletestaffCost")]
        public HttpResponseMessage DeleteStaffCost(HttpRequestMessage request, [FromBody]int staffCostId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                StaffCost staffCost = _MPROPEXService.GetStaffCost(staffCostId);

                if (staffCost != null)
                {
                    _MPROPEXService.DeleteStaffCost(staffCostId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No staffCost found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getstaffCost/{staffCostId}")]
        public HttpResponseMessage GetStaffCost(HttpRequestMessage request, int staffCostId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                StaffCost staffCost = _MPROPEXService.GetStaffCost(staffCostId);

                // notice no need to create a seperate model object since StaffCost entity will do just fine
                response = request.CreateResponse<StaffCost>(HttpStatusCode.OK, staffCost);

                return response;
            });
        }

        [HttpGet]
        [Route("availablestaffCosts")]
        public HttpResponseMessage GetAvailableStaffCosts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                StaffCostData[] staffCosts = _MPROPEXService.GetAllStaffCosts();

                return request.CreateResponse<StaffCostData[]>(HttpStatusCode.OK, staffCosts);
            });
        }
    }
}
