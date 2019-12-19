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
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Client.IFRS.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/interimqualitativenote")]
    [UsesDisposableService]
    public class InterimQualitativeNoteApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public InterimQualitativeNoteApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        //[HttpPost]
        //[Route("updateinterimqualitativenote")]
        //public HttpResponseMessage UpdateInterimQualitativeNote(HttpRequestMessage request, [FromBody]InterimQualitativeNote qualitativeNoteModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var qualitativeNote = _IFRSCoreService.UpdateInterimQualitativeNote(qualitativeNoteModel);

        //        return request.CreateResponse<InterimQualitativeNote>(HttpStatusCode.OK, qualitativeNote);
        //    });
        //}

        [HttpPost]
        [Route("deleteinterimqualitativenote")]
        public HttpResponseMessage DeleteInterimQualitativeNote(HttpRequestMessage request, [FromBody]int qualitativeNoteId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                InterimQualitativeNote qualitativeNote = _IFRSCoreService.GetInterimQualitativeNote(qualitativeNoteId);

                if (qualitativeNote != null)
                {
                    _IFRSCoreService.DeleteInterimQualitativeNote(qualitativeNoteId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No qualitativeNote found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getinterimqualitativenote/{qualitativeNoteId}")]
        public HttpResponseMessage GetInterimQualitativeNote(HttpRequestMessage request,int qualitativeNoteId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InterimQualitativeNote qualitativeNote = _IFRSCoreService.GetInterimQualitativeNote(qualitativeNoteId);

                // notice no need to create a seperate model object since InterimQualitativeNote entity will do just fine
                response = request.CreateResponse<InterimQualitativeNote>(HttpStatusCode.OK, qualitativeNote);

                return response;
            });
        }

        [HttpGet]
        [Route("getinterimqualitativenotebyType2/{rType}")]
        public HttpResponseMessage GetInterimQualitativeNoteByType(HttpRequestMessage request, int rType)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                InterimQualitativeNote[] qualitativeNote = _IFRSCoreService.GetInterimQualitativeNoteByType(rType);

                // notice no need to create a seperate model object since InterimQualitativeNote entity will do just fine
                response = request.CreateResponse<InterimQualitativeNote[]>(HttpStatusCode.OK, qualitativeNote);

                return response;
            });
        }

        [HttpGet]
        [Route("availableinterimqualitativenotes")]
        public HttpResponseMessage GetAvailableInterimQualitativeNotes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                InterimQualitativeNote[] qualitativeNotes = _IFRSCoreService.GetAllInterimQualitativeNotes();

                return request.CreateResponse<InterimQualitativeNote[]>(HttpStatusCode.OK, qualitativeNotes);
            });
        }

        [HttpPost]
        [Route("updateinterimqualitativenote")]
        public HttpResponseMessage UpdateInterimQualitativeNote(HttpRequestMessage request, [FromBody] QualitativeNoteModel param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSCoreService.UpdateInterimQualitativeNote(param.report, param.TopNote,param.BottomNote,param.RunDate,param.ReportType);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }

        [HttpGet]
        [Route("getinterimqualitativenotebyType/{rType}")]
        public HttpResponseMessage GetReportNamesbyType(HttpRequestMessage request, int rType)
        {
            return GetHttpResponse(request, () =>
            {
                string[] reportNames = _IFRSCoreService.GetReportNamesbyType(rType);
                List<KeyValueModel> val = new List<KeyValueModel>();
                foreach (var c in reportNames)
                    val.Add(new KeyValueModel()
                    {
                        Value = c

                    });

                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, val.ToArray());

            });
        }
    }
}