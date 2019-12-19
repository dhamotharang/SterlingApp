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
    [RoutePrefix("api/eclcomparism")]
    [UsesDisposableService]
    public class ECLComparismApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ECLComparismApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateeclComparism")]
        public HttpResponseMessage UpdateECLComparism(HttpRequestMessage request, [FromBody]ECLComparism eclComparismModel)
        {
            return GetHttpResponse(request, () =>
            {
                var eclComparism = _IFRS9Service.UpdateECLComparism(eclComparismModel);

                return request.CreateResponse<ECLComparism>(HttpStatusCode.OK, eclComparism);
            });
        }

        [HttpPost]
        [Route("deleteeclComparism")]
        public HttpResponseMessage DeleteECLComparism(HttpRequestMessage request, [FromBody]int eclComparismId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ECLComparism eclComparism = _IFRS9Service.GetECLComparism(eclComparismId);

                if (eclComparism != null)
                {
                    _IFRS9Service.DeleteECLComparism(eclComparismId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No eclComparism found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("geteclComparism/{eclComparismId}")]
        public HttpResponseMessage GetECLComparism(HttpRequestMessage request,int eclComparismId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ECLComparism eclComparism = _IFRS9Service.GetECLComparism(eclComparismId);

                // notice no need to create a seperate model object since ECLComparism entity will do just fine
                response = request.CreateResponse<ECLComparism>(HttpStatusCode.OK, eclComparism);

                return response;
            });
        }

        [HttpGet]
        [Route("availableeclComparisms")]
        public HttpResponseMessage GetAvailableECLComparisms(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ECLComparism[] eclComparisms = _IFRS9Service.GetAllECLComparisms();

                return request.CreateResponse<ECLComparism[]>(HttpStatusCode.OK, eclComparisms);
            });
        }
    }
}
