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
    [RoutePrefix("api/bscaption")]
    [UsesDisposableService]
    public class BSCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BSCaptionApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebscaption")]
        public HttpResponseMessage UpdateBSCaption(HttpRequestMessage request, [FromBody]BSCaption bsCaptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bsCaption = _MPRBSService.UpdateBSCaption(bsCaptionModel);

                return request.CreateResponse<BSCaption>(HttpStatusCode.OK, bsCaption);
            });
        }

        [HttpPost]
        [Route("deletebsCaption")]
        public HttpResponseMessage DeleteBSCaption(HttpRequestMessage request, [FromBody]int bsCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BSCaption bsCaption = _MPRBSService.GetBSCaption(bsCaptionId);

                if (bsCaption != null)
                {
                    _MPRBSService.DeleteBSCaption(bsCaptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bsCaption found under that ID.");

                return response;
            });
        }






        [HttpGet]
        [Route("getbudgetbsCaption/{budgetBSCaptionName}")]
        public HttpResponseMessage GetBudgetBSCaptionByName(HttpRequestMessage request, string budgetBSCaptionName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSCaptionData[] bsCaption = _MPRBSService.GetAllBudgetBSCaptionsByCaptionName(budgetBSCaptionName);

                // notice no need to create a seperate model object since BSCaption entity will do just fine
                response = request.CreateResponse<BSCaptionData[]>(HttpStatusCode.OK, bsCaption);

                return response;
            });
        }



        [HttpGet]
        [Route("getmprbsCaption/{mprBSCaptionName}")]
        public HttpResponseMessage GetMPRBSCaptionByName(HttpRequestMessage request, string mprBSCaptionName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSCaptionData[] bsCaption = _MPRBSService.GetAllMPRBSCaptionsByCaptionName(mprBSCaptionName);

                // notice no need to create a seperate model object since BSCaption entity will do just fine
                response = request.CreateResponse<BSCaptionData[]>(HttpStatusCode.OK, bsCaption);

                return response;
            });
        }




        [HttpGet]
        [Route("getbsCaption/{bsCaptionId}")]
        public HttpResponseMessage GetBSCaption(HttpRequestMessage request, int bsCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSCaption bsCaption = _MPRBSService.GetBSCaption(bsCaptionId);

                // notice no need to create a seperate model object since BSCaption entity will do just fine
                response = request.CreateResponse<BSCaption>(HttpStatusCode.OK, bsCaption);

                return response;
            });
        }



        [HttpGet]
        [Route("availablebscaptions")]
        public HttpResponseMessage GetAvailableBSCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSCaptionData[] bsCaptions = _MPRBSService.GetAllBSCaptions();

                return request.CreateResponse<BSCaptionData[]>(HttpStatusCode.OK, bsCaptions);
            });
        }

        [HttpGet]
        [Route("availablemprbscaptions")]
        public HttpResponseMessage GetAvailableMPRBSCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSCaptionData[] bsCaptions = _MPRBSService.GetAllMPRBSCaptions();

                return request.CreateResponse<BSCaptionData[]>(HttpStatusCode.OK, bsCaptions);
            });
        }


        [HttpGet]
        [Route("availablemprbscaption")]
        public HttpResponseMessage GetAvailableMPRBSCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSCaption[] bsCaptions = _MPRBSService.GetBSCaptions();

                return request.CreateResponse<BSCaption[]>(HttpStatusCode.OK, bsCaptions);
            });
        }

        [HttpGet]
        [Route("availablebudgetbscaptions")]
        public HttpResponseMessage GetAvailableBudgetBSCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSCaptionData[] bsCaptions = _MPRBSService.GetAllBudgetBSCaptions();

                return request.CreateResponse<BSCaptionData[]>(HttpStatusCode.OK, bsCaptions);
            });
        }



    }
}
