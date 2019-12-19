using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.CDQM.Contracts;
using Fintrak.Client.CDQM.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cdqmaddress")]
    [UsesDisposableService]
    public class CDQMAddressApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMAddressApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmaddress")]
        public HttpResponseMessage UpdateCDQMAddress(HttpRequestMessage request, [FromBody]CDQMAddress cdqmaddressModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmaddress = _CDQMService.UpdateCDQMAddress(cdqmaddressModel);

                return request.CreateResponse<CDQMAddress>(HttpStatusCode.OK, cdqmaddress);
            });
        }

        [HttpPost]
        [Route("deletecdqmAddress")]
        public HttpResponseMessage DeleteCDQMAddress(HttpRequestMessage request, [FromBody]int cdqmAddressId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMAddress cdqmAddress = _CDQMService.GetCDQMAddress(cdqmAddressId);

                if (cdqmAddress != null)
                {
                    _CDQMService.DeleteCDQMAddress(cdqmAddressId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmAddress found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmAddress/{cdqmAddressId}")]
        public HttpResponseMessage GetCDQMAddress(HttpRequestMessage request, int cdqmAddressId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMAddress cdqmAddress = _CDQMService.GetCDQMAddress(cdqmAddressId);

                // notice no need to create a seperate model object since CDQMAddress entity will do just fine
                response = request.CreateResponse<CDQMAddress>(HttpStatusCode.OK, cdqmAddress);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmAddresses")]
        public HttpResponseMessage GetAvailableCDQMAddresses(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMAddress[] cdqmAddresses = _CDQMService.GetAllCDQMAddresses();

                return request.CreateResponse<CDQMAddress[]>(HttpStatusCode.OK, cdqmAddresses);
            });
        }
    }
}
