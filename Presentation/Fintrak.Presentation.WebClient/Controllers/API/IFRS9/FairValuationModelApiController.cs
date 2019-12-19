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
    [RoutePrefix("api/fairvaluationmodel")]
    [UsesDisposableService]
    public class FairValuationModelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FairValuationModelApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatefairvaluationmodel")]
        public HttpResponseMessage UpdateFairValuationModel(HttpRequestMessage request, [FromBody]FairValuationModel fairvaluationmodelModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fairvaluationmodel = _IFRS9Service.UpdateFairValuationModel(fairvaluationmodelModel);

                return request.CreateResponse<FairValuationModel>(HttpStatusCode.OK, fairvaluationmodel);
            });
        }

        [HttpPost]
        [Route("deletefairvaluationmodel")]
        public HttpResponseMessage DeleteFairValuationModel(HttpRequestMessage request, [FromBody]int fairvaluationmodelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FairValuationModel fairvaluationmodel = _IFRS9Service.GetFairValuationModel(fairvaluationmodelId);

                if (fairvaluationmodel != null)
                {
                    _IFRS9Service.DeleteFairValuationModel(fairvaluationmodelId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No fairvaluationmodel found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getfairvaluationmodel/{fairvaluationmodelId}")]
        public HttpResponseMessage GetFairValuationModel(HttpRequestMessage request,int fairvaluationmodelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FairValuationModel fairvaluationmodel = _IFRS9Service.GetFairValuationModel(fairvaluationmodelId);

                // notice no need to create a seperate model object since FairValuationModel entity will do just fine
                response = request.CreateResponse<FairValuationModel>(HttpStatusCode.OK, fairvaluationmodel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefairvaluationmodels")]
        public HttpResponseMessage GetAvailableFairValuationModels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FairValuationModel[] fairvaluationmodels = _IFRS9Service.GetAllFairValuationModels();

                return request.CreateResponse<FairValuationModel[]>(HttpStatusCode.OK, fairvaluationmodels);
            });
        }
    }
}
