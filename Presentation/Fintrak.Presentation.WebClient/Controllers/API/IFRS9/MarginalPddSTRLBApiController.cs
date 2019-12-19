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

namespace Fintrak.Presentation.WebClient.API {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/MarginalPddSTRLB")]
    [UsesDisposableService]
    public class MarginalPddSTRLBApiController : ApiControllerBase {
        [ImportingConstructor]
        public MarginalPddSTRLBApiController(IIFRS9Service ifrs9Service) {
            _IFRS9Service = ifrs9Service;
        }
        IIFRS9Service _IFRS9Service;
        protected override void RegisterServices(List<IServiceContract> disposableServices) {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateMarginalPddSTRLB")]
        public HttpResponseMessage Updatemarginalpddstrlb(HttpRequestMessage request, [FromBody]MarginalPddSTRLB marginalpddstrlbModel) {
            return GetHttpResponse(request, () => {
                var marginalpddstrlb = _IFRS9Service.UpdateMarginalPddSTRLB(marginalpddstrlbModel);
                return request.CreateResponse<MarginalPddSTRLB>(HttpStatusCode.OK, marginalpddstrlb);
            });
        }


        [HttpPost]
        [Route("deleteMarginalPddSTRLB")]
        public HttpResponseMessage DeleteMarginalPddSTRLB(HttpRequestMessage request, [FromBody]int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                // not that calling the WCF service here will authenticate access to the data
                MarginalPddSTRLB marginalpddstrlb = _IFRS9Service.GetMarginalPddSTRLB(Id);
                if (marginalpddstrlb != null) {
                    _IFRS9Service.DeleteMarginalPddSTRLB(Id);
                    response = request.CreateResponse(HttpStatusCode.OK);
                } else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No MarginalPddSTRLB data found under that ID.");
                return response;
            });
        }


        [HttpGet]
        [Route("availableMarginalPddSTRLB")]
        public HttpResponseMessage GetAvailableMarginalPddSTRLBs(HttpRequestMessage request) {
            return GetHttpResponse(request, () => {
                MarginalPddSTRLB[] marginalpddstrlb = _IFRS9Service.GetAllMarginalPddSTRLB().ToArray();
                return request.CreateResponse<MarginalPddSTRLB[]>(HttpStatusCode.OK, marginalpddstrlb.ToArray());
            });
        }

        [HttpGet]
        [Route("getMarginalPddSTRLB/{Id}")]
        public HttpResponseMessage GetMarginalPddSTRLB(HttpRequestMessage request, int Id) {
            return GetHttpResponse(request, () => {
                HttpResponseMessage response = null;
                MarginalPddSTRLB marginalpddstrlb = _IFRS9Service.GetMarginalPddSTRLB(Id);
                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<MarginalPddSTRLB>(HttpStatusCode.OK, marginalpddstrlb);
                return response;
            });
        }


        [HttpGet]
        [Route("getMarginalPddSTRLBbysearch/{searchParam}")]
        public HttpResponseMessage GetMarginalPddSTRLBBySearch(HttpRequestMessage request, string searchParam) {
            return GetHttpResponse(request, () => {
                MarginalPddSTRLB[] marginalpddstrlb = _IFRS9Service.GetMarginalPddSTRLBBySearch(searchParam);
                return request.CreateResponse<MarginalPddSTRLB[]>(HttpStatusCode.OK, marginalpddstrlb.ToArray());
            });
        }


        [HttpGet]
        [Route("availableMarginalPddSTRLB/{defaultCount}")]
        public HttpResponseMessage GetAvailableMarginalPddSTRLB(HttpRequestMessage request, int defaultCount) {
            return GetHttpResponse(request, () => {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    MarginalPddSTRLB[] marginalpddstrlb = _IFRS9Service.GetMarginalPddSTRLBs(defaultCount, path + "ExportedData\\").ToArray();
                    var response = DownloadFileService.DownloadFile(path, "Marginal%20PDs.zip");
                    return response;
                }
                else
                {
                    MarginalPddSTRLB[] marginalpddstrlb = _IFRS9Service.GetMarginalPddSTRLBs(defaultCount, null).ToArray();
                    return request.CreateResponse<MarginalPddSTRLB[]>(HttpStatusCode.OK, marginalpddstrlb.ToArray());
                }
            });
        }

    }
}
