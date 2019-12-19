using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/incomecentralvaultschedule")]
    [UsesDisposableService]
    public class incomecentralvaultscheduleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public incomecentralvaultscheduleApiController(IMPRPLService mprPLService)
        {
            _MPRPLService = mprPLService;
        }

        IMPRPLService _MPRPLService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRPLService);
        }

        [HttpPost]
        [Route("updateincomeCentralVaultSchedule")]
        public HttpResponseMessage Updaterevenue(HttpRequestMessage request, [FromBody]IncomeCentralVaultSchedule incomeCentralVaultScheduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomecentralvaultschedule = _MPRPLService.UpdateIncomeCentralVaultSchedule(incomeCentralVaultScheduleModel);

                return request.CreateResponse<IncomeCentralVaultSchedule>(HttpStatusCode.OK, incomecentralvaultschedule);
            });
        }

        [HttpPost]
        [Route("deleteincomeCentralVaultSchedule")]
        public HttpResponseMessage DeleteincomeCentralVaultSchedule(HttpRequestMessage request, [FromBody]int incomeCentralVaultScheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCentralVaultSchedule incomecentralvaultschedule = _MPRPLService.GetIncomeCentralVaultSchedule(incomeCentralVaultScheduleId);

                if (incomecentralvaultschedule != null)
                {
                    _MPRPLService.DeleteRevenue(incomeCentralVaultScheduleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No incomecentralvaultschedule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeCentralVaultSchedule/{incomeCentralVaultScheduleId}")]
        public HttpResponseMessage Getrevenue(HttpRequestMessage request, int incomeCentralVaultScheduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCentralVaultSchedule incomecentralvaultschedule = _MPRPLService.GetIncomeCentralVaultSchedule(incomeCentralVaultScheduleId);

                // notice no need to create a seperate model object since IncomeCentralVaultSchedule entity will do just fine
                response = request.CreateResponse<IncomeCentralVaultSchedule>(HttpStatusCode.OK, incomecentralvaultschedule);

                return response;
            });
        }

        [HttpGet]
        [Route("availableIncomeCentralVaultSchedule")]
        public HttpResponseMessage GetAvailableIncomeCentralVaultSchedule(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                IncomeCentralVaultScheduleData[] incomecentralvaultschedule = _MPRPLService.GetAllIncomeCentralVaultSchedule();

                return request.CreateResponse<IncomeCentralVaultScheduleData[]>(HttpStatusCode.OK, incomecentralvaultschedule);
            });
        }

    }
}
