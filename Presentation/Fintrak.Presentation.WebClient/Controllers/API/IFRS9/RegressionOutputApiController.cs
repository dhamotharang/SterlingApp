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
    [RoutePrefix("api/regressionoutput")]
    [UsesDisposableService]
    public class RegressionOutputApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RegressionOutputApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateregressionoutput")]
        public HttpResponseMessage UpdateRegressionOutput(HttpRequestMessage request, [FromBody]RegressionOutput regressionoutputModel)
        {
            return GetHttpResponse(request, () =>
            {
                var regressionoutput = _IFRS9Service.UpdateRegressionOutput(regressionoutputModel);

                return request.CreateResponse<RegressionOutput>(HttpStatusCode.OK, regressionoutput);
            });
        }

        [HttpPost]
        [Route("deleteregressionoutput")]
        public HttpResponseMessage DeleteRegressionOutput(HttpRequestMessage request, [FromBody]int RegressionOutputId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RegressionOutput regressionoutput = _IFRS9Service.GetRegressionOutput(RegressionOutputId);

                if (regressionoutput != null)
                {
                    _IFRS9Service.DeleteRegressionOutput(RegressionOutputId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No RegressionOutput found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getregressionoutput/{RegressionOutputId}")]
        public HttpResponseMessage GetRegressionOutput(HttpRequestMessage request,int RegressionOutputId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RegressionOutput regressionoutput = _IFRS9Service.GetRegressionOutput(RegressionOutputId);

                // notice no need to create a seperate model object since RegressionOutput entity will do just fine
                response = request.CreateResponse<RegressionOutput>(HttpStatusCode.OK, regressionoutput);

                return response;
            });
        }

        [HttpGet]
        [Route("availableregressionoutputs")]
        public HttpResponseMessage GetAvailableRegressionOutputs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RegressionOutput[] regressionoutputs = _IFRS9Service.GetAllRegressionOutputs();

                return request.CreateResponse<RegressionOutput[]>(HttpStatusCode.OK, regressionoutputs);
            });
        }
    }
}
