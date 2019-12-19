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
    [RoutePrefix("api/opexbusinessrule")]
    [UsesDisposableService]
    public class OpexBusinessRuleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexBusinessRuleApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexbusinessrule")]
        public HttpResponseMessage UpdateOpexBusinessRule(HttpRequestMessage request, [FromBody]OpexBusinessRule opexBusinessRuleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexBusinessRule = _MPROPEXService.UpdateOpexBusinessRule(opexBusinessRuleModel);

                return request.CreateResponse<OpexBusinessRule>(HttpStatusCode.OK, opexBusinessRule);
            });
        }

        [HttpPost]
        [Route("deleteopexBusinessRule")]
        public HttpResponseMessage DeleteOpexBusinessRule(HttpRequestMessage request, [FromBody]int opexBusinessRuleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexBusinessRule opexBusinessRule = _MPROPEXService.GetOpexBusinessRule(opexBusinessRuleId);

                if (opexBusinessRule != null)
                {
                    _MPROPEXService.DeleteOpexBusinessRule(opexBusinessRuleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexBusinessRule/{opexBusinessRuleId}")]
        public HttpResponseMessage GetOpexBusinessRule(HttpRequestMessage request, int opexBusinessRuleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexBusinessRule opexBusinessRule = _MPROPEXService.GetOpexBusinessRule(opexBusinessRuleId);

                // notice no need to create a seperate model object since OpexBusinessRule entity will do just fine
                response = request.CreateResponse<OpexBusinessRule>(HttpStatusCode.OK, opexBusinessRule);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexBusinessRule")]
        public HttpResponseMessage GetAvailableOpexBusinessRule(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexBusinessRule[] opexBusinessRule = _MPROPEXService.GetAllOpexBusinessRules();


                return request.CreateResponse<OpexBusinessRule[]>(HttpStatusCode.OK, opexBusinessRule);
            });
        }
    }
}
