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
    [RoutePrefix("api/cdqmetlconfiguration")]
    [UsesDisposableService]
    public class CDQMETLConfigurationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMETLConfigurationApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmetlconfiguration")]
        public HttpResponseMessage UpdateCDQMETLConfiguration(HttpRequestMessage request, [FromBody]CDQMETLConfiguration cdqmETLConfigurationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmETLConfiguration = _CDQMService.UpdateCDQMETLConfiguration(cdqmETLConfigurationModel);

                return request.CreateResponse<CDQMETLConfiguration>(HttpStatusCode.OK, cdqmETLConfiguration);
            });
        }

        [HttpPost]
        [Route("deletecdqmETLConfiguration")]
        public HttpResponseMessage DeleteCDQMETLConfiguration(HttpRequestMessage request, [FromBody]int cdqmETLConfigurationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMETLConfiguration cdqmETLConfiguration = _CDQMService.GetCDQMETLConfiguration(cdqmETLConfigurationId);

                if (cdqmETLConfiguration != null)
                {
                    _CDQMService.DeleteCDQMETLConfiguration(cdqmETLConfigurationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmETLConfiguration found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmETLConfiguration/{cdqmETLConfigurationId}")]
        public HttpResponseMessage GetCDQMETLConfiguration(HttpRequestMessage request, int cdqmETLConfigurationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMETLConfiguration cdqmETLConfiguration = _CDQMService.GetCDQMETLConfiguration(cdqmETLConfigurationId);

                // notice no need to create a seperate model object since CDQMETLConfiguration entity will do just fine
                response = request.CreateResponse<CDQMETLConfiguration>(HttpStatusCode.OK, cdqmETLConfiguration);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmETLConfigurations")]
        public HttpResponseMessage GetAvailableCDQMETLConfigurations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMETLConfiguration[] cdqmETLConfigurations = _CDQMService.GetAllCDQMETLConfigurations();

                return request.CreateResponse<CDQMETLConfiguration[]>(HttpStatusCode.OK, cdqmETLConfigurations);
            });
        }
    }
}
