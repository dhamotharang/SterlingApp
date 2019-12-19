using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Client.Budget.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/modificationlevel")]
    [UsesDisposableService]
    public class ModificationLevelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ModificationLevelApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatemodificationLevel")]
        public HttpResponseMessage UpdateModificationLevel(HttpRequestMessage request, [FromBody]ModificationLevel modificationLevelModel)
        {
            return GetHttpResponse(request, () =>
            {
                var modificationLevel = _CoreService.UpdateModificationLevel(modificationLevelModel);

                return request.CreateResponse<ModificationLevel>(HttpStatusCode.OK, modificationLevel);
            });
        }

        [HttpPost]
        [Route("deletemodificationLevel")]
        public HttpResponseMessage DeleteModificationLevel(HttpRequestMessage request, [FromBody]int modificationLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ModificationLevel modificationLevel = _CoreService.GetModificationLevel(modificationLevelId);

                if (modificationLevel != null)
                {
                    _CoreService.DeleteModificationLevel(modificationLevelId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No modificationLevel found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmodificationLevel/{modificationLevelId}")]
        public HttpResponseMessage GetModificationLevel(HttpRequestMessage request, int modificationLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ModificationLevel modificationLevel = _CoreService.GetModificationLevel(modificationLevelId);

                // notice no need to create a seperate model object since ModificationLevel entity will do just fine
                response = request.CreateResponse<ModificationLevel>(HttpStatusCode.OK, modificationLevel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemodificationLevels")]
        public HttpResponseMessage GetAvailableModificationLevels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ModificationLevelData[] modificationLevels = _CoreService.GetAllModificationLevel();

                return request.CreateResponse<ModificationLevelData[]>(HttpStatusCode.OK, modificationLevels);
            });
        }
    }
}
