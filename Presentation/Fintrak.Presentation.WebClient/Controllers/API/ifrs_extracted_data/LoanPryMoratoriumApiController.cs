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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loanprymoratorium")]
    [UsesDisposableService]
    public class loanprymoratoriumApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public loanprymoratoriumApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateloanprymoratorium")]
        public HttpResponseMessage UpdateloanPry(HttpRequestMessage request, [FromBody]LoanPryMoratorium loanPryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanPry = _IFRSDataService.UpdateLoanPryMoratorium(loanPryModel);

                return request.CreateResponse<LoanPryMoratorium>(HttpStatusCode.OK, loanPry);
            });
        }



        [HttpPost]
        [Route("deleteloanprymoratorium")]
        public HttpResponseMessage Deleteprymoratorium(HttpRequestMessage request, [FromBody]int loanPryMoratoriumId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanPryMoratorium loanprymoratorium = _IFRSDataService.GetLoanPryMoratorium(loanPryMoratoriumId);

                if (loanprymoratorium != null)
                {
                    _IFRSDataService.DeleteLoanPryMoratorium(loanPryMoratoriumId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoanMoratoriumData found under that ID.");

                return response;
            });
        }



        [HttpGet]
        [Route("availableloanprymoratorium")]
        public HttpResponseMessage GetAvailableloanprymoratoriums(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanPryMoratorium[] loanprymoratorium = _IFRSDataService.GetAllLoanPryMoratorium().ToArray();

                return request.CreateResponse<LoanPryMoratorium[]>(HttpStatusCode.OK, loanprymoratorium.ToArray());
            });
        }

        [HttpGet]
        [Route("getloanprymoratorium/{loanPryMoratoriumId}")]
        public HttpResponseMessage GetLoanPryMoratorium(HttpRequestMessage request, int loanPryMoratoriumId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanPryMoratorium loanprymoratorium = _IFRSDataService.GetLoanPryMoratorium(loanPryMoratoriumId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoanPryMoratorium>(HttpStatusCode.OK, loanprymoratorium);

                return response;
            });
        }

       
    }
}
