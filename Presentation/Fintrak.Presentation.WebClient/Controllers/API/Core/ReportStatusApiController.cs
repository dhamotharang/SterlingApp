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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/reportstatus")]
    [UsesDisposableService]
    public class ReportStatusApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ReportStatusApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatereportstatus")]
        public HttpResponseMessage UpdateReportStatus(HttpRequestMessage request, [FromBody]ReportStatus reportstatusModel)
        {
            return GetHttpResponse(request, () =>
            {
                var reportstatus = _CoreService.UpdateReportStatus(reportstatusModel);

                return request.CreateResponse<ReportStatus>(HttpStatusCode.OK, reportstatus);
            });
        }

        [HttpPost]
        [Route("deletereportstatus")]
        public HttpResponseMessage DeleteReportStatus(HttpRequestMessage request, [FromBody]int reportstatusId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ReportStatus reportstatus = _CoreService.GetReportStatus(reportstatusId);

                if (reportstatus != null)
                {
                    _CoreService.DeleteReportStatus(reportstatusId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No reportstatus found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getreportstatus/{reportstatusId}")]
        public HttpResponseMessage GetReportStatus(HttpRequestMessage request, int reportstatusId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ReportStatus reportstatus = _CoreService.GetReportStatus(reportstatusId);

                // notice no need to create a seperate model object since ReportStatus entity will do just fine
                response = request.CreateResponse<ReportStatus>(HttpStatusCode.OK, reportstatus);

                return response;
            });
        }

        [HttpGet]
        [Route("availablereportstatus")]
        public HttpResponseMessage GetAvailableReportStatus(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ReportStatusData[] reportStatus = _CoreService.GetAllReportStatus().ToArray();

                return request.CreateResponse<ReportStatusData[]>(HttpStatusCode.OK, reportStatus);
            });
        }


    }
}
