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
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/costcentredefinition")]
    [UsesDisposableService]
    public class CostCentreDefinitionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CostCentreDefinitionApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatecostCentreDefinition")]
        public HttpResponseMessage UpdateCostCentreDefinition(HttpRequestMessage request, [FromBody]CostCentreDefinition costCentreDefinitionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var costCentreDefinition = _MPROPEXService.UpdateCostCentreDefinition(costCentreDefinitionModel);

                return request.CreateResponse<CostCentreDefinition>(HttpStatusCode.OK, costCentreDefinition);
            });
        }

        [HttpPost]
        [Route("deletecostCentreDefinition")]
        public HttpResponseMessage DeleteCostCentreDefinition(HttpRequestMessage request, [FromBody]int ccDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CostCentreDefinition costCentreDefinition = _MPROPEXService.GetCostCentreDefinition(ccDefinitionId);

                if (costCentreDefinition != null)
                {
                    _MPROPEXService.DeleteCostCentreDefinition(ccDefinitionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No costCentreDefinition found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcostCentreDefinition/{ccDefinitionId}")]
        public HttpResponseMessage GetCostCentreDefinition(HttpRequestMessage request, int ccDefinitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CostCentreDefinition costCentreDefinition = _MPROPEXService.GetCostCentreDefinition(ccDefinitionId);

                // notice no need to create a seperate model object since CostCentreDefinition entity will do just fine
                response = request.CreateResponse<CostCentreDefinition>(HttpStatusCode.OK, costCentreDefinition);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecostCentreDefinitions")]
        public HttpResponseMessage GetAvailableCostCentreDefinitions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CostCentreDefinition[] costCentreDefinitions = _MPROPEXService.GetAllCostCentreDefinitions();

                return request.CreateResponse<CostCentreDefinition[]>(HttpStatusCode.OK, costCentreDefinitions);
            });
        }
    }
}
