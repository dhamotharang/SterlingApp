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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/facilityStaging")]
    [UsesDisposableService]
    public class FacilityStagingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacilityStagingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateFacilityStaging")]
        public HttpResponseMessage UpdateFacilityStaging(HttpRequestMessage request, [FromBody]FacilityStaging facilityStagingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facilityStaging = _IFRS9Service.UpdateFacilityStaging(facilityStagingModel);

                return request.CreateResponse<FacilityStaging>(HttpStatusCode.OK, facilityStaging);
            });
        }

        [HttpPost]
        [Route("deleteFacilityStaging")]
        public HttpResponseMessage DeleteFacilityStaging(HttpRequestMessage request, [FromBody]int facId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacilityStaging facilityStaging = _IFRS9Service.GetFacilityStaging(facId);

                if (facilityStaging != null)
                {
                    _IFRS9Service.DeleteFacilityStaging(facId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No FacilityStaging found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getFacilityStaging/{facId}")]
        public HttpResponseMessage GetFacilityStaging(HttpRequestMessage request, int facId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacilityStaging facilityStaging = _IFRS9Service.GetFacilityStaging(facId);

                // notice no need to create a seperate model object since FacilityStaging entity will do just fine
                response = request.CreateResponse<FacilityStaging>(HttpStatusCode.OK, facilityStaging);

                return response;
            });
        }



        [HttpGet]
        [Route("availableFacilityStaging/{defaultCount}")]
        public HttpResponseMessage GetAvailableFacilityStaging(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    FacilityStaging[] FacilityStagings = _IFRS9Service.GetAllFacilityStagings(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "LOAN_AND_OD_STAGING.zip");
                    return response;
                }
                else
                {
                    FacilityStaging[] facilityStaging = _IFRS9Service.GetAllFacilityStagings(defaultCount, null).ToArray();

                    return request.CreateResponse<FacilityStaging[]>(HttpStatusCode.OK, facilityStaging);
                }
            });
        }

        [HttpGet]
        [Route("getFacilityStagingsByParam/{param}")]
        public HttpResponseMessage GetAvailableFacilityStagingsByParam(HttpRequestMessage request, string param)
        {
            return GetHttpResponse(request, () =>
            {
                FacilityStaging[] facilityStaging = _IFRS9Service.GetEntityByParam(param);

                return request.CreateResponse<FacilityStaging[]>(HttpStatusCode.OK, facilityStaging);
            });
        }


    }
}
