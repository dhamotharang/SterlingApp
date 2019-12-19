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
    [RoutePrefix("api/budgetinglevel")]
    [UsesDisposableService]
    public class BudgetingLevelApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BudgetingLevelApiController(ICoreService mprCoreService)
        {
            _CoreService = mprCoreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpPost]
        [Route("updatebudgetingLevel")]
        public HttpResponseMessage UpdateBudgetingLevel(HttpRequestMessage request, [FromBody]BudgetingLevel budgetingLevelModel)
        {
            return GetHttpResponse(request, () =>
            {
                var budgetingLevel = _CoreService.UpdateBudgetingLevel(budgetingLevelModel);

                return request.CreateResponse<BudgetingLevel>(HttpStatusCode.OK, budgetingLevel);
            });
        }

        [HttpPost]
        [Route("deletebudgetingLevel")]
        public HttpResponseMessage DeleteBudgetingLevel(HttpRequestMessage request, [FromBody]int budgetingLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BudgetingLevel budgetingLevel = _CoreService.GetBudgetingLevel(budgetingLevelId);

                if (budgetingLevel != null)
                {
                    _CoreService.DeleteBudgetingLevel(budgetingLevelId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No budgetingLevel found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbudgetingLevel/{budgetingLevelId}")]
        public HttpResponseMessage GetBudgetingLevel(HttpRequestMessage request, int budgetingLevelId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BudgetingLevel budgetingLevel = _CoreService.GetBudgetingLevel(budgetingLevelId);

                // notice no need to create a seperate model object since BudgetingLevel entity will do just fine
                response = request.CreateResponse<BudgetingLevel>(HttpStatusCode.OK, budgetingLevel);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebudgetingLevels")]
        public HttpResponseMessage GetAvailableBudgetingLevels(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BudgetingLevelData[] budgetingLevels = _CoreService.GetBudgetingLevels(string.Empty, string.Empty);

                return request.CreateResponse<BudgetingLevelData[]>(HttpStatusCode.OK, budgetingLevels);
            });
        }
    }
}
