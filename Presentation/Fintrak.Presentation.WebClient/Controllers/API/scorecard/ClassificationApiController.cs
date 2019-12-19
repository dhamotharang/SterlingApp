using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Scorecard.Contracts;
using Fintrak.Client.Scorecard.Entities;
using CodeEntities = Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/scdkpiclassification")]
    [UsesDisposableService]
    public class KPIClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public KPIClassificationApiController(IScorecardService scoreCardService)
        {
            _ScorecardService = scoreCardService;
        }

        IScorecardService _ScorecardService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ScorecardService);
        }

        [HttpPost]
        [Route("updateclassification")]
        public HttpResponseMessage UpdateClassification(HttpRequestMessage request, [FromBody]SCDKPIClassification classificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var classification = _ScorecardService.UpdateSCDKPIClassification(classificationModel);

                return request.CreateResponse<SCDKPIClassification>(HttpStatusCode.OK, classification);
            });
        }

        [HttpPost]
        [Route("deleteclassification")]
        public HttpResponseMessage DeleteClassification(HttpRequestMessage request, [FromBody]int classificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SCDKPIClassification classification = _ScorecardService.GetSCDKPIClassification(classificationId);

                if (classification != null)
                {
                    _ScorecardService.DeleteSCDKPIClassification(classificationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No classification found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getclassification/{classificationId}")]
        public HttpResponseMessage GetClassification(HttpRequestMessage request, int classificationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SCDKPIClassification classification = _ScorecardService.GetSCDKPIClassification(classificationId);

                // notice no need to create a seperate model object since Classification entity will do just fine
                response = request.CreateResponse<SCDKPIClassification>(HttpStatusCode.OK, classification);

                return response;
            });
        }

        [HttpGet]
        [Route("availableclassification")]
        public HttpResponseMessage GetAvailableClassifications(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SCDKPIClassificationData[] classification = _ScorecardService.GetAllSCDKPIClassifications();

                return request.CreateResponse<SCDKPIClassificationData[]>(HttpStatusCode.OK, classification);
            });
        }
    }
}
