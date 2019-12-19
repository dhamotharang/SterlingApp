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
    [RoutePrefix("api/neabranchsbushares")]
    [UsesDisposableService]
    public class NEABranchSBUSharesApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NEABranchSBUSharesApiController(IMPROPEXService mprOpexService)
        {
            _MPROPEXService = mprOpexService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateneabranchsbushares")]
        public HttpResponseMessage UpdateNEABranchSBUShares(HttpRequestMessage request, [FromBody]NEABranchSBUShares nEABranchSBUSharesModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nEABranchSBUShares = _MPROPEXService.UpdateNEABranchSBUShares(nEABranchSBUSharesModel);

                return request.CreateResponse<NEABranchSBUShares>(HttpStatusCode.OK, nEABranchSBUShares);
            });
        }

        [HttpPost]
        [Route("deleteneabranchsbushares")]
        public HttpResponseMessage DeleteNEABranchSBUShares(HttpRequestMessage request, [FromBody]int nEABranchSBUSharesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NEABranchSBUShares nEABranchSBUShares = _MPROPEXService.GetNEABranchSBUShares(nEABranchSBUSharesId);

                if (nEABranchSBUShares != null)
                {
                    _MPROPEXService.DeleteNEABranchSBUShares(nEABranchSBUSharesId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No Opex Business Rule found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getneabranchsbushares/{neabranchsbusharesId}")]
        public HttpResponseMessage GetNEABranchSBUShares(HttpRequestMessage request, int nEABranchSBUSharesId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NEABranchSBUShares nEABranchSBUShares = _MPROPEXService.GetNEABranchSBUShares(nEABranchSBUSharesId);

                // notice no need to create a seperate model object since NEABranchSBUShares entity will do just fine
                response = request.CreateResponse<NEABranchSBUShares>(HttpStatusCode.OK, nEABranchSBUShares);

                return response;
            });
        }

        [HttpGet]
        [Route("availableneabranchsbushares")]
        public HttpResponseMessage GetAvailableNEABranchSBUShares(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NEABranchSBUShares[] nEABranchSBUShares = _MPROPEXService.GetAllNEABranchSBUShares();


                return request.CreateResponse<NEABranchSBUShares[]>(HttpStatusCode.OK, nEABranchSBUShares);
            });
        }
    }
}
