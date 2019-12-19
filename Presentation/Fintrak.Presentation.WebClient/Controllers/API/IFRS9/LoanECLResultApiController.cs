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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loaneclresult")]
    [UsesDisposableService]
    public class LoanECLResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanECLResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateloaneclresult")]
        public HttpResponseMessage UpdateLoanECLResult(HttpRequestMessage request, [FromBody]LoanECLResult loaneclresultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loaneclresult = _IFRS9Service.UpdateLoanECLResult(loaneclresultModel);

                return request.CreateResponse<LoanECLResult>(HttpStatusCode.OK, loaneclresult);
            });
        }

        [HttpPost]
        [Route("deleteloaneclresult")]
        public HttpResponseMessage DeleteLoanECLResult(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanECLResult loaneclresult = _IFRS9Service.GetLoanECLResult(ID);

                if (loaneclresult != null)
                {
                    _IFRS9Service.DeleteLoanECLResult(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanECLResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getloaneclresult/{ID}")]
        public HttpResponseMessage GetLoanECLResult(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanECLResult loaneclresult = _IFRS9Service.GetLoanECLResult(ID);

                // notice no need to create a seperate model object since LoanECLResult entity will do just fine
                response = request.CreateResponse<LoanECLResult>(HttpStatusCode.OK, loaneclresult);

                return response;
            });
        }

        [HttpGet]
        [Route("availableloaneclresults/{defaultCount}")]
        public HttpResponseMessage GetAvailableLoanECLResults(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    LoanECLResult[] bondseclcomputationresults = _IFRS9Service.GetLoanECLResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Loans_ECL_Summary.zip");
                    return response;
                }
                else
                {
                    LoanECLResult[] bondseclcomputationresults = _IFRS9Service.GetLoanECLResults(defaultCount, null);
                    return request.CreateResponse<LoanECLResult[]>(HttpStatusCode.OK, bondseclcomputationresults);
                }
            });
        }



        [HttpGet]
        [Route("getLoanECLResultbysearch/{searchParam}")]
        public HttpResponseMessage GetLoanECLResultBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                LoanECLResult[] loaneclresults = _IFRS9Service.GetLoanECLResultBySearch(searchParam);
                return request.CreateResponse<LoanECLResult[]>(HttpStatusCode.OK, loaneclresults.ToArray());
            });
        }





    }
}
