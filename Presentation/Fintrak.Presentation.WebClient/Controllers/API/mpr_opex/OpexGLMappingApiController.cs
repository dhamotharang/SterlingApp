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
using Fintrak.Presentation.WebClient.Models;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/opexglmapping")]
    [UsesDisposableService]
    public class OpexGLMappingApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public OpexGLMappingApiController(IMPROPEXService mprOPEXService)
        {
            _MPROPEXService = mprOPEXService;
        }

        IMPROPEXService _MPROPEXService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_MPROPEXService);
        }

        [HttpPost]
        [Route("updateglMapping")]
        public HttpResponseMessage UpdateGLMapping(HttpRequestMessage request, [FromBody]OpexGLMapping glMappingModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMapping = _MPROPEXService.UpdateOpexGLMapping(glMappingModel);

                return request.CreateResponse<OpexGLMapping>(HttpStatusCode.OK, glMapping);
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
                OpexGLMapping glMapping = _MPROPEXService.GetOpexGLMapping(glMappingId);

                if (glMapping != null)
                {
                    _MPROPEXService.DeleteOpexGLMapping(glMappingId);

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

                OpexGLMapping glMapping = _MPROPEXService.GetOpexGLMapping(glMappingId);

                // notice no need to create a seperate model object since GLMapping entity will do just fine
                response = request.CreateResponse<OpexGLMapping>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMappings")]
        public HttpResponseMessage GetAvailableGLMappings(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                OpexGLMapping[] glMappings = _MPROPEXService.GetAllOpexGLMappings();

                return request.CreateResponse<OpexGLMapping[]>(HttpStatusCode.OK, glMappings);
            });
        }

        [HttpGet]
        [Route("getsubcaptions/{level}")]
        public HttpResponseMessage GetSubCaptionss(HttpRequestMessage request,int level)
        {
            return GetHttpResponse(request, () =>
            {
                OpexGLMapping[] glMappings = _MPROPEXService.GetAllOpexGLMappings();

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
                KeyValueData[] gls = _MPROPEXService.GetUnMappedOpexGLs();

                return request.CreateResponse<KeyValueData[]>(HttpStatusCode.OK, gls);
            });
        }

      
    }
}
