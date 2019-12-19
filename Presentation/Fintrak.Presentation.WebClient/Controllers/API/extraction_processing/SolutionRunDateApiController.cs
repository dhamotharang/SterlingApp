using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fintrak.Client.Core.Contracts;
using Fintrak.Presentation.WebClient.Core;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Entities;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/solutionrundate")]
    [UsesDisposableService]
    public class SolutionRunDateApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public SolutionRunDateApiController(IExtractionProcessService extractionProcessService)
        {
            _ExtractionProcessService = extractionProcessService;
        }

        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpPost]
        [Route("updatesolutionRunDate")]
        public HttpResponseMessage UpdateSolutionRunDate(HttpRequestMessage request, [FromBody]SolutionRunDate solutionRunDateModel)
           
        {
            return GetHttpResponse(request, () =>
            {
               
                solutionRunDateModel.Active = true;
                var solutionRunDate = _ExtractionProcessService.UpdateSolutionRunDate(solutionRunDateModel);

                return request.CreateResponse<SolutionRunDate>(HttpStatusCode.OK, solutionRunDate);
            });
        }

        [HttpPost]
        [Route("deletesolutionRunDate")]
        public HttpResponseMessage DeleteSolutionRunDate(HttpRequestMessage request, [FromBody]int solutionRunDateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                // not that calling the WCF service here will authenticate access to the data
                SolutionRunDate solutionRunDate = _ExtractionProcessService.GetSolutionRunDate(solutionRunDateId);

                if (solutionRunDate != null)
                {
                    _ExtractionProcessService.DeleteSolutionRunDate(solutionRunDateId);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No solutionRunDate found under that ID.");

                return response;
            });
        }

        [HttpGet]
        [Route("getsolutionRunDate/{solutionRunDateId}")]
        public HttpResponseMessage GetSolutionRunDate(HttpRequestMessage request, int solutionRunDateId)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                SolutionRunDate solutionRunDate = _ExtractionProcessService.GetSolutionRunDate(solutionRunDateId);

                // notice no need to create a seperate model object since SolutionRunDate entity will do just fine
                response = request.CreateResponse<SolutionRunDate>(HttpStatusCode.OK, solutionRunDate);

                return response;
            });
        }

        [HttpGet]
        [Route("getsolutionrundatebysolution/{solutionName}")]
        public HttpResponseMessage GetSolutionRunDateBySolution(HttpRequestMessage request,string solutionName)
        {
            return GetHttpResponse(request, () =>
            {
                SolutionRunDateData solutionRunDate = _ExtractionProcessService.GetSolutionRunDates().Where(c => c.SolutionName == solutionName).FirstOrDefault();

                return request.CreateResponse<SolutionRunDateData>(HttpStatusCode.OK, solutionRunDate);
            });
        }


        //[HttpGet]
        //[Route("getsolutionrundatebysolution/{loginId}")]
        //public HttpResponseMessage GetSolutionRunDateBylogin(HttpRequestMessage request, string loginId)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        SolutionRunDateData solutionRunDate = _ExtractionProcessService.GetSolutionRunDates().Where(c => c.SolutionId == loginId).FirstOrDefault();

        //        return request.CreateResponse<SolutionRunDateData>(HttpStatusCode.OK, solutionRunDate);
        //    });
        //}

        [HttpGet]
        [Route("getyears")]
        public HttpResponseMessage GetYear(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                var solutionRunDates = _ExtractionProcessService.GetSolutionRunDates().Select(c => c.RunDate.Year).Distinct<int>(); 

                var years = new List<KeyValueModel>();

                foreach (var runDate in solutionRunDates)
                {
                    years.Add(new KeyValueModel() { Key = runDate, Value = runDate.ToString() });
                }


                return request.CreateResponse<KeyValueModel[]>(HttpStatusCode.OK, years.ToArray());
            });
        }

        [HttpGet]
        [Route("availablesolutionRunDates")]
        public HttpResponseMessage GetAvailableSolutionRunDates(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SolutionRunDateData[] solutionRunDates = _ExtractionProcessService.GetSolutionRunDates();

                return request.CreateResponse<SolutionRunDateData[]>(HttpStatusCode.OK, solutionRunDates);
            });
        }

        [HttpGet]
        [Route("getsolutionrundatebylogin")]
        public HttpResponseMessage GetSolutionRunDateByLogin(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SolutionRunDateData[] solutionRunDates = _ExtractionProcessService.GetSolutionRunDateByLogin(User.Identity.Name);

                return request.CreateResponse<SolutionRunDateData[]>(HttpStatusCode.OK, solutionRunDates);
            });
        }


        [HttpGet]
        [Route("getsolutionrundatebyloginbydefault")]
        public HttpResponseMessage GetSolutionRunDateByLoginByDefault(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                string rundate = _ExtractionProcessService.GetSolutionRunDateByLoginByDefault(User.Identity.Name);

                return request.CreateResponse<string>(HttpStatusCode.OK, rundate);
            });
        }


        [HttpGet]
        [Route("getArchiveDate")]
        public HttpResponseMessage GetReferenceNos(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                SolutionRunDate[] solutionRunDate = _ExtractionProcessService.GetRunDate().ToArray();

                List<ArchiveDateModel> solutionRunDates = new List<ArchiveDateModel>();

                List<DateTime> solutiorundates = null;

                solutiorundates = solutionRunDate.Select(c => c.RunDate).Distinct().ToList();

                foreach (var c in solutiorundates)
                    solutionRunDates.Add(new ArchiveDateModel()
                    {
                        RunDate = c
                    });
                return request.CreateResponse<ArchiveDateModel[]>(HttpStatusCode.OK, solutionRunDates.ToArray());
            });
        }


        [HttpPost]
        [Route("restorearchive")]
        public HttpResponseMessage RestoreArchive(HttpRequestMessage request, [FromBody] RestoreArchive param)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                _ExtractionProcessService.RestoreArchive(param.Solutionid, param.Date);

                response = request.CreateResponse(HttpStatusCode.OK);

                return response;
            });
        }
        
    }
}
