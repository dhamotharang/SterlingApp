using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/facStaging")]
    [UsesDisposableService]
    public class FacStagingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacStagingApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateFacStaging")]
        public HttpResponseMessage UpdateFacStaging(HttpRequestMessage request, [FromBody]FacStaging facStagingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facStaging = _IFRSDataService.UpdateFacStaging(facStagingModel);

                return request.CreateResponse<FacStaging>(HttpStatusCode.OK, facStaging);
            });
        }


        [HttpPost]
        [Route("deleteFacStaging")]
        public HttpResponseMessage DeleteFacStaging(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacStaging facStaging = _IFRSDataService.GetFacStagingById(coldetsId);

                if (facStaging != null)
                {
                    _IFRSDataService.DeleteFacStaging(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        //[HttpGet]
        //[Route("availableFacStaging")]
        //public HttpResponseMessage GetAvailableFacStagings(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        FacStaging[] facStaging = _IFRSDataService.GetAllFacStaging().ToArray();

        //        return request.CreateResponse<FacStaging[]>(HttpStatusCode.OK, facStaging.ToArray());
        //    });
        //}

        [HttpGet]
        [Route("getFacStaging/{coldetsId}")]
        public HttpResponseMessage GetFacStaging(HttpRequestMessage request, int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacStaging facStaging = _IFRSDataService.GetFacStagingById(coldetsId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FacStaging>(HttpStatusCode.OK, facStaging);

                return response;
            });
        }

        [HttpGet]
        [Route("getFacStagingBySearch/{searchParam}")]
        public HttpResponseMessage GetFacStagingBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                FacStaging[] facStaging = _IFRSDataService.GetFacStagingBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, facStaging.ToArray());
            });
        }


        [HttpGet]
        [Route("availableFacStaging/{defaultCount}")]
        public HttpResponseMessage GetAvailableFacStaging(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetFacStaging(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LOAN_AND_OD_STAGING.zip");
                    return response;
                }
                else
                {
                    FacStaging[] facStaging = _IFRSDataService.GetFacStaging(defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, facStaging.ToArray());
                }
            });
        }
    }
}
