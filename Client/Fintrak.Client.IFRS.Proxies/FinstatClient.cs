using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
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

        //#region GLAdjustment

        //public GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment)
        //{
        //    return Channel.UpdateGLAdjustment(glAdjustment);
        //}

        //public void PostGLAdjustment(AdjustmentType adjustmentType)
        //{
        //    Channel.PostGLAdjustment(adjustmentType);
        //}

        //public void DeleteGLAdjustment(int glAdjustmentId)
        //{
        //    Channel.DeleteGLAdjustment(glAdjustmentId);
        //}

        //public void DeleteGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        //{
        //    Channel.DeleteGLAdjustmentByCode(adjustmentType, adjustmentCode);
        //}

        //public void ReverseGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        //{
        //    Channel.ReverseGLAdjustmentByCode(adjustmentType, adjustmentCode);
        //}

        //public void PurgeGLAdjustment(AdjustmentType adjustmentType)
        //{
        //    Channel.PurgeGLAdjustment(adjustmentType);
        //}

        //public GLAdjustment GetGLAdjustment(int glAdjustmentId)
        //{
        //    return Channel.GetGLAdjustment(glAdjustmentId);
        //}

        //public GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType, bool posted)
        //{
        //    return Channel.GetGLAdjustmentByStatus(adjustmentType, posted);
        //}

        //public GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType)
        //{
        //    return Channel.GetGLAdjustments(adjustmentType);
        //}


        //#endregion

        #region GLAdjustment

        public GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment)
        {
            return Channel.UpdateGLAdjustment(glAdjustment);
        }

        public void PostGLAdjustment(AdjustmentType adjustmentType, ReportType reportType)
        {
            Channel.PostGLAdjustment(adjustmentType, reportType);
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

        public void PurgeGLAdjustment(AdjustmentType adjustmentType,ReportType reportType)
        {
            Channel.PurgeGLAdjustment(adjustmentType, reportType);
        }

    public void PurgeUnpostedGLAdjustment(int adjustmentType)
        {
            Channel.PurgeUnpostedGLAdjustment(adjustmentType);
        }


        public void CallUpPrevAdjustment(int adjustmentType)
        {
            Channel.CallUpPrevAdjustment(adjustmentType);
        }
        public GLAdjustment GetGLAdjustment(int glAdjustmentId)
        {
            return Channel.GetGLAdjustment(glAdjustmentId);
        }

        public GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType, bool posted)
        {
            return Channel.GetGLAdjustmentByStatus(adjustmentType, posted);
        }

        public GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType, ReportType reportType)
        {
            return Channel.GetGLAdjustments(adjustmentType, reportType);
        }

        public GLAdjustmentDataMain[] GetGLAdjustmentsMain(int glAdjustmentId, AdjustmentType adjustmentType, ReportType reportType)
        {
            return Channel.GetGLAdjustmentsMain(glAdjustmentId, adjustmentType, reportType);
        }

        public GLAdjustmentDataMain[] GetGLAdjustmentByStatusM(AdjustmentType adjustmentType, ReportType reportType, bool posted)
        {
            return Channel.GetGLAdjustmentByStatusM(adjustmentType, reportType, posted);
        }


        public string[] GetUnMappedGlCodes()
        {
            return Channel.GetUnMappedGlCodes();
        }
        #endregion

        #region GLMapping

        public GLMapping UpdateGLMapping(GLMapping glMapping, int status)
        {
            //vvv
            return Channel.UpdateGLMapping(glMapping, status);
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
            //
        }

        public GLMappingData[] GetGLMappingsBySearch(int flag, string searchParam)
        {
            return Channel.GetGLMappingsBySearch(flag, searchParam);
            //
        }
        public GLMappingData[] GetGLMappings(int flag, int defaultCount, string path)
        {
            return Channel.GetGLMappings(flag, defaultCount, path);
        }
        public GLMappingData[] GetDistinctGLMappings()
        {
            return Channel.GetDistinctGLMappings();
        }
        public GLMappingData[] GetUnMappedGLs()
        {
            return Channel.GetUnMappedGLs();
        }
        public GLMappingData[] GetUnMappedGLbyGLCode(string glCode)
        {
            return Channel.GetUnMappedGLbyGLCode(glCode);
        }

        public GLMapping[] GetSubSubCaption(string caption)
        {
            return Channel.GetSubSubCaption(caption);
        }

        public string[] GetSubCaptionPosition(string caption)
        {
            return Channel.GetSubCaptionPosition(caption);
        }
        #endregion

        #region GLMappingMgt

        public GLMappingMgt UpdateGLMappingMgt(GLMappingMgt glMapping, int status)
        {
            //vvv
            return Channel.UpdateGLMappingMgt(glMapping, status);
        }

        public void DeleteGLMappingMgt(int glMappingId)
        {
            Channel.DeleteGLMappingMgt(glMappingId);
        }

        public GLMappingMgt GetGLMappingMgt(int glMappingId)
        {
            return Channel.GetGLMappingMgt(glMappingId);
        }

        public GLMappingMgt[] GetAllGLMappingMgts()
        {
            return Channel.GetAllGLMappingMgts();
            //
        }
        public GLMappingMgtData[] GetGLMappingMgts()
        {
            return Channel.GetGLMappingMgts();
        }
        public GLMappingMgtData[] GetUnMappedMgtGLs()
        {
            return Channel.GetUnMappedMgtGLs();
        }
        public GLMappingMgtData[] GetUnMappedMgtGLbyGLCode(string glCode)
        {
            return Channel.GetUnMappedMgtGLbyGLCode(glCode);
        }

        public GLMappingMgt[] GetMgtSubSubCaption(string caption)
        {
            return Channel.GetMgtSubSubCaption(caption);
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

        //public PostingDetail[] GetAllPostingDetails()
        //{
        //    return Channel.GetAllPostingDetails();
        //}

        public PostingDetailData[] GetPostingDetailsByType(int reportType)
        {
            return Channel.GetPostingDetailsByType(reportType);
        }

        #endregion

        #region PostingDetailContracts

        public PostingDetailContracts UpdatePostingDetailContracts(PostingDetailContracts postingDetail)
        {
            return Channel.UpdatePostingDetailContracts(postingDetail);
        }

        public void DeletePostingDetailContracts(int postingDetailId)
        {
            Channel.DeletePostingDetailContracts(postingDetailId);
        }

        public PostingDetailContracts GetPostingDetailContracts(int postingDetailId)
        {
            return Channel.GetPostingDetailContracts(postingDetailId);
        }

        public PostingDetailContracts[] GetPostingDetailContractsByFilter(string filter, string path)
        {
            return Channel.GetPostingDetailContractsByFilter(filter, path);
        }

        public string[] GetDistinctPostingFilters(int count)
        {
            return Channel.GetDistinctPostingFilters(count);
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

        public TrialBalance[] GetTrialBalancesByBranch(string branchCode)
        {
            return Channel.GetTrialBalancesByBranch(branchCode);
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

        public TrialBalanceGap[] GetGapTrialBalancesByBranch(string branchCode)
        {
            return Channel.GetGapTrialBalancesByBranch(branchCode);
        }

        #endregion

        #region TrialBalanceConsolidated

        public TrialBalanceConsolidated[] GetAllTrialBalanceConsolidated(string CompanyCode)
        {
            return Channel.GetAllTrialBalanceConsolidated(CompanyCode);
        }



        #endregion

        #region TrialBalanceConsolidatedIFRS

        public TrialBalanceConsolidatedIFRS[] GetAllTrialBalanceConsolidatedIFRS(string CompanyCode)
        {
            return Channel.GetAllTrialBalanceConsolidatedIFRS(CompanyCode);
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

        #region RevenueGLMapping

        public RevenueGLMapping UpdateRevenueGLMapping(RevenueGLMapping revenueGLMapping)
        {
            return Channel.UpdateRevenueGLMapping(revenueGLMapping);
        }

        public void DeleteRevenueGLMapping(int revenueGLMappingId)
        {
            Channel.DeleteRevenueGLMapping(revenueGLMappingId);
        }

        public RevenueGLMapping GetRevenueGLMapping(int revenueGLMappingId)
        {
            return Channel.GetRevenueGLMapping(revenueGLMappingId);
        }

        public RevenueGLMapping[] GetAllRevenueGLMappings()
        {
            return Channel.GetAllRevenueGLMappings();
        }

        public KeyValueData[] GetUnMappedRevenueGLs()
        {
            return Channel.GetUnMappedRevenueGLs();
        }

        #endregion

        #region BondSummary

        //public BondSummary UpdateBondSummary(BondSummary bondSummary)
        //{
        //    return Channel.UpdateBondSummary(bondSummary);
        //}

        //public void DeleteBondSummary(int BondId)
        //{
        //    Channel.DeleteBondSummary(BondId);
        //}

        //public BondSummary GetBondSummary(int BondId)
        //{
        //    return Channel.GetBondSummary(BondId);
        //}

        //public BondSummary[] GetAllBondSummarys()
        //{
        //    return Channel.GetAllBondSummarys();
        //}


        #endregion

        #region LedgerDetailSummary

        public LedgerDetailSummary UpdateLedgerDetailSummary(LedgerDetailSummary ledgerDetailSummary)
        {
            return Channel.UpdateLedgerDetailSummary(ledgerDetailSummary);
        }

        public void DeleteLedgerDetailSummary(int ledgerDetailSummaryId)
        {
            Channel.DeleteLedgerDetailSummary(ledgerDetailSummaryId);
        }

        public LedgerDetailSummary GetLedgerDetailSummary(int ledgerDetailSummaryId)
        {
            return Channel.GetLedgerDetailSummary(ledgerDetailSummaryId);
        }
        public LedgerDetailSummary[] GetAllLedgerDetailSummarys()
        {
            return Channel.GetAllLedgerDetailSummarys();
        }


        #endregion

        #region ChangesInEquity

        public ChangesInEquity UpdateChangesInEquity(ChangesInEquity changesInEquity)
        {
            return Channel.UpdateChangesInEquity(changesInEquity);
        }

        public void DeleteChangesInEquity(int ChangesInEquityId)
        {
            Channel.DeleteChangesInEquity(ChangesInEquityId);
        }

        public ChangesInEquity GetChangesInEquity(int ChangesInEquityId)
        {
            return Channel.GetChangesInEquity(ChangesInEquityId);
        }

        public ChangesInEquity[] GetAllChangesInEquitys()
        {
            return Channel.GetAllChangesInEquitys();
        }


        #endregion

        #region RatioDetail

        public RatioDetail UpdateRatioDetail(RatioDetail ratiodetail)
        {
            return Channel.UpdateRatioDetail(ratiodetail);
        }

        public void DeleteRatioDetail(int ratioID)
        {
            Channel.DeleteRatioDetail(ratioID);
        }

        public RatioDetail GetRatioDetail(int ratioID)
        {
            return Channel.GetRatioDetail(ratioID);
        }

        public RatioDetail[] GetAllRatioDetails()
        {
            return Channel.GetAllRatioDetails();
        }

        #endregion

        #region RatioCaption

        public RatioCaption UpdateRatioCaption(RatioCaption ratiocaption)
        {
            return Channel.UpdateRatioCaption(ratiocaption);
        }

        public void DeleteRatioCaption(int RatioCaptionID)
        {
            Channel.DeleteRatioCaption(RatioCaptionID);
        }

        public RatioCaption GetRatioCaption(int RatioCaptionID)
        {
            return Channel.GetRatioCaption(RatioCaptionID);
        }

        public RatioCaption[] GetAllRatioCaptions()
        {
            return Channel.GetAllRatioCaptions();
        }

        #endregion

        #region GLAArchive

        public GLAArchive[] GetGLAArchivesByRundate(DateTime rundate)
        {
            return Channel.GetGLAArchivesByRundate(rundate);
        }

        #endregion

        #region Calendar

        public Calendar UpdateCalendar(Calendar Calendar)
        {
            return Channel.UpdateCalendar(Calendar);
        }

        public void DeleteCalendar(int CalendarID)
        {
            Channel.DeleteCalendar(CalendarID);
        }

        public Calendar GetCalendar(int CalendarID)
        {
            return Channel.GetCalendar(CalendarID);
        }

        public Calendar[] GetAllCalendars()
        {
            return Channel.GetAllCalendars();
        }

        public Calendar[] GetCalendarException(DateTime runDate)
        {
            return Channel.GetCalendarException(runDate);
        }
        #endregion

    }
}
