using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.MPR.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexmanagementtree")]
    [UsesDisposableService]
    public class OpexManagementTreeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexManagementTreeApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateopexManagementTree")]
        public HttpResponseMessage UpdateOpexManagementTree(HttpRequestMessage request, [FromBody]OpexManagementTree opexManagementTreeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var opexManagementTree = _MPROPEXService.UpdateOpexManagementTree(opexManagementTreeModel);

                return request.CreateResponse<OpexManagementTree>(HttpStatusCode.OK, opexManagementTree);
            });
        }

        [HttpPost]
        [Route("deleteopexManagementTree")]
        public HttpResponseMessage DeleteOpexManagementTree(HttpRequestMessage request, [FromBody]int opexmgtTreeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                OpexManagementTree opexManagementTree = _MPROPEXService.GetOpexManagementTree(opexmgtTreeId);

                if (opexManagementTree != null)
                {
                    _MPROPEXService.DeleteOpexManagementTree(opexmgtTreeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No opexManagementTree found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getopexManagementTree/{opexmgtTreeId}")]
        public HttpResponseMessage GetOpexManagementTree(HttpRequestMessage request, int opexmgtTreeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                OpexManagementTree opexManagementTree = _MPROPEXService.GetOpexManagementTree(opexmgtTreeId);

                // notice no need to create a seperate model object since OpexManagementTree entity will do just fine
                response = request.CreateResponse<OpexManagementTree>(HttpStatusCode.OK, opexManagementTree);

                return response;
            });
        }

        [HttpGet]
        [Route("availableopexManagementTrees")]
        public HttpResponseMessage GetAvailableOpexManagementTrees(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexManagementTreeData[] opexManagementTrees = _MPROPEXService.GetAllOpexManagementTrees();

                return request.CreateResponse<OpexManagementTreeData[]>(HttpStatusCode.OK, opexManagementTrees);
            });
        }
    }
}
