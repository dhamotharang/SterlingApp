using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Client.Budget.Contracts
{
    [ServiceContract]
    public interface IOpexService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region OpexCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexCategory UpdateOpexCategory(OpexCategory opexCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexCategory(int opexCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexCategory GetOpexCategory(int opexCategoryId);

        [OperationContract]
        OpexCategory[] GetAllOpexCategorys();

        #endregion

        #region OpexEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexEntry UpdateOpexEntry(OpexEntry opexEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexEntry(int opexEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexEntry GetOpexEntry(int opexEntryId);

        [OperationContract]
        OpexEntry[] GetAllOpexEntrys();

        #endregion

        #region OpexItem

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexItem UpdateOpexItem(OpexItem opexItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexItem(int opexItemId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexItem GetOpexItem(int opexItemId);

        [OperationContract]
        OpexItem[] GetAllOpexItems();

        #endregion

        #region OpexVolumeBasedSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexVolumeBasedSetup UpdateOpexVolumeBasedSetup(OpexVolumeBasedSetup opexVolumeBasedSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexVolumeBasedSetup(int opexVolumeBasedSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexVolumeBasedSetup GetOpexVolumeBasedSetup(int opexVolumeBasedSetupId);

        [OperationContract]
        OpexVolumeBasedSetup[] GetAllOpexVolumeBasedSetups();

        #endregion

        #region OpexVolumeBasedRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OpexVolumeBasedRate UpdateOpexVolumeBasedRate(OpexVolumeBasedRate opexVolumeBasedRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOpexVolumeBasedRate(int opexVolumeBasedRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OpexVolumeBasedRate GetOpexVolumeBasedRate(int opexVolumeBasedRateId);

        [OperationContract]
        OpexVolumeBasedRate[] GetAllOpexVolumeBasedRates();

        #endregion

    }
}
