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
    [RoutePrefix("api/fairvaluebasisexemption")]
    [UsesDisposableService]
    public class FairValueBasisExemptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FairValueBasisExemptionApiController(IFinancialInstrumentService fiService)
        {
            _FIService = fiService;
        }

        IFinancialInstrumentService _FIService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FIService);
        }

        [HttpPost]
        [Route("updatefairvaluebasisexemption")]
        public HttpResponseMessage UpdateSetup(HttpRequestMessage request, [FromBody]FairValueBasisExemption fairValueBasisExemptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var fairValueBasisExemption = _FIService.UpdateFairValueBasisExemption(fairValueBasisExemptionModel);

                return request.CreateResponse<FairValueBasisExemption>(HttpStatusCode.OK, fairValueBasisExemption);
            });
        }

        [HttpPost]
        [Route("deletefairvaluebasisexemption")]
        public HttpResponseMessage DeleteSetup(HttpRequestMessage request, [FromBody]int fairValueBasisExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FairValueBasisExemption fairValueBasisExemption = _FIService.GetFairValueBasisExemption(fairValueBasisExemptionId);

                if (fairValueBasisExemption != null)
                {
                    _FIService.DeleteFairValueBasisExemption(fairValueBasisExemptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No fairvaluebasisexemption found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getfairvaluebasisexemption/{fairValueBasisExemptionId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request,int fairValueBasisExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FairValueBasisExemption fairValueBasisExemption = _FIService.GetFairValueBasisExemption(fairValueBasisExemptionId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FairValueBasisExemption>(HttpStatusCode.OK, fairValueBasisExemption);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefairvaluebasisexemptions")]
        public HttpResponseMessage GetAvailableSetups(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FairValueBasisExemptionData[] fairValueBasisExemptions = _FIService.GetAllFairValueBasisExemptions();

                return request.CreateResponse<FairValueBasisExemptionData[]>(HttpStatusCode.OK, fairValueBasisExemptions);
            });
        }
    }
}
