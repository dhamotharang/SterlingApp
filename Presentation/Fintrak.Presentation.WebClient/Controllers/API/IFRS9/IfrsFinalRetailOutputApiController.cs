using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Services;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/ifrsfinalretailoutput")]
    [UsesDisposableService]
    public class IfrsFinalRetailOutputApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IfrsFinalRetailOutputApiController(IIFRS9Service ifrs9Service)
        {
            _IFRS9Service = ifrs9Service;
        }

        IIFRS9Service _IFRS9Service;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRS9Service);
        }

        [HttpPost]
        [Route("updateIfrsFinalRetailOutput")]
        public HttpResponseMessage UpdateIfrsFinalRetailOutput(HttpRequestMessage request, [FromBody]IfrsFinalRetailOutput IfrsFinalRetailOutputModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsFinalRetailOutput = _IFRS9Service.UpdateIfrsFinalRetailOutput(IfrsFinalRetailOutputModel);

                return request.CreateResponse<IfrsFinalRetailOutput>(HttpStatusCode.OK, IfrsFinalRetailOutput);
            });
        }

        [HttpPost]
        [Route("deleteIfrsFinalRetailOutput")]
        public HttpResponseMessage DeleteIfrsFinalRetailOutput(HttpRequestMessage request, [FromBody]int InstrumentId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsFinalRetailOutput IfrsFinalRetailOutput = _IFRS9Service.GetIfrsFinalRetailOutput(InstrumentId);

                if (IfrsFinalRetailOutput != null)
                {
                    _IFRS9Service.DeleteIfrsFinalRetailOutput(InstrumentId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No IfrsFinalRetailOutput found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsFinalRetailOutputId/{Id}")]
        public HttpResponseMessage GetIfrsFinalRetailOutputId(HttpRequestMessage request, int Id)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsFinalRetailOutput IfrsFinalRetailOutput = _IFRS9Service.GetIfrsFinalRetailOutput(Id);

                // notice no need to create a seperate model object since IfrsFinalRetailOutput entity will do just fine
                response = request.CreateResponse<IfrsFinalRetailOutput>(HttpStatusCode.OK, IfrsFinalRetailOutput);

                return response;
            });
        }

        [HttpGet]
        [Route("getIfrsFinalRetailOutputByAccountNo/{accountNo}")]
        public HttpResponseMessage GetIfrsFinalRetailOutput(HttpRequestMessage request, string accountNo)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsFinalRetailOutput[] IfrsFinalRetailOutput = _IFRS9Service.GetIfrsFinalRetailOutputByAccountNo(accountNo);

                // notice no need to create a seperate model object since IfrsFinalRetailOutput entity will do just fine
                response = request.CreateResponse<IfrsFinalRetailOutput[]>(HttpStatusCode.OK, IfrsFinalRetailOutput);

                return response;
            });
        }

        [HttpGet]
        [Route("getAllIfrsFinalRetailOutputs")]
        public HttpResponseMessage GetAllIfrsFinalRetailOutput(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IfrsFinalRetailOutput[] IfrsFinalRetailOutput = _IFRS9Service.GetAllIfrsFinalRetailOutput();

                // notice no need to create a seperate model object since IfrsFinalRetailOutput entity will do just fine
                response = request.CreateResponse<IfrsFinalRetailOutput[]>(HttpStatusCode.OK, IfrsFinalRetailOutput);

                return response;
            });
        }
    }
}
