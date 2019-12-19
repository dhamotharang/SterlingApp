using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/operation")]
    [UsesDisposableService]
    public class OperationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OperationApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateoperation")]
        public HttpResponseMessage UpdateOperation(HttpRequestMessage request, [FromBody]Operation operationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var operation = _CoreService.UpdateOperation(operationModel);

                return request.CreateResponse<Operation>(HttpStatusCode.OK, operation);
            });
        }

        [HttpPost]
        [Route("deleteoperation")]
        public HttpResponseMessage DeleteOperation(HttpRequestMessage request, [FromBody]int operationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Operation operation = _CoreService.GetOperation(operationId);

                if (operation != null)
                {
                    _CoreService.DeleteOperation(operationId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No operation found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getoperation/{operationId}")]
        public HttpResponseMessage GetOperation(HttpRequestMessage request, int operationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Operation operation = _CoreService.GetOperation(operationId);

                // notice no need to create a seperate model object since Operation entity will do just fine
                response = request.CreateResponse<Operation>(HttpStatusCode.OK, operation);

                return response;
            });
        }

        [HttpGet]
        [Route("getoperationdetail/{operationId}")]
        public HttpResponseMessage GetOperationDetail(HttpRequestMessage request, int operationId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var operationModel = new OperationModel();
                operationModel.Operation = _CoreService.GetOperation(operationId);
                operationModel.OperationReview = _CoreService.GetOperationReviews(operationModel.Operation.Code);

                // notice no need to create a seperate model object since Operation entity will do just fine
                response = request.CreateResponse<OperationModel>(HttpStatusCode.OK, operationModel);

                return response;
            });
        }

        [HttpGet]
        [Route("availableoperations")]
        public HttpResponseMessage GetAvailableOperations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Operation[] operations = _CoreService.GetAllOperations();

                return request.CreateResponse<Operation[]>(HttpStatusCode.OK, operations);
            });
        }
    }
}
