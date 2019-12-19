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
    [RoutePrefix("api/instrumenttypeglmap")]
    [UsesDisposableService]
    public class InstrumentTypeGLMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InstrumentTypeGLMapApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateinstrumentTypeGLMap")]
        public HttpResponseMessage UpdateInstrumentTypeGLMap(HttpRequestMessage request, [FromBody]InstrumentTypeGLMap instrumentTypeGLMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var instrumentTypeGLMap = _FinstatService.UpdateInstrumentTypeGLMap(instrumentTypeGLMapModel);

                return request.CreateResponse<InstrumentTypeGLMap>(HttpStatusCode.OK, instrumentTypeGLMap);
            });
        }

        [HttpPost]
        [Route("deleteinstrumentTypeGLMap")]
        public HttpResponseMessage DeleteInstrumentTypeGLMap(HttpRequestMessage request, [FromBody]int instrumentTypeGLMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InstrumentTypeGLMap instrumentTypeGLMap = _FinstatService.GetInstrumentTypeGLMap(instrumentTypeGLMapId);

                if (instrumentTypeGLMap != null)
                {
                    _FinstatService.DeleteInstrumentTypeGLMap(instrumentTypeGLMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No instrumentTypeGLMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinstrumentTypeGLMap/{instrumentTypeGLMapId}")]
        public HttpResponseMessage GetInstrumentTypeGLMap(HttpRequestMessage request,int instrumentTypeGLMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InstrumentTypeGLMap instrumentTypeGLMap = _FinstatService.GetInstrumentTypeGLMap(instrumentTypeGLMapId);

                // notice no need to create a seperate model object since InstrumentTypeGLMap entity will do just fine
                response = request.CreateResponse<InstrumentTypeGLMap>(HttpStatusCode.OK, instrumentTypeGLMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinstrumentTypeGLMaps")]
        public HttpResponseMessage GetAvailableInstrumentTypeGLMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InstrumentTypeGLMapData[] instrumentTypeGLMaps = _FinstatService.GetAllInstrumentTypeGLMaps();

                return request.CreateResponse<InstrumentTypeGLMapData[]>(HttpStatusCode.OK, instrumentTypeGLMaps);
            });
        }
    }
}
