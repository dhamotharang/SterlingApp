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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/derivedcaption")]
    [UsesDisposableService]
    public class DerivedCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public DerivedCaptionApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        [HttpPost]
        [Route("updatederivedCaption")]
        public HttpResponseMessage UpdateDerivedCaption(HttpRequestMessage request, [FromBody]DerivedCaption derivedCaptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var derivedCaption = _IFRSCoreService.UpdateDerivedCaption(derivedCaptionModel);

                return request.CreateResponse<DerivedCaption>(HttpStatusCode.OK, derivedCaption);
            });
        }

        [HttpPost]
        [Route("deletederivedCaption")]
        public HttpResponseMessage DeleteDerivedCaption(HttpRequestMessage request, [FromBody]int derivedCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                DerivedCaption derivedCaption = _IFRSCoreService.GetDerivedCaption(derivedCaptionId);

                if (derivedCaption != null)
                {
                    _IFRSCoreService.DeleteDerivedCaption(derivedCaptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No derivedCaption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getderivedCaption/{derivedCaptionId}")]
        public HttpResponseMessage GetDerivedCaption(HttpRequestMessage request,int derivedCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                DerivedCaption derivedCaption = _IFRSCoreService.GetDerivedCaption(derivedCaptionId);

                // notice no need to create a seperate model object since DerivedCaption entity will do just fine
                response = request.CreateResponse<DerivedCaption>(HttpStatusCode.OK, derivedCaption);

                return response;
            });
        }

        [HttpGet]
        [Route("availablederivedCaptions")]
        public HttpResponseMessage GetAvailableDerivedCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                DerivedCaption[] derivedCaptions = _IFRSCoreService.GetAllDerivedCaptions();

                return request.CreateResponse<DerivedCaption[]>(HttpStatusCode.OK, derivedCaptions);
            });
        }
    }
}
