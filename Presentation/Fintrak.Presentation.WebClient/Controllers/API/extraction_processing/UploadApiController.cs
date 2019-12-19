using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/upload")]
    [UsesDisposableService]
    public class UploadApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public UploadApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updateupload")]
        public HttpResponseMessage UpdateUpload(HttpRequestMessage request, [FromBody]Upload uploadModel)
        {
            return GetHttpResponse(request, () =>
            {
                var upload = _ExtractionProcessService.UpdateUpload(uploadModel);

                return request.CreateResponse<Upload>(HttpStatusCode.OK, upload);
            });
        }

        [HttpPost]
        [Route("deleteupload")]
        public HttpResponseMessage DeleteUpload(HttpRequestMessage request, [FromBody]int uploadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                Upload upload = _ExtractionProcessService.GetUpload(uploadId);

                if (upload != null)
                {
                    _ExtractionProcessService.DeleteUpload(uploadId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No upload found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getupload/{uploadId}")]
        public HttpResponseMessage GetUpload(HttpRequestMessage request,int uploadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Upload upload = _ExtractionProcessService.GetUpload(uploadId);

                // notice no need to create a seperate model object since Upload entity will do just fine
                response = request.CreateResponse<Upload>(HttpStatusCode.OK, upload);

                return response;
            });
        }

        [HttpGet]
        [Route("getuploadwithchildren/{uploadId}")]
        public HttpResponseMessage GetUploadWithChildren(HttpRequestMessage request, int uploadId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var uploadModel = new UploadModel();

                uploadModel.Upload = _ExtractionProcessService.GetUpload(uploadId);
                uploadModel.UploadRoles = _ExtractionProcessService.GetUploadRoleByUpload(uploadId);

                // notice no need to create a seperate model object since Upload entity will do just fine
                response = request.CreateResponse<UploadModel>(HttpStatusCode.OK, uploadModel);

                return response;
            });
        }

        [HttpGet]
        [Route("getuploads")]
        public HttpResponseMessage GetAvailableUploads(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                UploadData[] uploads = _ExtractionProcessService.GetUploads();

                return request.CreateResponse<UploadData[]>(HttpStatusCode.OK, uploads);
            });
        }

        

    }
}
