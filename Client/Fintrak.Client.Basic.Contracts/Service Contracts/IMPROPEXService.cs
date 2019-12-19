using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Client.Basic.Contracts
{
    [ServiceContract]
    public interface IMPROPEXService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
       void RegisterModule();

        #region ActivityBase

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBase UpdateActivityBase(ActivityBase activityBase);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteActivityBase(int activityBaseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBase GetActivityBase(int activityBaseId);

        [OperationContract]
        ActivityBase[] GetAllActivityBases();

      
        #endregion

        #region ActivityBaseRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBaseRatio UpdateActivityBaseRatio(ActivityBaseRatio activityBaseRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteActivityBaseRatio(int activityBaseRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ActivityBaseRatio GetActivityBaseRatio(int activityBaseRatioId);

        [OperationContract]
        ActivityBaseRatio[] GetAllActivityBaseRatios();

        #endregion

        #region CostCentre

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CostCentre UpdateCostCentre(CostCentre costCentre);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCostCentre(int costCentreId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CostCentre GetCostCentre(int costCentreId);

        [OperationContract]
        CostCentreData[] GetAllCostCentres();

        [OperationContract]
        CostCentre[] GetParentCostCentres(string definitionCode);

        [OperationContract]
        CostCentre[] GetCostCentreByLevel(int level);

        [OperationContract]
        CostCentre[] GetCostCentreByDefinition(string definitionCode);

        #endregion

        #region CostCentreDefinition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CostCentreDefinition UpdateCostCentreDefinition(CostCentreDefinition costCentreDefinition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCostCentreDefinition(int ccDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CostCentreDefinition GetCostCentreDefinition(int ccDefinitionId);

        [OperationContract]
        CostCentreDefinition[] GetAllCostCentreDefinitions();

        #endregion

        #region ExpenseBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseBasis UpdateExpenseBasis(ExpenseBasis expenseBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseBasis(int expenseBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseBasis GetExpenseBasis(int expenseBasisId);

        [OperationContract]
        ExpenseBasis[] GetAllExpenseBasisInfo();


        #endregion

        #region ExpenseMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseMapping UpdateExpenseMapping(ExpenseMapping expenseMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseMapping(int expenseMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseMapping GetExpenseMapping(int expenseMappingId);

        [OperationContract]
        ExpenseMappingData[] GetAllExpenseMappings();

        #endregion

        #region ExpenseGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseGLMapping UpdateExpenseGLMapping(ExpenseGLMapping expenseGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseGLMapping(int expenseGLId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseGLMapping GetExpenseGLMapping(int expenseGLId);

        [OperationContract]
        ExpenseGLMappingData[] GetAllExpenseGLMappings();

        #endregion

        #region ExpenseProductMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseProductMapping UpdateExpenseProductMapping(ExpenseProductMapping expenseProductMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseProductMapping(int expenseProductId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseProductMapping GetExpenseProductMapping(int expenseProductId);

        [OperationContract]
        ExpenseProductMappingData[] GetAllExpenseProductMappings();

        #endregion

        #region ExpenseRawBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExpenseRawBasis UpdateExpenseRawBasis(ExpenseRawBasis expenseRawBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExpenseRawBasis(int expenseRawBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExpenseRawBasis GetExpenseRawBasis(int expenseRawBasisId);

        [OperationContract]
        ExpenseRawBasisData[] GetAllExpenseRawBasisInfo();

        #endregion

        #region OpexBusinessRule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexBusinessRule UpdateOpexBusinessRule(OpexBusinessRule opexBusinessRule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexBusinessRule(int opexBusinessRuleId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBusinessRule GetOpexBusinessRule(int opexBusinessRuleId);

        [OperationContract]
        OpexBusinessRule[] GetAllOpexBusinessRules();

        #endregion

        #region OpexManagementTree

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexManagementTree UpdateOpexManagementTree(OpexManagementTree opexManagementTree);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexManagementTree(int opexMgtTreeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexManagementTree GetOpexManagementTree(int opexMgtTreeId);

        [OperationContract]
        OpexManagementTreeData[] GetAllOpexManagementTrees();


        #endregion

        #region OpexMISReplacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexMISReplacement UpdateOpexMISReplacement(OpexMISReplacement opexMISReplacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexMISReplacement(int opexMISReplacementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexMISReplacement GetOpexMISReplacement(int opexMISReplacementId);

        [OperationContract]
        OpexMISReplacementData[] GetAllOpexMISReplacements();

        #endregion

        #region StaffCost

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        StaffCost UpdateStaffCost(StaffCost staffCost);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffCost(int staffCostId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        StaffCost GetStaffCost(int staffCostId);

        [OperationContract]
        StaffCostData[] GetAllStaffCosts();

        #endregion

        #region OpexAbcExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexAbcExemption UpdateOpexAbcExemption(OpexAbcExemption opexAbcExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexAbcExemption(int opexAbcExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexAbcExemption GetOpexAbcExemption(int opexAbcExemptionId);

        [OperationContract]
        OpexAbcExemptionData[] GetAllOpexAbcExemptions();

        #endregion

        #region OpexRawExpense

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexRawExpense UpdateOpexRawExpense(OpexRawExpense opexRawExpense);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexRawExpense(int opexRawExpenseId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexRawExpense GetOpexRawExpense(int opexRawExpenseId);

        [OperationContract]
        OpexRawExpense[] GetAllOpexRawExpenses();


        #endregion

        #region OpexGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexGLMapping UpdateOpexGLMapping(OpexGLMapping opexGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexGLMapping(int opexGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexGLMapping GetOpexGLMapping(int opexGLMappingId);

        [OperationContract]
        OpexGLMapping[] GetAllOpexGLMappings();

        [OperationContract]
        KeyValueData[] GetUnMappedOpexGLs();


        #endregion

        #region OpexReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexReport UpdateOpexReport(OpexReport opexReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexReport(int opexReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexReport GetOpexReport(int opexReportId);

        [OperationContract]
        OpexReportData[] GetAllOpexReports();

        #endregion

        #region OpexGLBasis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexGLBasis UpdateOpexGLBasis(OpexGLBasis opexGLBasis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexGLBasis(int opexGLBasisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexGLBasis GetOpexGLBasis(int opexGLBasisId);

        [OperationContract]
        OpexGLBasis[] GetAllOpexGLBasiss();

        #endregion

        #region OpexCheckList

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexCheckList UpdateOpexCheckList(OpexCheckList opexCheckList);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexCheckList(int opexCheckListId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexCheckList GetOpexCheckList(int opexCheckListId);

        [OperationContract]
        OpexCheckList[] GetAllOpexCheckLists();

        #endregion

        #region OpexBasisMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexBasisMapping UpdateOpexBasisMapping(OpexBasisMapping opexBasisMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexBasisMapping(int opexBasisMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexBasisMapping GetOpexBasisMapping(int opexBasisMappingId);

        [OperationContract]
        OpexBasisMapping[] GetAllOpexBasisMappings();

        #endregion


    }
}
