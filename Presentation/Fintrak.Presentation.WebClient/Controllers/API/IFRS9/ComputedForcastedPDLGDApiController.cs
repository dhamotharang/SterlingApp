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
    [RoutePrefix("api/computedforcastedpdlgd")]
    [UsesDisposableService]
    public class ComputedForcastedPDLGDApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ComputedForcastedPDLGDApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        //[HttpPost]
        //[Route("updatecomputedForcastedPDLGD")]
        //public HttpResponseMessage UpdateComputedForcastedPDLGD(HttpRequestMessage request, [FromBody]ComputedForcastedPDLGD computedForcastedPDLGDModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var computedForcastedPDLGD = _IFRS9Service.UpdateComputedForcastedPDLGD(computedForcastedPDLGDModel);

        //        return request.CreateResponse<ComputedForcastedPDLGD>(HttpStatusCode.OK, computedForcastedPDLGD);
        //    });
        //}

        //[HttpPost]
        //[Route("deletecomputedForcastedPDLGD")]
        //public HttpResponseMessage DeleteComputedForcastedPDLGD(HttpRequestMessage request, [FromBody]int computedForcastedPDLGDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        // not that calling the WCF service here will authenticate access to the data
        //        ComputedForcastedPDLGD computedForcastedPDLGD = _IFRS9Service.GetComputedForcastedPDLGD(computedForcastedPDLGDId);

        //        if (computedForcastedPDLGD != null)
        //        {
        //            _IFRS9Service.DeleteComputedForcastedPDLGD(computedForcastedPDLGDId);

        //            response = request.CreateResponse(HttpStatusCode.OK);
        //        }
        //        else
        //            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No computedForcastedPDLGD found under that ID.");

        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("getcomputedForcastedPDLGD/{computedForcastedPDLGDId}")]
        //public HttpResponseMessage GetComputedForcastedPDLGD(HttpRequestMessage request,int computedForcastedPDLGDId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;

        //        ComputedForcastedPDLGD computedForcastedPDLGD = _IFRS9Service.GetComputedForcastedPDLGD(computedForcastedPDLGDId);

        //        // notice no need to create a seperate model object since ComputedForcastedPDLGD entity will do just fine
        //        response = request.CreateResponse<ComputedForcastedPDLGD>(HttpStatusCode.OK, computedForcastedPDLGD);

        //        return response;
        //    });
        //}

        [HttpGet]
        [Route("availablecomputedForcastedPDLGDs")]
        public HttpResponseMessage GetAvailableComputedForcastedPDLGDs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ComputedForcastedPDLGD[] computedForcastedPDLGDs = _IFRS9Service.GetListComputedForcastedPDLGDs();

                return request.CreateResponse<ComputedForcastedPDLGD[]>(HttpStatusCode.OK, computedForcastedPDLGDs);
            });
        }
    }
}
