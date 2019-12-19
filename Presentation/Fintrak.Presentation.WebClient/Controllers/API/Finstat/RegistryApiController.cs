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
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/registry")]
    [UsesDisposableService]
    public class IFRSRegistryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRSRegistryApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        [HttpPost]
        [Route("updateregistry")]
        public HttpResponseMessage UpdateIFRSRegistry(HttpRequestMessage request, [FromBody]IFRSRegistry registryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var registry = _IFRSCoreService.UpdateIFRSRegistry(registryModel);

                return request.CreateResponse<IFRSRegistry>(HttpStatusCode.OK, registry);
            });
        }

        [HttpPost]
        [Route("deleteregistry")]
        public HttpResponseMessage DeleteIFRSRegistry(HttpRequestMessage request, [FromBody]int registryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSRegistry registry = _IFRSCoreService.GetIFRSRegistry(registryId);

                if (registry != null)
                {
                    _IFRSCoreService.DeleteIFRSRegistry(registryId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No registry found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getregistry/{registryId}")]
        public HttpResponseMessage GetIFRSRegistry(HttpRequestMessage request,int registryId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSRegistry registry = _IFRSCoreService.GetIFRSRegistry(registryId);

                // notice no need to create a seperate model object since IFRSRegistry entity will do just fine
                response = request.CreateResponse<IFRSRegistry>(HttpStatusCode.OK, registry);

                return response;
            });
        }

        [HttpGet]
        [Route("availableregistrys/{flag}")]
        public HttpResponseMessage GetAvailableRegistries(HttpRequestMessage request, int flag)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSRegistryData[] registrys = _IFRSCoreService.GetAllIFRSRegistries(flag);

                return request.CreateResponse<IFRSRegistryData[]>(HttpStatusCode.OK, registrys);
            });
        }

        [HttpGet]
        [Route("getmaincaptions/{flag}")]
        public HttpResponseMessage GetMainCaptions(HttpRequestMessage request, int flag)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSRegistryData[] registries = _IFRSCoreService.GetAllIFRSRegistries(flag);

                List<CaptionModel> captions = new List<CaptionModel>();

                List<string> distinctCaptionCodes = registries.Where(c => c.IsTotalLine == false).OrderBy(c => c.Caption).Select(c => c.Code).Distinct().ToList();

              

                foreach (var c in distinctCaptionCodes)
                {
                    var mainCap = registries.Where(i => i.Code == c).FirstOrDefault();
                    captions.Add(new CaptionModel()
                    {
                        Code = mainCap.Code,
                        Name = mainCap.Caption
                    });
                }
                    

                return request.CreateResponse<CaptionModel[]>(HttpStatusCode.OK, captions.ToArray());
            });
        }

        [HttpGet]
        [Route("getdistinctrefnotes")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string[] refNotes = _IFRSCoreService.GetDistinctRefNotes();
                List<ReferenceNoModel> refNos = new List<ReferenceNoModel>();
                foreach (var c in refNotes)
                    refNos.Add(new ReferenceNoModel()
                    {
                        RefNo = c

                    });

                return request.CreateResponse<ReferenceNoModel[]>(HttpStatusCode.OK, refNos.ToArray());
            });
        }
    }
}
