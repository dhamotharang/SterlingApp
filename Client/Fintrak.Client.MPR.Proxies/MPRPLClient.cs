using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Proxies
{
    [Export(typeof(IMPRPLService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRPLClient : UserClientBase<IMPRPLService>, IMPRPLService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region MPRGLMapping

        public MPRGLMapping UpdateMPRGLMapping(MPRGLMapping mprGLMapping)
        {
            return Channel.UpdateMPRGLMapping(mprGLMapping);
        }

        public void DeleteMPRGLMapping(int mprGLMappingId)
        {
            Channel.DeleteMPRGLMapping(mprGLMappingId);
        }

        public MPRGLMapping GetMPRGLMapping(int mprGLMappingId)
        {
            return Channel.GetMPRGLMapping(mprGLMappingId);
        }

        public MPRGLMappingData[] GetAllMPRGLMappings()
        {
            return Channel.GetAllMPRGLMappings();
        }

        public KeyValueData[] GetUnMappedPLGLs()
        {
            return Channel.GetUnMappedPLGLs();
        }


        #endregion

        #region PLCaption

        public PLCaption UpdatePLCaption(PLCaption plCaption)
        {
            return Channel.UpdatePLCaption(plCaption);
        }

        public void DeletePLCaption(int plCaptionId)
        {
            Channel.DeletePLCaption(plCaptionId);
        }

        public PLCaption GetPLCaption(int plCaptionId)
        {
            return Channel.GetPLCaption(plCaptionId);
        }

        public PLCaptionNewData[] GetPLCaptions()
        {
            return Channel.GetPLCaptions();
        }

        public PLCaptionData[] GetAllPLCaptions()
        {
            return Channel.GetAllPLCaptions();
        }

        public PLCaptionNewData[] GetAllMPRPLCaptions()
        {
            return Channel.GetAllMPRPLCaptions();
        }

        public PLCaptionNewData[] GetAllBudgetPLCaptions()
        {
            return Channel.GetAllBudgetPLCaptions();
        }

        public PLCaptionNewData[] GetAllBudgetPLCaptionsByCaptionName(string CaptionName)
        {
            return Channel.GetAllBudgetPLCaptionsByCaptionName(CaptionName);
        }

        public PLCaptionNewData[] GetAllMPRPLCaptionsByCaptionName(string CaptionName)
        {
            return Channel.GetAllMPRPLCaptionsByCaptionName(CaptionName);
        }

        #endregion

        #region GLReclassification

        public GLReclassification UpdateGLReclassification(GLReclassification glReclassification)
        {
            return Channel.UpdateGLReclassification(glReclassification);
        }

        public void DeleteGLReclassification(int glReclassificationId)
        {
            Channel.DeleteGLReclassification(glReclassificationId);
        }

        public GLReclassification GetGLReclassification(int glReclassificationId)
        {
            return Channel.GetGLReclassification(glReclassificationId);
        }

        public GLReclassificationData[] GetAllGLReclassifications()
        {
            return Channel.GetAllGLReclassifications();
        }


        #endregion

        #region GLException

        public GLException UpdateGLException(GLException glException)
        {
            return Channel.UpdateGLException(glException);
        }

        public void DeleteGLException(int glExceptionId)
        {
            Channel.DeleteGLException(glExceptionId);
        }

        public GLException GetGLException(int glExceptionId)
        {
            return Channel.GetGLException(glExceptionId);
        }

        public GLExceptionData[] GetAllGLExceptions()
        {
            return Channel.GetAllGLExceptions();
        }

      
        #endregion

        #region GLMIS

        public GLMIS UpdateGLMIS(GLMIS glMIS)
        {
            return Channel.UpdateGLMIS(glMIS);
        }

        public void DeleteGLMIS(int glMISId)
        {
            Channel.DeleteGLMIS(glMISId);
        }

        public GLMIS GetGLMIS(int glMISId)
        {
            return Channel.GetGLMIS(glMISId);
        }

        public GLMISData[] GetAllGLMISInfo()
        {
            return Channel.GetAllGLMISInfo();
        }


        #endregion

        #region MPRTotalLine

        public MPRTotalLine UpdateMPRTotalLine(MPRTotalLine mprTotalLine)
        {
            return Channel.UpdateMPRTotalLine(mprTotalLine);
        }

        public void DeleteMPRTotalLine(int mprTotalLineId)
        {
            Channel.DeleteMPRTotalLine(mprTotalLineId);
        }

        public MPRTotalLine GetMPRTotalLine(int mprTotalLineId)
        {
            return Channel.GetMPRTotalLine(mprTotalLineId);
        }

        public MPRTotalLineData[] GetAllMPRTotalLines()
        {
            return Channel.GetAllMPRTotalLines();
        }

   

        #endregion

        #region MPRTotalLineMakeUp

        public MPRTotalLineMakeUp UpdateMPRTotalLineMakeUp(MPRTotalLineMakeUp mprTotalLineMakeUp)
        {
            return Channel.UpdateMPRTotalLineMakeUp(mprTotalLineMakeUp);
        }

        public void DeleteMPRTotalLineMakeUp(int mprTotalLineMakeUpId)
        {
            Channel.DeleteMPRTotalLineMakeUp(mprTotalLineMakeUpId);
        }

        public MPRTotalLineMakeUp GetMPRTotalLineMakeUp(int mprTotalLineMakeUpId)
        {
            return Channel.GetMPRTotalLineMakeUp(mprTotalLineMakeUpId);
        }

        public MPRTotalLineMakeUpData[] GetAllMPRTotalLineMakeUps()
        {
            return Channel.GetAllMPRTotalLineMakeUps();
        }



        #endregion

        #region PLIncomeReport

        public PLIncomeReport UpdatePLIncomeReport(PLIncomeReport plIncomeReport)
        {
            return Channel.UpdatePLIncomeReport(plIncomeReport);
        }

        public void DeletePLIncomeReport(int plIncomeReportId)
        {
            Channel.DeletePLIncomeReport(plIncomeReportId);
        }

        public PLIncomeReport GetPLIncomeReport(int plIncomeReportId)
        {
            return Channel.GetPLIncomeReport(plIncomeReportId);
        }

        public PLIncomeReport[] GetAllPLIncomeReports()
        {
            return Channel.GetAllPLIncomeReports();
        }

        public PLIncomeReport[] GetPLIncomeReports(string searchType, string searchValue, int number)
        {
            return Channel.GetPLIncomeReports(searchType, searchValue, number);
        }


        #endregion

        #region PLIncomeReportAdjustment

        public PLIncomeReportAdjustment UpdatePLIncomeReportAdjustment(PLIncomeReportAdjustment plIncomeReportAdjustment)
        {
            return Channel.UpdatePLIncomeReportAdjustment(plIncomeReportAdjustment);
        }

        public void DeletePLIncomeReportAdjustment(int plIncomeReportAdjustmentId)
        {
            Channel.DeletePLIncomeReportAdjustment(plIncomeReportAdjustmentId);
        }

        public PLIncomeReportAdjustment GetPLIncomeReportAdjustment(int plIncomeReportAdjustmentId)
        {
            return Channel.GetPLIncomeReportAdjustment(plIncomeReportAdjustmentId);
        }

        public PLIncomeReportAdjustment[] GetAllPLIncomeReportAdjustments()
        {
            return Channel.GetAllPLIncomeReportAdjustments();
        }

        public PLIncomeReportAdjustment[] GetPLIncomeReportAdjustments(string searchType, string searchValue, int number)
        {
            return Channel.GetPLIncomeReportAdjustments(searchType, searchValue, number);
        }


        public PLIncomeReportAdjustment[] GetCodebyUser(string userName)
        {
            return Channel.GetCodebyUser(userName);
        }

        public PLIncomeReportAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            return Channel.GetBalanceSheetAdjustmentByCode(code, userName);
        }

        public void DeleteMPRBalanceSheetAdjustment(string code, string userName)
        {
            Channel.DeleteMPRBalanceSheetAdjustment(code, userName);
        }


        #endregion

        #region Revenue

        public Revenue UpdateRevenue(Revenue revenue)
        {
            return Channel.UpdateRevenue(revenue);
        }

        public void DeleteRevenue(int revenueId)
        {
            Channel.DeleteRevenue(revenueId);
        }

        public Revenue GetRevenue(int revenueId)
        {
            return Channel.GetRevenue(revenueId);
        }

        public Revenue[] GetRevenues(string searchType, string searchValue, int number)
        {
            return Channel.GetRevenues(searchType,searchValue, number);
        }


        public Revenue[] GetAllRevenues(string searchType, string searchValue, int number, DateTime runDate, DateTime toDate)
        {
            return Channel.GetAllRevenues(searchType, searchValue, number, runDate, toDate);
        }

        public Revenue[] GetAllRevenue()
        {
            return Channel.GetAllRevenue();
        }

        public Revenue[] GetRunDate()
        {
            return Channel.GetRunDate();
        }

        #endregion

        #region RevenueBudget

        public RevenueBudget UpdateRevenueBudget(RevenueBudget revenueBudget)
        {
            return Channel.UpdateRevenueBudget(revenueBudget);
        }

        public void DeleteRevenueBudget(int revenueBudgetId)
        {
            Channel.DeleteRevenueBudget(revenueBudgetId);
        }

        public RevenueBudget GetRevenueBudget(int revenueBudgetId)
        {
            return Channel.GetRevenueBudget(revenueBudgetId);
        }

        public RevenueBudget[] GetAllRevenueBudgets(string year)
        {
            return Channel.GetAllRevenueBudgets(year);
        }

        public RevenueBudget[] GetRevenueBudgets(string searchValue)
        {
            return Channel.GetRevenueBudgets(searchValue);
        }



        #endregion

        #region RevenueBudgetOfficer

        public RevenueBudgetOfficer UpdateRevenueBudgetOfficer(RevenueBudgetOfficer revenueBudgetOfficer)
        {
            return Channel.UpdateRevenueBudgetOfficer(revenueBudgetOfficer);
        }

        public void DeleteRevenueBudgetOfficer(int revenueBudgetOffId)
        {
            Channel.DeleteRevenueBudgetOfficer(revenueBudgetOffId);
        }

        public RevenueBudgetOfficer GetRevenueBudgetOfficer(int revenueBudgetOffId)
        {
            return Channel.GetRevenueBudgetOfficer(revenueBudgetOffId);
        }

        public RevenueBudgetOfficer[] GetAllRevenueBudgetOfficers(string year)
        {
            return Channel.GetAllRevenueBudgetOfficers(year);
        }

        public RevenueBudgetOfficer[] GetRevenueBudgetOfficers(string searchValue)
        {
            return Channel.GetRevenueBudgetOfficers(searchValue);
        }


        #endregion

        #region ProcessData

        public ProcessData UpdateProcessData(ProcessData processData)
        {
            return Channel.UpdateProcessData(processData);
        }

        public void DeleteProcessData(int processDataId)
        {
            Channel.DeleteProcessData(processDataId);
        }

        public ProcessData GetProcessData(int processDataId)
        {
            return Channel.GetProcessData(processDataId);
        }

        public ProcessData[] GetAllProcessData()
        {
            return Channel.GetAllProcessData();
        }


        #endregion

        #region IncomeCentralVaultSchedule

        public IncomeCentralVaultSchedule UpdateIncomeCentralVaultSchedule(IncomeCentralVaultSchedule incomeCentralVaultSchedule)
        {
            return Channel.UpdateIncomeCentralVaultSchedule(incomeCentralVaultSchedule);
        }

        public void DeleteIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId)
        {
            Channel.DeleteIncomeCentralVaultSchedule(incomeCentralVaultScheduleId);
        }

        public IncomeCentralVaultSchedule GetIncomeCentralVaultSchedule(int incomeCentralVaultScheduleId)
        {
            return Channel.GetIncomeCentralVaultSchedule(incomeCentralVaultScheduleId);
        }
        public IncomeCentralVaultScheduleData[] GetAllIncomeCentralVaultSchedule()
        {
            return Channel.GetAllIncomeCentralVaultSchedule();
        }


        #endregion

        #region MPRCommFee

        public MPRCommFee UpdateMPRCommFee(MPRCommFee mprcommfee)
        {
            return Channel.UpdateMPRCommFee(mprcommfee);
        }

        public void DeleteMPRCommFee(int mprcommfeeId)
        {
            Channel.DeleteMPRCommFee(mprcommfeeId);
        }

        public MPRCommFee GetMPRCommFee(int Commfee_Id)
        {
            return Channel.GetMPRCommFee(Commfee_Id);
        }

        public MPRCommFee[] GetMPRCommFees()
        {
            return Channel.GetMPRCommFees();
        }


        public MPRCommFee[] GetMPRCommFeesBySearch(string searchValue)
        {
            return Channel.GetMPRCommFeesBySearch(searchValue);
        }

        #endregion

    }
}
