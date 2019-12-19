using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/closedperiod")]
    [UsesDisposableService]
    public class ClosedPeriodApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ClosedPeriodApiController(IExtractionProcessService extractionProcessService)
        {
            _ExtractionProcessService = extractionProcessService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateclosedPeriod")]
        public HttpResponseMessage UpdateClosedPeriod(HttpRequestMessage request, [FromBody]ClosedPeriod closedPeriodModel)
        {
            return GetHttpResponse(request, () =>
            {
                closedPeriodModel.Active = false;
                var closedPeriod = _ExtractionProcessService.UpdateClosedPeriod(closedPeriodModel);

                return request.CreateResponse<ClosedPeriod>(HttpStatusCode.OK, closedPeriod);
            });
        }

        [HttpPost]
        [Route("deleteclosedPeriod")]
        public HttpResponseMessage DeleteClosedPeriod(HttpRequestMessage request, [FromBody]int closedPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ClosedPeriod closedPeriod = _ExtractionProcessService.GetClosedPeriod(closedPeriodId);

                if (closedPeriod != null)
                {
                    _ExtractionProcessService.DeleteClosedPeriod(closedPeriodId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No closedPeriod found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getclosedPeriod/{closedPeriodId}")]
        public HttpResponseMessage GetClosedPeriod(HttpRequestMessage request, int closedPeriodId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ClosedPeriod closedPeriod = _ExtractionProcessService.GetClosedPeriod(closedPeriodId);

                // notice no need to create a seperate model object since ClosedPeriod entity will do just fine
                response = request.CreateResponse<ClosedPeriod>(HttpStatusCode.OK, closedPeriod);

                return response;
            });
        }

        [HttpGet]
        [Route("getclosedperiodbysolution/{solutionName}")]
        public HttpResponseMessage GetClosedPeriodBySolution(HttpRequestMessage request,string solutionName)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodData closedPeriod = _ExtractionProcessService.GetClosedPeriods().Where(c => c.SolutionName == solutionName).FirstOrDefault();

                return request.CreateResponse<ClosedPeriodData>(HttpStatusCode.OK, closedPeriod);
            });
        }

        [HttpGet]
        [Route("availableclosedPeriods")]
        public HttpResponseMessage GetAvailableClosedPeriods(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodData[] closedPeriods = _ExtractionProcessService.GetClosedPeriods();

                return request.CreateResponse<ClosedPeriodData[]>(HttpStatusCode.OK, closedPeriods);
            });
        }

        [HttpGet]
        [Route("getclosedperiodbylogin/{solutionId}/{year}")]
        public HttpResponseMessage GetClosedPeriodByLogin(HttpRequestMessage request, int solutionId, int year)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodData[] closedPeriods = _ExtractionProcessService.GetClosedPeriodByLogin(User.Identity.Name).Where(c=>c.SolutionId == solutionId && c.Date.Year == year).ToArray();

                return request.CreateResponse<ClosedPeriodData[]>(HttpStatusCode.OK, closedPeriods);
            });
        }

        [HttpGet]
        [Route("getclosedperiodtobeopenbylogin/{solutionId}/{year}")]
        public HttpResponseMessage GetClosedPeriodToBeOpenByLogin(HttpRequestMessage request, int solutionId, int year)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodData[] closedPeriods = _ExtractionProcessService.GetClosedPeriodByLogin(User.Identity.Name).Where(c => c.SolutionId == solutionId && c.Date.Year == year && c.Deleted == true).ToArray();

                return request.CreateResponse<ClosedPeriodData[]>(HttpStatusCode.OK, closedPeriods);
            });
        }

        [HttpPost]
        [Route("closeperiod")]
        public HttpResponseMessage ClosePeriod(HttpRequestMessage request, [FromBody]ClosedPeriod closedPeriodModel)
        {
            return GetHttpResponse(request, () =>
            {
                var closedPeriod = _ExtractionProcessService.ClosePeriod(closedPeriodModel);

                return request.CreateResponse<ClosedPeriod>(HttpStatusCode.OK, closedPeriod);
            });
        }
    }
}
