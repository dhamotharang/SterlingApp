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
    [RoutePrefix("api/ifrsmonthlyead")]
    [UsesDisposableService]
    public class IfrsMonthlyEADApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsMonthlyEADApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsmonthlyead")]
        public HttpResponseMessage UpdateIfrsMonthlyEAD(HttpRequestMessage request, [FromBody]IfrsMonthlyEAD ifrsmonthlyeadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsmonthlyead = _IFRS9Service.UpdateIfrsMonthlyEAD(ifrsmonthlyeadModel);

                return request.CreateResponse<IfrsMonthlyEAD>(HttpStatusCode.OK, ifrsmonthlyead);
            });
        }

        [HttpPost]
        [Route("deleteifrsmonthlyead")]
        public HttpResponseMessage DeleteIfrsMonthlyEAD(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
               IfrsMonthlyEAD ifrsmonthlyead = _IFRS9Service.GetIfrsMonthlyEAD(Id);

                if (ifrsmonthlyead != null)
                {
                    _IFRS9Service.DeleteIfrsMonthlyEAD(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsmonthlyead found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsmonthlyead/{Id}")]
        public HttpResponseMessage GetIfrsMonthlyEAD(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

               IfrsMonthlyEAD ifrsmonthlyead = _IFRS9Service.GetIfrsMonthlyEAD(Id);

                // notice no need to create a seperate model object since IfrsMonthlyEADr entity will do just fine
                response = request.CreateResponse<IfrsMonthlyEAD>(HttpStatusCode.OK, ifrsmonthlyead);

                return response;
            });
        }

        [HttpGet]
        [Route("getallifrsmonthlyeadbysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsMonthlyEADBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsMonthlyEAD[] ifrsmonthlyead = _IFRS9Service.GetIfrsMonthlyEADBySearch(searchParam);

                return request.CreateResponse<IfrsMonthlyEAD[]>(HttpStatusCode.OK, ifrsmonthlyead);
            });
        }

    [HttpGet]
    [Route("getallifrsmonthlyead/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsMonthlyEAD(HttpRequestMessage request,int defaultcount)
    {
      return GetHttpResponse(request, () =>
      {
       IfrsMonthlyEAD[] ifrsmonthlyead = _IFRS9Service.GetAllIfrsMonthlyEAD(defaultcount);

        return request.CreateResponse<IfrsMonthlyEAD[]>(HttpStatusCode.OK, ifrsmonthlyead);
      });
    }


  }
}
