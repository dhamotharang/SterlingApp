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
    [RoutePrefix("api/incomecashcentrecode")]
    [UsesDisposableService]
    public class IncomeCashCentreCodeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeCashCentreCodeApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateincomecashcentrecode")]
        public HttpResponseMessage UpdateIncomeCashCentreCode(HttpRequestMessage request, [FromBody]IncomeCashCentreCode incomeCashCentreCodeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeCashCentreCode = _MPROPEXService.UpdateIncomeCashCentreCode(incomeCashCentreCodeModel);

                return request.CreateResponse<IncomeCashCentreCode>(HttpStatusCode.OK, incomeCashCentreCode);
            });
        }

        [HttpPost]
        [Route("deleteincomecashcentrecode")]
        public HttpResponseMessage DeleteIncomeCashCentreCode(HttpRequestMessage request, [FromBody]int incomeCashCentreCodeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeCashCentreCode incomeCashCentreCode = _MPROPEXService.GetIncomeCashCentreCode(incomeCashCentreCodeId);

                if (incomeCashCentreCode != null)
                {
                    _MPROPEXService.DeleteIncomeCashCentreCode(incomeCashCentreCodeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomecashcentrecode/{incomecashcentrecodeId}")]
        public HttpResponseMessage GetIncomeCashCentreCode(HttpRequestMessage request, int incomeCashCentreCodeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeCashCentreCode incomeCashCentreCode = _MPROPEXService.GetIncomeCashCentreCode(incomeCashCentreCodeId);

                // notice no need to create a seperate model object since IncomeCashCentreCode entity will do just fine
                response = request.CreateResponse<IncomeCashCentreCode>(HttpStatusCode.OK, incomeCashCentreCode);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomecashcentrecode")]
        public HttpResponseMessage GetAvailableIncomeCashCentreCode(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeCashCentreCode[] incomeCashCentreCode = _MPROPEXService.GetAllIncomeCashCentreCodes();


                return request.CreateResponse<IncomeCashCentreCode[]>(HttpStatusCode.OK, incomeCashCentreCode);
            });
        }
    }
}
