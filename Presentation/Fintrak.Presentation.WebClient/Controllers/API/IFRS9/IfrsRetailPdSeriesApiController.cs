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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/IfrsRetailPdSeries")]
    [UsesDisposableService]
    public class IfrsRetailPdSeriesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsRetailPdSeriesApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsRetailPdSeries")]
        public HttpResponseMessage UpdateIfrsRetailPdSeries(HttpRequestMessage request, [FromBody]IfrsRetailPdSeries IfrsRetailPdSeriesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsRetailPdSeries = _IFRS9Service.UpdateIfrsRetailPdSeries(IfrsRetailPdSeriesModel);

                return request.CreateResponse<IfrsRetailPdSeries>(HttpStatusCode.OK, IfrsRetailPdSeries);
            });
        }

        [HttpPost]
        [Route("deleteIfrsRetailPdSeries")]
        public HttpResponseMessage DeleteIfrsRetailPdSeries(HttpRequestMessage request, [FromBody]int PdSeriesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsRetailPdSeries IfrsRetailPdSeries = _IFRS9Service.GetIfrsRetailPdSeriesById(PdSeriesId);

                if (IfrsRetailPdSeries != null)
                {
                    _IFRS9Service.DeleteIfrsRetailPdSeries(PdSeriesId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsRetailPdSeries found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsRetailPdSeriesById/{PdSeriesId}")]
        public HttpResponseMessage GetIfrsRetailPdSeriesId(HttpRequestMessage request, int PdSeriesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsRetailPdSeries IfrsRetailPdSeries = _IFRS9Service.GetIfrsRetailPdSeriesById(PdSeriesId);

                // notice no need to create a seperate model object since IfrsRetailPdSeries entity will do just fine
                response = request.CreateResponse<IfrsRetailPdSeries>(HttpStatusCode.OK, IfrsRetailPdSeries);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsRetailPdSeries")]
        public HttpResponseMessage GetAvailableIfrsRetailPdSeries(HttpRequestMessage request, [FromUri] QueryOptions queryOptions)
        {
            return GetHttpResponse(request, () =>
            {
                if (string.IsNullOrEmpty(queryOptions.SortField)) queryOptions.SortField = "month";
                if (queryOptions.init)
                {
                    queryOptions.init = false;
                    queryOptions.CurrentPage = 1;
                    Double output;
                    Double.TryParse(queryOptions.FilterOption, out output);
                    if (queryOptions.FilterFieldType == "string")
                    {
                        queryOptions.TotalRecords = _IFRS9Service.GetTotalRecordsCount("ifrs_retail_pdseries", queryOptions.FilterField, queryOptions.FilterOption, null);
                    }
                    else
                    {
                        queryOptions.TotalRecords = _IFRS9Service.GetTotalRecordsCount("ifrs_retail_pdseries", queryOptions.FilterField, queryOptions.FilterOption, output);
                    }
                    queryOptions.TotalPages = QueryOptionsCalculator.CalculateTotalPages((UInt64)queryOptions.TotalRecords, queryOptions.PageSize);
                }
                if (queryOptions.TotalPages > 0 && queryOptions.TotalPages < (UInt64)queryOptions.CurrentPage)
                    queryOptions.CurrentPage = (int)queryOptions.TotalPages;

                IfrsRetailPdSeries[] IfrsRetailPdSeries = _IFRS9Service.GetAvailableIfrsRetailPdSeries(queryOptions);

                queryOptions.DisplayedRows = IfrsRetailPdSeries.Count();

                ResultData<IfrsRetailPdSeries> ResultData = new ResultData<IfrsRetailPdSeries>(IfrsRetailPdSeries.ToList(), queryOptions);

                return request.CreateResponse<ResultData<IfrsRetailPdSeries>>(HttpStatusCode.OK, ResultData);
            });
        }
    }
}
