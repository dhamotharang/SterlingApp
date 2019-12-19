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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/autopostingtemplate")]
    [UsesDisposableService]
    public class AutoPostingTemplateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AutoPostingTemplateApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateautoPostingTemplate")]
        public HttpResponseMessage UpdateAutoPostingTemplate(HttpRequestMessage request, [FromBody]AutoPostingTemplate autoPostingTemplateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var autoPostingTemplate = _FinstatService.UpdateAutoPostingTemplate(autoPostingTemplateModel);

                return request.CreateResponse<AutoPostingTemplate>(HttpStatusCode.OK, autoPostingTemplate);
            });
        }

        [HttpPost]
        [Route("deleteautoPostingTemplate")]
        public HttpResponseMessage DeleteAutoPostingTemplate(HttpRequestMessage request, [FromBody]int autoPostingTemplateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                AutoPostingTemplate autoPostingTemplate = _FinstatService.GetAutoPostingTemplate(autoPostingTemplateId);

                if (autoPostingTemplate != null)
                {
                    _FinstatService.DeleteAutoPostingTemplate(autoPostingTemplateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No autoPostingTemplate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getautoPostingTemplate/{autoPostingTemplateId}")]
        public HttpResponseMessage GetAutoPostingTemplate(HttpRequestMessage request,int autoPostingTemplateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                AutoPostingTemplate autoPostingTemplate = _FinstatService.GetAutoPostingTemplate(autoPostingTemplateId);

                // notice no need to create a seperate model object since AutoPostingTemplate entity will do just fine
                response = request.CreateResponse<AutoPostingTemplate>(HttpStatusCode.OK, autoPostingTemplate);

                return response;
            });
        }

        [HttpGet]
        [Route("availableautoPostingTemplates")]
        public HttpResponseMessage GetAvailableAutoPostingTemplates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                AutoPostingTemplate[] autoPostingTemplates = _FinstatService.GetAllAutoPostingTemplates();

                return request.CreateResponse<AutoPostingTemplate[]>(HttpStatusCode.OK, autoPostingTemplates);
            });
        }
    }
}
