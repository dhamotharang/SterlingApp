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
    [RoutePrefix("api/transition")]
    [UsesDisposableService]
    public class TransitionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TransitionApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatetransition")]
        public HttpResponseMessage UpdateTransition(HttpRequestMessage request, [FromBody]Transition transitionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var transition = _IFRS9Service.UpdateTransition(transitionModel);

                return request.CreateResponse<Transition>(HttpStatusCode.OK, transition);
            });
        }

        [HttpPost]
        [Route("deletetransition")]
        public HttpResponseMessage DeleteTransition(HttpRequestMessage request, [FromBody]int transitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Transition transition = _IFRS9Service.GetTransition(transitionId);

                if (transition != null)
                {
                    _IFRS9Service.DeleteTransition(transitionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No transition found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("gettransition/{transitionId}")]
        public HttpResponseMessage GetTransition(HttpRequestMessage request,int transitionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Transition transition = _IFRS9Service.GetTransition(transitionId);

                // notice no need to create a seperate model object since Transition entity will do just fine
                response = request.CreateResponse<Transition>(HttpStatusCode.OK, transition);

                return response;
            });
        }

        [HttpGet]
        [Route("availabletransitions")]
        public HttpResponseMessage GetAvailableTransitions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Transition[] transitions = _IFRS9Service.GetAllTransitions();

                return request.CreateResponse<Transition[]>(HttpStatusCode.OK, transitions);
            });
        }
    }
}
