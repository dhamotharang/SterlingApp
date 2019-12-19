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
    [RoutePrefix("api/expensebasis")]
    [UsesDisposableService]  
    public class ExpenseBasisApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ExpenseBasisApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateexpenseBasis")]
        public HttpResponseMessage UpdateExpenseBasis(HttpRequestMessage request, [FromBody]ExpenseBasis expenseBasisModel)
        {
            return GetHttpResponse(request, () =>
            {
                var expenseBasis = _MPROPEXService.UpdateExpenseBasis(expenseBasisModel);

                return request.CreateResponse<ExpenseBasis>(HttpStatusCode.OK, expenseBasis);
            });
        }

        [HttpPost]
        [Route("deleteexpenseBasis")]
        public HttpResponseMessage DeleteExpenseBasis(HttpRequestMessage request, [FromBody]int expenseBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ExpenseBasis expenseBasis = _MPROPEXService.GetExpenseBasis(expenseBasisId);

                if (expenseBasis != null)
                {
                    _MPROPEXService.DeleteExpenseBasis(expenseBasisId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Expense Basis found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseBasis/{expenseBasisId}")]
        public HttpResponseMessage GetExpenseBasis(HttpRequestMessage request, int expenseBasisId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseBasis expenseBasis = _MPROPEXService.GetExpenseBasis(expenseBasisId);

                // notice no need to create a seperate model object since ExpenseBasis entity will do just fine
                response = request.CreateResponse<ExpenseBasis>(HttpStatusCode.OK, expenseBasis);

                return response;
            });
        }

        [HttpGet]
        [Route("getexpenseBasisByCode/{code}")]
        public HttpResponseMessage GetExpenseBasisByCode(HttpRequestMessage request, string code)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ExpenseBasis expenseBasis = _MPROPEXService.GetAllExpenseBasisInfo().Where(c=>c.Code == code).FirstOrDefault();

                // notice no need to create a seperate model object since ExpenseBasis entity will do just fine
                response = request.CreateResponse<ExpenseBasis>(HttpStatusCode.OK, expenseBasis);

                return response;
            });
        }

        [HttpGet]
        [Route("availableexpenseBasis")]
        public HttpResponseMessage GetAvailableExpenseBasis(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ExpenseBasis[] expenseBasis = _MPROPEXService.GetAllExpenseBasisInfo();


                return request.CreateResponse<ExpenseBasis[]>(HttpStatusCode.OK, expenseBasis);
            });
        }
    }
}
