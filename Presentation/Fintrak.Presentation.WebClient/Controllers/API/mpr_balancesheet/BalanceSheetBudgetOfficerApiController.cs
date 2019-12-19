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
    [RoutePrefix("api/balancesheetbudgetofficer")]
    [UsesDisposableService]
    public class BalanceSheetBudgetOfficerApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BalanceSheetBudgetOfficerApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatebalancesheetbudgetofficer")]
        public HttpResponseMessage UpdateBalanceSheetBudgetOfficer(HttpRequestMessage request, [FromBody]BalanceSheetBudgetOfficer balancesheetBudgetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var balancesheetBudget = _MPRBSService.UpdateBalanceSheetBudgetOfficer(balancesheetBudgetModel);

                return request.CreateResponse<BalanceSheetBudgetOfficer>(HttpStatusCode.OK, balancesheetBudget);
            });
        }

        [HttpPost]
        [Route("deletebalancesheetbudgetofficer")]
        public HttpResponseMessage DeleteBalanceSheetBudgetOfficer(HttpRequestMessage request, [FromBody]int balancesheetBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BalanceSheetBudgetOfficer balancesheetBudget = _MPRBSService.GetBalanceSheetBudgetOfficer(balancesheetBudgetId);

                if (balancesheetBudget != null)
                {
                    _MPRBSService.DeleteBalanceSheetBudgetOfficer(balancesheetBudgetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No balancesheetBudget found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetbudgetofficer/{balancesheetBudgetId}")]
        public HttpResponseMessage GetBalanceSheetBudgetOfficer(HttpRequestMessage request, int balancesheetBudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BalanceSheetBudgetOfficer balancesheetBudget = _MPRBSService.GetBalanceSheetBudgetOfficer(balancesheetBudgetId);

                // notice no need to create a seperate model object since BalanceSheetBudgetOfficer entity will do just fine
                response = request.CreateResponse<BalanceSheetBudgetOfficer>(HttpStatusCode.OK, balancesheetBudget);

                return response;
            });
        }

        [HttpGet]
        [Route("getbalancesheetbudgetofficers/{year}")]
        public HttpResponseMessage GetBalanceSheetBudgetOfficers(HttpRequestMessage request,string year)
        {
            return GetHttpResponse(request, () =>
            {
                BalanceSheetBudgetOfficer[] balancesheetBudgets = _MPRBSService.GetAllBalanceSheetBudgetOfficers(year);

                return request.CreateResponse<BalanceSheetBudgetOfficer[]>(HttpStatusCode.OK, balancesheetBudgets);
            });
        }


        [HttpGet]
        [Route("getbalancesheetbudgetofficerssearch/{searchValue}")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request, string searchValue)
        {
            return GetHttpResponse(request, () =>
            {
                BalanceSheetBudgetOfficer[] balancesheetBudgets = _MPRBSService.GetBalanceSheetBudgetOfficers(searchValue);

                return request.CreateResponse<BalanceSheetBudgetOfficer[]>(HttpStatusCode.OK, balancesheetBudgets);
            });
        }
    }
}
