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
    [RoutePrefix("api/financialtype")]
    [UsesDisposableService]
    public class FinancialTypeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FinancialTypeApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatefinancialType")]
        public HttpResponseMessage UpdateFinancialType(HttpRequestMessage request, [FromBody]FinancialType financialTypeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var financialType = _CoreService.UpdateFinancialType(financialTypeModel);

                return request.CreateResponse<FinancialType>(HttpStatusCode.OK, financialType);
            });
        }

        [HttpPost]
        [Route("deletefinancialType")]
        public HttpResponseMessage DeleteFinancialType(HttpRequestMessage request, [FromBody]int financialTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FinancialType financialType = _CoreService.GetFinancialType(financialTypeId);

                if (financialType != null)
                {
                    _CoreService.DeleteFinancialType(financialTypeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No financialType found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getfinancialType/{financialTypeId}")]
        public HttpResponseMessage GetFinancialType(HttpRequestMessage request, int financialTypeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FinancialType financialType = _CoreService.GetFinancialType(financialTypeId);

                // notice no need to create a seperate model object since FinancialType entity will do just fine
                response = request.CreateResponse<FinancialType>(HttpStatusCode.OK, financialType);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefinancialTypes")]
        public HttpResponseMessage GetAvailableFinancialTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FinancialTypeData[] financialTypes = _CoreService.GetFinancialTypes();

                return request.CreateResponse<FinancialTypeData[]>(HttpStatusCode.OK, financialTypes);
            });
        }

        [HttpGet]
        [Route("getfintypes")]
        public HttpResponseMessage GetFinancialTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                FinancialTypeData[] financialTypes = _CoreService.GetFinancialTypes().Where(c=>c.ParentId == null).ToArray();

                return request.CreateResponse<FinancialTypeData[]>(HttpStatusCode.OK, financialTypes);
            });
        }

        [HttpGet]
        [Route("getfinsubtypes/{finType}")]
        public HttpResponseMessage GetFinancialSubTypes(HttpRequestMessage request, string finType)
        {
            return GetHttpResponse(request, () =>
            {
                FinancialTypeData[] financialTypes = _CoreService.GetFinancialTypes().Where(c => c.ParentName == finType).ToArray();

                return request.CreateResponse<FinancialTypeData[]>(HttpStatusCode.OK, financialTypes);
            });
        }
    }
}
