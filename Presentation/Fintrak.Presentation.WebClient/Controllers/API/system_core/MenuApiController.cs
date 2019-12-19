using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.SystemCore.Contracts;
using Fintrak.Client.SystemCore.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/menu")]
    [UsesDisposableService]
    public class MenuApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public MenuApiController(ICoreService coreService)
        {
            _CoreService = coreService;
        }

        ICoreService _CoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
        }

        [HttpGet]
        [Route("availablemenus")]
        public HttpResponseMessage GetAvailableMenus(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                Menu[] menus = _CoreService.GetAllMenus();

                return request.CreateResponse<Menu[]>(HttpStatusCode.OK, menus);
            });
        }
    }
}
