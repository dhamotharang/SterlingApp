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
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexreport")]
    [UsesDisposableService]
    public class OpexReportApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexReportApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        //[HttpPost]
        //[Route("updateopexReport")]
        //public HttpResponseMessage UpdateOpexReport(HttpRequestMessage request, [FromBody]OpexReport opexReportModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var opexReport = _MPROPEXService.UpdateOpexReport(opexReportModel);

        //        return request.CreateResponse<OpexReport>(HttpStatusCode.OK, opexReport);
        //    });
        //}

        //[HttpPost]
        //[Route("deleteopexReport")]
        //public HttpResponseMessage DeleteOpexReport(HttpRequestMessage request, [FromBody]int opexReportId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        OpexReport opexReport = _MPROPEXService.GetOpexReport(opexReportId);

        //        if (opexReport != null)
        //        {
        //            _MPROPEXService.DeleteOpexReport(opexReportId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No opexReport found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getopexReport/{opexReportId}")]
        //public HttpResponseMessage GetOpexReport(HttpRequestMessage request, int opexReportId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        OpexReport opexReport = _MPROPEXService.GetOpexReport(opexReportId);

        //        // notice no need to create a seperate model object since OpexReport entity will do just fine
        //        response = request.CreateResponse<OpexReport>(HttpStatusCode.OK, opexReport);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availableopexReports")]
        public HttpResponseMessage GetAvailableOpexReports(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexReportData[] opexReports = _MPROPEXService.GetAllOpexReports();

                return request.CreateResponse<OpexReportData[]>(HttpStatusCode.OK, opexReports);
            });
        }
    }
}
