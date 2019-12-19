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
    [RoutePrefix("api/lifetimepdclassification")]
    [UsesDisposableService]
    public class LifeTimePDClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LifeTimePDClassificationApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatelifeTimePDClassification")]
        public HttpResponseMessage UpdateLifeTimePDClassification(HttpRequestMessage request, [FromBody]LifeTimePDClassification lifeTimePDClassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var lifeTimePDClassification = _IFRS9Service.UpdateLifeTimePDClassification(lifeTimePDClassificationModel);

                return request.CreateResponse<LifeTimePDClassification>(HttpStatusCode.OK, lifeTimePDClassification);
            });
        }

        [HttpPost]
        [Route("deletelifeTimePDClassification")]
        public HttpResponseMessage DeleteLifeTimePDClassification(HttpRequestMessage request, [FromBody]int lifeTimePDClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LifeTimePDClassification lifeTimePDClassification = _IFRS9Service.GetLifeTimePDClassification(lifeTimePDClassificationId);

                if (lifeTimePDClassification != null)
                {
                    _IFRS9Service.DeleteLifeTimePDClassification(lifeTimePDClassificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No lifeTimePDClassification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getlifeTimePDClassification/{lifeTimePDClassificationId}")]
        public HttpResponseMessage GetLifeTimePDClassification(HttpRequestMessage request,int lifeTimePDClassificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LifeTimePDClassification lifeTimePDClassification = _IFRS9Service.GetLifeTimePDClassification(lifeTimePDClassificationId);

                // notice no need to create a seperate model object since LifeTimePDClassification entity will do just fine
                response = request.CreateResponse<LifeTimePDClassification>(HttpStatusCode.OK, lifeTimePDClassification);

                return response;
            });
        }

        [HttpGet]
        [Route("availablelifeTimePDClassifications")]
        public HttpResponseMessage GetAvailableLifeTimePDClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LifeTimePDClassification[] lifeTimePDClassifications = _IFRS9Service.GetAllLifeTimePDClassifications();

                return request.CreateResponse<LifeTimePDClassification[]>(HttpStatusCode.OK, lifeTimePDClassifications);
            });
        }
    }
}
