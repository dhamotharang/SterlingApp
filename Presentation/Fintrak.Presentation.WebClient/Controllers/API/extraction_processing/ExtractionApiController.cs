using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/extraction")]
    [UsesDisposableService]
    public class ExtractionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExtractionApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateextraction")]
        public HttpResponseMessage UpdateExtraction(HttpRequestMessage request, [FromBody]Extraction extractionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var extraction = _ExtractionProcessService.UpdateExtraction(extractionModel);

                return request.CreateResponse<Extraction>(HttpStatusCode.OK, extraction);
            });
        }

        [HttpPost]
        [Route("deleteextraction")]
        public HttpResponseMessage DeleteExtraction(HttpRequestMessage request, [FromBody]int extractionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Extraction extraction = _ExtractionProcessService.GetExtraction(extractionId);

                if (extraction != null)
                {
                    _ExtractionProcessService.DeleteExtraction(extractionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No extraction found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getextraction/{extractionId}")]
        public HttpResponseMessage GetExtraction(HttpRequestMessage request,int extractionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Extraction extraction = _ExtractionProcessService.GetExtraction(extractionId);

                // notice no need to create a seperate model object since Extraction entity will do just fine
                response = request.CreateResponse<Extraction>(HttpStatusCode.OK, extraction);

                return response;
            });
        }

        [HttpGet]
        [Route("getextractionwithchildren/{extractionId}")]
        public HttpResponseMessage GetExtractionWithChildren(HttpRequestMessage request, int extractionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var extractionModel = new ExtractionModel();

                extractionModel.Extraction = _ExtractionProcessService.GetExtraction(extractionId);
                extractionModel.ExtractionRoles = _ExtractionProcessService.GetExtractionRoleByExtraction(extractionId);

                // notice no need to create a seperate model object since Extraction entity will do just fine
                response = request.CreateResponse<ExtractionModel>(HttpStatusCode.OK, extractionModel);

                return response;
            });
        }

        [HttpGet]
        [Route("getextractions")]
        public HttpResponseMessage GetAvailableExtractions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExtractionData[] extractions = _ExtractionProcessService.GetExtractions();

                return request.CreateResponse<ExtractionData[]>(HttpStatusCode.OK, extractions);
            });
        }

        

    }
}
