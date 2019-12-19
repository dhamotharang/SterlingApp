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
    [RoutePrefix("api/cdqmcustomermis")]
    [UsesDisposableService]
    public class CDQMCustomerMISApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMCustomerMISApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmcustomermis")]
        public HttpResponseMessage UpdateCDQMCustomerMIS(HttpRequestMessage request, [FromBody]CDQMCustomerMIS cdqmCustomerMISModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmCustomerMIS = _CDQMService.UpdateCDQMCustomerMIS(cdqmCustomerMISModel);

                return request.CreateResponse<CDQMCustomerMIS>(HttpStatusCode.OK, cdqmCustomerMIS);
            });
        }

        [HttpPost]
        [Route("deletecdqmCustomerMIS")]
        public HttpResponseMessage DeleteCDQMCustomerMIS(HttpRequestMessage request, [FromBody]int cdqmCustomerMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMCustomerMIS cdqmCustomerMIS = _CDQMService.GetCDQMCustomerMIS(cdqmCustomerMISId);

                if (cdqmCustomerMIS != null)
                {
                    _CDQMService.DeleteCDQMCustomerMIS(cdqmCustomerMISId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmCustomerMIS found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmCustomerMIS/{cdqmCustomerMISId}")]
        public HttpResponseMessage GetCDQMCustomerMIS(HttpRequestMessage request, int cdqmCustomerMISId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMCustomerMIS cdqmCustomerMIS = _CDQMService.GetCDQMCustomerMIS(cdqmCustomerMISId);

                // notice no need to create a seperate model object since CDQMCustomerMIS entity will do just fine
                response = request.CreateResponse<CDQMCustomerMIS>(HttpStatusCode.OK, cdqmCustomerMIS);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmCustomerMISs")]
        public HttpResponseMessage GetAvailableCDQMCustomerMISs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMCustomerMIS[] cdqmCustomerMISs = _CDQMService.GetAllCDQMCustomerMIS();

                return request.CreateResponse<CDQMCustomerMIS[]>(HttpStatusCode.OK, cdqmCustomerMISs);
            });
        }
    }
}
