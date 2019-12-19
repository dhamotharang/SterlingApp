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
    [RoutePrefix("api/eclinputretail")]
    [UsesDisposableService]
    public class eclinputretailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public eclinputretailApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateeclinputretail")]
        public HttpResponseMessage Updateeclinputretail(HttpRequestMessage request, [FromBody]ECLInputRetail eclinputretailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var eclinputretail = _IFRS9Service.UpdatEclInputRetail(eclinputretailModel);

                return request.CreateResponse<ECLInputRetail>(HttpStatusCode.OK, eclinputretail);
            });
        }

        [HttpPost]
        [Route("deleteeclinputretail")]
        public HttpResponseMessage Deleteeclinputretail(HttpRequestMessage request, [FromBody]int eclinputretailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
               ECLInputRetail eclinputretail = _IFRS9Service.GetEclInputRetail(eclinputretailId);

                if (eclinputretail != null)
                {
                    _IFRS9Service.DeleteEclInputRetail(eclinputretailId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No eclinputretail found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("geteclinputretail/{eclinputretailId}")]
        public HttpResponseMessage Geteclinputretail(HttpRequestMessage request,int eclinputretailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ECLInputRetail eclinputretail = _IFRS9Service.GetEclInputRetail(eclinputretailId);

                // notice no need to create a seperate model object since eclinputretail entity will do just fine
                response = request.CreateResponse<ECLInputRetail>(HttpStatusCode.OK, eclinputretail);

                return response;
            });
        }

        [HttpGet]
        [Route("availableeclinputretails")]
        public HttpResponseMessage GetAvailableeclinputretails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ECLInputRetail[] eclinputretails = _IFRS9Service.GetAllEclInputRetails();

                return request.CreateResponse<ECLInputRetail[]>(HttpStatusCode.OK, eclinputretails);
            });
        }

        [HttpGet]
        [Route("geteclinputretailsbyrefno/{refNo}")]
        public HttpResponseMessage GetAvailableeclinputretailsBySource(HttpRequestMessage request, string refNo)
        {
            return GetHttpResponse(request, () =>
            {
                ECLInputRetail[] eclinputretails = _IFRS9Service.GetAllEclInputRetailsByRefno(refNo);

                return request.CreateResponse<ECLInputRetail[]>(HttpStatusCode.OK, eclinputretails);

            });
        }
    }
}
