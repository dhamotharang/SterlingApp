using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.Common.Services;
using System.Web.Hosting;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loanpry")]
    [UsesDisposableService]
    public class loanpryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public loanpryApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateloanpry")]
        public HttpResponseMessage UpdateloanPry(HttpRequestMessage request, [FromBody]LoanPry loanPryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanPry = _IFRSDataService.UpdateLoanPry(loanPryModel);

                return request.CreateResponse<LoanPry>(HttpStatusCode.OK, loanPry);
            });
        }


        [HttpPost]
        [Route("deleteloanpry")]
        public HttpResponseMessage DeleteloanPry(HttpRequestMessage request, [FromBody]int pryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanPry loanpry = _IFRSDataService.GetLoanPry(pryId);

                if (loanpry != null)
                {
                    _IFRSDataService.DeleteLoanPry(pryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanPrimaryData found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("availableloanpry")]
        public HttpResponseMessage GetAvailableloanprys(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanPry[] loanpry = _IFRSDataService.GetAllLoanPry().ToArray();

                return request.CreateResponse<LoanPry[]>(HttpStatusCode.OK, loanpry.ToArray());
            });
        }

        [HttpGet]
        [Route("getloanpry/{pryId}")]
        public HttpResponseMessage GetLoanPry(HttpRequestMessage request, int pryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanPry loanpry = _IFRSDataService.GetLoanPry(pryId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoanPry>(HttpStatusCode.OK, loanpry);

                return response;
            });
        }

        [HttpGet]
        [Route("getloanprybysch/{schType}")]
        public HttpResponseMessage GetLoanPrybySchedule(HttpRequestMessage request, string schType)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanPry[] loanpry = _IFRSDataService.GetLoanPryByScheduleType(schType);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<LoanPry[]>(HttpStatusCode.OK, loanpry.ToArray());

                return response;
            });
        }
        [HttpGet]
        [Route("getloanprybysearch/{searchParam}")]
        public HttpResponseMessage GetPryLoanBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                LoanPry[] pryLoan = _IFRSDataService.GetPryLoanBySearch(searchParam);

                return request.CreateResponse<LoanPry[]>(HttpStatusCode.OK, pryLoan.ToArray());
            });
        }


        [HttpGet]
        [Route("availablepryloan/{defaultCount}")]
        public HttpResponseMessage GetAvailablePryLoan(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if(defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetPryLoans(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Primary%20Details%20-%20Loans%20&%20OD.zip");
                    return response;
                } else
                {
                    LoanPry[] loanpry = _IFRSDataService.GetPryLoans(defaultCount, null).ToArray();

                    return request.CreateResponse<LoanPry[]>(HttpStatusCode.OK, loanpry.ToArray());
                }
            });
        }
    }
}
