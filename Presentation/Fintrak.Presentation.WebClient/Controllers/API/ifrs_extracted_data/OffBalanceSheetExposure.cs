using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/offbalancesheetexposure")]
    [UsesDisposableService]
    public class OffbalancesheetexposureApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OffbalancesheetexposureApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateoffbalancesheetexposure")]
        public HttpResponseMessage Updateoffbalancesheetexposure(HttpRequestMessage request, [FromBody]OffBalanceSheetExposure offbalancesheetexposureModel)
        {
            return GetHttpResponse(request, () =>
            {
                var offbalancesheetexposure = _IFRSDataService.UpdateOffBalanceSheetExposure(offbalancesheetexposureModel);

                return request.CreateResponse<OffBalanceSheetExposure>(HttpStatusCode.OK, offbalancesheetexposure);
            });
        }


        [HttpPost]
        [Route("deleteoffbalancesheetexposure")]
        public HttpResponseMessage Deleteoffbalancesheetexposure(HttpRequestMessage request, [FromBody]int obeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OffBalanceSheetExposure offbalancesheetexposure = _IFRSDataService.GetOffBalanceSheetExposure(obeId);

                if (offbalancesheetexposure != null)
                {
                    _IFRSDataService.DeleteOffBalanceSheetExposure(obeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Of BalancesheetExposure found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availableoffbalancesheetexposure")]
        public HttpResponseMessage GetAvailableoffbalancesheetexposures(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OffBalanceSheetExposure[] offbalancesheetexposure = _IFRSDataService.GetAllOffBalanceSheetExposure().ToArray();

                return request.CreateResponse<OffBalanceSheetExposure[]>(HttpStatusCode.OK, offbalancesheetexposure.ToArray());
            });
        }

        [HttpGet]
        [Route("gettoffbalancesheetexposure/{obeId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int obeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OffBalanceSheetExposure offbalancesheetexposure = _IFRSDataService.GetOffBalanceSheetExposure(obeId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<OffBalanceSheetExposure>(HttpStatusCode.OK, offbalancesheetexposure);

                return response;
            });
        }

        [HttpGet]
        [Route("getoffbalancesheetexposure/{portfolio}")]
        public HttpResponseMessage Getoffbalancesheetexposure(HttpRequestMessage request, string portfolio)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OffBalanceSheetExposure[] offbalancesheetexposure = _IFRSDataService.GetOffBalanceSheetExposureByPortfolio(portfolio);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<OffBalanceSheetExposure[]>(HttpStatusCode.OK, offbalancesheetexposure.ToArray());

                return response;
            });
        }



    }
}
