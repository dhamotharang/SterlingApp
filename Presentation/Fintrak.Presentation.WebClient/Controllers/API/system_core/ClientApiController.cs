using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using CodeEntities = Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/client")]
    [UsesDisposableService]
    public class ClientApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ClientApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateclient")]
        public HttpResponseMessage UpdateClient(HttpRequestMessage request, [FromBody]CodeEntities.Client clientModel)
        {
            return GetHttpResponse(request, () =>
            {
                var client = _CoreService.UpdateClient(clientModel);

                return request.CreateResponse<CodeEntities.Client>(HttpStatusCode.OK, client);
            });
        }

        [HttpPost]
        [Route("deleteclient")]
        public HttpResponseMessage DeleteClient(HttpRequestMessage request, [FromBody]int clientId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CodeEntities.Client client = _CoreService.GetClient(clientId);

                if (client != null)
                {
                    _CoreService.DeleteClient(clientId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No client found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getclient/{clientId}")]
        public HttpResponseMessage GetClient(HttpRequestMessage request, int clientId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CodeEntities.Client client = _CoreService.GetClient(clientId);

                // notice no need to create a seperate model object since Client entity will do just fine
                response = request.CreateResponse<CodeEntities.Client>(HttpStatusCode.OK, client);

                return response;
            });
        }

        [HttpGet]
        [Route("availableclient")]
        public HttpResponseMessage GetAvailableClients(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                CodeEntities.Client[] client = _CoreService.GetAllClients();

                return request.CreateResponse<CodeEntities.Client[]>(HttpStatusCode.OK, client);
            });
        }
    }
}
