using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(ICoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoreClient : UserClientBase<ICoreService>, ICoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region BudgetingLevel

        public BudgetingLevel UpdateBudgetingLevel(BudgetingLevel budgetingLevel)
        {
            return Channel.UpdateBudgetingLevel(budgetingLevel);
        }

        public void DeleteBudgetingLevel(int budgetingLevelId)
        {
           Channel.DeleteBudgetingLevel(budgetingLevelId);
        }

        public BudgetingLevel GetBudgetingLevel(int budgetingLevelId)
        {
            return Channel.GetBudgetingLevel(budgetingLevelId);
        }

        public BudgetingLevelData[] GetBudgetingLevels(string year, string reviewCode)
        {
            return Channel.GetBudgetingLevels(year, reviewCode);
        }

        #endregion

        #region Currency

        public Currency UpdateCurrency(Currency currency)
        {
            return Channel.UpdateCurrency(currency);
        }

        public void DeleteCurrency(int currencyId)
        {
            Channel.DeleteCurrency(currencyId);
        }

        public Currency GetCurrency(int currencyId)
        {
            return Channel.GetCurrency(currencyId);
        }

        public Currency[] GetAllCurrency()
        {
            return Channel.GetAllCurrency();
        }

        #endregion

        #region CurrencyRate

        public CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate)
        {
            return Channel.UpdateCurrencyRate(currencyRate);
        }

        public void DeleteCurrencyRate(int currencyRateId)
        {
            Channel.DeleteCurrencyRate(currencyRateId);
        }

        public CurrencyRate GetCurrencyRate(int currencyRateId)
        {
            return Channel.GetCurrencyRate(currencyRateId);
        }

        public CurrencyRate GetCurrencyRateByCode(string year, string reviewCode, RateTypeEnum rateType, string currencyCode)
        {
            return Channel.GetCurrencyRateByCode(year, reviewCode, rateType, currencyCode);
        }

        public CurrencyRate[] GetCurrencyRates(string year, string reviewCode, RateTypeEnum rateType)
        {
            return Channel.GetCurrencyRates(year, reviewCode, rateType);
        }

        #endregion

        #region ModificationLevel

        public ModificationLevel UpdateModificationLevel(ModificationLevel modificationLevel)
        {
            return Channel.UpdateModificationLevel(modificationLevel);
        }

        public void DeleteModificationLevel(int modificationLevelId)
        {
            Channel.DeleteModificationLevel(modificationLevelId);
        }

        public ModificationLevel GetModificationLevel(int modificationLevelId)
        {
            return Channel.GetModificationLevel(modificationLevelId);
        }

        public ModificationLevelData[] GetAllModificationLevel()
        {
            return Channel.GetAllModificationLevel();
        }

        #endregion

        #region Module

        public Module UpdateModule(Module module)
        {
            return Channel.UpdateModule(module);
        }

        public void DeleteModule(int moduleId)
        {
            Channel.DeleteModule(moduleId);
        }

        public Module GetModule(int moduleId)
        {
            return Channel.GetModule(moduleId);
        }

        public Module[] GetAllModule()
        {
            return Channel.GetAllModule();
        }

        #endregion

        #region PrimaryLock

        public PrimaryLock UpdatePrimaryLock(PrimaryLock primaryLock)
        {
            return Channel.UpdatePrimaryLock(primaryLock);
        }

        public void DeletePrimaryLock(int primaryLockId)
        {
            Channel.DeletePrimaryLock(primaryLockId);
        }

        public PrimaryLock GetPrimaryLock(int primaryLockId)
        {
            return Channel.GetPrimaryLock(primaryLockId);
        }

        public PrimaryLockData[] GetAllPrimaryLock()
        {
            return Channel.GetAllPrimaryLock();
        }

        #endregion

        #region SecondaryLock

        public SecondaryLock UpdateSecondaryLock(SecondaryLock secondaryLock)
        {
            return Channel.UpdateSecondaryLock(secondaryLock);
        }

        public void DeleteSecondaryLock(int secondaryLockId)
        {
            Channel.DeleteSecondaryLock(secondaryLockId);
        }

        public SecondaryLock GetSecondaryLock(int secondaryLockId)
        {
            return Channel.GetSecondaryLock(secondaryLockId);
        }

        public SecondaryLockData[] GetSecondaryLocks(string year, string reviewCode)
        {
            return Channel.GetSecondaryLocks(year, reviewCode);
        }

        #endregion

        #region SecondaryLockLevel

        public SecondaryLockLevel UpdateSecondaryLockLevel(SecondaryLockLevel secondaryLockLevel)
        {
            return Channel.UpdateSecondaryLockLevel(secondaryLockLevel);
        }

        public void DeleteSecondaryLockLevel(int secondaryLockLevelId)
        {
            Channel.DeleteSecondaryLockLevel(secondaryLockLevelId);
        }

        public SecondaryLockLevel GetSecondaryLockLevel(int secondaryLockLevelId)
        {
            return Channel.GetSecondaryLockLevel(secondaryLockLevelId);
        }

        public SecondaryLockLevelData[] GetSecondaryLockLevels()
        {
            return Channel.GetSecondaryLockLevels();
        }

        #endregion

        #region GeneralSetting

        public GeneralSetting UpdateGeneralSetting(GeneralSetting generalSetting)
        {
            return Channel.UpdateGeneralSetting(generalSetting);
        }

        public void DeleteGeneralSetting(int generalSettingId)
        {
            Channel.DeleteGeneralSetting(generalSettingId);
        }

        public GeneralSetting GetGeneralSetting(int generalSettingId)
        {
            return Channel.GetGeneralSetting(generalSettingId);
        }

        public GeneralSetting GetFirstGeneralSetting()
        {
            return Channel.GetFirstGeneralSetting();
        }

        #endregion

        #region Policy

        public Policy UpdatePolicy(Policy policy)
        {
            return Channel.UpdatePolicy(policy);
        }

        public void DeletePolicy(int policyId)
        {
            Channel.DeletePolicy(policyId);
        }

        public Policy GetPolicy(int policyId)
        {
            return Channel.GetPolicy(policyId);
        }

        public PolicyData[] GetAllPolicy()
        {
            return Channel.GetAllPolicy();
        }

        #endregion

        #region PolicyLevel

        public PolicyLevel UpdatePolicyLevel(PolicyLevel policyLevel)
        {
            return Channel.UpdatePolicyLevel(policyLevel);
        }

        public void DeletePolicyLevel(int policyLevelId)
        {
            Channel.DeletePolicyLevel(policyLevelId);
        }

        public PolicyLevel GetPolicyLevel(int policyLevelId)
        {
            return Channel.GetPolicyLevel(policyLevelId);
        }

        public PolicyLevelData[] GetPolicyLevels(string year, string reviewCode)
        {
            return Channel.GetPolicyLevels(year, reviewCode);
        }

        #endregion

        #region Operation

        public Operation UpdateOperation(Operation operation)
        {
            return Channel.UpdateOperation(operation);
        }

        public void DeleteOperation(int operationId)
        {
            Channel.DeleteOperation(operationId);
        }

        public Operation GetOperation(int operationId)
        {
            return Channel.GetOperation(operationId);
        }

        public Operation[] GetAllOperations()
        {
            return Channel.GetAllOperations();
        }

        public Operation GetOperationByCode(string code)
        {
            return Channel.GetOperationByCode(code);
        }


        #endregion

        #region OperationReview

        public OperationReview UpdateOperationReview(OperationReview operationReview)
        {
            return Channel.UpdateOperationReview(operationReview);
        }

        public void DeleteOperationReview(int operationReviewId)
        {
            Channel.DeleteOperationReview(operationReviewId);
        }

        public OperationReview GetOperationReview(int operationReviewId)
        {
            return Channel.GetOperationReview(operationReviewId);
        }

        public OperationReview[] GetOperationReviews(string operationCode)
        {
            return Channel.GetOperationReviews(operationCode);
        }

        //public OperationReview[] GetAllOperationReviews()
        //{
        //    return Channel.GetAllOperationReviews();
        //}


        #endregion

        

    }
}
