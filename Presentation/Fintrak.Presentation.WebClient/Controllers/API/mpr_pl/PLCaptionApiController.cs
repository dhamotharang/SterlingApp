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
    [RoutePrefix("api/plcaption")]
    [UsesDisposableService]
    public class PLCaptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PLCaptionApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateplCaption")]
        public HttpResponseMessage UpdatePLCaption(HttpRequestMessage request, [FromBody]PLCaption plCaptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var plCaption = _MPRPLService.UpdatePLCaption(plCaptionModel);

                return request.CreateResponse<PLCaption>(HttpStatusCode.OK, plCaption);
            });
        }

        [HttpPost]
        [Route("deleteplCaption")]
        public HttpResponseMessage DeletePLCaption(HttpRequestMessage request, [FromBody]int plCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PLCaption plCaption = _MPRPLService.GetPLCaption(plCaptionId);

                if (plCaption != null)
                {
                    _MPRPLService.DeletePLCaption(plCaptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No plCaption found under that ID.");

                return response;
            });
        }

        public HttpResponseMessage DeletePLCaption(HttpRequestMessage request)
        {
            return DeletePLCaption(request, 0);
        }

        [HttpGet]
        [Route("getplcaption/{plcaptionId}")]
        public HttpResponseMessage GetPLCaption(HttpRequestMessage request, int PLCaptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PLCaption plCaption = _MPRPLService.GetPLCaption(PLCaptionId);

                // notice no need to create a seperate model object since PLCaption entity will do just fine
                response = request.CreateResponse<PLCaption>(HttpStatusCode.OK, plCaption);

                return response;
            });
        }




        [HttpGet]
        [Route("getmprplcaption/{mprPLCaptionName}")]
        public HttpResponseMessage GetMPRPLCaption(HttpRequestMessage request, string mprPLCaptionName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PLCaptionNewData[] plCaption = _MPRPLService.GetAllMPRPLCaptionsByCaptionName(mprPLCaptionName);

                // notice no need to create a seperate model object since PLCaption entity will do just fine
                response = request.CreateResponse<PLCaptionNewData[]>(HttpStatusCode.OK, plCaption);

                return response;
            });
        }




        [HttpGet]
        [Route("getbudgetplcaption/{budgetPLCaptionName}")]
        public HttpResponseMessage GetBudgetPLCaption(HttpRequestMessage request, string budgetPLCaptionName)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PLCaptionNewData[] plCaption = _MPRPLService.GetAllBudgetPLCaptionsByCaptionName(budgetPLCaptionName);

                // notice no need to create a seperate model object since PLCaption entity will do just fine
                response = request.CreateResponse<PLCaptionNewData[]>(HttpStatusCode.OK, plCaption);

                return response;
            });
        }


        [HttpGet]
        [Route("availableplCaptions")]
        public HttpResponseMessage GetAvailablePLCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLCaptionData[] plCaptions = _MPRPLService.GetAllPLCaptions();

                return request.CreateResponse<PLCaptionData[]>(HttpStatusCode.OK, plCaptions);
            });
        }

        [HttpGet]
        [Route("availablemprplCaptions")]
        public HttpResponseMessage GetAvailableMPRPLCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLCaptionNewData[] plCaptions = _MPRPLService.GetAllMPRPLCaptions();

                return request.CreateResponse<PLCaptionNewData[]>(HttpStatusCode.OK, plCaptions);
            });
        }


        [HttpGet]
        [Route("availablemprplCaption")]
        public HttpResponseMessage GetAvailableMPRPLCaption(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLCaptionNewData[] plCaptions = _MPRPLService.GetPLCaptions();

                return request.CreateResponse<PLCaptionNewData[]>(HttpStatusCode.OK, plCaptions);
            });
        }

        [HttpGet]
        [Route("availablebudgetplCaptions")]
        public HttpResponseMessage GetAvailableBudgetPLCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PLCaptionNewData[] plCaptions = _MPRPLService.GetAllBudgetPLCaptions();

                return request.CreateResponse<PLCaptionNewData[]>(HttpStatusCode.OK, plCaptions);
            });
        }

    }
}
