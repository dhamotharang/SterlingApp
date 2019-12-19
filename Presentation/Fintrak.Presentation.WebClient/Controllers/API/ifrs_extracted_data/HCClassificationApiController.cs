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
    [RoutePrefix("api/HCClassification")]
    [UsesDisposableService]
    public class HCClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public HCClassificationApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateHCClassification")]
        public HttpResponseMessage UpdateHCClassification(HttpRequestMessage request, [FromBody]HCClassification HCClassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var HCClassification = _IFRSDataService.UpdateHCClassification(HCClassificationModel);

                return request.CreateResponse<HCClassification>(HttpStatusCode.OK, HCClassification);
            });
        }


        [HttpPost]
        [Route("deleteHCClassification")]
        public HttpResponseMessage DeleteHCClassification(HttpRequestMessage request, [FromBody]int coldetsId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                HCClassification HCClassification = _IFRSDataService.GetHCClassificationById(coldetsId);

                if (HCClassification != null)
                {
                    _IFRSDataService.DeleteHCClassification(coldetsId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("availableHCClassification")]
        public HttpResponseMessage GetAvailableHCClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HCClassification[] HCClassification = _IFRSDataService.GetAllHCClassification();

                return request.CreateResponse<HCClassification[]>(HttpStatusCode.OK, HCClassification);
            });
        }

        [HttpGet]
        [Route("getHCClassification/{Id}")]
        public HttpResponseMessage GetHCClassification(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                HCClassification HCClassification = _IFRSDataService.GetHCClassificationById(Id);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<HCClassification>(HttpStatusCode.OK, HCClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("getHCClassificationBySearch/{searchParam}")]
        public HttpResponseMessage GetHCClassificationBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                HCClassification[] HCClassification = _IFRSDataService.GetHCClassificationBySearch(searchParam);

                return request.CreateResponse(HttpStatusCode.OK, HCClassification.ToArray());
            });
        }

    }
}
