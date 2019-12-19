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
    [RoutePrefix("api/ifrssectorccf")]
    [UsesDisposableService]
    public class IfrsSectorCCFApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsSectorCCFApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsSectorCCF")]
        public HttpResponseMessage UpdateIfrsSectorCCF(HttpRequestMessage request, [FromBody]IfrsSectorCCF IfrsSectorCCFModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsSectorCCF = _IFRS9Service.UpdateIfrsSectorCCF(IfrsSectorCCFModel);

                return request.CreateResponse<IfrsSectorCCF>(HttpStatusCode.OK, IfrsSectorCCF);
            });
        }

        [HttpPost]
        [Route("deleteIfrsSectorCCF")]
        public HttpResponseMessage DeleteIfrsSectorCCF(HttpRequestMessage request, [FromBody]int SectorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsSectorCCF IfrsSectorCCF = _IFRS9Service.GetIfrsSectorCCFById(SectorId);

                if (IfrsSectorCCF != null)
                {
                    _IFRS9Service.DeleteIfrsSectorCCF(SectorId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsSectorCCF found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsSectorCCFById/{SectorId}")]
        public HttpResponseMessage GetIfrsSectorCCFById(HttpRequestMessage request, int SectorId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsSectorCCF IfrsSectorCCF = _IFRS9Service.GetIfrsSectorCCFById(SectorId);

                // notice no need to create a seperate model object since IfrsSectorCCF entity will do just fine
                response = request.CreateResponse<IfrsSectorCCF>(HttpStatusCode.OK, IfrsSectorCCF);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllSectorsForCCF")]
        public HttpResponseMessage GetAllSectorsForCCF(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Sector[] IfrsSectorCCF = _IFRS9Service.GetAllSectorsForCCF();

                // notice no need to create a seperate model object since IfrsSectorCCF entity will do just fine
                response = request.CreateResponse<Sector[]>(HttpStatusCode.OK, IfrsSectorCCF);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsSectorCCFs/{Type}")]
        public HttpResponseMessage GetAllIfrsSectorCCFs(HttpRequestMessage request, string Type)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsSectorCCF[] IfrsSectorCCFs = _IFRS9Service.GetAllIfrsSectorCCFs(Type);

                return request.CreateResponse<IfrsSectorCCF[]>(HttpStatusCode.OK, IfrsSectorCCFs);
            });
        }
    }
}
