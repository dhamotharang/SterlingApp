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
    [RoutePrefix("api/cdqmtitle")]
    [UsesDisposableService]
    public class CDQMTitleApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMTitleApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmtitle")]
        public HttpResponseMessage UpdateCDQMTitle(HttpRequestMessage request, [FromBody]CDQMTitle cdqmTitleModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmTitle = _CDQMService.UpdateCDQMTitle(cdqmTitleModel);

                return request.CreateResponse<CDQMTitle>(HttpStatusCode.OK, cdqmTitle);
            });
        }

        [HttpPost]
        [Route("deletecdqmTitle")]
        public HttpResponseMessage DeleteCDQMTitle(HttpRequestMessage request, [FromBody]int cdqmTitleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CDQMTitle cdqmTitle = _CDQMService.GetCDQMTitle(cdqmTitleId);

                if (cdqmTitle != null)
                {
                    _CDQMService.DeleteCDQMTitle(cdqmTitleId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No cdqmTitle found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcdqmTitle/{cdqmTitleId}")]
        public HttpResponseMessage GetCDQMTitle(HttpRequestMessage request, int cdqmTitleId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CDQMTitle cdqmTitle = _CDQMService.GetCDQMTitle(cdqmTitleId);

                // notice no need to create a seperate model object since CDQMTitle entity will do just fine
                response = request.CreateResponse<CDQMTitle>(HttpStatusCode.OK, cdqmTitle);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecdqmTitles")]
        public HttpResponseMessage GetAvailableCDQMTitles(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMTitle[] cdqmTitles = _CDQMService.GetAllCDQMTitles();

                return request.CreateResponse<CDQMTitle[]>(HttpStatusCode.OK, cdqmTitles);
            });
        }
    }
}
