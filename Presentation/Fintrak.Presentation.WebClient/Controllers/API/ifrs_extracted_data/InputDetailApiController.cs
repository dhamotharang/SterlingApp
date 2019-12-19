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
    [RoutePrefix("api/inputdetail")]
    [UsesDisposableService]
    public class InputDetailApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InputDetailApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateinputdetail")]
        public HttpResponseMessage UpdateInputDetail(HttpRequestMessage request, [FromBody]InputDetail inputDetailModel)
        {
            return GetHttpResponse(request, () =>
            {
                var inputDetail = _IFRSDataService.UpdateInputDetail(inputDetailModel);

                return request.CreateResponse<InputDetail>(HttpStatusCode.OK, inputDetail);
            });
        }

        [HttpPost]
        [Route("deleteinputdetail")]
        public HttpResponseMessage DeleteInputDetail(HttpRequestMessage request, [FromBody]int InputDetailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InputDetail inputDetail = _IFRSDataService.GetInputDetail(InputDetailId);

                if (inputDetail != null)
                {
                    _IFRSDataService.DeleteInputDetail(InputDetailId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No inputDetail found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinputdetail/{inputDetailId}")]
        public HttpResponseMessage GetInputDetail(HttpRequestMessage request, int InputDetailId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InputDetail inputDetail = _IFRSDataService.GetInputDetail(InputDetailId);

                // notice no need to create a seperate model object since InputDetail entity will do just fine
                response = request.CreateResponse<InputDetail>(HttpStatusCode.OK, inputDetail);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinputdetails")]
        public HttpResponseMessage GetAvailableInputDetails(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InputDetail[] inputDetails = _IFRSDataService.GetAllInputDetails();

                return request.CreateResponse<InputDetail[]>(HttpStatusCode.OK, inputDetails);
            });
        }

        [HttpGet]
        [Route("availableeclweightedavgs")]
        public HttpResponseMessage GetAvailableEclWeightedAvgs(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                EclWeightedAvg[] eclWeightedAvg = _IFRSDataService.GetAllEclWeightedAvgs();

                return request.CreateResponse<EclWeightedAvg[]>(HttpStatusCode.OK, eclWeightedAvg);
            });
        }

        [HttpGet]
        [Route("insertbyrefno/{refNo}")]
        public HttpResponseMessage InsertByRefno(HttpRequestMessage request, string RefNo)
        {
            return GetHttpResponse(request, () =>
            {
                //HttpResponseMessage response = null;

                int Status = _IFRSDataService.InsertByRefno(RefNo);

                return request.CreateResponse<int>(HttpStatusCode.OK, Status);

                //response = request.CreateResponse(HttpStatusCode.OK);

                //return response;
            });
        }

        [HttpGet]
        [Route("computeecl")]
        public HttpResponseMessage ComputeEcl(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSDataService.ComputeEcl();

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
