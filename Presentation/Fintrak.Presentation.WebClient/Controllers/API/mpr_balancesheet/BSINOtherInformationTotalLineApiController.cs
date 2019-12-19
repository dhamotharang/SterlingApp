using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bsinotherinformationtotalline")]
    [UsesDisposableService]
    public class BSINOtherInformationTotalLineApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BSINOtherInformationTotalLineApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebsinotherinformationtotalline")]
        public HttpResponseMessage UpdateBSINOtherInformationTotalLine(HttpRequestMessage request, [FromBody]BSINOtherInformationTotalLine bSINOtherInformationTotalLineModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bSINOtherInformationTotalLine = _MPRBSService.UpdateBSINOtherInformationTotalLine(bSINOtherInformationTotalLineModel);

                return request.CreateResponse<BSINOtherInformationTotalLine>(HttpStatusCode.OK, bSINOtherInformationTotalLine);
            });
        }

        [HttpPost]
        [Route("deletebsinotherinformationtotalline")]
        public HttpResponseMessage DeleteBSINOtherInformationTotalLine(HttpRequestMessage request, [FromBody]int bSINOtherInformationTotalLineId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BSINOtherInformationTotalLine bSINOtherInformationTotalLine = _MPRBSService.GetBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);

                if (bSINOtherInformationTotalLine != null)
                {
                    _MPRBSService.DeleteBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No BSINOtherInformationTotalLine found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getbsinotherinformationtotalline/{bSINOtherInformationTotalLineId}")]
        public HttpResponseMessage GetBSINOtherInformationTotalLine(HttpRequestMessage request, int bSINOtherInformationTotalLineId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSINOtherInformationTotalLine bSINOtherInformationTotalLine = _MPRBSService.GetBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);

                // notice no need to create a seperate model object since BSINOtherInformationTotalLine entity will do just fine
                response = request.CreateResponse<BSINOtherInformationTotalLine>(HttpStatusCode.OK, bSINOtherInformationTotalLine);

                return response;
            });
        }



        [HttpGet]
        [Route("availablebsinotherinformationtotallines")]
        public HttpResponseMessage GetAvailableBSINOtherInformationTotalLines(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSINOtherInformationTotalLine[] bSINOtherInformationTotalLines = _MPRBSService.GetAllBSINOtherInformationTotalLines();

                return request.CreateResponse<BSINOtherInformationTotalLine[]>(HttpStatusCode.OK, bSINOtherInformationTotalLines);
            });
        }

        [HttpGet]
        [Route("availablebgetallbsplothercaptions")]
        public HttpResponseMessage GetAvailableGetAllBsPlCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<BSCaption> bsPLCaption = _MPRBSService.GetAllBsPlOtherInfoCaptions();

                return request.CreateResponse<IEnumerable<BSCaption>>(HttpStatusCode.OK, bsPLCaption);
            });
        }





    }
}
