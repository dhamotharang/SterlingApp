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
    [RoutePrefix("api/sbu")]
    [UsesDisposableService]
    public class SbuApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SbuApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatesbu")]
        public HttpResponseMessage UpdateSbu(HttpRequestMessage request, [FromBody]Sbu sbuModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sbu = _MPRCoreService.UpdateSbu(sbuModel);

                return request.CreateResponse<Sbu>(HttpStatusCode.OK, sbu);
            });
        }


        [HttpPost]
        [Route("deletesbu")]
        public HttpResponseMessage DeleteSbu(HttpRequestMessage request, [FromBody]int sbuId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Sbu sbu = _MPRCoreService.GetSbu(sbuId);

                if (sbu != null)
                {
                    _MPRCoreService.DeleteSbu(sbuId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Sbu found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getsbu/{sbuId}")]
        public HttpResponseMessage GetSbu(HttpRequestMessage request, int sbuId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Sbu sbu = _MPRCoreService.GetSbu(sbuId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<Sbu>(HttpStatusCode.OK, sbu);

                return response;
            });
        }


        [HttpGet]
        [Route("availablesbu")]
        public HttpResponseMessage GetAvailableSbu(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Sbu[] sbu = _MPRCoreService.GetAllSbu();

                return request.CreateResponse<Sbu[]>(HttpStatusCode.OK, sbu);
            });
        }
    }
}
