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
    [RoutePrefix("api/facilityclassification")]
    [UsesDisposableService]
    public class FacilityClassificationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public FacilityClassificationApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updatefacilityclassification")]
        public HttpResponseMessage UpdatefacClassification(HttpRequestMessage request, [FromBody]FacilityClassification ofacClassificationModel)
        {
            return GetHttpResponse(request, () =>
            {
                var facClassification = _IFRS9Service.UpdateFacilityClassification(ofacClassificationModel);

                return request.CreateResponse<FacilityClassification>(HttpStatusCode.OK, facClassification);
            });
        }


        [HttpPost]
        [Route("deletefacilityclassification")]
        public HttpResponseMessage DeleteIFRSTbill(HttpRequestMessage request, [FromBody]int facClassId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                FacilityClassification facClassification = _IFRS9Service.GetFacilityClassificationbyId(facClassId);

                if (facClassification != null)
                {
                    _IFRS9Service.DeleteFacilityClassification(facClassId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No OBE found under that ID.");

                return response;
            });
        }
        [HttpGet]
        [Route("availablefacilityclassifications/{defaultcount}/{type}")]
        public HttpResponseMessage GetAvailableFacilityClassifications(HttpRequestMessage request,int defaultcount, string type)
        {
            return GetHttpResponse(request, () =>
            {
                FacilityClassification[] facilityclassifications = _IFRS9Service.GetFacilityClassification(defaultcount,type);

                return request.CreateResponse<FacilityClassification[]>(HttpStatusCode.OK, facilityclassifications);
            });
        }


        [HttpGet]
        [Route("getfacilityclassification/{obeId}")]
        public HttpResponseMessage GetBorrowing(HttpRequestMessage request, int obeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                FacilityClassification obeExposure = _IFRS9Service.GetFacilityClassificationbyId(obeId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<FacilityClassification>(HttpStatusCode.OK, obeExposure);

                return response;
            });
        }

        [HttpGet]
        [Route("availablefacilityclassificationsbysearch/{type}/{searchParam}")]
        public HttpResponseMessage GetAvailableFacilityClassificationsBySearch(HttpRequestMessage request, string type, string searchParam)
        {
            return GetHttpResponse(request, () =>
            {
                FacilityClassification[] facilityclassifications = _IFRS9Service.GetFacilityClassificationBySearch(type, searchParam);

                return request.CreateResponse<FacilityClassification[]>(HttpStatusCode.OK, facilityclassifications);
            });
        }


        [HttpGet]
        [Route("getproductTypes")]
        public HttpResponseMessage GetProductTypes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] productTypes = _IFRS9Service.GetProductTypes();
                List<KeyValueModel> val = new List<KeyValueModel>();
                foreach (var c in productTypes)
                    val.Add(new KeyValueModel()
                    {
                        Value = c

                    });

                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, val.ToArray());

            });
        }


        [HttpGet]
        [Route("getsubtypebyproducttype/{productType}")]
        public HttpResponseMessage GetSubTypeByProductType(HttpRequestMessage request,string productType)
        {
            return GetHttpResponse(request, () =>
            {
                string[] subTypes = _IFRS9Service.GetsubTypes(productType);
                List<KeyValueModel> val = new List<KeyValueModel>();
                foreach (var c in subTypes)
                    val.Add(new KeyValueModel()
                    {
                        Value = c

                    });

                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, val.ToArray());

            });
        }
    }
}
