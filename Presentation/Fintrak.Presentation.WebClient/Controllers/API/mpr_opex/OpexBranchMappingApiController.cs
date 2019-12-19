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
    [RoutePrefix("api/opexbranchmapping")]
    [UsesDisposableService]
    public class OpexBranchMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexBranchMappingApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexbranchmapping")]
        public HttpResponseMessage UpdateOpexBranchMapping(HttpRequestMessage request, [FromBody]OpexBranchMapping opexBranchMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexBranchMapping = _MPROPEXService.UpdateOpexBranchMapping(opexBranchMappingModel);

                return request.CreateResponse<OpexBranchMapping>(HttpStatusCode.OK, opexBranchMapping);
            });
        }

        [HttpPost]
        [Route("deleteopexbranchmapping")]
        public HttpResponseMessage DeleteOpexBranchMapping(HttpRequestMessage request, [FromBody]int opexBranchMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexBranchMapping opexBranchMapping = _MPROPEXService.GetOpexBranchMapping(opexBranchMappingId);

                if (opexBranchMapping != null)
                {
                    _MPROPEXService.DeleteOpexBranchMapping(opexBranchMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Expense Basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexbranchmapping/{opexBranchMappingId}")]
        public HttpResponseMessage GetOpexBranchMapping(HttpRequestMessage request, int opexBranchMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexBranchMapping opexBranchMapping = _MPROPEXService.GetOpexBranchMapping(opexBranchMappingId);

                // notice no need to create a seperate model object since OpexBranchMapping entity will do just fine
                response = request.CreateResponse<OpexBranchMapping>(HttpStatusCode.OK, opexBranchMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexbranchmapping")]
        public HttpResponseMessage GetAvailableOpexBranchMapping(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexBranchMapping[] opexBranchMapping = _MPROPEXService.GetAllOpexBranchMapping();


                return request.CreateResponse<OpexBranchMapping[]>(HttpStatusCode.OK, opexBranchMapping);
            });
        }
    }
}
