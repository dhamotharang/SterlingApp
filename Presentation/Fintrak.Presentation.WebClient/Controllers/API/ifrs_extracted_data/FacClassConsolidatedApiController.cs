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
    [RoutePrefix("api/facClassConsolidated")]
    [UsesDisposableService]
    public class FacClassConsolidatedApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacClassConsolidatedApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateFacClassConsolidated")]
        public HttpResponseMessage UpdateFacClassConsolidated(HttpRequestMessage request, [FromBody]FacClassConsolidated facClassConsolidatedModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facClassConsolidated = _IFRSDataService.UpdateFacClassConsolidated(facClassConsolidatedModel);

                return request.CreateResponse<FacClassConsolidated>(HttpStatusCode.OK, facClassConsolidated);
            });
        }


        [HttpPost]
        [Route("deleteFacClassConsolidated")]
        public HttpResponseMessage DeleteFacClassConsolidated(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacClassConsolidated facClassConsolidated = _IFRSDataService.GetFacClassConsolidatedById(coldetsId);

                if (facClassConsolidated != null)
                {
                    _IFRSDataService.DeleteFacClassConsolidated(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        //[HttpGet]
        //[Route("availableFacClassConsolidated")]
        //public HttpResponseMessage GetAvailableFacClassConsolidateds(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        FacClassConsolidated[] facClassConsolidated = _IFRSDataService.GetAllFacClassConsolidated().ToArray();

        //        return request.CreateResponse<FacClassConsolidated[]>(HttpStatusCode.OK, facClassConsolidated.ToArray());
        //    });
        //}

        [HttpGet]
        [Route("getFacClassConsolidated/{coldetsId}")]
        public HttpResponseMessage GetFacClassConsolidated(HttpRequestMessage request, int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacClassConsolidated facClassConsolidated = _IFRSDataService.GetFacClassConsolidatedById(coldetsId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FacClassConsolidated>(HttpStatusCode.OK, facClassConsolidated);

                return response;
            });
        }

        [HttpGet]
        [Route("getFacClassConsolidatedBysSearch/{searchParam}")]
        public HttpResponseMessage GetFacClassConsolidatedBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                FacClassConsolidated[] facClassConsolidated = _IFRSDataService.GetFacClassConsolidatedBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, facClassConsolidated.ToArray());
            });
        }


        [HttpGet]
        [Route("availableFacClassConsolidated/{defaultCount}")]
        public HttpResponseMessage GetAvailableFacClassConsolidated(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetFacClassConsolidated(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "FAC_CLASS_-_LOAN_OD_&_OBE.zip");
                    return response;
                }
                else
                {
                    FacClassConsolidated[] facClassConsolidated = _IFRSDataService.GetFacClassConsolidated(defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, facClassConsolidated.ToArray());
                }
            });
        }
    }
}
