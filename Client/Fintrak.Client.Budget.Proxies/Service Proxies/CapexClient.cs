using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(ICapexService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CapexClient : UserClientBase<ICapexService>, ICapexService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region CapexCategory

        public CapexCategory UpdateCapexCategory(CapexCategory capexCategory)
        {
            return Channel.UpdateCapexCategory(capexCategory);
        }

        public void DeleteCapexCategory(int capexCategoryId)
        {
           Channel.DeleteCapexCategory(capexCategoryId);
        }

        public CapexCategory GetCapexCategory(int capexCategoryId)
        {
            return Channel.GetCapexCategory(capexCategoryId);
        }

        public CapexCategoryData[] GetCapexCategories(string year, string reviewCode)
        {
            return Channel.GetCapexCategories(year, reviewCode);
        }

        #endregion

        #region CapexCost

        public CapexCost UpdateCapexCost(CapexCost capexCost)
        {
            return Channel.UpdateCapexCost(capexCost);
        }

        public void DeleteCapexCost(int capexCostId)
        {
            Channel.DeleteCapexCost(capexCostId);
        }

        public CapexCost GetCapexCost(int capexCostId)
        {
            return Channel.GetCapexCost(capexCostId);
        }

        public CapexCostData[] GetCapexCosts(string year, string reviewCode)
        {
            return Channel.GetCapexCosts(year, reviewCode);
        }

        public CapexCostData[] GetCapexCosts(string year, string reviewCode, string categoryCode, string definitionCode, string misCode)
        {
            return Channel.GetCapexCosts(year, reviewCode, categoryCode, definitionCode, misCode);
        }

        #endregion

        #region CapexEntry

        public CapexEntry UpdateCapexEntry(CapexEntry capexEntry)
        {
            return Channel.UpdateCapexEntry(capexEntry);
        }

        public void DeleteCapexEntry(int capexEntryId)
        {
            Channel.DeleteCapexEntry(capexEntryId);
        }

        public CapexEntry GetCapexEntry(int capexEntryId)
        {
            return Channel.GetCapexEntry(capexEntryId);
        }

        public CapexEntryData[] GetCapexEntries(string year, string reviewCode)
        {
            return Channel.GetCapexEntries(year, reviewCode);
        }

        public CapexEntryData[] GetCapexEntries(string year, string reviewCode, string categoryCode, string definitionCode, string misCode)
        {
            return Channel.GetCapexEntries(year, reviewCode, categoryCode, definitionCode, misCode);
        }

        #endregion

        #region CapexItem

        public CapexItem UpdateCapexItem(CapexItem capexItem)
        {
            return Channel.UpdateCapexItem(capexItem);
        }

        public void DeleteCapexItem(int capexItemId)
        {
            Channel.DeleteCapexItem(capexItemId);
        }

        public CapexItem GetCapexItem(int capexItemId)
        {
            return Channel.GetCapexItem(capexItemId);
        }

        public CapexItemData[] GetCapexItems(string year, string reviewCode)
        {
            return Channel.GetCapexItems(year, reviewCode);
        }

        public CapexItemData[] GetCapexItems(string year, string reviewCode, string categoryCode)
        {
            return Channel.GetCapexItems(year, reviewCode, categoryCode);
        }

        #endregion

        #region DepreciationRate

        public DepreciationRate UpdateDepreciationRate(DepreciationRate depreciationRate)
        {
            return Channel.UpdateDepreciationRate(depreciationRate);
        }

        public void DeleteDepreciationRate(int depreciationRateId)
        {
            Channel.DeleteDepreciationRate(depreciationRateId);
        }

        public DepreciationRate GetDepreciationRate(int depreciationRateId)
        {
            return Channel.GetDepreciationRate(depreciationRateId);
        }

        public DepreciationRateData[] GetDepreciationRates(string year, string reviewCode)
        {
            return Channel.GetDepreciationRates(year, reviewCode);
        }

        public DepreciationRateData[] GetDepreciationRates(string year, string reviewCode, string categoryCode)
        {
            return Channel.GetDepreciationRates(year, reviewCode, categoryCode);
        }

        #endregion



        

    }
}
