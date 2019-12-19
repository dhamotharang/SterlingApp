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
    [RoutePrefix("api/ifrscustomeraccount")]
    [UsesDisposableService]
    public class ifrscustomeraccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ifrscustomeraccountApiController(IExtractedDataService ifrsDataService)
        {
            _IFRSDataService = ifrsDataService;
        }

        IExtractedDataService _IFRSDataService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSDataService);
        }

        [HttpPost]
        [Route("updateifrscustomeraccount")]
        public HttpResponseMessage UpdateIfrsCustomerAccount(HttpRequestMessage request, [FromBody]IfrsCustomerAccount IfrsCustomerAccountModel)
        {
            return GetHttpResponse(request, () =>
            {
                var IfrsCustomerAccount = _IFRSDataService.UpdateIfrsCustomerAccount(IfrsCustomerAccountModel);

                return request.CreateResponse<IfrsCustomerAccount>(HttpStatusCode.OK, IfrsCustomerAccount);
            });
        }


        [HttpPost]
        [Route("deleteifrscustomeraccount")]
        public HttpResponseMessage DeleteIfrsCustomerAccount(HttpRequestMessage request, [FromBody]int IfrsCustomerAccountId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IfrsCustomerAccount ifrscustomeraccount = _IFRSDataService.GetIfrsCustomerAccount(IfrsCustomerAccountId);

                if (ifrscustomeraccount != null)
                {
                    _IFRSDataService.DeleteIfrsCustomerAccount(IfrsCustomerAccountId); 

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Bonds found under that ID.");

                return response;
            });
        }


        [HttpGet]
        [Route("availableifrscustomeraccount")]
        public HttpResponseMessage GetAvailableifrscustomeraccounts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IfrsCustomerAccount[] ifrscustomeraccount = _IFRSDataService.GetAllIfrsCustomerAccount().ToArray();

                return request.CreateResponse<IfrsCustomerAccount[]>(HttpStatusCode.OK, ifrscustomeraccount.ToArray());
            });
        }

        [HttpGet]
        [Route("getcustomer/{IfrsCustomerAccountId}")]
        public HttpResponseMessage GetSetup(HttpRequestMessage request, int IfrsCustomerAccountId)
        {
            return GetHttpResponse(request, () =>
            {
               // HttpResponseMessage response = null;

                IfrsCustomerAccount ifrscustomeraccount = _IFRSDataService.GetIfrsCustomerAccount(IfrsCustomerAccountId);

                // notice no need to create a seperate model object since Setup entity will do just fine
                return request.CreateResponse<IfrsCustomerAccount>(HttpStatusCode.OK, ifrscustomeraccount);

               
            });
        }

        [HttpGet]
        [Route("getdistinctsector")]
        public HttpResponseMessage GetDistinctSector(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] sectorlist = _IFRSDataService.GetDistinctSector();

                var model = new List<KeyValueData>();

                foreach (var s in sectorlist)
                    model.Add(new KeyValueData() { Key = s, Value = s });

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, model.ToArray());

            });
        }
      
    }
}
