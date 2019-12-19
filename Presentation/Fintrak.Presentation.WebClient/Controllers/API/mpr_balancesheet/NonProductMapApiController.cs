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
    [RoutePrefix("api/nonproductmap")]
    [UsesDisposableService]
    public class NonProductMapApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public NonProductMapApiController(IMPRBSService mprBSService)
        {
            _MPRBSService = mprBSService;
        }

        IMPRBSService _MPRBSService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRBSService);
        }

        [HttpPost]
        [Route("updatenonproductmap")]
        public HttpResponseMessage UpdateNonProductMap(HttpRequestMessage request, [FromBody]NonProductMap nonProductMapModel)
        {
            return GetHttpResponse(request, () =>
            {
                var nonProductMap = _MPRBSService.UpdateNonProductMap(nonProductMapModel);

                return request.CreateResponse<NonProductMap>(HttpStatusCode.OK, nonProductMap);
            });
        }

        [HttpPost]
        [Route("deletenonProductMap")]
        public HttpResponseMessage DeleteNonProductMap(HttpRequestMessage request, [FromBody]int nonProductMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                NonProductMap nonProductMap = _MPRBSService.GetNonProductMap(nonProductMapId);

                if (nonProductMap != null)
                {
                    _MPRBSService.DeleteNonProductMap(nonProductMapId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No nonProductMap found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getnonProductMap/{nonProductMapId}")]
        public HttpResponseMessage GetNonProductMap(HttpRequestMessage request, int nonProductMapId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                NonProductMap nonProductMap = _MPRBSService.GetNonProductMap(nonProductMapId);

                // notice no need to create a seperate model object since NonProductMap entity will do just fine
                response = request.CreateResponse<NonProductMap>(HttpStatusCode.OK, nonProductMap);

                return response;
            });
        }

        [HttpGet]
        [Route("availablenonProductMaps")]
        public HttpResponseMessage GetAvailableNonProductMaps(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                NonProductMapData[] nonProductMaps = _MPRBSService.GetAllNonProductMaps();

                return request.CreateResponse<NonProductMapData[]>(HttpStatusCode.OK, nonProductMaps);
            });
        }
    }
}
