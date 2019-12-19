using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Proxies
{
    [Export(typeof(IMPROPEXService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPROPEXClient : UserClientBase<IMPROPEXService>, IMPROPEXService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region ActivityBase

        public ActivityBase UpdateActivityBase(ActivityBase activityBase)
        {
            return Channel.UpdateActivityBase(activityBase);
        }

        public void DeleteActivityBase(int activityBaseId)
        {
            Channel.DeleteActivityBase(activityBaseId);
        }

        public ActivityBase GetActivityBase(int activityBaseId)
        {
            return Channel.GetActivityBase(activityBaseId);
        }

        public ActivityBase[] GetAllActivityBases()
        {
            return Channel.GetAllActivityBases();
        }

        #endregion

        #region ActivityBaseRatio

        public ActivityBaseRatio UpdateActivityBaseRatio(ActivityBaseRatio activityBaseRatio)
        {
            return Channel.UpdateActivityBaseRatio(activityBaseRatio);
        }

        public void DeleteActivityBaseRatio(int activityBaseRatioId)
        {
            Channel.DeleteActivityBaseRatio(activityBaseRatioId);
        }

        public ActivityBaseRatio GetActivityBaseRatio(int activityBaseRatioId)
        {
            return Channel.GetActivityBaseRatio(activityBaseRatioId);
        }

        public ActivityBaseRatio[] GetAllActivityBaseRatios()
        {
            return Channel.GetAllActivityBaseRatios();
        }

        #endregion

        #region CostCentre

        public CostCentre UpdateCostCentre(CostCentre costCentre)
        {
            return Channel.UpdateCostCentre(costCentre);
        }

        public void DeleteCostCentre(int costCentreId)
        {
            Channel.DeleteCostCentre(costCentreId);
        }

        public CostCentre GetCostCentre(int costCentreId)
        {
            return Channel.GetCostCentre(costCentreId);
        }

        public CostCentreData[] GetAllCostCentres()
        {
            return Channel.GetAllCostCentres();
        }

        public CostCentre[] GetParentCostCentres(string definitionCode)
        {
            return Channel.GetParentCostCentres(definitionCode);
        }

        public CostCentre[] GetCostCentreByLevel(int level)
        {
            return Channel.GetCostCentreByLevel(level);
        }

        public CostCentre[] GetCostCentreByDefinition(string definitionCode)
        {
            return Channel.GetCostCentreByDefinition(definitionCode);
        }


        #endregion

        #region CostCentreDefinition

        public CostCentreDefinition UpdateCostCentreDefinition(CostCentreDefinition costCentreDefinition)
        {
            return Channel.UpdateCostCentreDefinition(costCentreDefinition);
        }

        public void DeleteCostCentreDefinition(int ccDefinitionId)
        {
            Channel.DeleteCostCentreDefinition(ccDefinitionId);
        }

        public CostCentreDefinition GetCostCentreDefinition(int ccDefinitionId)
        {
            return Channel.GetCostCentreDefinition(ccDefinitionId);
        }

        public CostCentreDefinition[] GetAllCostCentreDefinitions()
        {
            return Channel.GetAllCostCentreDefinitions();
        }

      
        #endregion

        #region ExpenseBasis

        public ExpenseBasis UpdateExpenseBasis(ExpenseBasis expenseBasis)
        {
            return Channel.UpdateExpenseBasis(expenseBasis);
        }

        public void DeleteExpenseBasis(int expenseBasisId)
        {
            Channel.DeleteExpenseBasis(expenseBasisId);
        }

        public ExpenseBasis GetExpenseBasis(int expenseBasisId)
        {
            return Channel.GetExpenseBasis(expenseBasisId);
        }

        public ExpenseBasis[] GetAllExpenseBasisInfo()
        {
            return Channel.GetAllExpenseBasisInfo();
        }

        #endregion

        #region ExpenseMapping

        public ExpenseMapping UpdateExpenseMapping(ExpenseMapping expenseMapping)
        {
            return Channel.UpdateExpenseMapping(expenseMapping);
        }

        public void DeleteExpenseMapping(int expenseMappingId)
        {
            Channel.DeleteExpenseMapping(expenseMappingId);
        }

        public ExpenseMapping GetExpenseMapping(int expenseMappingId)
        {
            return Channel.GetExpenseMapping(expenseMappingId);
        }

        public ExpenseMappingData[] GetAllExpenseMappings()
        {
            return Channel.GetAllExpenseMappings();
        }



        #endregion

        #region ExpenseGLMapping

        public ExpenseGLMapping UpdateExpenseGLMapping(ExpenseGLMapping expenseGLMapping)
        {
            return Channel.UpdateExpenseGLMapping(expenseGLMapping);
        }

        public void DeleteExpenseGLMapping(int expenseGLId)
        {
            Channel.DeleteExpenseGLMapping(expenseGLId);
        }

        public ExpenseGLMapping GetExpenseGLMapping(int expenseGLId)
        {
            return Channel.GetExpenseGLMapping(expenseGLId);
        }

        public ExpenseGLMappingData[] GetAllExpenseGLMappings()
        {
            return Channel.GetAllExpenseGLMappings();
        }

   

        #endregion

        #region ExpenseProductMapping

        public ExpenseProductMapping UpdateExpenseProductMapping(ExpenseProductMapping expenseProductMapping)
        {
            return Channel.UpdateExpenseProductMapping(expenseProductMapping);
        }

        public void DeleteExpenseProductMapping(int expenseProductId)
        {
            Channel.DeleteExpenseProductMapping(expenseProductId);
        }

        public ExpenseProductMapping GetExpenseProductMapping(int expenseProductId)
        {
            return Channel.GetExpenseProductMapping(expenseProductId);
        }

        public ExpenseProductMappingData[] GetAllExpenseProductMappings()
        {
            return Channel.GetAllExpenseProductMappings();
        }



        #endregion

        #region ExpenseRawBasis

        public ExpenseRawBasis UpdateExpenseRawBasis(ExpenseRawBasis expenseRawBasis)
        {
            return Channel.UpdateExpenseRawBasis(expenseRawBasis);
        }

        public void DeleteExpenseRawBasis(int expenseRawBasisId)
        {
            Channel.DeleteExpenseRawBasis(expenseRawBasisId);
        }

        public ExpenseRawBasis GetExpenseRawBasis(int expenseRawBasisId)
        {
            return Channel.GetExpenseRawBasis(expenseRawBasisId);
        }

        public ExpenseRawBasisData[] GetAllExpenseRawBasisInfo()
        {
            return Channel.GetAllExpenseRawBasisInfo();
        }


        #endregion

        #region OpexBusinessRule

        public OpexBusinessRule UpdateOpexBusinessRule(OpexBusinessRule opexBusinessRule)
        {
            return Channel.UpdateOpexBusinessRule(opexBusinessRule);
        }

        public void DeleteOpexBusinessRule(int opexBusinessRuleId)
        {
            Channel.DeleteOpexBusinessRule(opexBusinessRuleId);
        }

        public OpexBusinessRule GetOpexBusinessRule(int opexBusinessRuleId)
        {
            return Channel.GetOpexBusinessRule(opexBusinessRuleId);
        }

        public OpexBusinessRule[] GetAllOpexBusinessRules()
        {
            return Channel.GetAllOpexBusinessRules();
        }


        #endregion

        #region OpexManagementTree

        public OpexManagementTree UpdateOpexManagementTree(OpexManagementTree opexManagementTree)
        {
            return Channel.UpdateOpexManagementTree(opexManagementTree);
        }

        public void DeleteOpexManagementTree(int opexMgtTreeId)
        {
            Channel.DeleteOpexManagementTree(opexMgtTreeId);
        }

        public OpexManagementTree GetOpexManagementTree(int opexMgtTreeId)
        {
            return Channel.GetOpexManagementTree(opexMgtTreeId);
        }

        public OpexManagementTreeData[] GetAllOpexManagementTrees()
        {
            return Channel.GetAllOpexManagementTrees();
        }


        #endregion

        #region OpexMISReplacement

        public OpexMISReplacement UpdateOpexMISReplacement(OpexMISReplacement opexMISReplacement)
        {
            return Channel.UpdateOpexMISReplacement(opexMISReplacement);
        }

        public void DeleteOpexMISReplacement(int opexMISReplacementId)
        {
            Channel.DeleteOpexMISReplacement(opexMISReplacementId);
        }

        public OpexMISReplacement GetOpexMISReplacement(int opexMISReplacementId)
        {
            return Channel.GetOpexMISReplacement(opexMISReplacementId);
        }

        public OpexMISReplacementData[] GetAllOpexMISReplacements()
        {
            return Channel.GetAllOpexMISReplacements();
        }



        #endregion

        #region StaffCost

        public StaffCost UpdateStaffCost(StaffCost staffCost)
        {
            return Channel.UpdateStaffCost(staffCost);
        }

        public void DeleteStaffCost(int staffCostId)
        {
            Channel.DeleteStaffCost(staffCostId);
        }

        public StaffCost GetStaffCost(int staffCostId)
        {
            return Channel.GetStaffCost(staffCostId);
        }

        public StaffCostData[] GetAllStaffCosts()
        {
            return Channel.GetAllStaffCosts();
        }



        #endregion

        #region OpexAbcExemption

        public OpexAbcExemption UpdateOpexAbcExemption(OpexAbcExemption opexAbcExemption)
        {
            return Channel.UpdateOpexAbcExemption(opexAbcExemption);
        }

        public void DeleteOpexAbcExemption(int opexAbcExemptionId)
        {
            Channel.DeleteOpexAbcExemption(opexAbcExemptionId);
        }

        public OpexAbcExemption GetOpexAbcExemption(int opexAbcExemptionId)
        {
            return Channel.GetOpexAbcExemption(opexAbcExemptionId);
        }

        public OpexAbcExemptionData[] GetAllOpexAbcExemptions()
        {
            return Channel.GetAllOpexAbcExemptions();
        }



        #endregion

        #region OpexRawExpense

        public OpexRawExpense UpdateOpexRawExpense(OpexRawExpense opexRawExpense)
        {
            return Channel.UpdateOpexRawExpense(opexRawExpense);
        }

        public void DeleteOpexRawExpense(int opexRawExpenseId)
        {
            Channel.DeleteOpexRawExpense(opexRawExpenseId);
        }

        public OpexRawExpense GetOpexRawExpense(int opexRawExpenseId)
        {
            return Channel.GetOpexRawExpense(opexRawExpenseId);
        }

        public OpexRawExpense[] GetAllOpexRawExpenses()
        {
            return Channel.GetAllOpexRawExpenses();
        }

        #endregion

        #region OpexGLMapping

        public OpexGLMapping UpdateOpexGLMapping(OpexGLMapping opexGLMapping)
        {
            return Channel.UpdateOpexGLMapping(opexGLMapping);
        }

        public void DeleteOpexGLMapping(int opexGLMappingId)
        {
            Channel.DeleteOpexGLMapping(opexGLMappingId);
        }

        public OpexGLMapping GetOpexGLMapping(int opexGLMappingId)
        {
            return Channel.GetOpexGLMapping(opexGLMappingId);
        }

        public OpexGLMapping[] GetAllOpexGLMappings()
        {
            return Channel.GetAllOpexGLMappings();
        }

        public KeyValueData[] GetUnMappedOpexGLs()
        {
            return Channel.GetUnMappedOpexGLs();
        }

        #endregion

        #region OpexReport

        public OpexReport UpdateOpexReport(OpexReport opexReport)
        {
            return Channel.UpdateOpexReport(opexReport);
        }

        public void DeleteOpexReport(int opexReportId)
        {
            Channel.DeleteOpexReport(opexReportId);
        }

        public OpexReport GetOpexReport(int opexReportId)
        {
            return Channel.GetOpexReport(opexReportId);
        }

        public OpexReportData[] GetAllOpexReports()
        {
            return Channel.GetAllOpexReports();
        }



        #endregion

        #region OpexGLBasis

        public OpexGLBasis UpdateOpexGLBasis(OpexGLBasis opexGLBasis)
        {
            return Channel.UpdateOpexGLBasis(opexGLBasis);
        }

        public void DeleteOpexGLBasis(int opexGLBasisId)
        {
            Channel.DeleteOpexGLBasis(opexGLBasisId);
        }

        public OpexGLBasis GetOpexGLBasis(int opexGLBasisId)
        {
            return Channel.GetOpexGLBasis(opexGLBasisId);
        }

        public OpexGLBasis[] GetAllOpexGLBasiss()
        {
            return Channel.GetAllOpexGLBasiss();
        }


        #endregion

        #region OpexCheckList

        public OpexCheckList UpdateOpexCheckList(OpexCheckList opexCheckList)
        {
            return Channel.UpdateOpexCheckList(opexCheckList);
        }

        public void DeleteOpexCheckList(int opexCheckListId)
        {
            Channel.DeleteOpexCheckList(opexCheckListId);
        }

        public OpexCheckList GetOpexCheckList(int opexCheckListId)
        {
            return Channel.GetOpexCheckList(opexCheckListId);
        }

        public OpexCheckList[] GetAllOpexCheckLists()
        {
            return Channel.GetAllOpexCheckLists();
        }


        #endregion

        #region OpexBasisMapping

        public OpexBasisMapping UpdateOpexBasisMapping(OpexBasisMapping opexBasisMapping)
        {
            return Channel.UpdateOpexBasisMapping(opexBasisMapping);
        }

        public void DeleteOpexBasisMapping(int opexBasisMappingId)
        {
            Channel.DeleteOpexBasisMapping(opexBasisMappingId);
        }

        public OpexBasisMapping GetOpexBasisMapping(int opexBasisMappingId)
        {
            return Channel.GetOpexBasisMapping(opexBasisMappingId);
        }

        public OpexBasisMapping[] GetAllOpexBasisMappings()
        {
            return Channel.GetAllOpexBasisMappings();
        }


        #endregion

        #region CheckList

        public CheckList UpdateCheckList(CheckList checkList)
        {
            return Channel.UpdateCheckList(checkList);
        }

        public void DeleteCheckList(int checkListId)
        {
            Channel.DeleteCheckList(checkListId);
        }

        public CheckList GetCheckList(int checkListId)
        {
            return Channel.GetCheckList(checkListId);
        }

        public CheckList[] GetAllCheckLists()
        {
            return Channel.GetAllCheckLists();
        }

        public CheckListData[] RunCheckList()
        {
            return Channel.RunCheckList();
        }

        


        #endregion

        #region HoExemptionMISCode

        public HoExemptionMISCode UpdateHoExemptionMISCode(HoExemptionMISCode hoExemptionMISCode)
        {
            return Channel.UpdateHoExemptionMISCode(hoExemptionMISCode);
        }

        public void DeleteHoExemptionMISCode(int id)
        {
            Channel.DeleteHoExemptionMISCode(id);
        }

        public HoExemptionMISCode GetHoExemptionMISCode(int id)
        {
            return Channel.GetHoExemptionMISCode(id);
        }

        public HoExemptionMISCode[] GetAllHoExemptionMISCodes()
        {
            return Channel.GetAllHoExemptionMISCodes();
        }




        #endregion

        #region FixedAssetSharingRatio

        public FixedAssetSharingRatio UpdateFixedAssetSharingRatio(FixedAssetSharingRatio fixedAssetSharingRatio)
        {
            return Channel.UpdateFixedAssetSharingRatio(fixedAssetSharingRatio);
        }

        public void DeleteFixedAssetSharingRatio(int fixedAssetSharingRatioId)
        {
            Channel.DeleteFixedAssetSharingRatio(fixedAssetSharingRatioId);
        }

        public FixedAssetSharingRatio GetFixedAssetSharingRatio(int fixedAssetSharingRatioId)
        {
            return Channel.GetFixedAssetSharingRatio(fixedAssetSharingRatioId);
        }

        public FixedAssetSharingRatio[] GetAllFixedAssetSharingRatios()
        {
            return Channel.GetAllFixedAssetSharingRatios();
        }

        #endregion

        #region IncomeCashCentreCode

        public IncomeCashCentreCode UpdateIncomeCashCentreCode(IncomeCashCentreCode incomeCashCentreCode)
        {
            return Channel.UpdateIncomeCashCentreCode(incomeCashCentreCode);
        }

        public void DeleteIncomeCashCentreCode(int incomeCashCentreCodeId)
        {
            Channel.DeleteIncomeCashCentreCode(incomeCashCentreCodeId);
        }

        public IncomeCashCentreCode GetIncomeCashCentreCode(int incomeCashCentreCodeId)
        {
            return Channel.GetIncomeCashCentreCode(incomeCashCentreCodeId);
        }

        public IncomeCashCentreCode[] GetAllIncomeCashCentreCodes()
        {
            return Channel.GetAllIncomeCashCentreCodes();
        }

        #endregion

        #region IncomeCentralVaultAccounts

        public IncomeCentralVaultAccounts UpdateIncomeCentralVaultAccounts(IncomeCentralVaultAccounts incomeCentralVaultAccounts)
        {
            return Channel.UpdateIncomeCentralVaultAccounts(incomeCentralVaultAccounts);
        }

        public void DeleteIncomeCentralVaultAccounts(int incomeCentralVaultAccountsId)
        {
            Channel.DeleteIncomeCentralVaultAccounts(incomeCentralVaultAccountsId);
        }

        public IncomeCentralVaultAccounts GetIncomeCentralVaultAccounts(int incomeCentralVaultAccountsId)
        {
            return Channel.GetIncomeCentralVaultAccounts(incomeCentralVaultAccountsId);
        }

        public IncomeCentralVaultAccounts[] GetAllIncomeCentralVaultAccounts()
        {
            return Channel.GetAllIncomeCentralVaultAccounts();
        }

        #endregion

        #region IncomeNEAGLSBU

        public IncomeNEAGLSBU UpdateIncomeNEAGLSBU(IncomeNEAGLSBU incomeNEAGLSBU)
        {
            return Channel.UpdateIncomeNEAGLSBU(incomeNEAGLSBU);
        }

        public void DeleteIncomeNEAGLSBU(int incomeNEAGLSBUId)
        {
            Channel.DeleteIncomeNEAGLSBU(incomeNEAGLSBUId);
        }

        public IncomeNEAGLSBU GetIncomeNEAGLSBU(int incomeNEAGLSBUId)
        {
            return Channel.GetIncomeNEAGLSBU(incomeNEAGLSBUId);
        }

        public IncomeNEAGLSBU[] GetAllIncomeNEAGLSBUs()
        {
            return Channel.GetAllIncomeNEAGLSBUs();
        }

        #endregion

        #region CategoryTransferPrice

        public CategoryTransferPrice UpdateCategoryTransferPrice(CategoryTransferPrice categoryTransferPrice)
        {
            return Channel.UpdateCategoryTransferPrice(categoryTransferPrice);
        }

        public void DeleteCategoryTransferPrice(int categoryTransferPriceId)
        {
            Channel.DeleteCategoryTransferPrice(categoryTransferPriceId);
        }

        public CategoryTransferPrice GetCategoryTransferPrice(int categoryTransferPriceId)
        {
            return Channel.GetCategoryTransferPrice(categoryTransferPriceId);
        }

        public CategoryTransferPrice[] GetAllCategoryTransferPrices()
        {
            return Channel.GetAllCategoryTransferPrices();
        }

        #endregion

        #region LowCostRemap

        public LowCostRemap UpdateLowCostRemap(LowCostRemap lowCostRemap)
        {
            return Channel.UpdateLowCostRemap(lowCostRemap);
        }

        public void DeleteLowCostRemap(int lowCostRemapId)
        {
            Channel.DeleteLowCostRemap(lowCostRemapId);
        }

        public LowCostRemap GetLowCostRemap(int lowCostRemapId)
        {
            return Channel.GetLowCostRemap(lowCostRemapId);
        }

        public LowCostRemap[] GetAllLowCostRemaps()
        {
            return Channel.GetAllLowCostRemaps();
        }

        #endregion

        #region NEABranchSBUShares

        public NEABranchSBUShares UpdateNEABranchSBUShares(NEABranchSBUShares nEABranchSBUShares)
        {
            return Channel.UpdateNEABranchSBUShares(nEABranchSBUShares);
        }

        public void DeleteNEABranchSBUShares(int nEABranchSBUSharesId)
        {
            Channel.DeleteNEABranchSBUShares(nEABranchSBUSharesId);
        }

        public NEABranchSBUShares GetNEABranchSBUShares(int nEABranchSBUSharesId)
        {
            return Channel.GetNEABranchSBUShares(nEABranchSBUSharesId);
        }

        public NEABranchSBUShares[] GetAllNEABranchSBUShares()
        {
            return Channel.GetAllNEABranchSBUShares();
        }

        #endregion

        #region NEABranchSharingRatio

        public NEABranchSharingRatio UpdateNEABranchSharingRatio(NEABranchSharingRatio nEABranchSharingRatio)
        {
            return Channel.UpdateNEABranchSharingRatio(nEABranchSharingRatio);
        }

        public void DeleteNEABranchSharingRatio(int nEABranchSharingRatioId)
        {
            Channel.DeleteNEABranchSharingRatio(nEABranchSharingRatioId);
        }

        public NEABranchSharingRatio GetNEABranchSharingRatio(int nEABranchSharingRatioId)
        {
            return Channel.GetNEABranchSharingRatio(nEABranchSharingRatioId);
        }

        public NEABranchSharingRatio[] GetAllNEABranchSharingRatios()
        {
            return Channel.GetAllNEABranchSharingRatios();
        }

        #endregion

        #region NEASharingRatio

        public NEASharingRatio UpdateNEASharingRatio(NEASharingRatio nEASharingRatio)
        {
            return Channel.UpdateNEASharingRatio(nEASharingRatio);
        }

        public void DeleteNEASharingRatio(int nEASharingRatioId)
        {
            Channel.DeleteNEASharingRatio(nEASharingRatioId);
        }

        public NEASharingRatio GetNEASharingRatio(int nEASharingRatioId)
        {
            return Channel.GetNEASharingRatio(nEASharingRatioId);
        }

        public NEASharingRatio[] GetAllNEASharingRatios()
        {
            return Channel.GetAllNEASharingRatios();
        }

        #endregion

        #region NEASharingRatioFcy

        public NEASharingRatioFcy UpdateNEASharingRatioFcy(NEASharingRatioFcy nEASharingRatioFcy)
        {
            return Channel.UpdateNEASharingRatioFcy(nEASharingRatioFcy);
        }

        public void DeleteNEASharingRatioFcy(int nEASharingRatioFcyId)
        {
            Channel.DeleteNEASharingRatioFcy(nEASharingRatioFcyId);
        }

        public NEASharingRatioFcy GetNEASharingRatioFcy(int nEASharingRatioFcyId)
        {
            return Channel.GetNEASharingRatioFcy(nEASharingRatioFcyId);
        }

        public NEASharingRatioFcy[] GetAllNEASharingRatioFcys()
        {
            return Channel.GetAllNEASharingRatioFcys();
        }

        #endregion

        #region OpexBranchMapping

        public OpexBranchMapping UpdateOpexBranchMapping(OpexBranchMapping opexBranchMapping)
        {
            return Channel.UpdateOpexBranchMapping(opexBranchMapping);
        }

        public void DeleteOpexBranchMapping(int opexBranchMappingId)
        {
            Channel.DeleteOpexBranchMapping(opexBranchMappingId);
        }

        public OpexBranchMapping GetOpexBranchMapping(int opexBranchMappingId)
        {
            return Channel.GetOpexBranchMapping(opexBranchMappingId);
        }

        public OpexBranchMapping[] GetAllOpexBranchMapping()
        {
            return Channel.GetAllOpexBranchMapping();
        }

        #endregion

    }
}
