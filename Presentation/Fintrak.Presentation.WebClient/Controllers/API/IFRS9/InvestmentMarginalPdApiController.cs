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
    [RoutePrefix("api/investmentmarginalpd")]
    [UsesDisposableService]
    public class InvestmentMarginalPdApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InvestmentMarginalPdApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateinvestmentmarginalpd")]
        public HttpResponseMessage UpdateInvestmentMarginalPd(HttpRequestMessage request, [FromBody]InvestmentMarginalPd investmentmarginalpdModel)
        {
            return GetHttpResponse(request, () =>
            {
                var investmentmarginalpd = _IFRS9Service.UpdateInvestmentMarginalPd(investmentmarginalpdModel);

                return request.CreateResponse<InvestmentMarginalPd>(HttpStatusCode.OK, investmentmarginalpd);
            });
        }

        [HttpPost]
        [Route("deleteinvestmentmarginalpd")]
        public HttpResponseMessage DeleteInvestmentMarginalPd(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InvestmentMarginalPd investmentmarginalpd = _IFRS9Service.GetInvestmentMarginalPd(ID);

                if (investmentmarginalpd != null)
                {
                    _IFRS9Service.DeleteInvestmentMarginalPd(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No InvestmentMarginalPd found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinvestmentmarginalpd/{ID}")]
        public HttpResponseMessage GetInvestmentMarginalPd(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InvestmentMarginalPd investmentmarginalpd = _IFRS9Service.GetInvestmentMarginalPd(ID);

                // notice no need to create a seperate model object since InvestmentMarginalPd entity will do just fine
                response = request.CreateResponse<InvestmentMarginalPd>(HttpStatusCode.OK, investmentmarginalpd);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinvestmentmarginalpds/{defaultCount}")]
        public HttpResponseMessage GetAvailableInvestmentMarginalPds(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                InvestmentMarginalPd[] investmentmarginalpds = _IFRS9Service.GetInvestmentMarginalPds(defaultCount);
                return request.CreateResponse<InvestmentMarginalPd[]>(HttpStatusCode.OK, investmentmarginalpds);
            });
        }



        [HttpGet]
        [Route("getInvestmentMarginalPdbysearch/{searchParam}")]
        public HttpResponseMessage GetInvestmentMarginalPdBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                InvestmentMarginalPd[] investmentmarginalpds = _IFRS9Service.GetInvestmentMarginalPdBySearch(searchParam);
                return request.CreateResponse<InvestmentMarginalPd[]>(HttpStatusCode.OK, investmentmarginalpds.ToArray());
            });
        }





    }
}
