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
    [RoutePrefix("api/servicese")]
    [UsesDisposableService]
    public class ServiceseApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ServiceseApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);

        }

        [HttpPost]
        [Route("updateservicese")]
        public HttpResponseMessage UpdateServicese(HttpRequestMessage request, [FromBody]Servicese serviceseModel)
        {
            return GetHttpResponse(request, () =>
            {
                var servicese = _MPRCoreService.UpdateServices(serviceseModel);

                return request.CreateResponse<Servicese>(HttpStatusCode.OK, servicese);
            });
        }


        [HttpPost]
        [Route("deleteservicese")]
        public HttpResponseMessage DeleteServicese(HttpRequestMessage request, [FromBody]int servicesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Servicese servicese = _MPRCoreService.GetServices(servicesId);

                if (servicese != null)
                {
                    _MPRCoreService.DeleteServices(servicesId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Servicese found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("getservicese/{servicesId}")]
        public HttpResponseMessage GetServicese(HttpRequestMessage request, int servicesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Servicese servicese = _MPRCoreService.GetServices(servicesId);

                // notice no need to create a seperate model object since CaptionMapping entity will do just fine
                response = request.CreateResponse<Servicese>(HttpStatusCode.OK, servicese);

                return response;
            });
        }


        [HttpGet]
        [Route("availableservicese")]
        public HttpResponseMessage GetAvailableServicese(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Servicese[] servicese = _MPRCoreService.GetAllServices();

                return request.CreateResponse<Servicese[]>(HttpStatusCode.OK, servicese);
            });
        }
    }
}
