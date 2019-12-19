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
    [RoutePrefix("api/costcentre")]
    [UsesDisposableService]
    public class CostCentreApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CostCentreApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updatecostCentre")]
        public HttpResponseMessage UpdateCostCentre(HttpRequestMessage request, [FromBody]CostCentre costCentreModel)
        {
            return GetHttpResponse(request, () =>
            {
                var costCentre = _MPROPEXService.UpdateCostCentre(costCentreModel);

                return request.CreateResponse<CostCentre>(HttpStatusCode.OK, costCentre);
            });
        }

        [HttpPost]
        [Route("deletecostCentre")]
        public HttpResponseMessage DeleteCostCentre(HttpRequestMessage request, [FromBody]int costCentreId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CostCentre costCentre = _MPROPEXService.GetCostCentre(costCentreId);

                if (costCentre != null)
                {
                    _MPROPEXService.DeleteCostCentre(costCentreId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No costCentre found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcostCentre/{costCentreId}")]
        public HttpResponseMessage GetCostCentre(HttpRequestMessage request, int costCentreId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CostCentre costCentre = _MPROPEXService.GetCostCentre(costCentreId);

                // notice no need to create a seperate model object since CostCentre entity will do just fine
                response = request.CreateResponse<CostCentre>(HttpStatusCode.OK, costCentre);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecostCentres")]
        public HttpResponseMessage GetAvailableCostCentres(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CostCentreData[] costCentres = _MPROPEXService.GetAllCostCentres();

                return request.CreateResponse<CostCentreData[]>(HttpStatusCode.OK, costCentres);
            });
        }

        [HttpGet]
        [Route("getparentcostcentres/{definitionCode}")]
        public HttpResponseMessage GetAvailableTeams(HttpRequestMessage request, string definitionCode)
        {
            return GetHttpResponse(request, () =>
            {

                CostCentre[] teams = _MPROPEXService.GetParentCostCentres(definitionCode);

                return request.CreateResponse<CostCentre[]>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getcostcentrebylevel/{level}")]
        public HttpResponseMessage GetTeamByLevel(HttpRequestMessage request, int level)
        {
            return GetHttpResponse(request, () =>
            {

                CostCentre[] teams = _MPROPEXService.GetCostCentreByLevel(level);

                return request.CreateResponse<CostCentre[]>(HttpStatusCode.OK, teams);
            });
        }

        [HttpGet]
        [Route("getcostcentrebydefinition/{definitionCode}")]
        public HttpResponseMessage GetTeamByDefinition(HttpRequestMessage request, string definitionCode)
        {
            return GetHttpResponse(request, () =>
            {

                CostCentre[] teams = _MPROPEXService.GetCostCentreByDefinition(definitionCode);

                return request.CreateResponse<CostCentre[]>(HttpStatusCode.OK, teams);
            });
        }
    }
}
