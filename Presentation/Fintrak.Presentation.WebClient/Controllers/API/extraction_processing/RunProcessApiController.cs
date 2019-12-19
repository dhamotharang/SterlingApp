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
using Fintrak.Shared.Common.Utils;

namespace Fintrak.Presentation.WebClient.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/runprocess")]
    [UsesDisposableService]
    public class RunProcessApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public RunProcessApiController(ICoreService coreService, IExtractionProcessService processProcessService)
        {
            _CoreService = coreService;
            _ExtractionProcessService = processProcessService;

        }

        ICoreService _CoreService;
        IExtractionProcessService _ExtractionProcessService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_CoreService);
            disposableServices.Add(_ExtractionProcessService);
        }

        [HttpGet]
        [Route("getrunprocesses/{solutionId}")]
        public HttpResponseMessage GetRunProcesses(HttpRequestMessage request, int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                var runEntities = new List<RunProcessSingleModel>();

                ProcessData[] processes = _ExtractionProcessService.GetProcessBySolution(solutionId,User.Identity.Name);

                var modules = processes.Select(c => c.ModuleName).Distinct();
                //var solutions = from a in processes.d
                //                select new KeyValueModel()
                //                {
                //                    Key = a.SolutionId,
                //                    Value = a.SolutionName
                //                };



                foreach (var module in modules)
                {

                    foreach (var process in processes)
                    {
                        if (process.ModuleName == module)
                        {
                            var runModel = new RunProcessSingleModel();
                            runModel.SolutionName = module;
                            runModel.ProcessTitle = process.Title;
                            runModel.ProcessId = process.ProcessId;
                            runModel.CanRun = false;

                            runEntities.Add(runModel);

                        }


                    }

                }
                
                return request.CreateResponse<RunProcessSingleModel[]>(HttpStatusCode.OK, runEntities.ToArray());
            });
        }

        [HttpGet]
        [Route("cancelprocessjob/{jobCode}")]
       // [Route("cancelprocessjob/{jobCode}/{startDate}/{endDate}")]
      //  public HttpResponseMessage CancelProcesss(HttpRequestMessage request, string jobCode, DateTime startDate, DateTime endDate)
        public HttpResponseMessage CancelProcesss(HttpRequestMessage request, string jobCode)
        {
            return GetHttpResponse(request, () =>
            {
              //Cancel Job

                ProcessJob[] jobs = _ExtractionProcessService.CancelProcessJobByCode(jobCode);

                return request.CreateResponse<ProcessJob[]>(HttpStatusCode.OK, jobs);
            });
        }

        [HttpPost]
        [Route("checkprocess")]
        public HttpResponseMessage CheckProcess(HttpRequestMessage request, [FromBody] int[] processIds)
        {
            return GetHttpResponse(request, () =>
            {
               var message  = string.Empty ;

               ProcessTriggerData[] processTriggers  = _ExtractionProcessService.GetProcessTriggerByRunDate().Where(c => processIds.Contains(c.ProcessId)  && (c.Status == PackageStatus.New || c.Status == PackageStatus.Pending || c.Status == PackageStatus.Running)).ToArray();

               if (processTriggers.Count() > 0)
               {
                   foreach (var trigger in processTriggers)
                   {
                       //if (trigger.Status == PackageStatus.Done)
                       //    message += "The process template for " + trigger.ProcessTitle + " has be completed.<br>";
                       //else 
                       if (trigger.Status == PackageStatus.New)
                           message += "The process template for " + trigger.ProcessTitle + " has been added for processing.<br>";
                       else if (trigger.Status == PackageStatus.Pending)
                           message += "The process template for " + trigger.ProcessTitle + " is still pending for processing.<br>";
                       else if (trigger.Status == PackageStatus.Running)
                           message += "The process template for " + trigger.ProcessTitle + " is currently running.<br>";
                   }
               }
               else
                   message = "Ok";
              
                return request.CreateResponse<string>(HttpStatusCode.OK, message);
            });
        }

         [HttpPost]
         [Route("startprocess/{jobId}/{runTime}")]
        public HttpResponseMessage RunProcesss(HttpRequestMessage request, int jobId,DateTime runTime, [FromBody] int[] processIds)
        {
            return GetHttpResponse(request, () =>
            {

                ProcessJob[] jobs = _ExtractionProcessService.RunProcessJob(jobId, processIds, runTime);

                return request.CreateResponse<ProcessJob[]>(HttpStatusCode.OK, jobs);
            });
        }

         [HttpGet]
         [Route("getprocesstriggers/{jobCode}")]
         public HttpResponseMessage GetProcessTriggers(HttpRequestMessage request,string jobCode)
         {
             return GetHttpResponse(request, () =>
             {
                 //Cancel Process

                 ProcessTriggerData[] processTriggers = _ExtractionProcessService.GetProcessTriggerByJob(jobCode);

                 return request.CreateResponse<ProcessTriggerData[]>(HttpStatusCode.OK, processTriggers);
             });
         }

         [HttpGet]
         [Route("getcurrentjobs")]
         public HttpResponseMessage GetCurrentProcessTriggers(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {
                 
                 ProcessJob[] jobs = _ExtractionProcessService.GetCurrentProcessJobs();

                 return request.CreateResponse<ProcessJob[]>(HttpStatusCode.OK, jobs);
             });
         }

         [HttpPost]
         [Route("updateprocessjob")]
         public HttpResponseMessage UpdateProcessJob(HttpRequestMessage request, [FromBody]ProcessJob processJobModel)
         {
             return GetHttpResponse(request, () =>
             {
                 processJobModel.Code = UniqueKeyGenerator.RNGCharacterMask(6, 8);
                 processJobModel.UserName = User.Identity.Name;
                 processJobModel.StartDate = DateTime.Now;
                 processJobModel.EndDate = DateTime.Now;
                 processJobModel.Remark = "Not started";
                 processJobModel.Status = PackageStatus.New;

                 var job = _ExtractionProcessService.UpdateProcessJob(processJobModel);

                 return request.CreateResponse<ProcessJob>(HttpStatusCode.OK, job);
             });
         }

         [HttpGet]
         [Route("getjobs")]
         public HttpResponseMessage GetCurrentExtractionTriggers(HttpRequestMessage request)
         {
             return GetHttpResponse(request, () =>
             {

                 ProcessJob[] jobs = _ExtractionProcessService.GetProcessJobByRunDate();

                 return request.CreateResponse<ProcessJob[]>(HttpStatusCode.OK, jobs);
             });
         }
     
        [HttpPost]
         [Route("restartservice/{serviceName}")]
         public HttpResponseMessage Startservice(HttpRequestMessage request, string serviceName)
         {
             return GetHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;

                 _ExtractionProcessService.RestartService(serviceName);

                 response = request.CreateResponse(HttpStatusCode.OK);

                 return response;
             });
         }
        [HttpGet]
        [Route("getservicestatus/{serviceName}")]
        public HttpResponseMessage GetServiceStatus(HttpRequestMessage request, string serviceName)
        {
            var message = string.Empty;
            return GetHttpResponse(request, () =>
            {
              //  HttpResponseMessage response = null;

             message =   _ExtractionProcessService.GetServiceStatus(serviceName);

               // response = request.CreateResponse(HttpStatusCode.OK);
                return request.CreateResponse<string>(HttpStatusCode.OK, message);
               // return response;
            });
        }
        [HttpGet]
        [Route("clearprocesshistory/{solutionId}")]
        public HttpResponseMessage ClearProcessHistory(HttpRequestMessage request, int solutionId)
        {
            return GetHttpResponse(request, () =>
            {
                //Cancel Job
                var message = string.Empty;

                _ExtractionProcessService.ClearProcessHistory(solutionId);

                message = "Ok";

                return request.CreateResponse<string>(HttpStatusCode.OK, message);
            });
        }
    }
}
