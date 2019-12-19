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
    [RoutePrefix("api/facOBEStaging")]
    [UsesDisposableService]
    public class FacOBEStagingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacOBEStagingApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateFacOBEStaging")]
        public HttpResponseMessage UpdateFacOBEStaging(HttpRequestMessage request, [FromBody]FacOBEStaging facOBEStagingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facOBEStaging = _IFRSDataService.UpdateFacOBEStaging(facOBEStagingModel);

                return request.CreateResponse<FacOBEStaging>(HttpStatusCode.OK, facOBEStaging);
            });
        }


        [HttpPost]
        [Route("deleteFacOBEStaging")]
        public HttpResponseMessage DeleteFacOBEStaging(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacOBEStaging facOBEStaging = _IFRSDataService.GetFacOBEStagingById(coldetsId);

                if (facOBEStaging != null)
                {
                    _IFRSDataService.DeleteFacOBEStaging(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        //[HttpGet]
        //[Route("availableFacOBEStaging")]
        //public HttpResponseMessage GetAvailableFacOBEStagings(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        FacOBEStaging[] facOBEStaging = _IFRSDataService.GetAllFacOBEStaging().ToArray();

        //        return request.CreateResponse<FacOBEStaging[]>(HttpStatusCode.OK, facOBEStaging.ToArray());
        //    });
        //}

        [HttpGet]
        [Route("getFacOBEStaging/{coldetsId}")]
        public HttpResponseMessage GetFacOBEStaging(HttpRequestMessage request, int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacOBEStaging facOBEStaging = _IFRSDataService.GetFacOBEStagingById(coldetsId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FacOBEStaging>(HttpStatusCode.OK, facOBEStaging);

                return response;
            });
        }

        [HttpGet]
        [Route("getFacOBEStagingBySearch/{searchParam}")]
        public HttpResponseMessage GetFacOBEStagingBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                FacOBEStaging[] facOBEStaging = _IFRSDataService.GetFacOBEStagingBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, facOBEStaging.ToArray());
            });
        }


        [HttpGet]
        [Route("availableFacOBEStaging/{defaultCount}")]
        public HttpResponseMessage GetAvailableFacOBEStaging(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetFacOBEStaging(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "BONDS_AND_GTEE_STAGING.zip");
                    return response;
                }
                else
                {
                    FacOBEStaging[] facOBEStaging = _IFRSDataService.GetFacOBEStaging(defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, facOBEStaging.ToArray());
                }
            });
        }
    }
}
