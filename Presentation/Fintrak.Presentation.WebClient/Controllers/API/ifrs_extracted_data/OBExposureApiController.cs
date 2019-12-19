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
    [RoutePrefix("api/obExposure")]
    [UsesDisposableService]
    public class OBExposureApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OBExposureApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateOBExposure")]
        public HttpResponseMessage UpdateOBExposure(HttpRequestMessage request, [FromBody]OBExposure obExposureModel)
        {
            return GetHttpResponse(request, () =>
            {
                var obExposure = _IFRSDataService.UpdateOBExposure(obExposureModel);

                return request.CreateResponse<OBExposure>(HttpStatusCode.OK, obExposure);
            });
        }


        [HttpPost]
        [Route("deleteOBExposure")]
        public HttpResponseMessage DeleteOBExposure(HttpRequestMessage request, [FromBody]int obeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OBExposure OBExposure = _IFRSDataService.GetOBExposurebyId(obeId);

                if (OBExposure != null)
                {
                    _IFRSDataService.DeleteOBExposure(obeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OBE found under that ID.");

                return response;
            });
        }
        [HttpGet]
        [Route("availableOBExposure/{flag}/{defaultCount}")]
        public HttpResponseMessage GetAvailableOBExposures(HttpRequestMessage request, int flag, int defaultCount)
        {
            return GetHttpResponse(request, () =>
            {
                if (defaultCount == 0)
                {
                    string path = HostingEnvironment.MapPath("~");
                    var fileStream = _IFRSDataService.GetOBExposure(flag, defaultCount, path + "ExportedData\\");
                    var fileName = flag == 1 ? "Pry_data_-_BONDS_AND_GTEE" : flag == 2 ? "Pry_data_-_LETTER_OF_CREDIT" : "Pry_data_-_OVERDRAFT";
                    var response = DownloadFileService.DownloadFile(path, fileName + ".zip");
                    return response;
                }
                else
                {
                    OBExposure[] OBExposure = _IFRSDataService.GetOBExposure(flag, defaultCount, null).ToArray();

                    return request.CreateResponse(HttpStatusCode.OK, OBExposure.ToArray());
                }
            });
        }


        [HttpGet]
        [Route("getOBExposure/{obeId}")]
        public HttpResponseMessage GetBorrowing(HttpRequestMessage request, int obeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OBExposure obeExposure = _IFRSDataService.GetOBExposurebyId(obeId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<OBExposure>(HttpStatusCode.OK, obeExposure);

                return response;
            });
        }

        [HttpGet]
        [Route("availableOBExposureBySearch/{flag}/{searchParam}")]
        public HttpResponseMessage GetAvailableOBExposuresBySearch(HttpRequestMessage request, int flag, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                OBExposure[] OBExposures = _IFRSDataService.GetOBExposureBySearch(flag, searchParam.Replace("__", "/"));

                return request.CreateResponse<OBExposure[]>(HttpStatusCode.OK, OBExposures);
            });
        }

        [HttpGet]
        [Route("getHC1")]
        public HttpResponseMessage GetProductTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HCClassification[] HCClassification = _IFRSDataService.GetAllHCClassification();

                IEnumerable<string> hc1 = HCClassification.Select(e => e.HC1).Distinct();

                return request.CreateResponse(HttpStatusCode.OK, hc1.Select(e => new { name = e, value = e }));

            });
        }

        [HttpGet]
        [Route("getHC2ByHC1/{hc1}")]
        public HttpResponseMessage GetProductTypes(HttpRequestMessage request, string hc1)
        {
            return GetHttpResponse(request, () =>
            {
                HCClassification[] HCClassification = _IFRSDataService.GetAllHCClassification();

                IEnumerable<string> hc2 = HCClassification.Where(e => e.HC1 == hc1).Select(e => e.HC2).Distinct();

                return request.CreateResponse(HttpStatusCode.OK, hc2.Select(e => new { name = e, value = e }));

            });
        }

        //[HttpGet]
        //[Route("getproductTypes")]
        //public HttpResponseMessage GetProductTypes(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        IEnumerable<string> productTypes = _IFRSDataService.GetProductTypes();

        //        return request.CreateResponse<IEnumerable<string>>(HttpStatusCode.OK, productTypes);

        //    });
        //}

        //[HttpGet]
        //[Route("getproductTypes")]
        //public HttpResponseMessage GetProductTypes(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        string[] productTypes = _IFRSDataService.GetProductTypes();
        //        List<KeyValueModel> val = new List<KeyValueModel>();
        //        foreach (var c in productTypes)
        //            val.Add(new KeyValueModel()
        //            {
        //                Value = c

        //            });

        //        return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, val.ToArray());

        //    });
        //}


        //[HttpGet]
        //[Route("getsubtypebyproducttype/{productType}")]
        //public HttpResponseMessage GetSubTypeByProductType(HttpRequestMessage request,string productType)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        string[] subTypes = _IFRSDataService.GetsubTypes(productType);
        //        List<KeyValueModel> val = new List<KeyValueModel>();
        //        foreach (var c in subTypes)
        //            val.Add(new KeyValueModel()
        //            {
        //                Value = c

        //            });

        //        return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, val.ToArray());

        //    });
        //}
    }
}
