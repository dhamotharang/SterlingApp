using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/staffloanscomputationresult")]
    [UsesDisposableService]
    public class StaffLoansComputationResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public StaffLoansComputationResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatestaffLoansComputationResult")]
        public HttpResponseMessage UpdateStaffLoansComputationResult(HttpRequestMessage request, [FromBody]StaffLoansComputationResult staffLoansComputationResultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var staffLoansComputationResult = _IFRS9Service.UpdateStaffLoansComputationResult(staffLoansComputationResultModel);

                return request.CreateResponse<StaffLoansComputationResult>(HttpStatusCode.OK, staffLoansComputationResult);
            });
        }

        [HttpPost]
        [Route("deletestaffLoansComputationResult")]
        public HttpResponseMessage DeleteStaffLoansComputationResult(HttpRequestMessage request, [FromBody]int StaffLoan_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                StaffLoansComputationResult staffLoansComputationResult = _IFRS9Service.GetStaffLoansComputationResult(StaffLoan_Id);

                if (staffLoansComputationResult != null)
                {
                    _IFRS9Service.DeleteStaffLoansComputationResult(StaffLoan_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No staffloanscomputationresult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getstaffLoansComputationResult/{StaffLoan_Id}")]
        public HttpResponseMessage GetStaffLoansComputationResult(HttpRequestMessage request, int StaffLoan_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                StaffLoansComputationResult staffLoansComputationResult = _IFRS9Service.GetStaffLoansComputationResult(StaffLoan_Id);

                // notice no need to create a seperate model object since StaffLoansComputationResult entity will do just fine
                response = request.CreateResponse<StaffLoansComputationResult>(HttpStatusCode.OK, staffLoansComputationResult);

                return response;
            });
        }

        [HttpGet]
        [Route("availablestaffLoansComputationResults")]
        public HttpResponseMessage GetAvailableStaffLoansComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                StaffLoansComputationResult[] staffLoansComputationResults = _IFRS9Service.GetAllStaffLoansComputationResults();

                return request.CreateResponse<StaffLoansComputationResult[]>(HttpStatusCode.OK, staffLoansComputationResults);
            });
        }

        [HttpGet]
        [Route("getstaffLoansComputationResultsBySearch/{SearchParam}")]
        public HttpResponseMessage GetStaffLoansComputationResultBySearch(HttpRequestMessage request, string SearchParam)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                StaffLoansComputationResult[] staffLoansComputationResults = _IFRS9Service.GetStaffLoansComputationResultBySearch(SearchParam);

                // notice no need to create a seperate model object since StaffLoansComputationResult entity will do just fine
                response = request.CreateResponse<StaffLoansComputationResult[]>(HttpStatusCode.OK, staffLoansComputationResults);

                return response;
            });
        }
    }
}
