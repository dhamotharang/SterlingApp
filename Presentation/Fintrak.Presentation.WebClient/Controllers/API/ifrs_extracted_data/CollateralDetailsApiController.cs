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
    [RoutePrefix("api/collateralDetails")]
    [UsesDisposableService]
    public class CollateralDetailsApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CollateralDetailsApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateCollateralDetails")]
        public HttpResponseMessage UpdateCollateralDetails(HttpRequestMessage request, [FromBody]CollateralDetails collateralDetailsModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanPry = _IFRSDataService.UpdateCollateralDetails(collateralDetailsModel);

                return request.CreateResponse<CollateralDetails>(HttpStatusCode.OK, loanPry);
            });
        }


        [HttpPost]
        [Route("deleteCollateralDetails")]
        public HttpResponseMessage DeleteCollateralDetails(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CollateralDetails collateralDetails = _IFRSDataService.GetCollateralDetailsById(coldetsId);

                if (collateralDetails != null)
                {
                    _IFRSDataService.DeleteCollateralDetails(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        //[HttpGet]
        //[Route("availableCollateralDetails")]
        //public HttpResponseMessage GetAvailableCollateralDetailss(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        CollateralDetails[] collateralDetails = _IFRSDataService.GetAllCollateralDetails().ToArray();

        //        return request.CreateResponse<CollateralDetails[]>(HttpStatusCode.OK, collateralDetails.ToArray());
        //    });
        //}

        [HttpGet]
        [Route("getCollateralDetails/{coldetsId}")]
        public HttpResponseMessage GetCollateralDetails(HttpRequestMessage request, int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CollateralDetails collateralDetails = _IFRSDataService.GetCollateralDetailsById(coldetsId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<CollateralDetails>(HttpStatusCode.OK, collateralDetails);

                return response;
            });
        }

        [HttpGet]
        [Route("getCollateralDetailsBySearch/{searchParam}")]
        public HttpResponseMessage GetCollateralDetailsBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                CollateralDetails[] collateralDetails = _IFRSDataService.GetCollateralDetailsBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, collateralDetails.ToArray());
            });
        }


        [HttpGet]
        [Route("availableCollateralDetails/{defaultCount}")]
        public HttpResponseMessage GetAvailableCollateralDetails(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetCollateralDetails(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Collateral_Information.zip");
                    return response;
                }
                else
                {
                    CollateralDetails[] collateralDetails = _IFRSDataService.GetCollateralDetails(defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, collateralDetails.ToArray());
                }
            });
        }
    }
}
