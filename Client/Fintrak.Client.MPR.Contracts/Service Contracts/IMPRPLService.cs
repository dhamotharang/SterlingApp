using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Client.MPR.Entities;
//using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
//using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Client.MPR.Contracts
{
    [ServiceContract]
    public interface IMPRPLService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region MPRGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRGLMapping UpdateMPRGLMapping(MPRGLMapping mprGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRGLMapping(int mprGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRGLMapping GetMPRGLMapping(int mprGLMappingId);

        [OperationContract]
        MPRGLMappingData[] GetAllMPRGLMappings();

        [OperationContract]
        KeyValueData[] GetUnMappedPLGLs();

        #endregion

        #region PLCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLCaption UpdatePLCaption(PLCaption plCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLCaption(int plCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLCaption GetPLCaption(int plCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLCaptionNewData[] GetPLCaptions();

        [OperationContract]
        PLCaptionData[] GetAllPLCaptions();

        [OperationContract]
        PLCaptionNewData[] GetAllMPRPLCaptions();

        [OperationContract]
        PLCaptionNewData[] GetAllBudgetPLCaptions();

        [OperationContract]
        PLCaptionNewData[] GetAllBudgetPLCaptionsByCaptionName(string CaptionName);

        [OperationContract]
        PLCaptionNewData[] GetAllMPRPLCaptionsByCaptionName(string CaptionName);


        #endregion

        #region GLReclassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLReclassification UpdateGLReclassification(GLReclassification glReclassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLReclassification(int glReclassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLReclassification GetGLReclassification(int glReclassificationId);

        [OperationContract]
        GLReclassificationData[] GetAllGLReclassifications();

        #endregion

        #region GLException

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLException UpdateGLException(GLException glException);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLException(int glExceptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLException GetGLException(int glExceptionId);

        [OperationContract]
        GLExceptionData[] GetAllGLExceptions();

        #endregion

        #region GLMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLMIS UpdateGLMIS(GLMIS glMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLMIS(int glMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLMIS GetGLMIS(int glMISId);

        [OperationContract]
        GLMISData[] GetAllGLMISInfo();


        #endregion

        #region MPRTotalLine

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRTotalLine UpdateMPRTotalLine(MPRTotalLine mprTotalLine);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRTotalLine(int mprTotalLineId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRTotalLine GetMPRTotalLine(int mprTotalLineId);

        [OperationContract]
        MPRTotalLineData[] GetAllMPRTotalLines();

        #endregion

        #region MPRTotalLineMakeUp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRTotalLineMakeUp UpdateMPRTotalLineMakeUp(MPRTotalLineMakeUp mprTotalLineMakeUp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRTotalLineMakeUp(int mprTotalLineMakeUpId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRTotalLineMakeUp GetMPRTotalLineMakeUp(int mprTotalLineMakeUpId);

        [OperationContract]
        MPRTotalLineMakeUpData[] GetAllMPRTotalLineMakeUps();

        #endregion

        #region PLIncomeReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLIncomeReport UpdatePLIncomeReport(PLIncomeReport plIncomeReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLIncomeReport(int plIncomeReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLIncomeReport GetPLIncomeReport(int plIncomeReportId);

        [OperationContract]
        PLIncomeReport[] GetAllPLIncomeReports();

        [OperationContract]
        PLIncomeReport[] GetPLIncomeReports(string searchType, string searchValue, int number);

        #endregion

        #region PLIncomeReportAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLIncomeReportAdjustment UpdatePLIncomeReportAdjustment(PLIncomeReportAdjustment plIncomeReportAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLIncomeReportAdjustment(int plIncomeReportAdjustmentd);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLIncomeReportAdjustment GetPLIncomeReportAdjustment(int plIncomeReportAdjustmentId);

        [OperationContract]
        PLIncomeReportAdjustment[] GetAllPLIncomeReportAdjustments();

        [OperationContract]
        PLIncomeReportAdjustment[] GetPLIncomeReportAdjustments(string searchType, string searchValue, int number);

        [OperationContract]
        PLIncomeReportAdjustment[] GetCodebyUser(string userName);

        [OperationContract]
        PLIncomeReportAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName);


        [OperationContract]
        void DeleteMPRBalanceSheetAdjustment(string code, string userName);


        #endregion

        #region Revenue

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Revenue UpdateRevenue(Revenue revenue);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenue(int revenueId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Revenue GetRevenue(int revenueId);

        [OperationContract]
        Revenue[] GetRevenues(string searchType, string searchValue, int number);

        [OperationContract]
        Revenue[] GetAllRevenues(string searchType, string searchValue, int number, DateTime runDate, DateTime toDate);

        [OperationContract]
        Revenue[] GetAllRevenue();

       [OperationContract]
        Revenue[] GetRunDate();

        #endregion

        #region RevenueBudget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueBudget UpdateRevenueBudget(RevenueBudget revenueBudget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueBudget(int revenueBudgetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueBudget GetRevenueBudget(int revenueBudgetId);

        [OperationContract]
        RevenueBudget[] GetAllRevenueBudgets(string year);


        [OperationContract]
        RevenueBudget[] GetRevenueBudgets(string searchValue);

        #endregion
        
        #region RevenueBudgetOfficer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueBudgetOfficer UpdateRevenueBudgetOfficer(RevenueBudgetOfficer revenueBudgetOfficer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueBudgetOfficer(int revenueBudgetOffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueBudgetOfficer GetRevenueBudgetOfficer(int revenueBudgetOffId);

        [OperationContract]
        RevenueBudgetOfficer[] GetAllRevenueBudgetOfficers(string year);

        [OperationContract]
        RevenueBudgetOfficer[] GetRevenueBudgetOfficers(string searchValue);

        #endregion
        
        #region ProcessData

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProcessData UpdateProcessData(ProcessData processData);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProcessData(int processDataId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProcessData GetProcessData(int processDataId);

        [OperationContract]
        ProcessData[] GetAllProcessData();

        #endregion

        #region IncomeCentralVaultSchedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCentralVaultSchedule UpdateIncomeCentralVaultSchedule(IncomeCentralVaultSchedule incomeCentralVaultSchedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCentralVaultSchedule GetIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId);


        [OperationContract]
        IncomeCentralVaultScheduleData[] GetAllIncomeCentralVaultSchedule();

        #endregion

        #region MPRCommFee

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRCommFee UpdateMPRCommFee(MPRCommFee MPRCommFee);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRCommFee(int MPRCommFeeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRCommFee GetMPRCommFee(int CommFee_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRCommFee[] GetMPRCommFees();

        [OperationContract]
        //MPRCommFee[] GetMPRCommFeesBy(string searchType, string searchValue);
        MPRCommFee[] GetMPRCommFeesBySearch(string searchValue);

        #endregion

    }
}
