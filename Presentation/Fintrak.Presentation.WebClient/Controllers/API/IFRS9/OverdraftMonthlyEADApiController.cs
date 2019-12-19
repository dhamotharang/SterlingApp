using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/overdraftmonthlyead")]
    [UsesDisposableService]
    public class OverdraftMonthlyEADApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OverdraftMonthlyEADApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateoverdraftmonthlyead")]
        public HttpResponseMessage UpdateOverdraftMonthlyEAD(HttpRequestMessage request, [FromBody]OverdraftMonthlyEAD overdraftmonthlyeadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var overdraftmonthlyead = _IFRS9Service.UpdateOverdraftMonthlyEAD(overdraftmonthlyeadModel);

                return request.CreateResponse<OverdraftMonthlyEAD>(HttpStatusCode.OK, overdraftmonthlyead);
            });
        }

        [HttpPost]
        [Route("deleteoverdraftmonthlyead")]
        public HttpResponseMessage DeleteOverdraftMonthlyEAD(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
               OverdraftMonthlyEAD overdraftmonthlyead = _IFRS9Service.GetOverdraftMonthlyEAD(Id);

                if (overdraftmonthlyead != null)
                {
                    _IFRS9Service.DeleteOverdraftMonthlyEAD(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No overdraftmonthlyead found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getoverdraftmonthlyead/{Id}")]
        public HttpResponseMessage GetOverdraftMonthlyEAD(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

               OverdraftMonthlyEAD overdraftmonthlyead = _IFRS9Service.GetOverdraftMonthlyEAD(Id);

                // notice no need to create a seperate model object since OverdraftMonthlyEADr entity will do just fine
                response = request.CreateResponse<OverdraftMonthlyEAD>(HttpStatusCode.OK, overdraftmonthlyead);

                return response;
            });
        }

        [HttpGet]
        [Route("getalloverdraftmonthlyeadbysearch/{searchParam}")]
        public HttpResponseMessage GetOverdraftMonthlyEADBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                OverdraftMonthlyEAD[] overdraftmonthlyead = _IFRS9Service.GetOverdraftMonthlyEADBySearch(searchParam);

                return request.CreateResponse<OverdraftMonthlyEAD[]>(HttpStatusCode.OK, overdraftmonthlyead);
            });
        }

    [HttpGet]
    [Route("getalloverdraftmonthlyead/{defaultcount}")]
    public HttpResponseMessage GetAllOverdraftMonthlyEAD(HttpRequestMessage request,int defaultcount)
    {
      return GetHttpResponse(request, () =>
      {
       OverdraftMonthlyEAD[] overdraftmonthlyead = _IFRS9Service.GetAllOverdraftMonthlyEAD(defaultcount);

        return request.CreateResponse<OverdraftMonthlyEAD[]>(HttpStatusCode.OK, overdraftmonthlyead);
      });
    }


  }
}
