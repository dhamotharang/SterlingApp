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
    [RoutePrefix("api/staff")]
    [UsesDisposableService]
    public class StaffApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public StaffApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatestaff")]
        public HttpResponseMessage UpdateStaff(HttpRequestMessage request, [FromBody]Staff staffModel)
        {
            return GetHttpResponse(request, () =>
            {
                var staff = _CoreService.UpdateStaff(staffModel);

                return request.CreateResponse<Staff>(HttpStatusCode.OK, staff);
            });
        }

        [HttpPost]
        [Route("deletestaff")]
        public HttpResponseMessage DeleteStaff(HttpRequestMessage request, [FromBody]int staffId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Staff staff = _CoreService.GetStaff(staffId);

                if (staff != null)
                {
                    _CoreService.DeleteStaff(staffId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No staff found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getstaff/{staffId}")]
        public HttpResponseMessage GetStaff(HttpRequestMessage request,int staffId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Staff staff = _CoreService.GetStaff(staffId);

                // notice no need to create a seperate model object since Staff entity will do just fine
                response = request.CreateResponse<Staff>(HttpStatusCode.OK, staff);

                return response;
            });
        }

        [HttpGet]
        [Route("availablestaffs")]
        public HttpResponseMessage GetAvailableStaffs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Staff[] staffs = _CoreService.GetAllStaffs();

                return request.CreateResponse<Staff[]>(HttpStatusCode.OK, staffs);
            });
        }
    }
}
