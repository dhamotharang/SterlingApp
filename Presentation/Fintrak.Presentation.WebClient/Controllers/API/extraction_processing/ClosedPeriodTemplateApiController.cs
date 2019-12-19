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
    [RoutePrefix("api/closedperiodtemplate")]
    [UsesDisposableService]
    public class ClosedPeriodTemplateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ClosedPeriodTemplateApiController(IExtractionProcessService extractionProcessService)
        {
            _ExtractionProcessService = extractionProcessService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateclosedPeriodTemplate")]
        public HttpResponseMessage UpdateClosedPeriodTemplate(HttpRequestMessage request, [FromBody]ClosedPeriodTemplate closedPeriodTemplateModel)
        {
            return GetHttpResponse(request, () =>
            {
                closedPeriodTemplateModel.Active = true;
                var closedPeriodTemplate = _ExtractionProcessService.UpdateClosedPeriodTemplate(closedPeriodTemplateModel);

                return request.CreateResponse<ClosedPeriodTemplate>(HttpStatusCode.OK, closedPeriodTemplate);
            });
        }

        [HttpPost]
        [Route("deleteclosedPeriodTemplate")]
        public HttpResponseMessage DeleteClosedPeriodTemplate(HttpRequestMessage request, [FromBody]int closedPeriodTemplateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ClosedPeriodTemplate closedPeriodTemplate = _ExtractionProcessService.GetClosedPeriodTemplate(closedPeriodTemplateId);

                if (closedPeriodTemplate != null)
                {
                    _ExtractionProcessService.DeleteClosedPeriodTemplate(closedPeriodTemplateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No closedPeriodTemplate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getclosedPeriodTemplate/{closedPeriodTemplateId}")]
        public HttpResponseMessage GetClosedPeriodTemplate(HttpRequestMessage request, int closedPeriodTemplateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ClosedPeriodTemplate closedPeriodTemplate = _ExtractionProcessService.GetClosedPeriodTemplate(closedPeriodTemplateId);

                // notice no need to create a seperate model object since ClosedPeriodTemplate entity will do just fine
                response = request.CreateResponse<ClosedPeriodTemplate>(HttpStatusCode.OK, closedPeriodTemplate);

                return response;
            });
        }

        [HttpGet]
        [Route("getclosedperiodtemplatebysolution/{solutionName}")]
        public HttpResponseMessage GetClosedPeriodTemplateBySolution(HttpRequestMessage request,string solutionName)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodTemplateData closedPeriodTemplate = _ExtractionProcessService.GetClosedPeriodTemplates().Where(c => c.SolutionName == solutionName).FirstOrDefault();

                return request.CreateResponse<ClosedPeriodTemplateData>(HttpStatusCode.OK, closedPeriodTemplate);
            });
        }

        [HttpGet]
        [Route("availableclosedPeriodTemplates")]
        public HttpResponseMessage GetAvailableClosedPeriodTemplates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodTemplateData[] closedPeriodTemplates = _ExtractionProcessService.GetClosedPeriodTemplates();

                return request.CreateResponse<ClosedPeriodTemplateData[]>(HttpStatusCode.OK, closedPeriodTemplates);
            });
        }

        [HttpGet]
        [Route("getclosedperiodtemplatebylogin")]
        public HttpResponseMessage GetClosedPeriodTemplateByLogin(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ClosedPeriodTemplateData[] closedPeriodTemplates = _ExtractionProcessService.GetClosedPeriodTemplateByLogin(User.Identity.Name);

                return request.CreateResponse<ClosedPeriodTemplateData[]>(HttpStatusCode.OK, closedPeriodTemplates);
            });
        }
    }
}
