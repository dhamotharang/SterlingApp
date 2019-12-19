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
    [RoutePrefix("api/glmappingmgt")]
    [UsesDisposableService]
    public class GLMappingMgtMgtApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public GLMappingMgtMgtApiController(IFinstatService finstatService)
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
        public HttpResponseMessage UpdateGLMappingMgt(HttpRequestMessage request, [FromBody]GLMappingMgtModel glMappingMgtModel)
        {
            return GetHttpResponse(request, () =>
            {
                var glMapping = _FinstatService.UpdateGLMappingMgt(glMappingMgtModel.GLMappingMgt, glMappingMgtModel.Status);

                return request.CreateResponse<GLMappingMgt>(HttpStatusCode.OK, glMapping);
                //vb
            });
        }

        [HttpPost]
        [Route("deleteglMapping")]
        public HttpResponseMessage DeleteGLMappingMgt(HttpRequestMessage request, [FromBody]int glMappingMgtId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                GLMappingMgt glMapping = _FinstatService.GetGLMappingMgt(glMappingMgtId);

                if (glMapping != null)
                {
                    _FinstatService.DeleteGLMappingMgt(glMappingMgtId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No glMappingMgt found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getglMapping/{glMappingMgtId}")]
        public HttpResponseMessage GetGLMappingMgt(HttpRequestMessage request, int glMappingMgtId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMappingMgt glMapping = _FinstatService.GetGLMappingMgt(glMappingMgtId);

                // notice no need to create a seperate model object since GLMappingMgt entity will do just fine
                response = request.CreateResponse<GLMappingMgt>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }

        [HttpGet]
        [Route("availableglMappings")]
        public HttpResponseMessage GetAvailableGLMappingMgts(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                GLMappingMgtData[] glMappings = _FinstatService.GetGLMappingMgts();

                return request.CreateResponse<GLMappingMgtData[]>(HttpStatusCode.OK, glMappings);
            });
        }
        ///{jobCode}/{startDate}
        [HttpGet]
        [Route("getsubcaptions/{level}/{captionCode}")]
        public HttpResponseMessage GetSubCaptionss(HttpRequestMessage request, int level, string captionCode)
        {
            return GetHttpResponse(request, () =>
            {
                GLMappingMgt[] glMappings = _FinstatService.GetAllGLMappingMgts();

                List<CaptionModel> captions = new List<CaptionModel>();

                List<string> distinctCaptions = null;

                if (level == 0)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption).Distinct().ToList();
                }
                else if (level == 1)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption1).Distinct().ToList();
                }
                else if (level == 2)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption2).Distinct().ToList();
                }
                else if (level == 3)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption3).Distinct().ToList();
                }
                else if (level == 4)
                {
                    distinctCaptions = glMappings.Select(c => c.SubCaption4).Distinct().ToList();
                }

                else if (level == 5)
                {
                    distinctCaptions = glMappings.Where(c => c.CaptionCode == captionCode).Select(c => c.SubCaption).Distinct().ToList();
                }

                foreach (var c in distinctCaptions)
                    captions.Add(new CaptionModel()
                    {
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
                GLMappingMgtData[] gls = _FinstatService.GetUnMappedMgtGLs();

                return request.CreateResponse<GLMappingMgtData[]>(HttpStatusCode.OK, gls);
            });
        }

        [HttpGet]
        [Route("getunmappedmgtgl/{glCode}")]
        public HttpResponseMessage GetGLMappingMgt(HttpRequestMessage request, string glCode)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMappingMgtData glMapping = _FinstatService.GetUnMappedMgtGLbyGLCode(glCode).FirstOrDefault();

                // notice no need to create a seperate model object since GLMappingMgt entity will do just fine
                response = request.CreateResponse<GLMappingMgtData>(HttpStatusCode.OK, glMapping);

                return response;
            });
        }


        [HttpGet]
        [Route("getsubsubcaptions/{caption}")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request, string caption)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                GLMappingMgt[] glMapping = _FinstatService.GetMgtSubSubCaption(caption);
                List<SubSubCaptionModel> captions = new List<SubSubCaptionModel>();
                List<string> distinctsubsubCaptions = null;
                distinctsubsubCaptions = glMapping.Select(c => c.SubCaption1).Distinct().ToList();

                foreach (var c in distinctsubsubCaptions)
                    captions.Add(new SubSubCaptionModel()
                    {
                        SubCaption1 = c
                    });

                // notice no need to create a seperate model object since GLMappingMgt entity will do just fine
                response = request.CreateResponse<SubSubCaptionModel[]>(HttpStatusCode.OK, captions.ToArray());

                return response;
            });
        }
    }
}
