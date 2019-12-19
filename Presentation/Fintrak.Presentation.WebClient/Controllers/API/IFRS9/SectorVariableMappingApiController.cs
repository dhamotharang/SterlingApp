using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/sectorvariablemapping")]
    [UsesDisposableService]
    public class SectorVariableMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SectorVariableMappingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesectorVariableMapping")]
        public HttpResponseMessage UpdateSectorVariableMapping(HttpRequestMessage request, [FromBody]SectorVariableMapping sectorVariableMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sectorVariableMapping = _IFRS9Service.UpdateSectorVariableMapping(sectorVariableMappingModel);

                return request.CreateResponse<SectorVariableMapping>(HttpStatusCode.OK, sectorVariableMapping);
            });
        }

        [HttpPost]
        [Route("deletesectorVariableMapping")]
        public HttpResponseMessage DeleteSectorVariableMapping(HttpRequestMessage request, [FromBody]int sectorVariableMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SectorVariableMapping sectorVariableMapping = _IFRS9Service.GetSectorVariableMapping(sectorVariableMappingId);

                if (sectorVariableMapping != null)
                {
                    _IFRS9Service.DeleteSectorVariableMapping(sectorVariableMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sectorVariableMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsectorVariableMapping/{sectorVariableMappingId}")]
        public HttpResponseMessage GetSectorVariableMapping(HttpRequestMessage request,int sectorVariableMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SectorVariableMapping sectorVariableMapping = _IFRS9Service.GetSectorVariableMapping(sectorVariableMappingId);

                // notice no need to create a seperate model object since SectorVariableMapping entity will do just fine
                response = request.CreateResponse<SectorVariableMapping>(HttpStatusCode.OK, sectorVariableMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesectorVariableMappings")]
        public HttpResponseMessage GetAvailableSectorVariableMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SectorVariableMappingData[] sectorVariableMappings = _IFRS9Service.GetAllSectorVariableMappings();

                return request.CreateResponse<SectorVariableMappingData[]>(HttpStatusCode.OK, sectorVariableMappings);
            });
        }
    }
}
