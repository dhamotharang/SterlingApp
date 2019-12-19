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

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/bondcomputationresultzero")]
    [UsesDisposableService]
    public class BondComputationResultZeroApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public BondComputationResultZeroApiController(IIFRSDataViewService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IIFRSDataViewService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }



        [HttpGet]
        [Route("getbondcomputationresultzero/{refno}")]
        public HttpResponseMessage GetBondComputationResultZero(HttpRequestMessage request, string refno)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                BondComputationResultZero[] bondcomputationresultzero = _IFRSDataService.GetBondComputationResultZerobyRefNo(refno);

                // notice no need to create a seperate model object since BondComputationResultZero entity will do just fine
                response = request.CreateResponse<BondComputationResultZero[]>(HttpStatusCode.OK, bondcomputationresultzero);

                return response;
            });
        }


        [HttpGet]
        [Route("getbondcomputationresultzerodistinctrefno")]
        public HttpResponseMessage GetBondComputationResultZeroResultDistinctRefNo(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IEnumerable<string> bondcomputationresultzero = _IFRSDataService.GetBondComputationResultZeroDistinctRefNo();

                //return request.CreateResponse<BondComputationResultZero[]>(HttpStatusCode.OK, bondcomputationresultzero);

                return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, bondcomputationresultzero);

            });
        }

        [HttpGet]
        [Route("availablebondcomputationresultzeros")] 
        public HttpResponseMessage GetAvailableBondComputationResultZeros(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputationResultZero[] bondcomputationresultzero = _IFRSDataService.GetBondComputationResultZeros();

                return request.CreateResponse<BondComputationResultZero[]>(HttpStatusCode.OK, bondcomputationresultzero);
            });
        }


        [HttpGet]
        [Route("getrefnos")]
        public HttpResponseMessage GetBalanceSheets(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                BondComputationResultZero[] bondcomputationresultzeros = _IFRSDataService.GetRefNoBondComputationResultZero();

                return request.CreateResponse<BondComputationResultZero[]>(HttpStatusCode.OK, bondcomputationresultzeros);
            });
        }

        //[HttpGet]
        //[Route("getrefnos")]
        //public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        BondComputationResultZero[] bondcomputationresultzeros = _IFRSDataService.GetBondComputationResultZeros();

        //        List<ReferenceNoModel> refNos = new List<ReferenceNoModel>();

        //        List<string> distinctRefNos = null;

        //        distinctRefNos = bondcomputationresultzeros.Select(c => c.RefNo).Distinct().ToList();

        //        foreach (var c in distinctRefNos)
        //            refNos.Add(new ReferenceNoModel()
        //            {
        //                RefNo = c

        //            });
        //        return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, refNos.ToArray());
        //    });
        //}
    }
}

