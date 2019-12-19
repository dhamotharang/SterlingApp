
//api/ifrsbudget/availableifrsbudgets

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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsbudget")]
    [UsesDisposableService]
    public class IFRSBudgetApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRSBudgetApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateifrsbudget")]
        public HttpResponseMessage UpdateIFRSBudget(HttpRequestMessage request, [FromBody]IFRSBudget ifrsBudgetModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsBudget = _FinstatService.UpdateIFRSBudget(ifrsBudgetModel);

                return request.CreateResponse<IFRSBudget>(HttpStatusCode.OK, ifrsBudget);
            });
        }

        [HttpPost]
        [Route("deleteifrsbudget")]
        public HttpResponseMessage DeleteIFRSBudget(HttpRequestMessage request, [FromBody]int ifrsbudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSBudget ifrsBudget = _FinstatService.GetIFRSBudget(ifrsbudgetId);

                if (ifrsBudget != null)
                {
                    _FinstatService.DeleteIFRSBudget(ifrsbudgetId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No ifrsBudget found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsbudget/{ifrsbudgetId}")]
        public HttpResponseMessage GetIFRSBudget(HttpRequestMessage request, int ifrsbudgetId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSBudget ifrsBudget = _FinstatService.GetIFRSBudget(ifrsbudgetId);

                // notice no need to create a seperate model object since IFRSBudget entity will do just fine
                response = request.CreateResponse<IFRSBudget>(HttpStatusCode.OK, ifrsBudget);

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrsbudgets")] 
        public HttpResponseMessage GetAvailableIFRSBudgets(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSBudget[] ifrsBudgets = _FinstatService.GetAllIFRSBudgets();

                return request.CreateResponse<IFRSBudget[]>(HttpStatusCode.OK, ifrsBudgets);
            });
        }

    }
}