using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexbasismapping")]
    [UsesDisposableService]  
    public class OpexBasisMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexBasisMappingApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexBasisMapping")]
        public HttpResponseMessage UpdateOpexBasisMapping(HttpRequestMessage request, [FromBody]OpexBasisMapping opexBasisMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexBasisMapping = _MPROPEXService.UpdateOpexBasisMapping(opexBasisMappingModel);

                return request.CreateResponse<OpexBasisMapping>(HttpStatusCode.OK, opexBasisMapping);
            });
        }

        [HttpPost]
        [Route("deleteopexBasisMapping")]
        public HttpResponseMessage DeleteOpexBasisMapping(HttpRequestMessage request, [FromBody]int opexBasisMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexBasisMapping opexBasisMapping = _MPROPEXService.GetOpexBasisMapping(opexBasisMappingId);

                if (opexBasisMapping != null)
                {
                    _MPROPEXService.DeleteOpexBasisMapping(opexBasisMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Expense Basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexBasisMapping/{opexBasisMappingId}")]
        public HttpResponseMessage GetOpexBasisMapping(HttpRequestMessage request, int opexBasisMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexBasisMapping opexBasisMapping = _MPROPEXService.GetOpexBasisMapping(opexBasisMappingId);

                // notice no need to create a seperate model object since OpexBasisMapping entity will do just fine
                response = request.CreateResponse<OpexBasisMapping>(HttpStatusCode.OK, opexBasisMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexBasisMapping")]
        public HttpResponseMessage GetAvailableOpexBasisMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexBasisMapping[] opexBasisMapping = _MPROPEXService.GetAllOpexBasisMappings();


                return request.CreateResponse<OpexBasisMapping[]>(HttpStatusCode.OK, opexBasisMapping);
            });
        }
    }
}
