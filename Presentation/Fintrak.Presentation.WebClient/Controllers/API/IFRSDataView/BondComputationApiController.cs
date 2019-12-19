using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using System.Web.Hosting;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondcomputation")]
    [UsesDisposableService]
    public class BondComputationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondComputationApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getbondcomputation")]
        public HttpResponseMessage GetBondComputation(HttpRequestMessage request, string RefNo, DateTime? Date)
        {
            return GetHttpResponse(request, () =>
            {
                if (RefNo.Contains("ExportData "))
                {
                    string path = HostingEnvironment.MapPath("~");
                    BondComputation[] bondcomputation = _IFRSDataService.GetBondComputationResultbyRefNo(RefNo, Date, path + "ExportedData\\");
                    var response = DownloadFileService.DownloadFile(path, "Bond_Daily_Schedule.zip");
                    return response;
                }
                else
                {
                    BondComputation[] bondcomputation = _IFRSDataService.GetBondComputationResultbyRefNo(RefNo, Date, null);

                    // notice no need to create a seperate model object since BondComputation entity will do just fine
                    return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);
                }
            });
        }


        [HttpGet]
        [Route("getbondcomputationdistinctrefno2")]
        public HttpResponseMessage GetBondComputationResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputation[] bondcomputation = _IFRSDataService.GetBondComputationResultDistinctRefNo();

                return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);
            });
        }

        [HttpGet]
        [Route("availablebondcomputation")]
        public HttpResponseMessage GetAvailableBondComputations(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputation[] bondcomputation = _IFRSDataService.GetAllBondComputations();

                return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputation);
            });
        }


        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                //BondComputation[] bondcomputations = _IFRSDataService.GetRefNoBondComputation();

                //return request.CreateResponse<BondComputation[]>(HttpStatusCode.OK, bondcomputations);
                string[] refNo = _IFRSDataService.GetDistinctRefNo();

                var dropDown = refNo.Select(e => new
                {
                    name = e,
                    icon = "",
                    maker = "",
                    ticked = false
                });

                return request.CreateResponse(HttpStatusCode.OK, dropDown);
            });
        }
    }
}
