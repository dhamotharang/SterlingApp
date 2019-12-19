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
    [RoutePrefix("api/sector")]
    [UsesDisposableService]
    public class SectorApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SectorApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatesector")]
        public HttpResponseMessage UpdateSector(HttpRequestMessage request, [FromBody]Sector sectorModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sector = _IFRS9Service.UpdateSector(sectorModel);

                return request.CreateResponse<Sector>(HttpStatusCode.OK, sector);
            });
        }

        [HttpPost]
        [Route("deletesector")]
        public HttpResponseMessage DeleteSector(HttpRequestMessage request, [FromBody]int sectorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Sector sector = _IFRS9Service.GetSector(sectorId);

                if (sector != null)
                {
                    _IFRS9Service.DeleteSector(sectorId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No sector found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsector/{sectorId}")]
        public HttpResponseMessage GetSector(HttpRequestMessage request,int sectorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Sector sector = _IFRS9Service.GetSector(sectorId);

                // notice no need to create a seperate model object since Sector entity will do just fine
                response = request.CreateResponse<Sector>(HttpStatusCode.OK, sector);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesectors")]
        public HttpResponseMessage GetAvailableSectors(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Sector[] sectors = _IFRS9Service.GetAllSectors();

                return request.CreateResponse<Sector[]>(HttpStatusCode.OK, sectors);
            });
        }

        [HttpGet]
        [Route("getsectorsbysource/{source}")]
        public HttpResponseMessage GetAvailableSectorsBySource(HttpRequestMessage request, string Source)
        {
            return GetHttpResponse(request, () =>
            {
                Sector[] sectors = _IFRS9Service.GetSectorBySource(Source);

                return request.CreateResponse<Sector[]>(HttpStatusCode.OK, sectors);
            });
        }
    }
}
