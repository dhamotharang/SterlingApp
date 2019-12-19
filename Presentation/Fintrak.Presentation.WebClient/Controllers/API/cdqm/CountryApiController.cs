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
    [RoutePrefix("api/cdqmcountry")]
    [UsesDisposableService]
    public class CDQMCountryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMCountryApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmcountry")]
        public HttpResponseMessage UpdateCDQMCountry(HttpRequestMessage request, [FromBody]CDQMCountry cdqmCountryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmCountry = _CDQMService.UpdateCDQMCountry(cdqmCountryModel);

                return request.CreateResponse<CDQMCountry>(HttpStatusCode.OK, cdqmCountry);
            });
        }

        [HttpPost]
        [Route("deletecdqmCountry")]
        public HttpResponseMessage DeleteCDQMCountry(HttpRequestMessage request, [FromBody]int cdqmCountryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMCountry cdqmCountry = _CDQMService.GetCDQMCountry(cdqmCountryId);

                if (cdqmCountry != null)
                {
                    _CDQMService.DeleteCDQMCountry(cdqmCountryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmCountry found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmCountry/{cdqmCountryId}")]
        public HttpResponseMessage GetCDQMCountry(HttpRequestMessage request, int cdqmCountryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMCountry cdqmCountry = _CDQMService.GetCDQMCountry(cdqmCountryId);

                // notice no need to create a seperate model object since CDQMCountry entity will do just fine
                response = request.CreateResponse<CDQMCountry>(HttpStatusCode.OK, cdqmCountry);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmCountries")]
        public HttpResponseMessage GetAvailableCDQMCountries(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMCountry[] cdqmCountries = _CDQMService.GetAllCDQMCountries();

                return request.CreateResponse<CDQMCountry[]>(HttpStatusCode.OK, cdqmCountries);
            });
        }
    }
}
