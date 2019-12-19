using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.CDQM.Contracts;
using Fintrak.Client.CDQM.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cdqmgendergroup")]
    [UsesDisposableService]
    public class CDQMGenderGroupApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMGenderGroupApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmgendergroup")]
        public HttpResponseMessage UpdateCDQMGenderGroup(HttpRequestMessage request, [FromBody]CDQMGenderGroup cdqmGenderGroupModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmGenderGroup = _CDQMService.UpdateCDQMGenderGroup(cdqmGenderGroupModel);

                return request.CreateResponse<CDQMGenderGroup>(HttpStatusCode.OK, cdqmGenderGroup);
            });
        }

        [HttpPost]
        [Route("deletecdqmGenderGroup")]
        public HttpResponseMessage DeleteCDQMGenderGroup(HttpRequestMessage request, [FromBody]int cdqmGenderGroupId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMGenderGroup cdqmGenderGroup = _CDQMService.GetCDQMGenderGroup(cdqmGenderGroupId);

                if (cdqmGenderGroup != null)
                {
                    _CDQMService.DeleteCDQMGenderGroup(cdqmGenderGroupId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmGenderGroup found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmGenderGroup/{cdqmGenderGroupId}")]
        public HttpResponseMessage GetCDQMGenderGroup(HttpRequestMessage request, int cdqmGenderGroupId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMGenderGroup cdqmGenderGroup = _CDQMService.GetCDQMGenderGroup(cdqmGenderGroupId);

                // notice no need to create a seperate model object since CDQMGenderGroup entity will do just fine
                response = request.CreateResponse<CDQMGenderGroup>(HttpStatusCode.OK, cdqmGenderGroup);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmGenderGroups")]
        public HttpResponseMessage GetAvailableCDQMGenderGroups(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMGenderGroup[] cdqmGenderGroups = _CDQMService.GetAllCDQMGenderGroups();

                return request.CreateResponse<CDQMGenderGroup[]>(HttpStatusCode.OK, cdqmGenderGroups);
            });
        }
    }
}
