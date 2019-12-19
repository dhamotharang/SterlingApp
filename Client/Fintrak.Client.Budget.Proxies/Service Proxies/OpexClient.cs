using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(IOpexService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexClient : UserClientBase<IOpexService>, IOpexService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region OpexCategory

        public OpexCategory UpdateOpexCategory(OpexCategory opexCategory)
        {
            return Channel.UpdateOpexCategory(opexCategory);
        }

        public void DeleteOpexCategory(int opexCategoryId)
        {
           Channel.DeleteOpexCategory(opexCategoryId);
        }

        public OpexCategory GetOpexCategory(int opexCategoryId)
        {
            return Channel.GetOpexCategory(opexCategoryId);
        }

        public OpexCategory[] GetAllOpexCategorys()
        {
            return Channel.GetAllOpexCategorys();
        }

        #endregion

        #region OpexEntry

        public OpexEntry UpdateOpexEntry(OpexEntry opexEntry)
        {
            return Channel.UpdateOpexEntry(opexEntry);
        }

        public void DeleteOpexEntry(int opexEntryId)
        {
            Channel.DeleteOpexEntry(opexEntryId);
        }

        public OpexEntry GetOpexEntry(int opexEntryId)
        {
            return Channel.GetOpexEntry(opexEntryId);
        }

        public OpexEntry[] GetAllOpexEntrys()
        {
            return Channel.GetAllOpexEntrys();
        }

        #endregion

        #region OpexItem

        public OpexItem UpdateOpexItem(OpexItem opexItem)
        {
            return Channel.UpdateOpexItem(opexItem);
        }

        public void DeleteOpexItem(int opexItemId)
        {
            Channel.DeleteOpexItem(opexItemId);
        }

        public OpexItem GetOpexItem(int opexItemId)
        {
            return Channel.GetOpexItem(opexItemId);
        }

        public OpexItem[] GetAllOpexItems()
        {
            return Channel.GetAllOpexItems();
        }

        #endregion

        #region OpexVolumeBasedSetup

        public OpexVolumeBasedSetup UpdateOpexVolumeBasedSetup(OpexVolumeBasedSetup opexVolumeBasedSetup)
        {
            return Channel.UpdateOpexVolumeBasedSetup(opexVolumeBasedSetup);
        }

        public void DeleteOpexVolumeBasedSetup(int opexVolumeBasedSetupId)
        {
            Channel.DeleteOpexVolumeBasedSetup(opexVolumeBasedSetupId);
        }

        public OpexVolumeBasedSetup GetOpexVolumeBasedSetup(int opexVolumeBasedSetupId)
        {
            return Channel.GetOpexVolumeBasedSetup(opexVolumeBasedSetupId);
        }

        public OpexVolumeBasedSetup[] GetAllOpexVolumeBasedSetups()
        {
            return Channel.GetAllOpexVolumeBasedSetups();
        }

        #endregion

        #region OpexVolumeBasedRate

        public OpexVolumeBasedRate UpdateOpexVolumeBasedRate(OpexVolumeBasedRate opexVolumeBasedRate)
        {
            return Channel.UpdateOpexVolumeBasedRate(opexVolumeBasedRate);
        }

        public void DeleteOpexVolumeBasedRate(int opexVolumeBasedRateId)
        {
            Channel.DeleteOpexVolumeBasedRate(opexVolumeBasedRateId);
        }

        public OpexVolumeBasedRate GetOpexVolumeBasedRate(int opexVolumeBasedRateId)
        {
            return Channel.GetOpexVolumeBasedRate(opexVolumeBasedRateId);
        }

        public OpexVolumeBasedRate[] GetAllOpexVolumeBasedRates()
        {
            return Channel.GetAllOpexVolumeBasedRates();
        }

        #endregion



        

    }
}
