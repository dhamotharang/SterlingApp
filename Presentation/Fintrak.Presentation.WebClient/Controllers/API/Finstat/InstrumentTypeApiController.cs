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
    [RoutePrefix("api/instrumenttype")]
    [UsesDisposableService]
    public class InstrumentTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InstrumentTypeApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateinstrumentType")]
        public HttpResponseMessage UpdateInstrumentType(HttpRequestMessage request, [FromBody]InstrumentType instrumentTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var instrumentType = _FinstatService.UpdateInstrumentType(instrumentTypeModel);

                return request.CreateResponse<InstrumentType>(HttpStatusCode.OK, instrumentType);
            });
        }

        [HttpPost]
        [Route("deleteinstrumentType")]
        public HttpResponseMessage DeleteInstrumentType(HttpRequestMessage request, [FromBody]int instrumentTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InstrumentType instrumentType = _FinstatService.GetInstrumentType(instrumentTypeId);

                if (instrumentType != null)
                {
                    _FinstatService.DeleteInstrumentType(instrumentTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No instrumentType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinstrumentType/{instrumentTypeId}")]
        public HttpResponseMessage GetInstrumentType(HttpRequestMessage request,int instrumentTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InstrumentType instrumentType = _FinstatService.GetInstrumentType(instrumentTypeId);

                // notice no need to create a seperate model object since InstrumentType entity will do just fine
                response = request.CreateResponse<InstrumentType>(HttpStatusCode.OK, instrumentType);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinstrumentTypes")]
        public HttpResponseMessage GetAvailableInstrumentTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InstrumentTypeData[] instrumentTypes = _FinstatService.GetAllInstrumentTypes();

                return request.CreateResponse<InstrumentTypeData[]>(HttpStatusCode.OK, instrumentTypes);
            });
        }
    }
}
