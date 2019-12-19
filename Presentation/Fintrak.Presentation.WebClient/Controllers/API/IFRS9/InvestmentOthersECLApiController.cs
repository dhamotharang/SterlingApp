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
    [RoutePrefix("api/investmentothersecl")]
    [UsesDisposableService]
    public class InvestmentOthersECLApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InvestmentOthersECLApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateinvestmentothersecl")]
        public HttpResponseMessage UpdateInvestmentOthersECL(HttpRequestMessage request, [FromBody]InvestmentOthersECL investmentOthersECLModel)
        {
            return GetHttpResponse(request, () =>
            {
                var investmentOthersECL = _IFRS9Service.UpdateInvestmentOthersECL(investmentOthersECLModel);

                return request.CreateResponse<InvestmentOthersECL>(HttpStatusCode.OK, investmentOthersECL);
            });
        }

        [HttpPost]
        [Route("deleteinvestmentothersecl")]
        public HttpResponseMessage DeleteInvestmentOthersECL(HttpRequestMessage request, [FromBody]int ecl_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InvestmentOthersECL investmentOthersECL = _IFRS9Service.GetInvestmentOthersECL(ecl_Id);

                if (investmentOthersECL != null)
                {
                    _IFRS9Service.DeleteInvestmentOthersECL(ecl_Id);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No InvestmentOthersECL found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinvestmentothersecl/{ecl_Id}")]
        public HttpResponseMessage GetInvestmentOthersECL(HttpRequestMessage request, int ecl_Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InvestmentOthersECL investmentOthersECL = _IFRS9Service.GetInvestmentOthersECL(ecl_Id);

                // notice no need to create a seperate model object since InvestmentOthersECL entity will do just fine
                response = request.CreateResponse<InvestmentOthersECL>(HttpStatusCode.OK, investmentOthersECL);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinvestmentothersecls")]
        public HttpResponseMessage GetAvailableInvestmentOthersECLs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InvestmentOthersECL[] investmentOthersECLs = _IFRS9Service.GetAllInvestmentOthersECLs();

                return request.CreateResponse<InvestmentOthersECL[]>(HttpStatusCode.OK, investmentOthersECLs);
            });
        }

        [HttpGet]
        [Route("getinvestmentotherseclsbyrefno/{refno}")]
        public HttpResponseMessage GetAvailableInvestmentOthersECLsByRefNo(HttpRequestMessage request, string RefNo)
        {
            return GetHttpResponse(request, () =>
            {
                InvestmentOthersECL[] investmentOthersECLs = _IFRS9Service.GetInvestmentOthersECLByRefNo(RefNo);

                return request.CreateResponse<InvestmentOthersECL[]>(HttpStatusCode.OK, investmentOthersECLs);
            });
        }
    }
}
