using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.CDQM.Contracts;
using Fintrak.Client.CDQM.Entities;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/cdqmcustomercheck")]
    [UsesDisposableService]
    public class CDQMCustomerCheckApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public CDQMCustomerCheckApiController(ICDQMService cdqmService)
        {
            _CDQMService = cdqmService;
        }

        ICDQMService _CDQMService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CDQMService);
        }

        [HttpPost]
        [Route("updatecdqmcustomerpersistent")]
        public HttpResponseMessage UpdateCDQMCustomerPersistent(HttpRequestMessage request, [FromBody]CDQMCustomerPersistent cdqmCustomerPersistentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmCustomerPersistent = _CDQMService.UpdateCDQMCustomerPersistent(cdqmCustomerPersistentModel);

                return request.CreateResponse<CDQMCustomerPersistent>(HttpStatusCode.OK, cdqmCustomerPersistent);
            });
        }

        [HttpPost]
        [Route("updatecdqmcustomerduplicate")]
        public HttpResponseMessage UpdateCDQMCustomerDuplicate(HttpRequestMessage request, [FromBody]CDQMCustomerDuplicate cdqmCustomerDuplicateModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmCustomerDuplicate = _CDQMService.UpdateCDQMCustomerDuplicate(cdqmCustomerDuplicateModel);

                return request.CreateResponse<CDQMCustomerDuplicate>(HttpStatusCode.OK, cdqmCustomerDuplicate);
            });
        }

        [HttpPost]
        [Route("updatecustomer")]
        public HttpResponseMessage UpdateCustomer(HttpRequestMessage request, [FromBody]CDQMCustomerPersistent cdqmCustomerPersistentModel)
        {
            return GetHttpResponse(request, () =>
            {
                var cdqmCustomerPersistent = _CDQMService.UpdateCustomer(cdqmCustomerPersistentModel);

                return request.CreateResponse<CDQMCustomerPersistent[]>(HttpStatusCode.OK, cdqmCustomerPersistent);
            });
        }

        [HttpGet]
        [Route("getcdqmcustomerpersistent/{groupId}")]
        public HttpResponseMessage GetCustomerPersistents(HttpRequestMessage request, string groupId)
        {
            return GetHttpResponse(request, () =>
            {
                CDQMCustomerPersistent[] cdqmCustomerPersistents = _CDQMService.GetCustomerPersistents(groupId);

                return request.CreateResponse<CDQMCustomerPersistent[]>(HttpStatusCode.OK, cdqmCustomerPersistents);
            });
        }

        [HttpGet]
        [Route("getcustomergroupids")]
        public HttpResponseMessage GetCustomerGroupIds(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] groupIds = _CDQMService.GetCustomerGroupIDs();

                List<KeyValueModel> idModels = new List<KeyValueModel>();

                foreach (var id in groupIds)
                {
                    idModels.Add(new KeyValueModel() {  Key=0,Value = id});
                }

                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, idModels.ToArray());
            });
        }


    }
}
