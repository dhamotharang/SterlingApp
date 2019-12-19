using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
{
    [Export(typeof(IIFRSLoanService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSLoanClient : UserClientBase<IIFRSLoanService>, IIFRSLoanService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region CollateralRealizationPeriod

        public CollateralRealizationPeriod UpdateCollateralRealizationPeriod(CollateralRealizationPeriod collateralRealizationPeriod)
        {
            return Channel.UpdateCollateralRealizationPeriod(collateralRealizationPeriod);
        }

        public void DeleteCollateralRealizationPeriod(int collateralRealizationPeriodId)
        {
            Channel.DeleteCollateralRealizationPeriod(collateralRealizationPeriodId);
        }

        public CollateralRealizationPeriod GetCollateralRealizationPeriod(int collateralRealizationPeriodId)
        {
            return Channel.GetCollateralRealizationPeriod(collateralRealizationPeriodId);
        }

        public CollateralRealizationPeriodData[] GetAllCollateralRealizationPeriods()
        {
            return Channel.GetAllCollateralRealizationPeriods();
        }

       

        #endregion

        #region CollateralType

        public CollateralType UpdateCollateralType(CollateralType collateralType)
        {
            return Channel.UpdateCollateralType(collateralType);
        }

        public void DeleteCollateralType(int collateralTypeId)
        {
            Channel.DeleteCollateralType(collateralTypeId);
        }

        public CollateralType GetCollateralType(int collateralTypeId)
        {
            return Channel.GetCollateralType(collateralTypeId);
        }

        public CollateralTypeData[] GetCollateralTypeByCategory(string categoryCode)
        {
            return Channel.GetCollateralTypeByCategory(categoryCode);
        }

        public CollateralTypeData[] GetAllCollateralTypes()
        {
            return Channel.GetAllCollateralTypes();
        }



        #endregion

        #region IFRSProduct

        public IFRSProduct UpdateIFRSProduct(IFRSProduct ifrsProduct)
        {
            return Channel.UpdateIFRSProduct(ifrsProduct);
        }

        public void DeleteIFRSProduct(int ifrsProductId)
        {
            Channel.DeleteIFRSProduct(ifrsProductId);
        }

        public IFRSProduct GetIFRSProduct(int ifrsProductId)
        {
            return Channel.GetIFRSProduct(ifrsProductId);
        }

        public IFRSProductData[] GetAllIFRSProducts()
        {
            return Channel.GetAllIFRSProducts();
        }


        public IFRSProduct GetLoanProducts(int ProductTypeId)
        {
            return Channel.GetIFRSProduct(ProductTypeId);
        }


        #endregion

        #region CollateralCategory

        public CollateralCategory UpdateCollateralCategory(CollateralCategory collateralCategory)
        {
            return Channel.UpdateCollateralCategory(collateralCategory);
        }

        public void DeleteCollateralCategory(int collateralCategoryId)
        {
            Channel.DeleteCollateralCategory(collateralCategoryId);
        }

        public CollateralCategory GetCollateralCategory(int collateralCategoryId)
        {
            return Channel.GetCollateralCategory(collateralCategoryId);
        }

        public CollateralCategory[] GetAllCollateralCategorys()
        {
            return Channel.GetAllCollateralCategorys();
        }


        #endregion

        #region CollateralInformation

        public CollateralInformation UpdateCollateralInformation(CollateralInformation collateralInformation)
        {
            return Channel.UpdateCollateralInformation(collateralInformation);
        }

        public void DeleteCollateralInformation(int collateralInformationId)
        {
            Channel.DeleteCollateralInformation(collateralInformationId);
        }

        public CollateralInformation GetCollateralInformation(int collateralInformationId)
        {
            return Channel.GetCollateralInformation(collateralInformationId);
        }

        public CollateralInformationData[] GetAllCollateralInformations()
        {
            return Channel.GetAllCollateralInformations();
        }


        #endregion

        #region CreditRiskRating

        public CreditRiskRating UpdateCreditRiskRating(CreditRiskRating creditRiskRating)
        {
            return Channel.UpdateCreditRiskRating(creditRiskRating);
        }

        public void DeleteCreditRiskRating(int creditRiskRatingId)
        {
            Channel.DeleteCreditRiskRating(creditRiskRatingId);
        }

        public CreditRiskRating GetCreditRiskRating(int creditRiskRatingId)
        {
            return Channel.GetCreditRiskRating(creditRiskRatingId);
        }

        public CreditRiskRating[] GetAllCreditRiskRatings()
        {
            return Channel.GetAllCreditRiskRatings();
        }


        #endregion

        #region ImpairmentOverride

        public ImpairmentOverride UpdateImpairmentOverride(ImpairmentOverride impairmentOverride)
        {
            return Channel.UpdateImpairmentOverride(impairmentOverride);
        }

        public void DeleteImpairmentOverride(int impairmentOverrideId)
        {
            Channel.DeleteImpairmentOverride(impairmentOverrideId);
        }

        public ImpairmentOverride GetImpairmentOverride(int impairmentOverrideId)
        {
            return Channel.GetImpairmentOverride(impairmentOverrideId);
        }

        public ImpairmentOverrideData[] GetAllImpairmentOverrides()
        {
            return Channel.GetAllImpairmentOverrides();
        }


        #endregion

        #region LoanSetup

        public LoanSetup UpdateLoanSetup(LoanSetup loanSetup)
        {
            return Channel.UpdateLoanSetup(loanSetup);
        }

        public void DeleteLoanSetup(int loanSetupId)
        {
            Channel.DeleteLoanSetup(loanSetupId);
        }

        public LoanSetup GetLoanSetup(int loanSetupId)
        {
            return Channel.GetLoanSetup(loanSetupId);
        }

        public LoanSetupData[] GetAllLoanSetups()
        {
            return Channel.GetAllLoanSetups();
        }


        #endregion

        #region ScheduleType

        public ScheduleType UpdateScheduleType(ScheduleType scheduleType)
        {
            return Channel.UpdateScheduleType(scheduleType);
        }

        public void DeleteScheduleType(int scheduleTypeId)
        {
            Channel.DeleteScheduleType(scheduleTypeId);
        }

        public ScheduleType GetScheduleType(int scheduleTypeId)
        {
            return Channel.GetScheduleType(scheduleTypeId);
        }

        public ScheduleType[] GetAllScheduleTypes()
        {
            return Channel.GetAllScheduleTypes();
        }


        #endregion

        #region WatchListedLoan

        public WatchListedLoan UpdateWatchListedLoan(WatchListedLoan watchListedLoan)
        {
            return Channel.UpdateWatchListedLoan(watchListedLoan);
        }

        public void DeleteWatchListedLoan(int watchListedLoanId)
        {
            Channel.DeleteWatchListedLoan(watchListedLoanId);
        }

        public WatchListedLoan GetWatchListedLoan(int watchListedLoanId)
        {
            return Channel.GetWatchListedLoan(watchListedLoanId);
        }

        public WatchListedLoan[] GetAllWatchListedLoans()
        {
            return Channel.GetAllWatchListedLoans();
        }


        #endregion

        #region IndividualImpairment

        public IndividualImpairment UpdateIndividualImpairment(IndividualImpairment individualImpairment)
        {
            return Channel.UpdateIndividualImpairment(individualImpairment);
        }

        public void DeleteIndividualImpairment(int individualImpairmentId)
        {
            Channel.DeleteIndividualImpairment(individualImpairmentId);
        }

        public IndividualImpairment GetIndividualImpairment(int individualImpairmentId)
        {
            return Channel.GetIndividualImpairment(individualImpairmentId);
        }

        public IndividualImpairmentData[] GetAlllndividualImpairments()
        {
            return Channel.GetAlllndividualImpairments();
        }

        public string[] GetAvailableReferenceNumbers()
        {
            return Channel.GetAvailableReferenceNumbers();
        }

         public IndividualImpairmentData[] GetIndividualImpairments(string refno)
        {
            return Channel.GetIndividualImpairments(refno);
        }

        #endregion

        #region IndividualSchedule

        public IndividualSchedule UpdateIndividualSchedule(IndividualSchedule individualSchedule)
        {
            return Channel.UpdateIndividualSchedule(individualSchedule);
        }

        public void DeleteIndividualSchedule(int individualScheduleId)
        {
            Channel.DeleteIndividualSchedule(individualScheduleId);
        }

        public IndividualSchedule GetIndividualSchedule(int individualScheduleId)
        {
            return Channel.GetIndividualSchedule(individualScheduleId);
        }

        public IndividualScheduleData[] GetAllIndividualSchedules()
        {
            return Channel.GetAllIndividualSchedules();
        }

        public string[] GetDistinctRefNo()
        {
            return Channel.GetDistinctRefNo();
        }


        public IndividualScheduleData[] GetIndividualSchedulebyRefNo(string refno)
        {
            return Channel.GetIndividualSchedulebyRefNo(refno);
        }
        #endregion


    }
}
