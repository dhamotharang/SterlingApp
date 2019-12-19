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
    [RoutePrefix("api/eclcalculationmodel")]
    [UsesDisposableService]
    public class EclCalculationModelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public EclCalculationModelApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateeclcalculationmodel")]
        public HttpResponseMessage UpdateEclCalculationModel(HttpRequestMessage request, [FromBody]EclCalculationModel eclcalculationmodelModel)
        {
            return GetHttpResponse(request, () =>
            {
                var eclcalculationmodel = _IFRS9Service.UpdateEclCalculationModel(eclcalculationmodelModel);

                return request.CreateResponse<EclCalculationModel>(HttpStatusCode.OK, eclcalculationmodel);
            });
        }

        [HttpPost]
        [Route("deleteeclcalculationmodel")]
        public HttpResponseMessage DeleteEclCalculationModel(HttpRequestMessage request, [FromBody]int eclcalculationmodelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                EclCalculationModel eclcalculationmodel = _IFRS9Service.GetEclCalculationModel(eclcalculationmodelId);

                if (eclcalculationmodel != null)
                {
                    _IFRS9Service.DeleteEclCalculationModel(eclcalculationmodelId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No eclcalculationmodel found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("geteclcalculationmodel/{eclcalculationmodelId}")]
        public HttpResponseMessage GetEclCalculationModel(HttpRequestMessage request,int eclcalculationmodelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                EclCalculationModel eclcalculationmodel = _IFRS9Service.GetEclCalculationModel(eclcalculationmodelId);

                // notice no need to create a seperate model object since EclCalculationModel entity will do just fine
                response = request.CreateResponse<EclCalculationModel>(HttpStatusCode.OK, eclcalculationmodel);

                return response;
            });
        }

        [HttpGet]
        [Route("availableeclcalculationmodels")]
        public HttpResponseMessage GetAvailableEclCalculationModels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                EclCalculationModel[] eclcalculationmodels = _IFRS9Service.GetAllEclCalculationModels();

                return request.CreateResponse<EclCalculationModel[]>(HttpStatusCode.OK, eclcalculationmodels);
            });
        }
    }
}
