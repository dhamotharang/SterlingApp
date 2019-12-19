using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;

namespace Fintrak.Client.Budget.Contracts
{
    [ServiceContract]
    public interface IFeeService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region FeeCalculationType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeCalculationType UpdateFeeCalculationType(FeeCalculationType feeCalculationType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeCalculationType(int feeCalculationTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeCalculationType GetFeeCalculationType(int feeCalculationTypeId);

        [OperationContract]
        FeeCalculationType[] GetAllFeeCalculationTypes();

        #endregion

        #region FeeCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeCaption UpdateFeeCaption(FeeCaption feeCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeCaption(int feeCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeCaption GetFeeCaption(int feeCaptionId);

        [OperationContract]
        FeeCaption[] GetAllFeeCaptions();

        #endregion

        #region FeeCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeCategory UpdateFeeCategory(FeeCategory feeCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeCategory(int feeCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeCategory GetFeeCategory(int feeCategoryId);

        [OperationContract]
        FeeCategory[] GetAllFeeCategorys();

        #endregion

        #region FeeEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeEntry UpdateFeeEntry(FeeEntry feeEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeEntry(int feeEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeEntry GetFeeEntry(int feeEntryId);

        [OperationContract]
        FeeEntry[] GetAllFeeEntrys();

        #endregion

        #region FeeGroup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeGroup UpdateFeeGroup(FeeGroup feeGroup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeGroup(int feeGroupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeGroup GetFeeGroup(int feeGroupId);

        [OperationContract]
        FeeGroup[] GetAllFeeGroups();

        #endregion

        #region FeeGroupEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeGroupEntry UpdateFeeGroupEntry(FeeGroupEntry feeGroupEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeGroupEntry(int feeGroupEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeGroupEntry GetFeeGroupEntry(int feeGroupEntryId);

        [OperationContract]
        FeeGroupEntry[] GetAllFeeGroupEntrys();

        #endregion

        #region FeeItem

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeItem UpdateFeeItem(FeeItem feeItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeItem(int feeItemId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeItem GetFeeItem(int feeItemId);

        [OperationContract]
        FeeItem[] GetAllFeeItems();

        #endregion

        #region FeeMovement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeMovement UpdateFeeMovement(FeeMovement feeMovement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeMovement(int feeMovementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeMovement GetFeeMovement(int feeMovementId);

        [OperationContract]
        FeeMovement[] GetAllFeeMovements();

        #endregion

        #region FeeSharedExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeSharedExemption UpdateFeeSharedExemption(FeeSharedExemption feeSharedExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeSharedExemption(int feeSharedExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeSharedExemption GetFeeSharedExemption(int feeSharedExemptionId);

        [OperationContract]
        FeeSharedExemption[] GetAllFeeSharedExemptions();

        #endregion

        #region FeeSharedRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeSharedRatio UpdateFeeSharedRatio(FeeSharedRatio feeSharedRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeSharedRatio(int feeSharedRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeSharedRatio GetFeeSharedRatio(int feeSharedRatioId);

        [OperationContract]
        FeeSharedRatio[] GetAllFeeSharedRatios();

        #endregion

        #region FeeVolumeBasedSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FeeVolumeBasedSetup UpdateFeeVolumeBasedSetup(FeeVolumeBasedSetup feeVolumeBasedSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFeeVolumeBasedSetup(int feeVolumeBasedSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FeeVolumeBasedSetup GetFeeVolumeBasedSetup(int feeVolumeBasedSetupId);

        [OperationContract]
        FeeVolumeBasedSetup[] GetAllFeeVolumeBasedSetups();

        #endregion

    

    }
}
