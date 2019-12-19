using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/packagesetup")]
    [UsesDisposableService]
    public class PackageSetupApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public PackageSetupApiController(IExtractionProcessService extractionProcessService)
        {
            _ExtractionProcessService = extractionProcessService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updatepackagesetup")]
        public HttpResponseMessage UpdatePackageSetup(HttpRequestMessage request, [FromBody]PackageSetup packagesetupModel)
        {
            return GetHttpResponse(request, () =>
            {
                var packagesetup = _ExtractionProcessService.UpdatePackageSetup(packagesetupModel);

                return request.CreateResponse<PackageSetup>(HttpStatusCode.OK, packagesetup);
            });
        }

        [HttpGet]
        [Route("getpackagesetup")]
        public HttpResponseMessage GetPackageSetup(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                PackageSetup packagesetup = _ExtractionProcessService.GetFirstPackageSetup();

                // notice no need to create a seperate model object since PackageSetup entity will do just fine
                response = request.CreateResponse<PackageSetup>(HttpStatusCode.OK, packagesetup);

                return response;
            });
        }
    }
}
