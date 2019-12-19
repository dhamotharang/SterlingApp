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
    [RoutePrefix("api/fiscalYear")]
    [UsesDisposableService]
    public class FiscalYearApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FiscalYearApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatefiscalYear")]
        public HttpResponseMessage UpdateFiscalYear(HttpRequestMessage request, [FromBody]FiscalYear fiscalYearModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fiscalYear = _CoreService.UpdateFiscalYear(fiscalYearModel);

                return request.CreateResponse<FiscalYear>(HttpStatusCode.OK, fiscalYear);
            });
        }

        [HttpPost]
        [Route("deletefiscalYear")]
        public HttpResponseMessage DeleteFiscalYear(HttpRequestMessage request, [FromBody]int fiscalYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FiscalYear fiscalYear = _CoreService.GetFiscalYear(fiscalYearId);

                if (fiscalYear != null)
                {
                    _CoreService.DeleteFiscalYear(fiscalYearId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No fiscalYear found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopenfiscalYear")]
        public HttpResponseMessage GetOpenFiscalYear(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FiscalYear fiscalYear = _CoreService.GetOpenFiscalYear();

                // notice no need to create a seperate model object since FiscalYear entity will do just fine
                response = request.CreateResponse<FiscalYear>(HttpStatusCode.OK, fiscalYear);

                return response;
            });
        }

        [HttpGet]
        [Route("getfiscalYear/{fiscalYearId}")]
        public HttpResponseMessage GetFiscalYear(HttpRequestMessage request, int fiscalYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FiscalYear fiscalYear = _CoreService.GetFiscalYear(fiscalYearId);

                // notice no need to create a seperate model object since FiscalYear entity will do just fine
                response = request.CreateResponse<FiscalYear>(HttpStatusCode.OK, fiscalYear);

                return response;
            });
        }

        [HttpGet]
        [Route("getfiscalyearwithperiods/{fiscalyearId}")]
        public HttpResponseMessage GetFiscalYearWithPeriod(HttpRequestMessage request, int fiscalYearId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var period = new PeriodModel();

                FiscalYear fiscalYear = _CoreService.GetFiscalYear(fiscalYearId);
                FiscalPeriodData[] fiscalPeriods = _CoreService.GetFiscalPeriodByYear(fiscalYearId);

                period.FiscalYear = fiscalYear;
                period.FiscalPeriods = fiscalPeriods;

                // notice no need to create a seperate model object since FiscalYear entity will do just fine
                response = request.CreateResponse<PeriodModel>(HttpStatusCode.OK, period);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefiscalYears")]
        public HttpResponseMessage GetAvailableFiscalYears(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FiscalYear[] fiscalYears = _CoreService.GetAllFiscalYears();

                return request.CreateResponse<FiscalYear[]>(HttpStatusCode.OK, fiscalYears);
            });
        }

        [HttpGet]
        [Route("getyears")]
        public HttpResponseMessage GetYear(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var solutionRunDates = _CoreService.GetAllFiscalYears().Select(c => c.EndDate.Year).Distinct<int>(); ;

                var years = new List<KeyValueModel>();

                foreach (var runDate in solutionRunDates)
                {
                    years.Add(new KeyValueModel() { Key = runDate, Value = runDate.ToString() });
                }


                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, years.ToArray());
            });
        }
    }
}
