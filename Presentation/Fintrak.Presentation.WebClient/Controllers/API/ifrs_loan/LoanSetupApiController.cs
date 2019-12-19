using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Fintrak.Business.Basic.Contracts;
//using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/loansetup")]
    [UsesDisposableService]
    public class LoanSetupApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoanSetupApiController(IIFRSLoanService loanService)
        {
            _LoanService = loanService;
        }

        IIFRSLoanService _LoanService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_LoanService);
        }

        [HttpPost]
        [Route("updateloansetup")]
        public HttpResponseMessage UpdateSetup(HttpRequestMessage request, [FromBody]LoanSetup loanSetupModel)
        {
            return GetHttpResponse(request, () =>
            {
                var loanSetup = _LoanService.UpdateLoanSetup(loanSetupModel);

                return request.CreateResponse<LoanSetup>(HttpStatusCode.OK, loanSetup);
            });
        }

        [HttpPost]
        [Route("deleteloansetup")]
        public HttpResponseMessage DeleteSetup(HttpRequestMessage request, [FromBody]int loanSetupId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                LoanSetup loanSetup = _LoanService.GetLoanSetup(loanSetupId);

                if (loanSetup != null)
                {
                    _LoanService.DeleteLoanSetup(loanSetupId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No loansetup found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getloansetup/{loanSetupId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int loanSetupId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                LoanSetup loanSetup = _LoanService.GetLoanSetup(loanSetupId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoanSetup>(HttpStatusCode.OK, loanSetup);

                return response;
            });
        }

        [HttpGet]
        [Route("availableloansetups")]
        public HttpResponseMessage GetAvailableSetups(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                LoanSetupData[] loanSetups = _LoanService.GetAllLoanSetups();

                return request.CreateResponse<LoanSetupData[]>(HttpStatusCode.OK, loanSetups);
            });
        }

        [HttpPost, HttpGet]
        
        [Route("deleteloansetuplist/{loanSetupId}")]
        public HttpResponseMessage DeleteLoanSetupIdList(string loanSetupId)
        {
            _LoanService.DeleteLoanSetupIdList(loanSetupId);
            return Request.CreateResponse(HttpStatusCode.OK);
            
        }


    }
}
