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
    [RoutePrefix("api/cdqmmerchant")]
    [UsesDisposableService]
    public class CDQMMerchantApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMMerchantApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmmerchant")]
        public HttpResponseMessage UpdateCDQMMerchant(HttpRequestMessage request, [FromBody]CDQMMerchant cdqmMerchantModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmMerchant = _CDQMService.UpdateCDQMMerchant(cdqmMerchantModel);

                return request.CreateResponse<CDQMMerchant>(HttpStatusCode.OK, cdqmMerchant);
            });
        }

        [HttpPost]
        [Route("deletecdqmMerchant")]
        public HttpResponseMessage DeleteCDQMMerchant(HttpRequestMessage request, [FromBody]int cdqmMerchantId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMMerchant cdqmMerchant = _CDQMService.GetCDQMMerchant(cdqmMerchantId);

                if (cdqmMerchant != null)
                {
                    _CDQMService.DeleteCDQMMerchant(cdqmMerchantId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmMerchant found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmMerchant/{cdqmMerchantId}")]
        public HttpResponseMessage GetCDQMMerchant(HttpRequestMessage request, int cdqmMerchantId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMMerchant cdqmMerchant = _CDQMService.GetCDQMMerchant(cdqmMerchantId);

                // notice no need to create a seperate model object since CDQMMerchant entity will do just fine
                response = request.CreateResponse<CDQMMerchant>(HttpStatusCode.OK, cdqmMerchant);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmMerchants")]
        public HttpResponseMessage GetAvailableCDQMMerchants(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMMerchant[] cdqmMerchants = _CDQMService.GetAllCDQMMerchants();

                return request.CreateResponse<CDQMMerchant[]>(HttpStatusCode.OK, cdqmMerchants);
            });
        }
    }
}
