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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/managementtree")]
    [UsesDisposableService]
    public class ManagementTreeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ManagementTreeApiController(IMPRCoreService mprCoreService)
        {
            _MPRCoreService = mprCoreService;
        }

        IMPRCoreService _MPRCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPRCoreService);
        }

        [HttpPost]
        [Route("updatemanagementtree")]
        public HttpResponseMessage UpdateManagementTree(HttpRequestMessage request, [FromBody]ManagementTree teammanagementtreeModel)
        {
            return GetHttpResponse(request, () =>
            {
                var teammanagementtree = _MPRCoreService.UpdateManagementTree(teammanagementtreeModel);

                return request.CreateResponse<ManagementTree>(HttpStatusCode.OK, teammanagementtree);
            });
        }

        [HttpPost]
        [Route("deletemanagementTree")]
        public HttpResponseMessage DeleteManagementTree(HttpRequestMessage request, [FromBody]int managementTreeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                ManagementTree managementTree = _MPRCoreService.GetManagementTree(managementTreeId);

                if (managementTree != null)
                {
                    _MPRCoreService.DeleteManagementTree(managementTreeId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No managementTree found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getmanagementTree/{managementTreeId}")]
        public HttpResponseMessage GetManagementTree(HttpRequestMessage request, int managementTreeId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                ManagementTree managementTree = _MPRCoreService.GetManagementTree(managementTreeId);

                // notice no need to create a seperate model object since ManagementTree entity will do just fine
                response = request.CreateResponse<ManagementTree>(HttpStatusCode.OK, managementTree);

                return response;
            });
        }

        [HttpGet]
        [Route("availablemanagementTrees")]
        public HttpResponseMessage GetAvailableManagementTrees(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ManagementTreeData[] managementTrees = _MPRCoreService.GetAllManagementTrees();

                return request.CreateResponse<ManagementTreeData[]>(HttpStatusCode.OK, managementTrees);
            });
        }

        [HttpGet]
        [Route("getmanagementTrees")]
        public HttpResponseMessage GetManagementTrees(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ManagementTreeData[] managementTrees = _MPRCoreService.GetAllManagementTrees();

                var accounts = managementTrees.Select(c => c.AccountNo).Distinct();

                List<ManagementTreeModel> roots = new List<ManagementTreeModel>();

                foreach (var account in accounts)
                {
                    List<ManagementTreeModel> children = new List<ManagementTreeModel>();

                    var nodes = managementTrees.Where(c => c.AccountNo == account);

                    foreach (var node in nodes)
                    {
                        children.Add(new ManagementTreeModel()
                        {
                            Column1 = node.TeamDefinitionName,
                            Column2 = node.TeamName,
                            Column3 = node.AccountOfficerDefinitionName,
                            Column4 = node.AccountOfficerName,
                            Column5 = node.Rate 
                        });
                    }

                    var rate = nodes.Sum(c => c.Rate);

                    roots.Add(new ManagementTreeModel()
                    {
                        Column1 = account,
                        Column2 = "",
                        Column3 = "",
                        Column4 = "",
                        Column5 = rate,
                        children = children.ToArray()
                    });
                }

                return request.CreateResponse<ManagementTreeModel[]>(HttpStatusCode.OK, roots.ToArray());
            });
        }
    }
}
