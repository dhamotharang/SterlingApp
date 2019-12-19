using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Fintrak.Business.Basic.Contracts;
//using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsreportpackviewer")]
    [UsesDisposableService]
    public class IFRSReportPackViewerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRSReportPackViewerApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        [HttpGet]
        [Route("availableifrsreportpack")]
        public HttpResponseMessage AvailableReportPackViewer(HttpRequestMessage request, [FromBody]IFRSReportPack ifrsreportpack)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSReportPack[] reportPacks = _IFRSCoreService.GetAllIFRSReportPacks();

                return request.CreateResponse<IFRSReportPack[]>(HttpStatusCode.OK, reportPacks);
            });
        }

        [HttpPost]
        [Route("returnreporturl")]
        public HttpResponseMessage ReturnReportUrl(HttpRequestMessage request, [FromBody] IFRSReportParam param)
        {
            return GetHttpResponse(request, () =>
            {            
              string rptUrl=  _IFRSCoreService.ReturnReportUrl(param.ReportName, param.RunDate);

              return request.CreateResponse<string>(HttpStatusCode.OK, rptUrl);
            });
        }

        [HttpGet]
        [Route("availablerundate")]
        public HttpResponseMessage AvailableRunDates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSReport[] ifrsreport = _IFRSCoreService.GetAllRunDates().ToArray();

                List<IFRSReportModel> runDate = new List<IFRSReportModel>();

                List<DateTime> rundates = null;

                rundates = ifrsreport.Select(c => c.RunDate ).Distinct().ToList();

                foreach (var c in rundates)
                    runDate.Add(new IFRSReportModel()
                    {
                        RunDate = c
                    });
                return request.CreateResponse<IFRSReportModel[]>(HttpStatusCode.OK, runDate.ToArray());
            });
        }

    }
}
