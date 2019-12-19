using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Entities;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/checkdataavailability")]
    [UsesDisposableService]
    public class CheckDataAvalabilityApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CheckDataAvalabilityApiController(IExtractionProcessService extractionProcessService)
        {
            _ExtractionProcessService = extractionProcessService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

       
        [HttpGet]
        [Route("availabledatacheck")]
        public HttpResponseMessage GetDataAvailablity(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CheckDataAvailability[] checkDataAvailability = _ExtractionProcessService.GetAllDataAvailability();

                return request.CreateResponse<CheckDataAvailability[]>(HttpStatusCode.OK, checkDataAvailability);
            });
        }

        [HttpPost]
        [Route("checkstagingbyrundate")]
        public HttpResponseMessage CheckStaging(HttpRequestMessage request, [FromBody] MaturityParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _ExtractionProcessService.CheckDataAvailabilitybyRunDate(param.Date);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

    }
}
