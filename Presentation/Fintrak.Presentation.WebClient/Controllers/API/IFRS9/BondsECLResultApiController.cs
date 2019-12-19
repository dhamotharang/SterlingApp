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
    [RoutePrefix("api/bondseclresult")]
    [UsesDisposableService]
    public class BondsECLResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondsECLResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatebondseclresult")]
        public HttpResponseMessage UpdateBondsECLResult(HttpRequestMessage request, [FromBody]BondsECLResult bondseclresultModel)
        {
            return GetHttpResponse(request, () =>
            {
                var bondseclresult = _IFRS9Service.UpdateBondsECLResult(bondseclresultModel);

                return request.CreateResponse<BondsECLResult>(HttpStatusCode.OK, bondseclresult);
            });
        }

        [HttpPost]
        [Route("deletebondseclresult")]
        public HttpResponseMessage DeleteBondsECLResult(HttpRequestMessage request, [FromBody]int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                BondsECLResult bondseclresult = _IFRS9Service.GetBondsECLResult(ID);

                if (bondseclresult != null)
                {
                    _IFRS9Service.DeleteBondsECLResult(ID);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No BondsECLResult found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getbondseclresult/{ID}")]
        public HttpResponseMessage GetBondsECLResult(HttpRequestMessage request,int ID)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondsECLResult bondseclresult = _IFRS9Service.GetBondsECLResult(ID);

                // notice no need to create a seperate model object since BondsECLResult entity will do just fine
                response = request.CreateResponse<BondsECLResult>(HttpStatusCode.OK, bondseclresult);

                return response;
            });
        }

        [HttpGet]
        [Route("availablebondseclresults/{defaultCount}")]
        public HttpResponseMessage GetAvailableBondsECLResults(HttpRequestMessage request, int defaultCount){
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    BondsECLResult[] bondseclcomputationresults = _IFRS9Service.GetBondsECLResults(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Bonds_ECL_Summary.zip");
                    return response;
                }
                else
                {
                    BondsECLResult[] bondseclcomputationresults = _IFRS9Service.GetBondsECLResults(defaultCount, null);
                    return request.CreateResponse<BondsECLResult[]>(HttpStatusCode.OK, bondseclcomputationresults);
                }
            });
        }



        [HttpGet]
        [Route("getBondsECLResultbysearch/{searchParam}")]
        public HttpResponseMessage GetBondsECLResultBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                BondsECLResult[] bondseclresults = _IFRS9Service.GetBondsECLResultBySearch(searchParam);
                return request.CreateResponse<BondsECLResult[]>(HttpStatusCode.OK, bondseclresults.ToArray());
            });
        }





    }
}
