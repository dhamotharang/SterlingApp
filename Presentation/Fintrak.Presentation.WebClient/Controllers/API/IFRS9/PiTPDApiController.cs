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
    [RoutePrefix("api/pitpd")]
    [UsesDisposableService]
    public class PiTPDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PiTPDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        //[HttpPost]
        //[Route("updatepitPD")]
        //public HttpResponseMessage UpdatePiTPD(HttpRequestMessage request, [FromBody]PiTPD pitPDModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var pitPD = _IFRS9Service.UpdatePiTPD(pitPDModel);

        //        return request.CreateResponse<PiTPD>(HttpStatusCode.OK, pitPD);
        //    });
        //}

        //[HttpPost]
        //[Route("deletepitPD")]
        //public HttpResponseMessage DeletePiTPD(HttpRequestMessage request, [FromBody]int pitPDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        PiTPD pitPD = _IFRS9Service.GetPiTPD(pitPDId);

        //        if (pitPD != null)
        //        {
        //            _IFRS9Service.DeletePiTPD(pitPDId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No pitPD found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getpitPD/{pitPDId}")]
        //public HttpResponseMessage GetPiTPD(HttpRequestMessage request,int pitPDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        PiTPD pitPD = _IFRS9Service.GetPiTPD(pitPDId);

        //        // notice no need to create a seperate model object since PiTPD entity will do just fine
        //        response = request.CreateResponse<PiTPD>(HttpStatusCode.OK, pitPD);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availablepitPDs")]
        public HttpResponseMessage GetAvailablePiTPDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PiTPD[] pitPDs = _IFRS9Service.GetAllPiTPDs();

                return request.CreateResponse<PiTPD[]>(HttpStatusCode.OK, pitPDs);
            });
        }

        [HttpGet]
        [Route("availablepitPDComparisms")]
        public HttpResponseMessage GetAvailablePiTPDsComparism(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                PiTPDComparism[] pitPDsComparism = _IFRS9Service.GetAllPiTPDComparisms();

                return request.CreateResponse<PiTPDComparism[]>(HttpStatusCode.OK, pitPDsComparism);
            });
        }

        [HttpPost]
        [Route("regresspd")]
        public HttpResponseMessage RegressPD(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _IFRS9Service.RegressPD();


                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
