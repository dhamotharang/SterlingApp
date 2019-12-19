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
    [RoutePrefix("api/ifrsinvestment")]
    [UsesDisposableService]
    public class IfrsInvestmentApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsInvestmentApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateifrsinvestment")]
        public HttpResponseMessage UpdateIfrsInvestment(HttpRequestMessage request, [FromBody]IfrsInvestment ifrsinvestmentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsinvestment = _IFRS9Service.UpdateIfrsInvestment(ifrsinvestmentModel);

                return request.CreateResponse<IfrsInvestment>(HttpStatusCode.OK, ifrsinvestment);
            });
        }

        [HttpPost]
        [Route("deleteifrsinvestment")]
        public HttpResponseMessage DeleteIfrsInvestment(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsInvestment ifrsinvestment = _IFRS9Service.GetIfrsInvestment(Id);

                if (ifrsinvestment != null)
                {
                    _IFRS9Service.DeleteIfrsInvestment(Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsinvestment found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsinvestment/{Id}")]
        public HttpResponseMessage GetIfrsInvestment(HttpRequestMessage request,int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsInvestment ifrsinvestment = _IFRS9Service.GetIfrsInvestment(Id);

                // notice no need to create a seperate model object since IfrsInvestmentr entity will do just fine
                response = request.CreateResponse<IfrsInvestment>(HttpStatusCode.OK, ifrsinvestment);

                return response;
            });
        }

   
    [HttpGet]
    [Route("getallifrsinvestments")]
    public HttpResponseMessage GetAllIfrsInvestment(HttpRequestMessage request)
    {
      return GetHttpResponse(request, () =>
      {
        IfrsInvestment[] ifrsinvestment = _IFRS9Service.GetAllIfrsInvestments();

        return request.CreateResponse<IfrsInvestment[]>(HttpStatusCode.OK, ifrsinvestment);
      });
    }


  }
}
