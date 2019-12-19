using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
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

        public PLCaptionData[] GetAllPLCaptions()
        {
            return Channel.GetAllPLCaptions();
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

        public Revenue[] GetAllRevenues()
        {
            return Channel.GetAllRevenues();
        }


        public Revenue[] GetRevenues(string searchType, string searchValue, int number)
        {
            return Channel.GetRevenues(searchType, searchValue, number);
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


        #endregion

    }
}
