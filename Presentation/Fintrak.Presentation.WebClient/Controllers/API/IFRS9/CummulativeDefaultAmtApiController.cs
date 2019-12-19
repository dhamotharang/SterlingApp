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
    [RoutePrefix("api/cummulativedefaultamt")]
    [UsesDisposableService]
    public class CummulativeDefaultAmtApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CummulativeDefaultAmtApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatecummulativedefaultamt")]
        public HttpResponseMessage UpdateCummulativeDefaultAmt(HttpRequestMessage request, [FromBody]CummulativeDefaultAmt cummulativedefaultamtModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cummulativedefaultamt = _IFRS9Service.UpdateCummulativeDefaultAmt(cummulativedefaultamtModel);

                return request.CreateResponse<CummulativeDefaultAmt>(HttpStatusCode.OK, cummulativedefaultamt);
            });
        }

        [HttpPost]
        [Route("deletecummulativedefaultamt")]
        public HttpResponseMessage DeleteCummulativeDefaultAmt(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                CummulativeDefaultAmt cummulativedefaultamt = _IFRS9Service.GetCummulativeDefaultAmt(ID);

                if (cummulativedefaultamt != null)
                {
                    _IFRS9Service.DeleteCummulativeDefaultAmt(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No CummulativeDefaultAmt found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getcummulativedefaultamt/{ID}")]
        public HttpResponseMessage GetCummulativeDefaultAmt(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                CummulativeDefaultAmt cummulativedefaultamt = _IFRS9Service.GetCummulativeDefaultAmt(ID);

                // notice no need to create a seperate model object since CummulativeDefaultAmt entity will do just fine
                response = request.CreateResponse<CummulativeDefaultAmt>(HttpStatusCode.OK, cummulativedefaultamt);

                return response;
            });
        }

        [HttpGet]
        [Route("availablecummulativedefaultamts/{defaultCount}")]
        public HttpResponseMessage GetAvailableCummulativeDefaultAmts(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    CummulativeDefaultAmt[] cummulativedefaultamts = _IFRS9Service.GetCummulativeDefaultAmts(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Cumulative%20Default%20Amounts.zip");
                    return response;
                }
                else
                {
                    CummulativeDefaultAmt[] cummulativedefaultamts = _IFRS9Service.GetCummulativeDefaultAmts(defaultCount, null);
                    return request.CreateResponse<CummulativeDefaultAmt[]>(HttpStatusCode.OK, cummulativedefaultamts);
                }
            });
        }



        [HttpGet]
        [Route("getCummulativeDefaultAmtbysearch/{searchParam}")]
        public HttpResponseMessage GetCummulativeDefaultAmtBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                CummulativeDefaultAmt[] cummulativedefaultamts = _IFRS9Service.GetCummulativeDefaultAmtBySearch(searchParam);
                return request.CreateResponse<CummulativeDefaultAmt[]>(HttpStatusCode.OK, cummulativedefaultamts.ToArray());
            });
        }





    }
}
