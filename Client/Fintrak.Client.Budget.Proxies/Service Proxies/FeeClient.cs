using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(IFeeService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FeeClient : UserClientBase<IFeeService>, IFeeService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region FeeCalculationType

        public FeeCalculationType UpdateFeeCalculationType(FeeCalculationType feeCalculationType)
        {
            return Channel.UpdateFeeCalculationType(feeCalculationType);
        }

        public void DeleteFeeCalculationType(int feeCalculationTypeId)
        {
           Channel.DeleteFeeCalculationType(feeCalculationTypeId);
        }

        public FeeCalculationType GetFeeCalculationType(int feeCalculationTypeId)
        {
            return Channel.GetFeeCalculationType(feeCalculationTypeId);
        }

        public FeeCalculationType[] GetAllFeeCalculationTypes()
        {
            return Channel.GetAllFeeCalculationTypes();
        }

        #endregion

        #region FeeCaption

        public FeeCaption UpdateFeeCaption(FeeCaption feeCaption)
        {
            return Channel.UpdateFeeCaption(feeCaption);
        }

        public void DeleteFeeCaption(int feeCaptionId)
        {
            Channel.DeleteFeeCaption(feeCaptionId);
        }

        public FeeCaption GetFeeCaption(int feeCaptionId)
        {
            return Channel.GetFeeCaption(feeCaptionId);
        }

        public FeeCaption[] GetAllFeeCaptions()
        {
            return Channel.GetAllFeeCaptions();
        }

        #endregion

        #region FeeCategory

        public FeeCategory UpdateFeeCategory(FeeCategory feeCategory)
        {
            return Channel.UpdateFeeCategory(feeCategory);
        }

        public void DeleteFeeCategory(int feeCategoryId)
        {
            Channel.DeleteFeeCategory(feeCategoryId);
        }

        public FeeCategory GetFeeCategory(int feeCategoryId)
        {
            return Channel.GetFeeCategory(feeCategoryId);
        }

        public FeeCategory[] GetAllFeeCategorys()
        {
            return Channel.GetAllFeeCategorys();
        }

        #endregion

        #region FeeEntry

        public FeeEntry UpdateFeeEntry(FeeEntry feeEntry)
        {
            return Channel.UpdateFeeEntry(feeEntry);
        }

        public void DeleteFeeEntry(int feeEntryId)
        {
            Channel.DeleteFeeEntry(feeEntryId);
        }

        public FeeEntry GetFeeEntry(int feeEntryId)
        {
            return Channel.GetFeeEntry(feeEntryId);
        }

        public FeeEntry[] GetAllFeeEntrys()
        {
            return Channel.GetAllFeeEntrys();
        }

        #endregion

        #region FeeGroup

        public FeeGroup UpdateFeeGroup(FeeGroup feeGroup)
        {
            return Channel.UpdateFeeGroup(feeGroup);
        }

        public void DeleteFeeGroup(int feeGroupId)
        {
            Channel.DeleteFeeGroup(feeGroupId);
        }

        public FeeGroup GetFeeGroup(int feeGroupId)
        {
            return Channel.GetFeeGroup(feeGroupId);
        }

        public FeeGroup[] GetAllFeeGroups()
        {
            return Channel.GetAllFeeGroups();
        }

        #endregion

        #region FeeGroupEntry

        public FeeGroupEntry UpdateFeeGroupEntry(FeeGroupEntry feeGroupEntry)
        {
            return Channel.UpdateFeeGroupEntry(feeGroupEntry);
        }

        public void DeleteFeeGroupEntry(int feeGroupEntryId)
        {
            Channel.DeleteFeeGroupEntry(feeGroupEntryId);
        }

        public FeeGroupEntry GetFeeGroupEntry(int feeGroupEntryId)
        {
            return Channel.GetFeeGroupEntry(feeGroupEntryId);
        }

        public FeeGroupEntry[] GetAllFeeGroupEntrys()
        {
            return Channel.GetAllFeeGroupEntrys();
        }

        #endregion

        #region FeeItem

        public FeeItem UpdateFeeItem(FeeItem feeItem)
        {
            return Channel.UpdateFeeItem(feeItem);
        }

        public void DeleteFeeItem(int feeItemId)
        {
            Channel.DeleteFeeItem(feeItemId);
        }

        public FeeItem GetFeeItem(int feeItemId)
        {
            return Channel.GetFeeItem(feeItemId);
        }

        public FeeItem[] GetAllFeeItems()
        {
            return Channel.GetAllFeeItems();
        }

        #endregion

        #region FeeMovement

        public FeeMovement UpdateFeeMovement(FeeMovement feeMovement)
        {
            return Channel.UpdateFeeMovement(feeMovement);
        }

        public void DeleteFeeMovement(int feeMovementId)
        {
            Channel.DeleteFeeMovement(feeMovementId);
        }

        public FeeMovement GetFeeMovement(int feeMovementId)
        {
            return Channel.GetFeeMovement(feeMovementId);
        }

        public FeeMovement[] GetAllFeeMovements()
        {
            return Channel.GetAllFeeMovements();
        }

        #endregion

        #region FeeSharedExemption

        public FeeSharedExemption UpdateFeeSharedExemption(FeeSharedExemption feeSharedExemption)
        {
            return Channel.UpdateFeeSharedExemption(feeSharedExemption);
        }

        public void DeleteFeeSharedExemption(int feeSharedExemptionId)
        {
            Channel.DeleteFeeSharedExemption(feeSharedExemptionId);
        }

        public FeeSharedExemption GetFeeSharedExemption(int feeSharedExemptionId)
        {
            return Channel.GetFeeSharedExemption(feeSharedExemptionId);
        }

        public FeeSharedExemption[] GetAllFeeSharedExemptions()
        {
            return Channel.GetAllFeeSharedExemptions();
        }

        #endregion

        #region FeeSharedRatio

        public FeeSharedRatio UpdateFeeSharedRatio(FeeSharedRatio feeSharedRatio)
        {
            return Channel.UpdateFeeSharedRatio(feeSharedRatio);
        }

        public void DeleteFeeSharedRatio(int feeSharedRatioId)
        {
            Channel.DeleteFeeSharedRatio(feeSharedRatioId);
        }

        public FeeSharedRatio GetFeeSharedRatio(int feeSharedRatioId)
        {
            return Channel.GetFeeSharedRatio(feeSharedRatioId);
        }

        public FeeSharedRatio[] GetAllFeeSharedRatios()
        {
            return Channel.GetAllFeeSharedRatios();
        }

        #endregion

        #region FeeVolumeBasedSetup

        public FeeVolumeBasedSetup UpdateFeeVolumeBasedSetup(FeeVolumeBasedSetup feeVolumeBasedSetup)
        {
            return Channel.UpdateFeeVolumeBasedSetup(feeVolumeBasedSetup);
        }

        public void DeleteFeeVolumeBasedSetup(int feeVolumeBasedSetupId)
        {
            Channel.DeleteFeeVolumeBasedSetup(feeVolumeBasedSetupId);
        }

        public FeeVolumeBasedSetup GetFeeVolumeBasedSetup(int feeVolumeBasedSetupId)
        {
            return Channel.GetFeeVolumeBasedSetup(feeVolumeBasedSetupId);
        }

        public FeeVolumeBasedSetup[] GetAllFeeVolumeBasedSetups()
        {
            return Channel.GetAllFeeVolumeBasedSetups();
        }

        #endregion

        

    }
}
