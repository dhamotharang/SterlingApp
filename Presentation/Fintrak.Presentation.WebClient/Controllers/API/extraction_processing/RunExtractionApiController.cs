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
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Client.Core.Contracts;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/runextraction")]
    [UsesDisposableService]
    public class RunExtractionApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RunExtractionApiController(ICoreService coreService, IExtractionProcessService extractionProcessService)
        {
            _CoreService = coreService;
            _ExtractionProcessService = extractionProcessService;

        }

        ICoreService _CoreService;
        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
            disposableServices.Add(_ExtractionProcessService);
        }

        //[HttpGet]
        //[Route("getrunextractions")]
        //public HttpResponseMessage GetRunExtractions(HttpRequestMessage request)
        //{
        //    return GetHttpResponse(request, () =>
        //    {
        //        var runEntities = new List<RunExtractionSingleModel>();
              
        //        ExtractionData[] extractions = _ExtractionProcessService.GetExtractionByLogin(User.Identity.Name);

        //        var solutions = extractions.Select(c => c.SolutionName).Distinct();
        //        //var solutions = from a in extractions.d
        //        //                select new KeyValueModel()
        //        //                {
        //        //                    Key = a.SolutionId,
        //        //                    Value = a.SolutionName
        //        //                };

           

        //        foreach (var solution in solutions)
        //        {
                   
        //            foreach (var extraction in extractions)
        //            {
        //                if (extraction.SolutionName == solution)
        //                {
        //                    var runModel = new RunExtractionSingleModel();
        //                    runModel.SolutionName = solution;
        //                    runModel.ExtrationTitle = extraction.Title;
        //                    runModel.ExtractionId = extraction.ExtractionId;
        //                    runModel.CanRun = false;

        //                    runEntities.Add(runModel);

        //                }

                       
        //            }
            
        //        }
                
        //        return request.CreateResponse<RunExtractionSingleModel[]>(HttpStatusCode.OK, runEntities.ToArray());
        //    });
        //}


        [HttpGet]
        [Route("getrunextractions/{solutionId}")]
        public HttpResponseMessage GetRunExtractions(HttpRequestMessage request, int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                var runEntities = new List<RunExtractionSingleModel>();

                ExtractionData[] extractions = _ExtractionProcessService.GetExtractionBySolution(solutionId, User.Identity.Name);

                var solutions = extractions.Select(c => c.SolutionName).Distinct();
                //var solutions = from a in extractions.d
                //                select new KeyValueModel()
                //                {
                //                    Key = a.SolutionId,
                //                    Value = a.SolutionName
                //                };



                foreach (var solution in solutions)
                {

                    foreach (var extraction in extractions)
                    {
                        if (extraction.SolutionName == solution)
                        {
                            var runModel = new RunExtractionSingleModel();
                            runModel.SolutionId = solutionId;
                            runModel.SolutionName = solution;
                            runModel.ExtrationTitle = extraction.Title;
                            runModel.ExtractionId = extraction.ExtractionId;
                            runModel.CanRun = false;

                            runEntities.Add(runModel);

                        }


                    }

                }

                return request.CreateResponse<RunExtractionSingleModel[]>(HttpStatusCode.OK, runEntities.ToArray());
            });
        }

        [HttpGet]
        [Route("cancelextractionjob/{jobCode}/{startDate}/{endDate}")]
        public HttpResponseMessage CancelExtractions(HttpRequestMessage request, string jobCode, DateTime startDate, DateTime endDate)
        {
            return GetHttpResponse(request, () =>
            {
              //Cancel Job

                ExtractionJob[] jobs = _ExtractionProcessService.CancelExtractionJobByCode(jobCode, startDate, endDate);

                return request.CreateResponse<ExtractionJob[]>(HttpStatusCode.OK, jobs);
            });
        }

        [HttpPost]
        [Route("checkextraction/{startDate}/{endDate}")]
        public HttpResponseMessage CheckExtractions(HttpRequestMessage request, DateTime startDate, DateTime endDate, [FromBody] int[] extractionIds)
        {
            return GetHttpResponse(request, () =>
            {
               var message  = string.Empty ;

               ExtractionTriggerData[] extractionTriggers  = _ExtractionProcessService.GetExtractionTriggerByRunDate(startDate, endDate).Where(c => extractionIds.Contains(c.ExtractionId)  && (c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running)).ToArray();

               if (extractionTriggers.Count() > 0)
               {
                   foreach (var trigger in extractionTriggers)
                   {
                       //if (trigger.Status == PackageStatus.Done)
                       //    message += "The extraction template for " + trigger.ExtractionTitle + " has be completed.<br>";
                       //else 

                       if (trigger.Status == PackageStatus.New)
                           message += "The extraction template for " + trigger.ExtractionTitle + " has just been added for extraction.<br>";
                       else if (trigger.Status == PackageStatus.Pending)
                           message += "The extraction template for " + trigger.ExtractionTitle + " is still pending for extraction.<br>";
                       else if (trigger.Status == PackageStatus.Running)
                           message += "The extraction template for " + trigger.ExtractionTitle + " is currently running.<br>";
                   }
               }
               else
                   message = "Ok";
              
                return request.CreateResponse<string>(HttpStatusCode.OK, message);
            });
        }

         [HttpPost]
         [Route("startextraction/{jobId}/{startDate}/{endDate}/{runTime}")]
        public HttpResponseMessage RunExtractions(HttpRequestMessage request, int jobId, DateTime startDate, DateTime endDate, DateTime runTime, [FromBody] int[] extractionIds)
        {
            return GetHttpResponse(request, () =>
            {

                ExtractionJob[] jobs = _ExtractionProcessService.RunExtractionJob(jobId, extractionIds, startDate, endDate, runTime);

                return request.CreateResponse<ExtractionJob[]>(HttpStatusCode.OK, jobs);
            });
        }

         [HttpGet]
         [Route("getextractiontriggers/{jobCode}")]
         public HttpResponseMessage GetExtractionTriggers(HttpRequestMessage request,string jobCode)
         {
             return GetHttpResponse(request, () =>
             {
                 //Cancel Extraction

                 ExtractionTriggerData[] extractionTriggers = _ExtractionProcessService.GetExtractionTriggerByJob(jobCode);

                 return request.CreateResponse<ExtractionTriggerData[]>(HttpStatusCode.OK, extractionTriggers);
             });
         }

         [HttpGet]
         [Route("getcurrentjobs")]
         public HttpResponseMessage GetCurrentExtractionTriggers(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {
                 
                 ExtractionJob[] jobs = _ExtractionProcessService.GetCurrentExtractionJobs();

                 return request.CreateResponse<ExtractionJob[]>(HttpStatusCode.OK, jobs);
             });
         }

         [HttpGet]
         [Route("getjobs/{startDate}/{endDate}")]
         public HttpResponseMessage GetCurrentExtractionTriggers(HttpRequestMessage request,DateTime startDate, DateTime endDate)
         {
             return GetHttpResponse(request, () =>
             {

                 ExtractionJob[] jobs = _ExtractionProcessService.GetExtractionJobByDate(startDate, endDate);

                 return request.CreateResponse<ExtractionJob[]>(HttpStatusCode.OK, jobs);
             });
         }

         [HttpPost]
         [Route("updateextractionjob")]
         public HttpResponseMessage UpdateExtractionJob(HttpRequestMessage request, [FromBody]ExtractionJob extractionJobModel)
         {
             return GetHttpResponse(request, () =>
             {
                 extractionJobModel.UserName = User.Identity.Name;
                 var job = _ExtractionProcessService.UpdateExtractionJob(extractionJobModel);

                 return request.CreateResponse<ExtractionJob>(HttpStatusCode.OK, job);
             });
         }

         [HttpGet]
         [Route("clearextractionhistory/{solutionId}")]
         public HttpResponseMessage ClearExtractionHistory(HttpRequestMessage request, int solutionId)
         {
             return GetHttpResponse(request, () =>
             {
                 //Cancel Job
                 var message = string.Empty;

                  _ExtractionProcessService.ClearExtractionHistory(solutionId);

                  message = "Ok";

                  return request.CreateResponse<string>(HttpStatusCode.OK, message);
             });
         }

         
    }
}
