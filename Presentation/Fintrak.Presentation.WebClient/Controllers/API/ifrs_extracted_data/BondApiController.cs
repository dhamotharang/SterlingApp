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
    [RoutePrefix("api/ifrsbond")]
    [UsesDisposableService]
    public class ifrsbondApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ifrsbondApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateifrsbond")]
        public HttpResponseMessage UpdateifrsBond(HttpRequestMessage request, [FromBody]IFRSBonds ifrsBondModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsBond = _IFRSDataService.UpdateIFRSBonds(ifrsBondModel);

                return request.CreateResponse<IFRSBonds>(HttpStatusCode.OK, ifrsBond);
            });
        }


        [HttpPost]
        [Route("deletebond")]
        public HttpResponseMessage DeleteIFRSBond(HttpRequestMessage request, [FromBody]int bondId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSBonds ifrsbond = _IFRSDataService.GetIFRSBonds(bondId);

                if (ifrsbond != null)
                {
                    _IFRSDataService.DeleteIFRSBonds(bondId); 

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Bonds found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("availableifrsbond")]
        public HttpResponseMessage GetAvailableifrsbonds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSBonds[] ifrsbond = _IFRSDataService.GetAllIFRSBonds().ToArray();

                return request.CreateResponse<IFRSBonds[]>(HttpStatusCode.OK, ifrsbond.ToArray());
            });
        }

        [HttpGet]
        [Route("getbonddata/{bondDataId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int bondDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSBonds ifrsbond = _IFRSDataService.GetIFRSBonds(bondDataId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IFRSBonds>(HttpStatusCode.OK, ifrsbond);

                return response;
            });
        }

        [HttpGet]
        [Route("getifrsbond/{classification}")]
        public HttpResponseMessage GetifrsdBond(HttpRequestMessage request, string classification)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSBonds[] ifrsbond = _IFRSDataService.GetBondsByClassification(classification);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IFRSBonds[]>(HttpStatusCode.OK, ifrsbond.ToArray());

                return response;
            });
        }

        [HttpGet]
        [Route("getbondbymatdate/{matureDate}")]
        public HttpResponseMessage GetTbondbyDate(HttpRequestMessage request, DateTime matureDate)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSBonds[] ifrsbond = _IFRSDataService.GetbondsByMaturityDate(matureDate);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IFRSBonds[]>(HttpStatusCode.OK, ifrsbond.ToArray());

                return response;
            });
        }

        [HttpPost]
        [Route("updatebondbymatdate")]
        public HttpResponseMessage MarketYieldUpdate(HttpRequestMessage request, [FromBody] MaturityParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSDataService.UpdatebondsByMaturityDate(param.Date, param.Amount);
         
                response = request.CreateResponse(HttpStatusCode.OK);
                
                 return response;
            });
        }

        [HttpGet]
        [Route("getMaturityDate")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSBonds[] ifrsbond = _IFRSDataService.GetAllIFRSBonds().ToArray();

                List<MaturityDateModel> maturityDate = new List<MaturityDateModel>();

                List<DateTime> maturitydates = null;

                maturitydates = ifrsbond.Select(c => c.MaturityDate).Distinct().ToList();

                foreach (var c in maturitydates)
                    maturityDate.Add(new MaturityDateModel()
                    {
                        MaturityDate = c
                    });
                return request.CreateResponse<MaturityDateModel[]>(HttpStatusCode.OK, maturityDate.ToArray());
            });
        }
    }
}
