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
    [RoutePrefix("api/balancesheetthreshold")]
    [UsesDisposableService]
    public class BalanceSheetThresholdApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BalanceSheetThresholdApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebalancesheetthreshold")]
        public HttpResponseMessage UpdateBalanceSheetThreshold(HttpRequestMessage request, [FromBody]BalanceSheetThreshold balancesheetThresholdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var balancesheetThreshold = _MPRBSService.UpdateBalanceSheetThreshold(balancesheetThresholdModel);

                return request.CreateResponse<BalanceSheetThreshold>(HttpStatusCode.OK, balancesheetThreshold);
            });
        }

        [HttpPost]
        [Route("deletebalancesheetThreshold")]
        public HttpResponseMessage DeleteBalanceSheetThreshold(HttpRequestMessage request, [FromBody]int balancesheetThresholdId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BalanceSheetThreshold balancesheetThreshold = _MPRBSService.GetBalanceSheetThreshold(balancesheetThresholdId);

                if (balancesheetThreshold != null)
                {
                    _MPRBSService.DeleteBalanceSheetThreshold(balancesheetThresholdId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No balancesheetThreshold found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetThreshold/{balancesheetThresholdId}")]
        public HttpResponseMessage GetBalanceSheetThreshold(HttpRequestMessage request, int balancesheetThresholdId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BalanceSheetThreshold balancesheetThreshold = _MPRBSService.GetBalanceSheetThreshold(balancesheetThresholdId);

                // notice no need to create a seperate model object since BalanceSheetThreshold entity will do just fine
                response = request.CreateResponse<BalanceSheetThreshold>(HttpStatusCode.OK, balancesheetThreshold);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebalancesheetThresholds")]
        public HttpResponseMessage GetAvailableBalanceSheetThresholds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BalanceSheetThresholdData[] balancesheetThresholds = _MPRBSService.GetAllBalanceSheetThresholds();

                return request.CreateResponse<BalanceSheetThresholdData[]>(HttpStatusCode.OK, balancesheetThresholds);
            });
        }
    }
}
