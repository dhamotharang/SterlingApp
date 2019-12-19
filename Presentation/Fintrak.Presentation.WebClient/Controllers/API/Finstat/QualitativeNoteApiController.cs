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
    [RoutePrefix("api/qualitativenote")]
    [UsesDisposableService]
    public class QualitativeNoteApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public QualitativeNoteApiController(IIFRSCoreService ifrsCoreService)
        {
            _IFRSCoreService = ifrsCoreService;
        }

        IIFRSCoreService _IFRSCoreService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_IFRSCoreService);
        }

        //[HttpPost]
        //[Route("updatequalitativenote")]
        //public HttpResponseMessage UpdateQualitativeNote(HttpRequestMessage request, [FromBody]QualitativeNote qualitativeNoteModel)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var qualitativeNote = _IFRSCoreService.UpdateQualitativeNote(qualitativeNoteModel);

        //        return request.CreateResponse<QualitativeNote>(HttpStatusCode.OK, qualitativeNote);
        //    });
        //}

        [HttpPost]
        [Route("deletequalitativenote")]
        public HttpResponseMessage DeleteQualitativeNote(HttpRequestMessage request, [FromBody]int qualitativeNoteId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                QualitativeNote qualitativeNote = _IFRSCoreService.GetQualitativeNote(qualitativeNoteId);

                if (qualitativeNote != null)
                {
                    _IFRSCoreService.DeleteQualitativeNote(qualitativeNoteId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No qualitativeNote found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getqualitativenote/{qualitativeNoteId}")]
        public HttpResponseMessage GetQualitativeNote(HttpRequestMessage request,int qualitativeNoteId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                QualitativeNote qualitativeNote = _IFRSCoreService.GetQualitativeNote(qualitativeNoteId);

                // notice no need to create a seperate model object since QualitativeNote entity will do just fine
                response = request.CreateResponse<QualitativeNote>(HttpStatusCode.OK, qualitativeNote);

                return response;
            });
        }

        [HttpGet]
        [Route("availablequalitativenotes")]
        public HttpResponseMessage GetAvailableQualitativeNotes(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                QualitativeNote[] qualitativeNotes = _IFRSCoreService.GetAllQualitativeNotes();

                return request.CreateResponse<QualitativeNote[]>(HttpStatusCode.OK, qualitativeNotes);
            });
        }

        [HttpPost]
        [Route("updatequalitativenote")]
        public HttpResponseMessage UpdateQualitativeNote(HttpRequestMessage request, [FromBody] QualitativeNoteModel param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _IFRSCoreService.UpdateQualitativeNote(param.RefNote, param.TopNote,param.BottomNote,param.RunDate,param.ReportType);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
    }
}
