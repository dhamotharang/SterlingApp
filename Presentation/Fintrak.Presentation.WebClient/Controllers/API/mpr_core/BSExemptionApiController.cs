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
    [RoutePrefix("api/bsexemption")]
    [UsesDisposableService]
    public class BSExemptionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BSExemptionApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatebsExemption")]
        public HttpResponseMessage UpdateBSExemption(HttpRequestMessage request, [FromBody]BSExemption bsExemptionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bsExemption = _MPRCoreService.UpdateBSExemption(bsExemptionModel);

                return request.CreateResponse<BSExemption>(HttpStatusCode.OK, bsExemption);
            });
        }


        [HttpPost]
        [Route("deletebsExemption")]
        public HttpResponseMessage DeleteBSExemption(HttpRequestMessage request, [FromBody]int bsExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BSExemption bsExemption = _MPRCoreService.GetBSExemption(bsExemptionId);

                if (bsExemption != null)
                {
                    _MPRCoreService.DeleteBSExemption(bsExemptionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No bsExemption found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getbsExemption/{bsExemptionId}")]
        public HttpResponseMessage GetBSExemption(HttpRequestMessage request, int bsExemptionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BSExemption bsExemption = _MPRCoreService.GetBSExemption(bsExemptionId);

                // notice no need to create a seperate model object since BSExemption entity will do just fine
                response = request.CreateResponse<BSExemption>(HttpStatusCode.OK, bsExemption);

                return response;
            });
        }


        [HttpGet]
        [Route("availablebsExemptions")]
        public HttpResponseMessage GetAvailableBSExemptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BSExemption[] bsExemptions = _MPRCoreService.GetAllBSExemptions();

                return request.CreateResponse<BSExemption[]>(HttpStatusCode.OK, bsExemptions);
            });
        }
    }
}
