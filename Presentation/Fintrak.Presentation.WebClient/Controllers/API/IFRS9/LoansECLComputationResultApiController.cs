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
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/LoansECLComputationResult")]
    [UsesDisposableService]
    public class LoansECLComputationResultApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public LoansECLComputationResultApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateLoansECLComputationResult")]
        public HttpResponseMessage Updateloanseclcomputationresult(HttpRequestMessage request, [FromBody]LoansECLComputationResult loanseclcomputationresultModel)
        {
            return GetHttpResponse(request, () => {
                var loanseclcomputationresult = _IFRS9Service.UpdateLoansECLComputationResult(loanseclcomputationresultModel);
                return request.CreateResponse<LoansECLComputationResult>(HttpStatusCode.OK, loanseclcomputationresult);
            });
        }


        [HttpPost]
        [Route("deleteLoansECLComputationResult")]
        public HttpResponseMessage DeleteLoansECLComputationResult(HttpRequestMessage request, [FromBody]int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                LoansECLComputationResult loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResult(Id);
                if (loanseclcomputationresult != null)
                {
                    _IFRS9Service.DeleteLoansECLComputationResult(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No LoansECLComputationResult data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableLoansECLComputationResult")]
        public HttpResponseMessage GetAvailableLoansECLComputationResults(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () => {
                LoansECLComputationResult[] loanseclcomputationresult = _IFRS9Service.GetAllLoansECLComputationResult().ToArray();
                return request.CreateResponse<LoansECLComputationResult[]>(HttpStatusCode.OK, loanseclcomputationresult.ToArray());
            });
        }

        [HttpGet]
        [Route("getLoansECLComputationResult/{Id}")]
        public HttpResponseMessage GetLoansECLComputationResult(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                LoansECLComputationResult loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResult(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<LoansECLComputationResult>(HttpStatusCode.OK, loanseclcomputationresult);
                return response;
            });
        }


        [HttpGet]
        [Route("getLoansECLComputationResultbysearch/{searchParam}")]
        public HttpResponseMessage GetLoansECLComputationResultBySearch(HttpRequestMessage request, string searchParam)
        {
            return GetHttpResponse(request, () => {
                if (searchParam.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    LoansECLComputationResult[] loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResultBySearch(searchParam, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Loans_&_OD_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    LoansECLComputationResult[] loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResultBySearch(searchParam, null);
                    return request.CreateResponse<LoansECLComputationResult[]>(HttpStatusCode.OK, loanseclcomputationresult.ToArray());
                }
            });
        }


        [HttpGet]
        [Route("availableLoansECLComputationResult/{defaultCount}")]
        public HttpResponseMessage GetAvailableLoansECLComputationResult(HttpRequestMessage request, int defaultCount)
        {
            return GetHttpResponse(request, () => {
                if (defaultCount <= 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    LoansECLComputationResult[] loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResults(defaultCount, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Loans_&_OD_ECL_Detailed_Results.zip");
                    return response;
                }
                else
                {
                    LoansECLComputationResult[] loanseclcomputationresult = _IFRS9Service.GetLoansECLComputationResults(defaultCount, null);

                    return request.CreateResponse<LoansECLComputationResult[]>(HttpStatusCode.OK, loanseclcomputationresult);
                }
            });
        }

    }
}
