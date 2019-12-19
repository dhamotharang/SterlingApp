using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/generalTransferPrice")]
    [UsesDisposableService]
    public class GeneralTransferPriceApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GeneralTransferPriceApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updategeneraltransferprice")]
        public HttpResponseMessage UpdateGeneralTransferPrice(HttpRequestMessage request, [FromBody]GeneralTransferPrice generaltransferpriceModel)
        {
            return GetHttpResponse(request, () =>
            {
                var generaltransferprice = _MPRCoreService.UpdateGeneralTransferPrice(generaltransferpriceModel);

                return request.CreateResponse<GeneralTransferPrice>(HttpStatusCode.OK, generaltransferprice);
            });
        }

        [HttpPost]
        [Route("deletegeneraltransferprice")]
        public HttpResponseMessage DeleteGeneralTransferPrice(HttpRequestMessage request, [FromBody]int generaltransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GeneralTransferPrice generaltransferprice = _MPRCoreService.GetGeneralTransferPrice(generaltransferpriceId);

                if (generaltransferprice != null)
                {
                    _MPRCoreService.DeleteGeneralTransferPrice(generaltransferpriceId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Account Transfer Price found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getgeneraltransferprice/{generaltransferpriceId}")]
        public HttpResponseMessage GetGeneralTransferPrice(HttpRequestMessage request, int generaltransferpriceId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GeneralTransferPrice generaltransferprice = _MPRCoreService.GetGeneralTransferPrice(generaltransferpriceId);

                // notice no need to create a seperate model object since GeneralTransferPrice entity will do just fine
                response = request.CreateResponse<GeneralTransferPrice>(HttpStatusCode.OK, generaltransferprice);

                return response;
            });
        }

        [HttpGet]
        [Route("generaltransferprice")]
        public HttpResponseMessage GetAvailableGeneralTransferPrices(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GeneralTransferPriceData[] generaltransferprice = _MPRCoreService.GetAllGeneralTransferPrices();

                return request.CreateResponse<GeneralTransferPriceData[]>(HttpStatusCode.OK, generaltransferprice);
            });
        }

        [HttpPost]
        [Route("deleteselectedlist/{selectedIds}")]
        public HttpResponseMessage DeleteSelectedIdList(string selectedIds)
        {
            _MPRCoreService.DeleteGTPSelectedIds(selectedIds);
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}
