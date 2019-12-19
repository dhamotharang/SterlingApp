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
    [RoutePrefix("api/staffs")]
    [UsesDisposableService]
    public class StaffsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public StaffsApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatestaff")]
        public HttpResponseMessage UpdateStaffs(HttpRequestMessage request, [FromBody]Staffs staffModel)
        {
            return GetHttpResponse(request, () =>
            {
                var staff = _MPRCoreService.UpdateStaffs(staffModel);

                return request.CreateResponse<Staffs>(HttpStatusCode.OK, staff);
            });
        }

        [HttpPost]
        [Route("deletestaff")]
        public HttpResponseMessage DeleteStaffs(HttpRequestMessage request, [FromBody]int staffId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Staffs staff = _MPRCoreService.GetStaffs(staffId);

                if (staff != null)
                {
                    _MPRCoreService.DeleteStaffs(staffId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No staff found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getstaff/{staffId}")]
        public HttpResponseMessage GetStaffs(HttpRequestMessage request,int staffId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Staffs staff = _MPRCoreService.GetStaffs(staffId);

                // notice no need to create a seperate model object since Staffs entity will do just fine
                response = request.CreateResponse<Staffs>(HttpStatusCode.OK, staff);

                return response;
            });
        }

        [HttpGet]
        [Route("availablestaff")]
        public HttpResponseMessage GetAvailableStaffs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Staffs[] staffs = _MPRCoreService.GetAllStaffs();

                return request.CreateResponse<Staffs[]>(HttpStatusCode.OK, staffs);
            });
        }
    }
}
