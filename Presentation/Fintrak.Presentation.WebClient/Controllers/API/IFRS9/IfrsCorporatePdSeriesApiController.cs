using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Shared.Common.Services.QueryService;
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/IfrsCorporatePdSeries")]
    [UsesDisposableService]
    public class IfrsCorporatePdSeriesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsCorporatePdSeriesApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsCorporatePdSeries")]
        public HttpResponseMessage UpdateIfrsCorporatePdSeries(HttpRequestMessage request, [FromBody]IfrsCorporatePdSeries IfrsCorporatePdSeriesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsCorporatePdSeries = _IFRS9Service.UpdateIfrsCorporatePdSeries(IfrsCorporatePdSeriesModel);

                return request.CreateResponse(HttpStatusCode.OK, IfrsCorporatePdSeries);
            });
        }

        [HttpPost]
        [Route("deleteIfrsCorporatePdSeries")]
        public HttpResponseMessage DeleteIfrsCorporatePdSeries(HttpRequestMessage request, [FromBody]int PdSeriesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCorporatePdSeries IfrsCorporatePdSeries = _IFRS9Service.GetIfrsCorporatePdSeriesById(PdSeriesId);

                if (IfrsCorporatePdSeries != null)
                {
                    _IFRS9Service.DeleteIfrsCorporatePdSeries(PdSeriesId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsCorporatePdSeries found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsCorporatePdSeriesById/{Sno}")]
        public HttpResponseMessage GetIfrsCorporatePdSeriesId(HttpRequestMessage request, int Sno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsCorporatePdSeries IfrsCorporatePdSeries = _IFRS9Service.GetIfrsCorporatePdSeriesById(Sno);

                // notice no need to create a seperate model object since IfrsCorporatePdSeries entity will do just fine
                response = request.CreateResponse(HttpStatusCode.OK, IfrsCorporatePdSeries);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsCorporatePdSeries")]
        public HttpResponseMessage GetAvailableIfrsCorporatePdSeries(HttpRequestMessage request, [FromUri] QueryOptions queryOptions)
        {
            return GetHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(queryOptions.SortField)) queryOptions.SortField = "seq";
                if (queryOptions.init)
                {
                    queryOptions.init = false;
                    queryOptions.CurrentPage = 1;
                    Double output;
                    Double.TryParse(queryOptions.FilterOption, out output);
                    if (queryOptions.FilterFieldType == "string")
                    {
                        queryOptions.TotalRecords = _IFRS9Service.GetTotalRecordsCount("ifrs_Corporate_pdseries", queryOptions.FilterField, queryOptions.FilterOption, null);
                    }
                    else
                    {
                        queryOptions.TotalRecords = _IFRS9Service.GetTotalRecordsCount("ifrs_Corporate_pdseries", queryOptions.FilterField, queryOptions.FilterOption, output);
                    }
                    queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages((UInt64)queryOptions.TotalRecords, queryOptions.PageSize);
                }
                if (queryOptions.TotalPages > 0 && queryOptions.TotalPages < (UInt64)queryOptions.CurrentPage)
                    queryOptions.CurrentPage = (int)queryOptions.TotalPages;

                IfrsCorporatePdSeries[] IfrsCorporatePdSeries = _IFRS9Service.GetAvailableIfrsCorporatePdSeries(queryOptions);

                queryOptions.DisplayedRows = IfrsCorporatePdSeries.Count();

                ResultData<IfrsCorporatePdSeries> ResultData = new ResultData<IfrsCorporatePdSeries>(IfrsCorporatePdSeries.ToList(), queryOptions);

                return request.CreateResponse(HttpStatusCode.OK, ResultData);
            });
        }

        [HttpGet]
        [Route("exportAllIfrsCorporatePdSeries")]
        public HttpResponseMessage ExportAllIfrsCorporatePdSeries(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string path = HostingEnvironment.MapPath("~");
                var fileStream = _IFRS9Service.GetExportIfrsCorporatePdSeries(path + "ExportedData\\");
                var response = DownloadFileService.DownloadFile(path, "Corporate%20PDs.zip");
                return response;
            });
        }
    }
}
