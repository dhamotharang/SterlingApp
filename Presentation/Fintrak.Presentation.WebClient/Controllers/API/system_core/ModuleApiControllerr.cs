using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/module")]
    [UsesDisposableService]
    public class ModuleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ModuleApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatemodule")]
        public HttpResponseMessage UpdateModule(HttpRequestMessage request, [FromBody]Module moduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var module = _CoreService.UpdateModule(moduleModel);

                return request.CreateResponse<Module>(HttpStatusCode.OK, module);
            });
        }

        [HttpPost]
        [Route("moduleupdate")]
        public HttpResponseMessage ModuleUpdate(HttpRequestMessage request, [FromBody]ModuleData moduleModel)
        {
            return GetHttpResponse(request, () =>
            {
                Module module = _CoreService.GetModule(moduleModel.ModuleId);

                module.CanUpdate = !module.CanUpdate;
                module = _CoreService.UpdateModule(module);

                return request.CreateResponse<Module>(HttpStatusCode.OK, module);
            });
        }

        [HttpPost]
        [Route("deletemodule")]
        public HttpResponseMessage DeleteModule(HttpRequestMessage request, [FromBody]int moduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Module module = _CoreService.GetModule(moduleId);

                if (module != null)
                {
                    _CoreService.DeleteModule(moduleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No module found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmodule/{moduleId}")]
        public HttpResponseMessage GetModule(HttpRequestMessage request,int moduleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Module module = _CoreService.GetModule(moduleId);

                // notice no need to create a seperate model object since Module entity will do just fine
                response = request.CreateResponse<Module>(HttpStatusCode.OK, module);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemodules")]
        public HttpResponseMessage GetAvailableModules(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ModuleData[] modules = _CoreService.GetModules();

                return request.CreateResponse<ModuleData[]>(HttpStatusCode.OK, modules);
            });
        }
    }
}
