using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/balancesheetbudget")]
    [UsesDisposableService]
    public class BalanceSheetBudgetApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BalanceSheetBudgetApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebalancesheetbudget")]
        public HttpResponseMessage UpdateBalanceSheetBudget(HttpRequestMessage request, [FromBody]BalanceSheetBudget balancesheetBudgetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var balancesheetBudget = _MPRBSService.UpdateBalanceSheetBudget(balancesheetBudgetModel);

                return request.CreateResponse<BalanceSheetBudget>(HttpStatusCode.OK, balancesheetBudget);
            });
        }

        [HttpPost]
        [Route("deletebalancesheetBudget")]
        public HttpResponseMessage DeleteBalanceSheetBudget(HttpRequestMessage request, [FromBody]int balancesheetBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BalanceSheetBudget balancesheetBudget = _MPRBSService.GetBalanceSheetBudget(balancesheetBudgetId);

                if (balancesheetBudget != null)
                {
                    _MPRBSService.DeleteBalanceSheetBudget(balancesheetBudgetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No balancesheetBudget found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetBudget/{balancesheetBudgetId}")]
        public HttpResponseMessage GetBalanceSheetBudget(HttpRequestMessage request, int balancesheetBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BalanceSheetBudget balancesheetBudget = _MPRBSService.GetBalanceSheetBudget(balancesheetBudgetId);

                // notice no need to create a seperate model object since BalanceSheetBudget entity will do just fine
                response = request.CreateResponse<BalanceSheetBudget>(HttpStatusCode.OK, balancesheetBudget);

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetbudgets/{year}")]
        public HttpResponseMessage GetBalanceSheetBudgets(HttpRequestMessage request,string year)
        {
            return GetHttpResponse(request, () =>
            {
                BalanceSheetBudget[] balancesheetBudgets = _MPRBSService.GetAllBalanceSheetBudgets(year);

                return request.CreateResponse<BalanceSheetBudget[]>(HttpStatusCode.OK, balancesheetBudgets);
            });
        }

        [HttpGet]
        [Route("getbalancesheetbudgetsearch/{searchValue}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchValue)
        {
            return GetHttpResponse(request, () =>
            {
                BalanceSheetBudget[] balancesheetBudgets = _MPRBSService.GetBalanceSheetBudgets(searchValue);

                return request.CreateResponse<BalanceSheetBudget[]>(HttpStatusCode.OK, balancesheetBudgets);
            });
        }

        [HttpPost]
        [Route("deleteselectedlist/{selectedIds}")]
        public HttpResponseMessage DeleteSelectedIdList(string selectedIds)
        {
            _MPRBSService.DeleteBSBSelectedIds(selectedIds);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
