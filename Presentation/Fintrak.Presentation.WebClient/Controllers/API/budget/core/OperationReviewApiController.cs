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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/operationreview")]
    [UsesDisposableService]
    public class OperationReviewApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OperationReviewApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updateoperationreview")]
        public HttpResponseMessage UpdateOperationReview(HttpRequestMessage request, [FromBody]OperationReview operationReviewModel)
        {
            return GetHttpResponse(request, () =>
            {
                var operationReview = _CoreService.UpdateOperationReview(operationReviewModel);

                return request.CreateResponse<OperationReview>(HttpStatusCode.OK, operationReview);
            });
        }

        [HttpPost]
        [Route("deleteoperationReview")]
        public HttpResponseMessage DeleteOperationReview(HttpRequestMessage request, [FromBody]int operationReviewId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OperationReview operationReview = _CoreService.GetOperationReview(operationReviewId);

                if (operationReview != null)
                {
                    _CoreService.DeleteOperationReview(operationReviewId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No operationReview found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getoperationReview/{operationReviewId}")]
        public HttpResponseMessage GetOperationReview(HttpRequestMessage request, int operationReviewId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OperationReview operationReview = _CoreService.GetOperationReview(operationReviewId);

                // notice no need to create a seperate model object since OperationReview entity will do just fine
                response = request.CreateResponse<OperationReview>(HttpStatusCode.OK, operationReview);

                return response;
            });
        }

        [HttpGet]
        [Route("getoperationreviewbyoperation/{operationCode}")]
        public HttpResponseMessage GetAvailableOperationReviews(HttpRequestMessage request, string operationCode)
        {
            return GetHttpResponse(request, () =>
            {
                OperationReview[] operationReviews = _CoreService.GetOperationReviews(operationCode);

                return request.CreateResponse<OperationReview[]>(HttpStatusCode.OK, operationReviews);
            });
        }
    }
}
