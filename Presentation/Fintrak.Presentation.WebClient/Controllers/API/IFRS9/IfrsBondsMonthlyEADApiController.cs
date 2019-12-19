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
    [RoutePrefix("api/ifrsbondsmonthlyead")]
    [UsesDisposableService]
    public class IfrsBondsMonthlyEADApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsBondsMonthlyEADApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsbondsmonthlyead")]
        public HttpResponseMessage UpdateIfrsBondsMonthlyEAD(HttpRequestMessage request, [FromBody]IfrsBondsMonthlyEAD ifrsbondsmonthlyeadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsbondsmonthlyead = _IFRS9Service.UpdateIfrsBondsMonthlyEAD(ifrsbondsmonthlyeadModel);

                return request.CreateResponse<IfrsBondsMonthlyEAD>(HttpStatusCode.OK, ifrsbondsmonthlyead);
            });
        }

        [HttpPost]
        [Route("deleteifrsbondsmonthlyead")]
        public HttpResponseMessage DeleteIfrsBondsMonthlyEAD(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
               IfrsBondsMonthlyEAD ifrsbondsmonthlyead = _IFRS9Service.GetIfrsBondsMonthlyEAD(Id);

                if (ifrsbondsmonthlyead != null)
                {
                    _IFRS9Service.DeleteIfrsBondsMonthlyEAD(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsbondsmonthlyead found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getallifrsbondsmonthlyeadbysearch/{searchParam}")]
        public HttpResponseMessage GetIfrsMonthlyEADBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsBondsMonthlyEAD[] ifrsbondmonthlyead = _IFRS9Service.GetIfrsBondMonthlyEADBySearch(searchParam).OrderBy(c=>c.date_pmt).ToArray();

                return request.CreateResponse<IfrsBondsMonthlyEAD[]>(HttpStatusCode.OK, ifrsbondmonthlyead);
            });
        }

        [HttpGet]
        [Route("getifrsbondsmonthlyead/{Id}")]
        public HttpResponseMessage GetIfrsBondsMonthlyEAD(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

               IfrsBondsMonthlyEAD ifrsbondsmonthlyead = _IFRS9Service.GetIfrsBondsMonthlyEAD(Id);

                // notice no need to create a seperate model object since IfrsBondsMonthlyEADr entity will do just fine
                response = request.CreateResponse<IfrsBondsMonthlyEAD>(HttpStatusCode.OK, ifrsbondsmonthlyead);

                return response;
            });
        }


    [HttpGet]
    [Route("getallifrsbondsmonthlyead/{defaultcount}")]
    public HttpResponseMessage GetAllIfrsBondsMonthlyEAD(HttpRequestMessage request,int defaultcount)
    {
      return GetHttpResponse(request, () =>
      {
       IfrsBondsMonthlyEAD[] ifrsbondsmonthlyead = _IFRS9Service.GetAllIfrsBondsMonthlyEAD(defaultcount).OrderBy(c=>c.RefNo).ThenBy(c=>c.date_pmt).ToArray();

        return request.CreateResponse<IfrsBondsMonthlyEAD[]>(HttpStatusCode.OK, ifrsbondsmonthlyead);
      });
    }


  }
}
