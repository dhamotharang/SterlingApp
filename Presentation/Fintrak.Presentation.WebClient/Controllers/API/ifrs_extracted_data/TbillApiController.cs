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
    [RoutePrefix("api/ifrstbill")]
    [UsesDisposableService]
    public class ifrstbillApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ifrstbillApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateifrstbill")]
        public HttpResponseMessage UpdateifrsTbill(HttpRequestMessage request, [FromBody]IFRSTbills ifrsTbillModel)
        {
            return GetHttpResponse(request, () =>
            {
                var ifrsTbill = _IFRSDataService.UpdateIFRSTbills(ifrsTbillModel);

                return request.CreateResponse<IFRSTbills>(HttpStatusCode.OK, ifrsTbill);
            });
        }


        [HttpPost]
        [Route("deleteifrstbill")]
        public HttpResponseMessage DeleteIFRSTbill(HttpRequestMessage request, [FromBody]int tbillId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSTbills ifrsTbill = _IFRSDataService.GetIFRSTbills(tbillId);

                if (ifrsTbill != null)
                {
                    _IFRSDataService.DeleteIFRSTbills(tbillId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Tbill found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("availableifrstbill")]
        public HttpResponseMessage GetAvailableifrstbills(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSTbills[] ifrstbill = _IFRSDataService.GetAllIFRSTbills().ToArray();

                return request.CreateResponse<IFRSTbills[]>(HttpStatusCode.OK, ifrstbill.ToArray());
            });
        }

        [HttpGet]
        [Route("gettbilldata/{tbillDataId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int tbillDataId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSTbills ifrstbill = _IFRSDataService.GetIFRSTbills(tbillDataId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                response = request.CreateResponse<IFRSTbills>(HttpStatusCode.OK, ifrstbill);

                return response;
            });
        }

        [HttpGet]
        [Route("getifrstbill/{classification}/{type}")]
        public HttpResponseMessage GetifrsdBond(HttpRequestMessage request, string classification, int type)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSTbills[] ifrstbill = _IFRSDataService.GetTbillsByClassification(classification, type);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IFRSTbills[]>(HttpStatusCode.OK, ifrstbill.ToArray());

                return response;
            });
        }

        [HttpGet]
        [Route("gettbillbymatdate/{matureDate}/{type}")]
        public HttpResponseMessage GetTbillbyDate(HttpRequestMessage request, DateTime matureDate, int type)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSTbills[] ifrstbill = _IFRSDataService.GetTbillsByMaturityDate(matureDate, type);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IFRSTbills[]>(HttpStatusCode.OK, ifrstbill.ToArray());

                return response;
            });
        }

        [HttpGet]
        [Route("getMaturityDate/{type}")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request, int type)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSTbills[] ifrstbill = _IFRSDataService.GetAllIFRSTbills().Where(x => x.Flag == type).ToArray();

                List<MaturityDateModel> maturityDate = new List<MaturityDateModel>();

                List<DateTime> maturitydates = null;

                maturitydates = ifrstbill.Select(c => c.MaturityDate).Distinct().ToList();
        
                foreach (var c in maturitydates)
                    maturityDate.Add(new MaturityDateModel()
                    {
                        MaturityDate = c
                    });
                return request.CreateResponse<MaturityDateModel[]>(HttpStatusCode.OK, maturityDate.ToArray());
            });
        }

        [HttpPost]
        [Route("updatetbillbymatdate")]
        public HttpResponseMessage YieldUpdate(HttpRequestMessage request, [FromBody] MaturityParam param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSDataService.UpdateTbillsByMaturityDate(param.Date, param.Amount);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpGet]
        [Route("getlistbytype/{type}")]
        public HttpResponseMessage GetTbillbyDate(HttpRequestMessage request, int type)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSTbills[] ifrstbill = _IFRSDataService.GetListByType(type);

                // notice no need to create a seperate model object since TBillsComputationResult entity will do just fine
                response = request.CreateResponse<IFRSTbills[]>(HttpStatusCode.OK, ifrstbill.ToArray());

                return response;
            });
        }
    }
}
