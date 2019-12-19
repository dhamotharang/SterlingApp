using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/solution")]
    [UsesDisposableService]
    public class SolutionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SolutionApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatesolution")]
        public HttpResponseMessage UpdateSolution(HttpRequestMessage request, [FromBody]Solution solutionModel)
        {
            return GetHttpResponse(request, () =>
            {
                var solution = _CoreService.UpdateSolution(solutionModel);

                return request.CreateResponse<Solution>(HttpStatusCode.OK, solution);
            });
        }

        [HttpPost]
        [Route("deletesolution")]
        public HttpResponseMessage DeleteSolution(HttpRequestMessage request, [FromBody]int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Solution solution = _CoreService.GetSolution(solutionId);

                if (solution != null)
                {
                    _CoreService.DeleteSolution(solutionId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No solution found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsolution/{solutionId}")]
        public HttpResponseMessage GetSolution(HttpRequestMessage request,int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Solution solution = _CoreService.GetSolution(solutionId);

                // notice no need to create a seperate model object since Solution entity will do just fine
                response = request.CreateResponse<Solution>(HttpStatusCode.OK, solution);

                return response;
            });
        }

        [HttpGet]
        [Route("availablesolutions")]
        public HttpResponseMessage GetAvailableSolutions(HttpRequestMessage request)
        {
                      return GetHttpResponse(request, () =>
            {
                Solution[] solutions = _CoreService.GetAllSolutions();

                return request.CreateResponse<Solution[]>(HttpStatusCode.OK, solutions);
            });
        }
    }
}
