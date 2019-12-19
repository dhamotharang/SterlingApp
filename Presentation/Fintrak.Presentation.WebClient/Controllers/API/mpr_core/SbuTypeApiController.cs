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
    [RoutePrefix("api/sbutype")]
    [UsesDisposableService]
    public class SbuTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SbuTypeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updatesbutype")]
        public HttpResponseMessage UpdateSbuType(HttpRequestMessage request, [FromBody]SbuType sbuTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var sbuType = _MPRCoreService.UpdateSbuType(sbuTypeModel);

                return request.CreateResponse<SbuType>(HttpStatusCode.OK, sbuType);
            });
        }


        [HttpPost]
        [Route("deletesbutype")]
        public HttpResponseMessage DeleteSbuType(HttpRequestMessage request, [FromBody]int sbuTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SbuType sbuType = _MPRCoreService.GetSbuType(sbuTypeId);

                if (sbuType != null)
                {
                    _MPRCoreService.DeleteSbuType(sbuTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No SbuType found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getsbutype/{sbuTypeId}")]
        public HttpResponseMessage GetSbuType(HttpRequestMessage request, int sbuTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SbuType sbuType = _MPRCoreService.GetSbuType(sbuTypeId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<SbuType>(HttpStatusCode.OK, sbuType);

                return response;
            });
        }


        [HttpGet]
        [Route("availablesbutype")]
        public HttpResponseMessage GetAvailableSbuType(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SbuType[] sbuType = _MPRCoreService.GetAllSbuType();

                return request.CreateResponse<SbuType[]>(HttpStatusCode.OK, sbuType);
            });
        }
    }
}
