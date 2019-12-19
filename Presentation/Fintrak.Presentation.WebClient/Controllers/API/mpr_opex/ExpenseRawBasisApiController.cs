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
    [RoutePrefix("api/expenserawbasis")]
    [UsesDisposableService]  
    public class ExpenseRawBasisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExpenseRawBasisApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateexpenseRawBasis")]
        public HttpResponseMessage UpdateExpenseRawBasis(HttpRequestMessage request, [FromBody]ExpenseRawBasis expenseRawBasisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var expenseRawBasis = _MPROPEXService.UpdateExpenseRawBasis(expenseRawBasisModel);

                return request.CreateResponse<ExpenseRawBasis>(HttpStatusCode.OK, expenseRawBasis);
            });
        }

        [HttpPost]
        [Route("deleteexpenseRawBasis")]
        public HttpResponseMessage DeleteExpenseRawBasis(HttpRequestMessage request, [FromBody]int expenseRawBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExpenseRawBasis expenseRawBasis = _MPROPEXService.GetExpenseRawBasis(expenseRawBasisId);

                if (expenseRawBasis != null)
                {
                    _MPROPEXService.DeleteExpenseRawBasis(expenseRawBasisId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Expense Basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseRawBasis/{expenseRawBasisId}")]
        public HttpResponseMessage GetExpenseRawBasis(HttpRequestMessage request, int expenseRawBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseRawBasis expenseRawBasis = _MPROPEXService.GetExpenseRawBasis(expenseRawBasisId);

                // notice no need to create a seperate model object since ExpenseRawBasis entity will do just fine
                response = request.CreateResponse<ExpenseRawBasis>(HttpStatusCode.OK, expenseRawBasis);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexpenseRawBasis")]
        public HttpResponseMessage GetAvailableExpenseRawBasis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExpenseRawBasisData[] expenseRawBasis = _MPROPEXService.GetAllExpenseRawBasisInfo();


                return request.CreateResponse<ExpenseRawBasisData[]>(HttpStatusCode.OK, expenseRawBasis);
            });
        }
    }
}
