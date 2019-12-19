using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/offbalancesheetecl")]
    [UsesDisposableService]
    public class OffBalancesheetECLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OffBalancesheetECLApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateoffBalancesheetECL")]
        public HttpResponseMessage UpdateOffBalancesheetECL(HttpRequestMessage request, [FromBody]OffBalancesheetECL offBalancesheetECLModel)
        {
            return GetHttpResponse(request, () =>
            {
                var offBalancesheetECL = _IFRS9Service.UpdateOffBalancesheetECL(offBalancesheetECLModel);

                return request.CreateResponse<OffBalancesheetECL>(HttpStatusCode.OK, offBalancesheetECL);
            });
        }

        [HttpPost]
        [Route("deleteoffBalancesheetECL")]
        public HttpResponseMessage DeleteOffBalancesheetECL(HttpRequestMessage request, [FromBody]int offBalancesheetECLId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OffBalancesheetECL offBalancesheetECL = _IFRS9Service.GetOffBalancesheetECL(offBalancesheetECLId);

                if (offBalancesheetECL != null)
                {
                    _IFRS9Service.DeleteOffBalancesheetECL(offBalancesheetECLId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No offBalancesheetECL found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getoffBalancesheetECL/{obe_Id}")]
        public HttpResponseMessage GetOffBalancesheetECL(HttpRequestMessage request, int obe_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OffBalancesheetECL offBalancesheetECL = _IFRS9Service.GetOffBalancesheetECL(obe_Id);

                // notice no need to create a seperate model object since OffBalancesheetECL entity will do just fine
                response = request.CreateResponse<OffBalancesheetECL>(HttpStatusCode.OK, offBalancesheetECL);

                return response;
            });
        }

        [HttpGet]
        [Route("availableoffBalancesheetECLs")]
        public HttpResponseMessage GetAvailableOffBalancesheetECLs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OffBalancesheetECL[] offBalancesheetECLs = _IFRS9Service.GetAllOffBalancesheetECLs();

                return request.CreateResponse<OffBalancesheetECL[]>(HttpStatusCode.OK, offBalancesheetECLs);
            });
        }

        [HttpGet]
        [Route("getoffBalancesheetECLsBySearch/{SearchParam}")]
        public HttpResponseMessage GetOffBalancesheetECLBySearch(HttpRequestMessage request, string SearchParam)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OffBalancesheetECL[] offBalancesheetECLs = _IFRS9Service.GetOffBalancesheetECLBySearch(SearchParam);

                // notice no need to create a seperate model object since IfrsCorporateEcl entity will do just fine
                response = request.CreateResponse<OffBalancesheetECL[]>(HttpStatusCode.OK, offBalancesheetECLs);

                return response;
            });
        }
    }
}
