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
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/chartofaccount")]
    [UsesDisposableService]
    public class ChartOfAccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ChartOfAccountApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatechartOfAccount")]
        public HttpResponseMessage UpdateChartOfAccount(HttpRequestMessage request, [FromBody]ChartOfAccount chartOfAccountModel)
        {
            return GetHttpResponse(request, () =>
            {
                var chartOfAccount = _CoreService.UpdateChartOfAccount(chartOfAccountModel);

                return request.CreateResponse<ChartOfAccount>(HttpStatusCode.OK, chartOfAccount);
            });
        }

        [HttpPost]
        [Route("deletechartOfAccount")]
        public HttpResponseMessage DeleteChartOfAccount(HttpRequestMessage request, [FromBody]int chartOfAccountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ChartOfAccount chartOfAccount = _CoreService.GetChartOfAccount(chartOfAccountId);

                if (chartOfAccount != null)
                {
                    _CoreService.DeleteChartOfAccount(chartOfAccountId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No chartOfAccount found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getchartOfAccount/{chartOfAccountId}")]
        public HttpResponseMessage GetChartOfAccount(HttpRequestMessage request,int chartOfAccountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ChartOfAccount chartOfAccount = _CoreService.GetChartOfAccount(chartOfAccountId);

                // notice no need to create a seperate model object since ChartOfAccount entity will do just fine
                response = request.CreateResponse<ChartOfAccount>(HttpStatusCode.OK, chartOfAccount);

                return response;
            });
        }

        [HttpGet]
        [Route("availablechartOfAccounts")]
        public HttpResponseMessage GetAvailableChartOfAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ChartOfAccountData[] chartOfAccounts = _CoreService.GetChartOfAccounts();

                return request.CreateResponse<ChartOfAccountData[]>(HttpStatusCode.OK, chartOfAccounts);
            });
        }

        [HttpGet]
        [Route("getviewchartofaccount")]
        public HttpResponseMessage GetViewChartOfAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ChartOfAccountData[] chartOfAccounts = _CoreService.GetChartOfAccounts().Where(c=>c.AccountType == AccountTypeEnum.View).ToArray();

                return request.CreateResponse<ChartOfAccountData[]>(HttpStatusCode.OK, chartOfAccounts);
            });
        }

        [HttpGet]
        [Route("getnonviewchartofaccount")]
        public HttpResponseMessage GetNonViewChartOfAccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ChartOfAccountData[] chartOfAccounts = _CoreService.GetChartOfAccounts().Where(c => c.AccountType != AccountTypeEnum.View).ToArray();

                return request.CreateResponse<ChartOfAccountData[]>(HttpStatusCode.OK, chartOfAccounts);
            });
        }
    }
}
