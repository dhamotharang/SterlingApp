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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrs9dashboard")]
    [UsesDisposableService]
    public class IFRS9DashboardApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRS9DashboardApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpGet]
        [Route("portfolioexposure")]
        public HttpResponseMessage GetPortfolioExposure(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>

            {
                HttpResponseMessage response = null;
                var item = new PortfolioModel();
                PortfolioExposure[] portfolioExposures = _IFRS9Service.GetAllPortfolioExposures();
                string portfolioExposureschart = _IFRS9Service.GetAllPortfolioExposuresChart();
   
                item.PortfolioExposure = portfolioExposures.ToArray();
                item.PortfolioExposureChart = portfolioExposureschart;

                // notice no need to create a seperate model object since CollateralCategory entity will do just fine
                response = request.CreateResponse<PortfolioModel>(HttpStatusCode.OK, item);
             
                return response;
            });
        }

        [HttpGet]
        [Route("sectorialexposure")]
        public HttpResponseMessage GetSectorialExposure(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var item = new PortfolioModel();
                SectorialExposure[] sectorialExposures = _IFRS9Service.GetAllSectorialExposures();
                string sectorialExposureschart = _IFRS9Service.GetAllSectorialExposuresChart();

                item.SectorialExposure = sectorialExposures.ToArray();
                item.SectorialExposureChart = sectorialExposureschart;
               // return request.CreateResponse<SectorialExposure[]>(HttpStatusCode.OK, sectorialExposures);
                response = request.CreateResponse<PortfolioModel>(HttpStatusCode.OK, item);
                return response;
            });
        }

        [HttpGet]
        [Route("summaryreport")]
        public HttpResponseMessage Getsummaryreport(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var item = new PortfolioModel();
                SummaryReport[] summaryReports = _IFRS9Service.GetAllSummaryReports();
                string summaryReportChart = _IFRS9Service.GetAllSummaryReportsChart();

                item.SummaryReport = summaryReports.ToArray();
                item.SummaryReportChart = summaryReportChart;

                // notice no need to create a seperate model object since CollateralCategory entity will do just fine
                response = request.CreateResponse<PortfolioModel>(HttpStatusCode.OK, item);

                return response;
            });
        }

        [HttpGet]
        [Route("bucketexposure")]
        public HttpResponseMessage GetBucketExposure(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var item = new PortfolioModel();
                BucketExposure[] bucketExposures = _IFRS9Service.GetAllBucketExposures();
               string bucketExposureschart = _IFRS9Service.GetAllBucketExposuresChart();

                item.BucketExposure = bucketExposures.ToArray();
                item.BucketExposureChart = bucketExposureschart;
                // return request.CreateResponse<SectorialExposure[]>(HttpStatusCode.OK, sectorialExposures);
                response = request.CreateResponse<PortfolioModel>(HttpStatusCode.OK, item);
                return response;
            });
        }
    }
}
