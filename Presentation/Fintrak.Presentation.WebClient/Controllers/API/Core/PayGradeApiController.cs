using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/payGrade")]
    [UsesDisposableService]
    public class PayGradeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PayGradeApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatepayGrade")]
        public HttpResponseMessage UpdatePayGrade(HttpRequestMessage request, [FromBody]PayGrade payGradeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var payGrade = _CoreService.UpdatePayGrade(payGradeModel);

                return request.CreateResponse<PayGrade>(HttpStatusCode.OK, payGrade);
            });
        }

        [HttpPost]
        [Route("deletepayGrade")]
        public HttpResponseMessage DeletePayGrade(HttpRequestMessage request, [FromBody]int payGradeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                PayGrade payGrade = _CoreService.GetPayGrade(payGradeId);

                if (payGrade != null)
                {
                    _CoreService.DeletePayGrade(payGradeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No payGrade found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getpayGrade/{payGradeId}")]
        public HttpResponseMessage GetPayGrade(HttpRequestMessage request,int payGradeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PayGrade payGrade = _CoreService.GetPayGrade(payGradeId);

                // notice no need to create a seperate model object since PayGrade entity will do just fine
                response = request.CreateResponse<PayGrade>(HttpStatusCode.OK, payGrade);

                return response;
            });
        }

        [HttpGet]
        [Route("availablepayGrades")]
        public HttpResponseMessage GetAvailablePayGrades(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PayGrade[] payGrades = _CoreService.GetAllPayGrades();

                return request.CreateResponse<PayGrade[]>(HttpStatusCode.OK, payGrades);
            });
        }
    }
}
