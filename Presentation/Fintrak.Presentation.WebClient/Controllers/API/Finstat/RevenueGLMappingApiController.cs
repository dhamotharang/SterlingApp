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
    [RoutePrefix("api/revenueglmapping")]
    [UsesDisposableService]
    public class RevenueGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RevenueGLMappingApiController(IFinstatService finstatService)
        {
            _FinstatService = finstatService;
        }

        IFinstatService _FinstatService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_FinstatService);
        }

        [HttpPost]
        [Route("updateglMapping")]
        public HttpResponseMessage UpdateGLMapping(HttpRequestMessage request, [FromBody]RevenueGLMapping glMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMapping = _FinstatService.UpdateRevenueGLMapping(glMappingModel);

                return request.CreateResponse<RevenueGLMapping>(HttpStatusCode.OK, glMapping);
            });
        }

        [HttpPost]
        [Route("deleteglMapping")]
        public HttpResponseMessage DeleteGLMapping(HttpRequestMessage request, [FromBody]int glMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                RevenueGLMapping glMapping = _FinstatService.GetRevenueGLMapping(glMappingId);

                if (glMapping != null)
                {
                    _FinstatService.DeleteRevenueGLMapping(glMappingId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glMapping found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglMapping/{glMappingId}")]
        public HttpResponseMessage GetGLMapping(HttpRequestMessage request,int glMappingId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                RevenueGLMapping glMapping = _FinstatService.GetRevenueGLMapping(glMappingId);

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<RevenueGLMapping>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMappings")]
        public HttpResponseMessage GetAvailableGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueGLMapping[] glMappings = _FinstatService.GetAllRevenueGLMappings();

                return request.CreateResponse<RevenueGLMapping[]>(HttpStatusCode.OK, glMappings);
            });
        }

        [HttpGet]
        [Route("getsubcaptions/{level}")]
        public HttpResponseMessage GetSubCaptionss(HttpRequestMessage request,int level)
        {
            return GetHttpResponse(request, () =>
            {
                RevenueGLMapping[] glMappings = _FinstatService.GetAllRevenueGLMappings();

                List<CaptionModel> captions = new List<CaptionModel>();

                List<string> distinctCaptions = null;

                if (level == 0){
                    distinctCaptions = glMappings.Select(c => c.Caption).Distinct().ToList();
                }
                else if (level == 1)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption).Distinct().ToList();
                }
                else if (level == 2)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption1).Distinct().ToList();
                }
                else if (level == 3)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption2).Distinct().ToList();
                }
                else if (level == 4)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption3).Distinct().ToList();
                }
                else if (level == 5)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption4).Distinct().ToList();
                }
                
                foreach (var c in distinctCaptions)
                    captions.Add(new CaptionModel() {
                        Code = c,
                        Name = c
                    });

                return request.CreateResponse<CaptionModel[]>(HttpStatusCode.OK, captions.ToArray());
            });
        }

        [HttpGet]
        [Route("getunmappedgl")]
        public HttpResponseMessage GetUnMappedGL(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                KeyValueData[] gls = _FinstatService.GetUnMappedRevenueGLs();

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, gls);
            });
        }

      
    }
}
