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
    [RoutePrefix("api/incomeneaglsbu")]
    [UsesDisposableService]
    public class IncomeNEAGLSBUApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IncomeNEAGLSBUApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateincomeneaglsbu")]
        public HttpResponseMessage UpdateIncomeNEAGLSBU(HttpRequestMessage request, [FromBody]IncomeNEAGLSBU incomeNEAGLSBUModel)
        {
            return GetHttpResponse(request, () =>
            {
                var incomeNEAGLSBU = _MPROPEXService.UpdateIncomeNEAGLSBU(incomeNEAGLSBUModel);

                return request.CreateResponse<IncomeNEAGLSBU>(HttpStatusCode.OK, incomeNEAGLSBU);
            });
        }

        [HttpPost]
        [Route("deleteincomeneaglsbu")]
        public HttpResponseMessage DeleteIncomeNEAGLSBU(HttpRequestMessage request, [FromBody]int incomeNEAGLSBUId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IncomeNEAGLSBU incomeNEAGLSBU = _MPROPEXService.GetIncomeNEAGLSBU(incomeNEAGLSBUId);

                if (incomeNEAGLSBU != null)
                {
                    _MPROPEXService.DeleteIncomeNEAGLSBU(incomeNEAGLSBUId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getincomeneaglsbu/{incomeneaglsbuId}")]
        public HttpResponseMessage GetIncomeNEAGLSBU(HttpRequestMessage request, int incomeNEAGLSBUId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IncomeNEAGLSBU incomeNEAGLSBU = _MPROPEXService.GetIncomeNEAGLSBU(incomeNEAGLSBUId);

                // notice no need to create a seperate model object since IncomeNEAGLSBU entity will do just fine
                response = request.CreateResponse<IncomeNEAGLSBU>(HttpStatusCode.OK, incomeNEAGLSBU);

                return response;
            });
        }

        [HttpGet]
        [Route("availableincomeneaglsbu")]
        public HttpResponseMessage GetAvailableIncomeNEAGLSBU(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IncomeNEAGLSBU[] incomeNEAGLSBU = _MPROPEXService.GetAllIncomeNEAGLSBUs();


                return request.CreateResponse<IncomeNEAGLSBU[]>(HttpStatusCode.OK, incomeNEAGLSBU);
            });
        }
    }
}
