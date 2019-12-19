using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Budget.Framework;

namespace Fintrak.Client.Budget.Contracts
{
    [ServiceContract]
    public interface ICapexService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region CapexCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CapexCategory UpdateCapexCategory(CapexCategory capexCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCapexCategory(int capexCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CapexCategory GetCapexCategory(int capexCategoryId);

        //[OperationContract]
        //CapexCategory[] GetAllCapexCategorys();

        [OperationContract]
        CapexCategoryData[] GetCapexCategories(string year, string reviewCode);



        #endregion

        #region CapexCost

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CapexCost UpdateCapexCost(CapexCost capexCost);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCapexCost(int capexCostId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CapexCost GetCapexCost(int capexCostId);

        //[OperationContract]
        //CapexCost[] GetAllCapexCosts();


        [OperationContract]
        CapexCostData[] GetCapexCosts(string year, string reviewCode);

        [OperationContract]
        CapexCostData[] GetCapexCosts(string year, string reviewCode, string categoryCode, string definitionCode, string misCode);

        #endregion

        #region CapexEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CapexEntry UpdateCapexEntry(CapexEntry capexEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCapexEntry(int capexEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CapexEntry GetCapexEntry(int capexEntryId);

        //[OperationContract]
        //CapexEntry[] GetAllCapexEntrys();

        [OperationContract]
        CapexEntryData[] GetCapexEntries(string year, string reviewCode);

        [OperationContract]
        CapexEntryData[] GetCapexEntries(string year, string reviewCode, string categoryCode, string definitionCode, string misCode);

        #endregion

        #region CapexItem

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CapexItem UpdateCapexItem(CapexItem capexItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCapexItem(int capexItemId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CapexItem GetCapexItem(int capexItemId);


        [OperationContract]
        CapexItemData[] GetCapexItems(string year, string reviewCode);

        [OperationContract]
        CapexItemData[] GetCapexItems(string year, string reviewCode, string categoryCode);

        #endregion

        #region DepreciationRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DepreciationRate UpdateDepreciationRate(DepreciationRate depreciationRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDepreciationRate(int depreciationRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DepreciationRate GetDepreciationRate(int depreciationRateId);

        [OperationContract]
        DepreciationRateData[] GetDepreciationRates(string year, string reviewCode);

        [OperationContract]
        DepreciationRateData[] GetDepreciationRates(string year, string reviewCode, string categoryCode);

        #endregion


    

    }
}
