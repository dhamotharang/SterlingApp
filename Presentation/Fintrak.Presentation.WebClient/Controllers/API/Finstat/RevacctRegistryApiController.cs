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
    [RoutePrefix("api/revacctregistry")]
    [UsesDisposableService]
    public class IFRSRevacctRegistryApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public IFRSRevacctRegistryApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        [HttpPost]
        [Route("updaterevacctregistry")]
        public HttpResponseMessage UpdateIFRSRevacctRegistry(HttpRequestMessage request, [FromBody]IFRSRevacctRegistry revacctregistryModel)
        {
            return GetHttpResponse(request, () =>
            {
                var revacctregistry = _IFRSCoreService.UpdateIFRSRevacctRegistry(revacctregistryModel);

                return request.CreateResponse<IFRSRevacctRegistry>(HttpStatusCode.OK, revacctregistry);
            });
        }

        [HttpPost]
        [Route("deleterevacctregistry")]
        public HttpResponseMessage DeleteIFRSRevacctRegistry(HttpRequestMessage request, [FromBody]int revenueId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                IFRSRevacctRegistry revacctregistry = _IFRSCoreService.GetIFRSRevacctRegistry(revenueId);

                if (revacctregistry != null)
                {
                    _IFRSCoreService.DeleteIFRSRevacctRegistry(revenueId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No revenue registry found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getrevacctregistry/{revenueId}")]
        public HttpResponseMessage GetIFRSRevacctRegistry(HttpRequestMessage request,int revenueId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IFRSRevacctRegistry revacctregistry = _IFRSCoreService.GetIFRSRevacctRegistry(revenueId);

                // notice no need to create a seperate model object since IFRSRevacctRegistry entity will do just fine
                response = request.CreateResponse<IFRSRevacctRegistry>(HttpStatusCode.OK, revacctregistry);

                return response;
            });
        }

        [HttpGet]
        [Route("availablerevacctregistrys")]
        public HttpResponseMessage GetAvailableRegistries(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSRevacctRegistryData[] revacctregistrys = _IFRSCoreService.GetAllIFRSRevacctRegistries();

                return request.CreateResponse<IFRSRevacctRegistryData[]>(HttpStatusCode.OK, revacctregistrys);
            });
        }

        [HttpGet]
        [Route("getmaincaptions")]
        public HttpResponseMessage GetMainCaptions(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                IFRSRevacctRegistryData[] revacctregistries = _IFRSCoreService.GetAllIFRSRevacctRegistries();

                List<CaptionModel> captions = new List<CaptionModel>();

                List<string> distinctCaptionCodes = revacctregistries.Where(c => c.IsTotalLine == false).OrderBy(c => c.Caption).Select(c => c.Code).Distinct().ToList();

              

                foreach (var c in distinctCaptionCodes)
                {
                    var mainCap = revacctregistries.Where(i => i.Code == c).FirstOrDefault();
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
