using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Entities;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;
using System.Web;


namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/runupload")]
    [UsesDisposableService]
    public class RunUploadApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RunUploadApiController(IExtractionProcessService coreService)
        {
            _ExtractionProcessService = coreService;
            HttpContext.Current.Server.ScriptTimeout = 3600;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("uploaddata")]
        public HttpResponseMessage UploadData(HttpRequestMessage request, [FromBody]HttpPostedFileBase uploadModel)
        {
            return GetHttpResponse(request, () =>
            {

                //var formData = new FormData();
                var item = uploadModel;
                return request.CreateResponse(HttpStatusCode.OK);
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
        [Route("getuploadbysolution/{solutionId}")]
        public HttpResponseMessage GetUploadBySolution(HttpRequestMessage request, int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                Upload[] upload = _ExtractionProcessService.GetUploadBySolution(solutionId);

                // notice no need to create a seperate model object since Upload entity will do just fine
                response = request.CreateResponse<Upload[]>(HttpStatusCode.OK, upload);

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

        [HttpPost]
        [Route("uploadcsv")]
        public HttpResponseMessage UploadCSV(HttpRequestMessage request, [FromBody]CSVModel csvModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpContext.Current.Server.ScriptTimeout = 3600;
                var results = _ExtractionProcessService.UploadCSV(int.Parse(csvModel.UploadId), csvModel.Content);//csvModel.Truncate,csvModel.PostUploadAction

                return request.CreateResponse<UploadResult[]>(HttpStatusCode.OK, results);
            });
        }

        [HttpPost]
        [Route("uploadcsvbycode/{uploadId}")]
        public HttpResponseMessage UploadCSVByCode(HttpRequestMessage request, [FromBody]CSVModel csvModel)
        {
            return GetHttpResponse(request, () =>
            {
                var results = _ExtractionProcessService.UploadCSVByCode(csvModel.UploadId, csvModel.Content);

                return request.CreateResponse<UploadResult[]>(HttpStatusCode.OK, results);
            });        
        }
        [HttpGet]
        [Route("verify/{sppVerify}")]
        public HttpResponseMessage Verification(HttpRequestMessage request,string sppVerify)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var verificationmsg= _ExtractionProcessService.VerificationMsg(sppVerify);

                // notice no need to create a seperate model object since Upload entity will do just fine
                response = request.CreateResponse<UploadResult[]>(HttpStatusCode.OK, verificationmsg);

                return response;
            });
        }
    }
}
