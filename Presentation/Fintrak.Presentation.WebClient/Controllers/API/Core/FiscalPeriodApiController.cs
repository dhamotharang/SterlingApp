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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/fiscalPeriod")]
    [UsesDisposableService]
    public class FiscalPeriodApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FiscalPeriodApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatefiscalPeriod")]
        public HttpResponseMessage UpdateFiscalPeriod(HttpRequestMessage request, [FromBody]FiscalPeriod fiscalPeriodModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fiscalPeriod = _CoreService.UpdateFiscalPeriod(fiscalPeriodModel);

                return request.CreateResponse<FiscalPeriod>(HttpStatusCode.OK, fiscalPeriod);
            });
        }

        [HttpPost]
        [Route("deletefiscalPeriod")]
        public HttpResponseMessage DeleteFiscalPeriod(HttpRequestMessage request, [FromBody]int fiscalPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FiscalPeriod fiscalPeriod = _CoreService.GetFiscalPeriod(fiscalPeriodId);

                if (fiscalPeriod != null)
                {
                    _CoreService.DeleteFiscalPeriod(fiscalPeriodId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No fiscalPeriod found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopenfiscalPeriod")]
        public HttpResponseMessage GetOpenFiscalPeriod(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FiscalPeriodData fiscalPeriod = _CoreService.GetOpenFiscalPeriod();

                // notice no need to create a seperate model object since FiscalPeriod entity will do just fine
                response = request.CreateResponse<FiscalPeriodData>(HttpStatusCode.OK, fiscalPeriod);

                return response;
            });
        }

        [HttpGet]
        [Route("getfiscalPeriod/{fiscalPeriodId}")]
        public HttpResponseMessage GetFiscalPeriod(HttpRequestMessage request, int fiscalPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FiscalPeriod fiscalPeriod = _CoreService.GetFiscalPeriod(fiscalPeriodId);

                // notice no need to create a seperate model object since FiscalPeriod entity will do just fine
                response = request.CreateResponse<FiscalPeriod>(HttpStatusCode.OK, fiscalPeriod);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefiscalPeriods")]
        public HttpResponseMessage GetAvailableFiscalPeriods(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FiscalPeriod[] fiscalPeriods = _CoreService.GetAllFiscalPeriods();

                return request.CreateResponse<FiscalPeriod[]>(HttpStatusCode.OK, fiscalPeriods);
            });
        }

        [HttpGet]
        [Route("getfiscalPeriods/{fiscalYearId}")]
        public HttpResponseMessage GetFiscalPeriodByYear(HttpRequestMessage request,int fiscalYearId)
        {
            return GetHttpResponse(request, () =>
            {
                FiscalPeriodData[] fiscalPeriods = _CoreService.GetFiscalPeriodByYear(fiscalYearId);

                return request.CreateResponse<FiscalPeriodData[]>(HttpStatusCode.OK, fiscalPeriods);
            });
        }
    }
}
