using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using Fintrak.Shared.Core.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Core.Proxies
{
    [Export(typeof(IExtractionProcessService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtractionProcessClient : UserClientBase<IExtractionProcessService>, IExtractionProcessService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region PackageSetup

        public PackageSetup UpdatePackageSetup(PackageSetup packageSetup)
        {
            return Channel.UpdatePackageSetup(packageSetup);
        }

        public PackageSetup GetFirstPackageSetup()
        {
            return Channel.GetFirstPackageSetup();
        }

        #endregion

        #region Extraction

        public Extraction UpdateExtraction(Extraction extraction)
        {
            return Channel.UpdateExtraction(extraction);
        }

        public void DeleteExtraction(int extractionId)
        {
            Channel.DeleteExtraction(extractionId);
        }

        public Extraction GetExtraction(int extractionId)
        {
            return Channel.GetExtraction(extractionId);
        }

        public Extraction[] GetAllExtractions()
        {
            return Channel.GetAllExtractions();
        }

        public ExtractionData[] GetExtractions()
        {
            return Channel.GetExtractions();
        }

        public ExtractionData[] GetExtractionByLogin(string loginID)
        {
            return Channel.GetExtractionByLogin(loginID);
        }

        public ExtractionData[] GetExtractionBySolution(int solutionId, string loginID)
        {
            return Channel.GetExtractionBySolution(solutionId, loginID);
        }

        #endregion

        #region ExtractionRole

        public ExtractionRole UpdateExtractionRole(ExtractionRole extractionRole)
        {
            return Channel.UpdateExtractionRole(extractionRole);
        }

        public void DeleteExtractionRole(int extractionRoleId)
        {
            Channel.DeleteExtractionRole(extractionRoleId);
        }

        public ExtractionRole GetExtractionRole(int extractionRoleId)
        {
            return Channel.GetExtractionRole(extractionRoleId);
        }

        public ExtractionRole[] GetAllExtractionRoles()
        {
            return Channel.GetAllExtractionRoles();
        }

        public ExtractionRoleData[] GetExtractionRoles()
        {
            return Channel.GetExtractionRoles();
        }

        public ExtractionRoleData[] GetExtractionRoleByExtraction(int extractionId)
        {
            return Channel.GetExtractionRoleByExtraction(extractionId);
        }

        #endregion

        #region Process

        public Process UpdateProcess(Process process)
        {
            return Channel.UpdateProcess(process);
        }

        public void DeleteProcess(int processId)
        {
            Channel.DeleteProcess(processId);
        }

        public Process GetProcess(int processId)
        {
            return Channel.GetProcess(processId);
        }

        public Process[] GetAllProcesses()
        {
            return Channel.GetAllProcesses();
        }

        public ProcessData[] GetProcesses()
        {
            return Channel.GetProcesses();
        }

        public ProcessData[] GetProcessBySolution(int solutionId, string loginID)
        {
            return Channel.GetProcessBySolution(solutionId,loginID);
        }


        #endregion

        #region ProcessRole

        public ProcessRole UpdateProcessRole(ProcessRole processRole)
        {
            return Channel.UpdateProcessRole(processRole);
        }

        public void DeleteProcessRole(int processRoleId)
        {
            Channel.DeleteProcessRole(processRoleId);
        }

        public ProcessRole GetProcessRole(int processRoleId)
        {
            return Channel.GetProcessRole(processRoleId);
        }

        public ProcessRole[] GetAllProcessRoles()
        {
            return Channel.GetAllProcessRoles();
        }

        public ProcessRoleData[] GetProcessRoles()
        {
            return Channel.GetProcessRoles();
        }

        public ProcessRoleData[] GetProcessRoleByProcess(int processId)
        {
            return Channel.GetProcessRoleByProcess(processId);
        }

        #endregion

        #region ExtractionTrigger

        public ExtractionTrigger UpdateExtractionTrigger(ExtractionTrigger extractionTrigger)
        {
            return Channel.UpdateExtractionTrigger(extractionTrigger);
        }

        public void DeleteExtractionTrigger(int extractionTriggerId)
        {
            Channel.DeleteExtractionTrigger(extractionTriggerId);
        }

        public ExtractionTrigger GetExtractionTrigger(int extractionTriggerId)
        {
            return Channel.GetExtractionTrigger(extractionTriggerId);
        }

        public ExtractionTrigger[] GetAllExtractionTrigger()
        {
            return Channel.GetAllExtractionTrigger();
        }

        public ExtractionTriggerData[] GetExtractionTriggers()
        {
            return Channel.GetExtractionTriggers();
        }


        public ExtractionTriggerData[] GetExtractionTriggerByExtraction(int extractionId)
        {
            return Channel.GetExtractionTriggerByExtraction(extractionId);
        }

        public ExtractionTriggerData[] GetExtractionTriggerByJob(string jobCode)
        {
            return Channel.GetExtractionTriggerByJob(jobCode);
        }

        public ExtractionTriggerData[] GetExtractionTriggerByRunDate(DateTime startDate,DateTime endDate)
        {
            return Channel.GetExtractionTriggerByRunDate(startDate,endDate);
        }

        public ExtractionTriggerData[] GetExtractionTriggerByRunTime(DateTime runTime)
        {
            return Channel.GetExtractionTriggerByRunTime(runTime);
        }

        public ExtractionTriggerData[] RunExtraction(int jobId,int[] extractionIds,DateTime startDate, DateTime endDate, DateTime runTime)
        {
            return Channel.RunExtraction(jobId, extractionIds, startDate, endDate, runTime);
        }

        public ExtractionTriggerData[] CancelExtractions(DateTime startDate, DateTime endDate)
        {
            return Channel.CancelExtractions(startDate, endDate);
        }

        public ExtractionTriggerData[] CancelExtractionByCode(string code,DateTime startDate, DateTime endDate)
        {
            return Channel.CancelExtractionByCode(code,startDate, endDate);
        }

        #endregion

        #region ProcessTrigger

        public ProcessTrigger UpdateProcessTrigger(ProcessTrigger processTrigger)
        {
            return Channel.UpdateProcessTrigger(processTrigger);
        }

        public void DeleteProcessTrigger(int processTriggerId)
        {
            Channel.DeleteProcessTrigger(processTriggerId);
        }

        public ProcessTrigger GetProcessTrigger(int processTriggerId)
        {
            return Channel.GetProcessTrigger(processTriggerId);
        }

        public ProcessTrigger[] GetAllProcessTrigger()
        {
            return Channel.GetAllProcessTrigger();
        }

        public ProcessTriggerData[] GetProcessTriggers()
        {
            return Channel.GetProcessTriggers();
        }


        public ProcessTriggerData[] GetProcessTriggerByProcess(int processId)
        {
            return Channel.GetProcessTriggerByProcess(processId);
        }

        public ProcessTriggerData[] GetProcessTriggerByJob(string jobCode)
        {
            return Channel.GetProcessTriggerByJob(jobCode);
        }

        public ProcessTriggerData[] GetProcessTriggerByRunDate()
        {
            return Channel.GetProcessTriggerByRunDate();
        }

        public ProcessTriggerData[] GetProcessTriggerByRunTime(DateTime runTime)
        {
            return Channel.GetProcessTriggerByRunTime(runTime);
        }

        public ProcessTriggerData[] RunProcess(int processId, DateTime runTime)
        {
            return Channel.RunProcess(processId, runTime);
        }

        public ProcessTriggerData[] CancelProcesses(DateTime startDate, DateTime endDate)
        {
            return Channel.CancelProcesses(startDate, endDate);
        }

        public ProcessTriggerData[] CancelProcessByCode(string code,DateTime startDate, DateTime endDate)
        {
            return Channel.CancelProcessByCode(code, startDate, endDate);
        }

        #endregion

        #region SolutionRunDate

        public SolutionRunDate UpdateSolutionRunDate(SolutionRunDate solutionRunDate)
        {
            return Channel.UpdateSolutionRunDate(solutionRunDate);
        }

        public void DeleteSolutionRunDate(int solutionRunDateId)
        {
            Channel.DeleteSolutionRunDate(solutionRunDateId);
        }

        public SolutionRunDate GetSolutionRunDate(int solutionRunDateId)
        {
            return Channel.GetSolutionRunDate(solutionRunDateId);
        }

        public SolutionRunDate[] GetAllSolutionRunDates()
        {
            return Channel.GetAllSolutionRunDates();
        }

        public SolutionRunDateData[] GetSolutionRunDates()
        {
            return Channel.GetSolutionRunDates();
        }

        public SolutionRunDateData[] GetSolutionRunDateByLogin(string loginID)
        {
            return Channel.GetSolutionRunDateByLogin(loginID);
        }

        public string GetSolutionRunDateByLoginByDefault(string loginID)
        {
            return Channel.GetSolutionRunDateByLoginByDefault(loginID);
        }

        public void RestoreArchive(int solutionid, DateTime date)
        {
            Channel.RestoreArchive(solutionid, date);
        }

        public SolutionRunDate[] GetRunDate()
        {
            return Channel.GetRunDate();
        }

        #endregion

        #region ClosedPeriod

        public ClosedPeriod UpdateClosedPeriod(ClosedPeriod closedPeriod)
        {
            return Channel.UpdateClosedPeriod(closedPeriod);
        }

        public void DeleteClosedPeriod(int closedPeriodId)
        {
            Channel.DeleteClosedPeriod(closedPeriodId);
        }

        public ClosedPeriod GetClosedPeriod(int closedPeriodId)
        {
            return Channel.GetClosedPeriod(closedPeriodId);
        }

        public ClosedPeriod[] GetAllClosedPeriods()
        {
            return Channel.GetAllClosedPeriods();
        }

        public ClosedPeriodData[] GetClosedPeriods()
        {
            return Channel.GetClosedPeriods();
        }

        public ClosedPeriodData[] GetClosedPeriodByLogin(string loginID)
        {
            return Channel.GetClosedPeriodByLogin(loginID);
        }

        public ClosedPeriod ClosePeriod(ClosedPeriod closedPeriod)
        {
            return Channel.ClosePeriod(closedPeriod);
        }

        #endregion

        #region ClosedPeriodTemplate

        public ClosedPeriodTemplate UpdateClosedPeriodTemplate(ClosedPeriodTemplate closedPeriodTemplate)
        {
            return Channel.UpdateClosedPeriodTemplate(closedPeriodTemplate);
        }

        public void DeleteClosedPeriodTemplate(int closedPeriodTemplateId)
        {
            Channel.DeleteClosedPeriodTemplate(closedPeriodTemplateId);
        }

        public ClosedPeriodTemplate GetClosedPeriodTemplate(int closedPeriodTemplateId)
        {
            return Channel.GetClosedPeriodTemplate(closedPeriodTemplateId);
        }

        public ClosedPeriodTemplate[] GetAllClosedPeriodTemplates()
        {
            return Channel.GetAllClosedPeriodTemplates();
        }

        public ClosedPeriodTemplateData[] GetClosedPeriodTemplates()
        {
            return Channel.GetClosedPeriodTemplates();
        }

        public ClosedPeriodTemplateData[] GetClosedPeriodTemplateByLogin(string loginID)
        {
            return Channel.GetClosedPeriodTemplateByLogin(loginID);
        }

        #endregion

        #region ExtractionJob

        public ExtractionJob UpdateExtractionJob(ExtractionJob extractionJob)
        {
            return Channel.UpdateExtractionJob(extractionJob);
        }

        public void DeleteExtractionJob(int extractionJobId)
        {
            Channel.DeleteExtractionJob(extractionJobId);
        }

        public ExtractionJob GetExtractionJob(int extractionJobId)
        {
            return Channel.GetExtractionJob(extractionJobId);
        }

        public ExtractionJob[] GetCurrentExtractionJobs()
        {
            return Channel.GetCurrentExtractionJobs();
        }

        public ExtractionJob[] GetExtractionJobByDate(DateTime startDate,DateTime endDate)
        {
            return Channel.GetExtractionJobByDate(startDate, endDate);
        }

        public ExtractionJob[] RunExtractionJob(int jobId, int[] extractionIds, DateTime startDate, DateTime endDate, DateTime runTime)
        {
            return Channel.RunExtractionJob(jobId, extractionIds, startDate, endDate, runTime);
        }

        public ExtractionJob[] CancelExtractionJobByCode(string jobCode, DateTime startDate, DateTime endDate)
        {
            return Channel.CancelExtractionJobByCode(jobCode, startDate, endDate);
        }

        public void ClearExtractionHistory(int solutionId)
        {
            Channel.ClearExtractionHistory(solutionId);
        }

        #endregion

        #region ProcessJob

        public ProcessJob UpdateProcessJob(ProcessJob processJob)
        {
            return Channel.UpdateProcessJob(processJob);
        }

        public void DeleteProcessJob(int processJobId)
        {
            Channel.DeleteProcessJob(processJobId);
        }

        public ProcessJob GetProcessJob(int processJobId)
        {
            return Channel.GetProcessJob(processJobId);
        }

        public ProcessJob[] GetCurrentProcessJobs()
        {
            return Channel.GetCurrentProcessJobs();
        }

        public ProcessJob[] GetProcessJobByRunDate()
        {
            return Channel.GetProcessJobByRunDate();
        }

        public ProcessJob[] RunProcessJob(int jobId, int[] processIds, DateTime runTime)
        {
            return Channel.RunProcessJob(jobId, processIds, runTime);
        }

        public ProcessJob[] CancelProcessJobByCode(string jobCode)
        {
            return Channel.CancelProcessJobByCode(jobCode);
        }
        public void RestartService(string serviceName)
        {
             Channel.RestartService(serviceName);
        }

        public string GetServiceStatus(string serviceName)
        {
            return Channel.GetServiceStatus(serviceName);
        }

        public void ClearProcessHistory(int solutionId)
        {
            Channel.ClearProcessHistory(solutionId);
        }
        #endregion

        #region Upload

        public Upload UpdateUpload(Upload upload)
        {
            return Channel.UpdateUpload(upload);
        }

        public void DeleteUpload(int uploadId)
        {
            Channel.DeleteUpload(uploadId);
        }

        public Upload GetUpload(int uploadId)
        {
            return Channel.GetUpload(uploadId);
        }
        public Upload[] GetUploadBySolution(int solutionId)
        {
            return Channel.GetUploadBySolution(solutionId);
        }

        
        public Upload[] GetAllUploads()
        {
            return Channel.GetAllUploads();
        }

        public UploadData[] GetUploads()
        {
            return Channel.GetUploads();
        }

        public UploadResult[] UploadCSV(int uploadId, string csvText)//, bool truncate, bool postUploadAction
        {
            return Channel.UploadCSV(uploadId, csvText);//, truncate, postUploadAction
        }

        public UploadResult[] UploadCSVByCode(string uploadCode, string csvText)
        {
            return Channel.UploadCSVByCode(uploadCode, csvText);
        }

        public UploadResult[] VerificationMsg(string sppVerify)
        {
            return Channel.VerificationMsg(sppVerify);
        }
        
        #endregion

        #region UploadRole

        public UploadRole UpdateUploadRole(UploadRole uploadRole)
        {
            return Channel.UpdateUploadRole(uploadRole);
        }

        public void DeleteUploadRole(int uploadRoleId)
        {
            Channel.DeleteUploadRole(uploadRoleId);
        }

        public UploadRole GetUploadRole(int uploadRoleId)
        {
            return Channel.GetUploadRole(uploadRoleId);
        }

        public UploadRole[] GetAllUploadRoles()
        {
            return Channel.GetAllUploadRoles();
        }

        public UploadRoleData[] GetUploadRoles()
        {
            return Channel.GetUploadRoles();
        }

        public UploadRoleData[] GetUploadRoleByUpload(int uploadId)
        {
            return Channel.GetUploadRoleByUpload(uploadId);
        }

        #endregion


        #region CheckDataAvailability

        public CheckDataAvailability[] GetAllDataAvailability()
        {
            return Channel.GetAllDataAvailability();
        }

        public void CheckDataAvailabilitybyRunDate(DateTime runDate)
        {
            Channel.CheckDataAvailabilitybyRunDate(runDate);
        }


        #endregion

        #region CheckifrsDataAvailability

        public CheckifrsDataAvailability[] GetAllifrsDataAvailability()
        {
            return Channel.GetAllifrsDataAvailability();
        }

        public void CheckifrsDataAvailabilitybyRunDate(DateTime runDate)
        {
            Channel.CheckifrsDataAvailabilitybyRunDate(runDate);
        }


        #endregion


    }
}

