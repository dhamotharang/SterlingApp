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
    [RoutePrefix("api/sectormapping")]
    [UsesDisposableService]
    public class SectorMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SectorMappingApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesectormapping")]
        public HttpResponseMessage UpdateSectorMapping(HttpRequestMessage request, [FromBody]SectorMapping sectorMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sectorMapping = _IFRS9Service.UpdateSectorMapping(sectorMappingModel);

                return request.CreateResponse<SectorMapping>(HttpStatusCode.OK, sectorMapping);
            });
        }

        [HttpPost]
        [Route("deletesectormapping")]
        public HttpResponseMessage DeleteSectorMapping(HttpRequestMessage request, [FromBody]int SectorMapping_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SectorMapping sectorMapping = _IFRS9Service.GetSectorMapping(SectorMapping_Id);

                if (sectorMapping != null)
                {
                    _IFRS9Service.DeleteSectorMapping(SectorMapping_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sector mapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsectormapping/{SectorMapping_Id}")]
        public HttpResponseMessage GetSectorMapping(HttpRequestMessage request, int SectorMapping_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SectorMapping sectorMapping = _IFRS9Service.GetSectorMapping(SectorMapping_Id);

                // notice no need to create a seperate model object since SectorMapping entity will do just fine
                response = request.CreateResponse<SectorMapping>(HttpStatusCode.OK, sectorMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesectormappings")]
        public HttpResponseMessage GetAvailableSectorMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SectorMapping[] sectorMappings = _IFRS9Service.GetAllSectorMappings();

                return request.CreateResponse<SectorMapping[]>(HttpStatusCode.OK, sectorMappings);
            });
        }

        //[HttpGet]
        //[Route("getsectormappingsbysource/{source}")]
        //public HttpResponseMessage GetAvailableSectorMappingsBySource(HttpRequestMessage request, string Source)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        SectorMapping[] sectorMappings = _IFRS9Service.GetSectorMappingBySource(Source);

        //        return request.CreateResponse<SectorMapping[]>(HttpStatusCode.OK, sectorMappings);
        //    });
        //}
    }
}
