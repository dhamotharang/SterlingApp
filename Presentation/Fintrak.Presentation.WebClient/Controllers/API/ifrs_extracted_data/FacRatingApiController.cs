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
    [RoutePrefix("api/facRating")]
    [UsesDisposableService]
    public class FacRatingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacRatingApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateFacRating")]
        public HttpResponseMessage UpdateFacRating(HttpRequestMessage request, [FromBody]FacRating facRatingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facRating = _IFRSDataService.UpdateFacRating(facRatingModel);

                return request.CreateResponse<FacRating>(HttpStatusCode.OK, facRating);
            });
        }


        [HttpPost]
        [Route("deleteFacRating")]
        public HttpResponseMessage DeleteFacRating(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacRating facRating = _IFRSDataService.GetFacRatingById(coldetsId);

                if (facRating != null)
                {
                    _IFRSDataService.DeleteFacRating(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        //[HttpGet]
        //[Route("availableFacRating")]
        //public HttpResponseMessage GetAvailableFacRatings(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        FacRating[] facRating = _IFRSDataService.GetAllFacRating().ToArray();

        //        return request.CreateResponse<FacRating[]>(HttpStatusCode.OK, facRating.ToArray());
        //    });
        //}

        [HttpGet]
        [Route("getFacRating/{coldetsId}")]
        public HttpResponseMessage GetFacRating(HttpRequestMessage request, int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacRating facRating = _IFRSDataService.GetFacRatingById(coldetsId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FacRating>(HttpStatusCode.OK, facRating);

                return response;
            });
        }

        [HttpGet]
        [Route("getFacRatingBySearch/{searchParam}")]
        public HttpResponseMessage GetFacRatingBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                FacRating[] facRating = _IFRSDataService.GetFacRatingBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, facRating.ToArray());
            });
        }


        [HttpGet]
        [Route("availableFacRating/{defaultCount}")]
        public HttpResponseMessage GetAvailableFacRating(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetFacRating(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "LOAN_&_OD_RATINGS.zip");
                    return response;
                }
                else
                {
                    FacRating[] facRating = _IFRSDataService.GetFacRating(defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, facRating.ToArray());
                }
            });
        }
    }
}
