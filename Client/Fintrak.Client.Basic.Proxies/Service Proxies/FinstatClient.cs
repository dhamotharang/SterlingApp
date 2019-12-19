using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
{
    [Export(typeof(IFinstatService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FinstatClient : UserClientBase<IFinstatService>, IFinstatService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region AutoPostingTemplate

        public AutoPostingTemplate UpdateAutoPostingTemplate(AutoPostingTemplate autoPostingTemplate)
        {
            return Channel.UpdateAutoPostingTemplate(autoPostingTemplate);
        }

        public void DeleteAutoPostingTemplate(int autoPostingTemplateId)
        {
           Channel.DeleteAutoPostingTemplate(autoPostingTemplateId);
        }

        public AutoPostingTemplate GetAutoPostingTemplate(int autoPostingTemplateId)
        {
            return Channel.GetAutoPostingTemplate(autoPostingTemplateId);
        }

        public AutoPostingTemplate[] GetAllAutoPostingTemplates()
        {
            return Channel.GetAllAutoPostingTemplates();
        }

        #endregion

        #region GLAdjustment

        public GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment)
        {
            return Channel.UpdateGLAdjustment(glAdjustment);
        }

        public void PostGLAdjustment(AdjustmentType adjustmentType)
        {
            Channel.PostGLAdjustment(adjustmentType);
        }

        public void DeleteGLAdjustment(int glAdjustmentId)
        {
            Channel.DeleteGLAdjustment(glAdjustmentId);
        }

        public void DeleteGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        {
            Channel.DeleteGLAdjustmentByCode(adjustmentType, adjustmentCode);
        }

        public void ReverseGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        {
            Channel.ReverseGLAdjustmentByCode(adjustmentType, adjustmentCode);
        }

        public void PurgeGLAdjustment(AdjustmentType adjustmentType)
        {
            Channel.PurgeGLAdjustment(adjustmentType);
        }

        public GLAdjustment GetGLAdjustment(int glAdjustmentId)
        {
            return Channel.GetGLAdjustment(glAdjustmentId);
        }

        public GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType, bool posted)
        {
            return Channel.GetGLAdjustmentByStatus(adjustmentType, posted);
        }

        public GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType)
        {
            return Channel.GetGLAdjustments(adjustmentType);
        }


        #endregion

        #region GLMapping

        public GLMapping UpdateGLMapping(GLMapping glMapping)
        {
            return Channel.UpdateGLMapping(glMapping);
        }

        public void DeleteGLMapping(int glMappingId)
        {
            Channel.DeleteGLMapping(glMappingId);
        }

        public GLMapping GetGLMapping(int glMappingId)
        {
            return Channel.GetGLMapping(glMappingId);
        }

        public GLMapping[] GetAllGLMappings()
        {
            return Channel.GetAllGLMappings();
        }
        public GLMappingData[] GetGLMappings()
        {
            return Channel.GetGLMappings();
        }
        public GLMappingData[] GetUnMappedGLs()
        {
            return Channel.GetUnMappedGLs();
        }
        public GLMappingData[] GetUnMappedGLbyGLCode(string glCode)
        {
            return Channel.GetUnMappedGLbyGLCode(glCode);
        }
        
        #endregion

        #region GLType

        public GLType UpdateGLType(GLType glType)
        {
            return Channel.UpdateGLType(glType);
        }

        public void DeleteGLType(int glTypeId)
        {
            Channel.DeleteGLType(glTypeId);
        }

        public GLType GetGLType(int glTypeId)
        {
            return Channel.GetGLType(glTypeId);
        }

        public GLType[] GetAllGLTypes()
        {
            return Channel.GetAllGLTypes();
        }

        //public GLTypeData[] GetGLTypees()
        //{
        //    return Channel.GetGLTypees();
        //}


        #endregion

        #region InstrumentType

        public InstrumentType UpdateInstrumentType(InstrumentType instrumentType)
        {
            return Channel.UpdateInstrumentType(instrumentType);
        }

        public void DeleteInstrumentType(int instrumentTypeId)
        {
            Channel.DeleteInstrumentType(instrumentTypeId);
        }

        public InstrumentType GetInstrumentType(int instrumentTypeId)
        {
            return Channel.GetInstrumentType(instrumentTypeId);
        }

        public InstrumentTypeData[] GetAllInstrumentTypes()
        {
            return Channel.GetAllInstrumentTypes();
        }

        //public InstrumentTypeData[] GetInstrumentTypes()
        //{
        //    return Channel.GetInstrumentTypes();
        //}

        //public InstrumentTypeData[] GetInstrumentTypeByProcess(int processId)
        //{
        //    return Channel.GetInstrumentTypeByProcess(processId);
        //}

        #endregion

        #region InstrumentTypeGLMap

        public InstrumentTypeGLMap UpdateInstrumentTypeGLMap(InstrumentTypeGLMap instrumentTypeGLMap)
        {
            return Channel.UpdateInstrumentTypeGLMap(instrumentTypeGLMap);
        }

        public void DeleteInstrumentTypeGLMap(int instrumentTypeGLMapId)
        {
            Channel.DeleteInstrumentTypeGLMap(instrumentTypeGLMapId);
        }

        public InstrumentTypeGLMap GetInstrumentTypeGLMap(int instrumentTypeGLMapId)
        {
            return Channel.GetInstrumentTypeGLMap(instrumentTypeGLMapId);
        }

        public InstrumentTypeGLMapData[] GetAllInstrumentTypeGLMaps()
        {
            return Channel.GetAllInstrumentTypeGLMaps();
        }

        //public InstrumentTypeGLMapData[] GetInstrumentTypeGLMaps()
        //{
        //    return Channel.GetInstrumentTypeGLMaps();
        //}


        //public InstrumentTypeGLMapData[] GetInstrumentTypeGLMapByExtraction(int extractionId)
        //{
        //    return Channel.GetInstrumentTypeGLMapByExtraction(extractionId);
        //}

        //public InstrumentTypeGLMapData[] GetInstrumentTypeGLMapByRunDate(DateTime startDate,DateTime endDate)
        //{
        //    return Channel.GetInstrumentTypeGLMapByRunDate(startDate,endDate);
        //}

        //public InstrumentTypeGLMapData[] GetInstrumentTypeGLMapByRunTime(DateTime runTime)
        //{
        //    return Channel.GetInstrumentTypeGLMapByRunTime(runTime);
        //}

        //public InstrumentTypeGLMapData[] RunExtraction(int extractionId,DateTime startDate, DateTime endDate, DateTime runTime)
        //{
        //    return Channel.RunExtraction(extractionId,startDate, endDate, runTime);
        //}

        //public InstrumentTypeGLMapData[] CancelExtractions(DateTime startDate, DateTime endDate)
        //{
        //    return Channel.CancelExtractions(startDate, endDate);
        //}

        #endregion

        #region PostingDetail

        public PostingDetail UpdatePostingDetail(PostingDetail postingDetail)
        {
            return Channel.UpdatePostingDetail(postingDetail);
        }

        public void DeletePostingDetail(int postingDetailId)
        {
            Channel.DeletePostingDetail(postingDetailId);
        }

        public PostingDetail GetPostingDetail(int postingDetailId)
        {
            return Channel.GetPostingDetail(postingDetailId);
        }

        public PostingDetail[] GetAllPostingDetails()
        {
            return Channel.GetAllPostingDetails();
        }

        #endregion

        #region IFRSReport

        public IFRSReport UpdateIFRSReport(IFRSReport ifrsReport)
        {
            return Channel.UpdateIFRSReport(ifrsReport);
        }

        public void DeleteIFRSReport(int ifrsReportId)
        {
            Channel.DeleteIFRSReport(ifrsReportId);
        }

        public IFRSReport GetIFRSReport(int ifrsReportId)
        {
            return Channel.GetIFRSReport(ifrsReportId);
        }

        public IFRSReport[] GetAllIFRSReports()
        {
            return Channel.GetAllIFRSReports();
        }




        #endregion

        #region TransactionDetail

        public TransactionDetail UpdateTransactionDetail(TransactionDetail transactionDetail)
        {
            return Channel.UpdateTransactionDetail(transactionDetail);
        }

        public void DeleteTransactionDetail(int transactionDetailId)
        {
            Channel.DeleteTransactionDetail(transactionDetailId);
        }

        public TransactionDetail GetTransactionDetail(int transactionDetailId)
        {
            return Channel.GetTransactionDetail(transactionDetailId);
        }

        public TransactionDetail[] GetAllTransactionDetails()
        {
            return Channel.GetAllTransactionDetails();
        }

       
        #endregion

        #region TrialBalance

        public TrialBalance UpdateTrialBalance(TrialBalance trialBalance)
        {
            return Channel.UpdateTrialBalance(trialBalance);
        }

        public void DeleteTrialBalance(int trialBalanceId)
        {
            Channel.DeleteTrialBalance(trialBalanceId);
        }

        public TrialBalance GetTrialBalance(int trialBalanceId)
        {
            return Channel.GetTrialBalance(trialBalanceId);
        }

        public TrialBalance[] GetAllTrialBalances()
        {
            return Channel.GetAllTrialBalances();
        }

        public TrialBalance[] GetTrialBalanceByGL(string glCode)
        {
            return Channel.GetTrialBalanceByGL(glCode);
        }

        #endregion

        #region TrialBalanceGap

        public TrialBalanceGap UpdateTrialBalanceGap(TrialBalanceGap trialBalanceGap)
        {
            return Channel.UpdateTrialBalanceGap(trialBalanceGap);
        }

        public void DeleteTrialBalanceGap(int trialBalanceGapId)
        {
            Channel.DeleteTrialBalanceGap(trialBalanceGapId);
        }

        public TrialBalanceGap GetTrialBalanceGap(int trialBalanceGapId)
        {
            return Channel.GetTrialBalanceGap(trialBalanceGapId);
        }
        public TrialBalanceGap[] GetAllTrialBalanceGaps()
        {
            return Channel.GetAllTrialBalanceGaps();
        }

        public TrialBalanceGap[] GetTrialBalanceGapByGL(string glCode)
        {
            return Channel.GetTrialBalanceGapByGL(glCode);
        }

        #endregion


        #region IFRSBudget

        public IFRSBudget UpdateIFRSBudget(IFRSBudget ifrsBudget)
        {
            return Channel.UpdateIFRSBudget(ifrsBudget);
        }

        public void DeleteIFRSBudget(int ifrsbudgetId)
        {       
            Channel.DeleteIFRSBudget(ifrsbudgetId);
        }

        public IFRSBudget GetIFRSBudget(int ifrsbudgetId)
        {
            return Channel.GetIFRSBudget(ifrsbudgetId);
        }

        public IFRSBudget[] GetAllIFRSBudgets()
        {
            return Channel.GetAllIFRSBudgets();
        }


        #endregion

    }
}
