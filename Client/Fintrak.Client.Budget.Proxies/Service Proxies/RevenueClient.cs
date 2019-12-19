using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Budget.Contracts;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Budget.Proxies
{
    [Export(typeof(IRevenueService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RevenueClient : UserClientBase<IRevenueService>, IRevenueService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region ProductCategory

        public ProductCategory UpdateProductCategory(ProductCategory productCategory)
        {
            return Channel.UpdateProductCategory(productCategory);
        }

        public void DeleteProductCategory(int productCategoryId)
        {
           Channel.DeleteProductCategory(productCategoryId);
        }

        public ProductCategory GetProductCategory(int productCategoryId)
        {
            return Channel.GetProductCategory(productCategoryId);
        }

        public ProductCategory[] GetAllProductCategorys()
        {
            return Channel.GetAllProductCategorys();
        }

        #endregion

        #region Product

        public Product UpdateProduct(Product product)
        {
            return Channel.UpdateProduct(product);
        }

        public void DeleteProduct(int productId)
        {
            Channel.DeleteProduct(productId);
        }

        public Product GetProduct(int productId)
        {
            return Channel.GetProduct(productId);
        }

        public Product[] GetAllProducts()
        {
            return Channel.GetAllProducts();
        }

        #endregion

        #region ProductClassification

        public ProductClassification UpdateProductClassification(ProductClassification productClassification)
        {
            return Channel.UpdateProductClassification(productClassification);
        }

        public void DeleteProductClassification(int productClassificationId)
        {
            Channel.DeleteProductClassification(productClassificationId);
        }

        public ProductClassification GetProductClassification(int productClassificationId)
        {
            return Channel.GetProductClassification(productClassificationId);
        }

        public ProductClassification[] GetAllProductClassifications()
        {
            return Channel.GetAllProductClassifications();
        }

        #endregion

        #region ProductGroup

        public ProductGroup UpdateProductGroup(ProductGroup productGroup)
        {
            return Channel.UpdateProductGroup(productGroup);
        }

        public void DeleteProductGroup(int productGroupId)
        {
            Channel.DeleteProductGroup(productGroupId);
        }

        public ProductGroup GetProductGroup(int productGroupId)
        {
            return Channel.GetProductGroup(productGroupId);
        }

        public ProductGroup[] GetAllProductGroups()
        {
            return Channel.GetAllProductGroups();
        }

        #endregion

        #region ProductGroupEntry

        public ProductGroupEntry UpdateProductGroupEntry(ProductGroupEntry productGroupEntry)
        {
            return Channel.UpdateProductGroupEntry(productGroupEntry);
        }

        public void DeleteProductGroupEntry(int productGroupEntryId)
        {
            Channel.DeleteProductGroupEntry(productGroupEntryId);
        }

        public ProductGroupEntry GetProductGroupEntry(int productGroupEntryId)
        {
            return Channel.GetProductGroupEntry(productGroupEntryId);
        }

        public ProductGroupEntry[] GetAllProductGroupEntrys()
        {
            return Channel.GetAllProductGroupEntrys();
        }

        #endregion

        #region ProductInterestRate

        public ProductInterestRate UpdateProductInterestRate(ProductInterestRate productInterestRate)
        {
            return Channel.UpdateProductInterestRate(productInterestRate);
        }

        public void DeleteProductInterestRate(int productInterestRateId)
        {
            Channel.DeleteProductInterestRate(productInterestRateId);
        }

        public ProductInterestRate GetProductInterestRate(int productInterestRateId)
        {
            return Channel.GetProductInterestRate(productInterestRateId);
        }

        public ProductInterestRate[] GetAllProductInterestRates()
        {
            return Channel.GetAllProductInterestRates();
        }

        #endregion

        #region ProductPoolRate

        public ProductPoolRate UpdateProductPoolRate(ProductPoolRate productPoolRate)
        {
            return Channel.UpdateProductPoolRate(productPoolRate);
        }

        public void DeleteProductPoolRate(int productPoolRateId)
        {
            Channel.DeleteProductPoolRate(productPoolRateId);
        }

        public ProductPoolRate GetProductPoolRate(int productPoolRateId)
        {
            return Channel.GetProductPoolRate(productPoolRateId);
        }

        public ProductPoolRate[] GetAllProductPoolRates()
        {
            return Channel.GetAllProductPoolRates();
        }

        #endregion

        #region ProductSharedRatio

        public ProductSharedRatio UpdateProductSharedRatio(ProductSharedRatio productSharedRatio)
        {
            return Channel.UpdateProductSharedRatio(productSharedRatio);
        }

        public void DeleteProductSharedRatio(int productSharedRatioId)
        {
            Channel.DeleteProductSharedRatio(productSharedRatioId);
        }

        public ProductSharedRatio GetProductSharedRatio(int productSharedRatioId)
        {
            return Channel.GetProductSharedRatio(productSharedRatioId);
        }

        public ProductSharedRatio[] GetAllProductSharedRatios()
        {
            return Channel.GetAllProductSharedRatios();
        }

        #endregion

        #region ProductVolumeBasedRate

        public ProductVolumeBasedRate UpdateProductVolumeBasedRate(ProductVolumeBasedRate productVolumeBasedRate)
        {
            return Channel.UpdateProductVolumeBasedRate(productVolumeBasedRate);
        }

        public void DeleteProductVolumeBasedRate(int productVolumeBasedRateId)
        {
            Channel.DeleteProductVolumeBasedRate(productVolumeBasedRateId);
        }

        public ProductVolumeBasedRate GetProductVolumeBasedRate(int productVolumeBasedRateId)
        {
            return Channel.GetProductVolumeBasedRate(productVolumeBasedRateId);
        }

        public ProductVolumeBasedRate[] GetAllProductVolumeBasedRates()
        {
            return Channel.GetAllProductVolumeBasedRates();
        }

        #endregion

        #region ProductVolumeBasedSetup

        public ProductVolumeBasedSetup UpdateProductVolumeBasedSetup(ProductVolumeBasedSetup productVolumeBasedSetup)
        {
            return Channel.UpdateProductVolumeBasedSetup(productVolumeBasedSetup);
        }

        public void DeleteProductVolumeBasedSetup(int productVolumeBasedSetupId)
        {
            Channel.DeleteProductVolumeBasedSetup(productVolumeBasedSetupId);
        }

        public ProductVolumeBasedSetup GetProductVolumeBasedSetup(int productVolumeBasedSetupId)
        {
            return Channel.GetProductVolumeBasedSetup(productVolumeBasedSetupId);
        }

        public ProductVolumeBasedSetup[] GetAllProductVolumeBasedSetups()
        {
            return Channel.GetAllProductVolumeBasedSetups();
        }

        #endregion

        #region RevenueCaption

        public RevenueCaption UpdateRevenueCaption(RevenueCaption revenueCaption)
        {
            return Channel.UpdateRevenueCaption(revenueCaption);
        }

        public void DeleteRevenueCaption(int revenueCaptionId)
        {
            Channel.DeleteRevenueCaption(revenueCaptionId);
        }

        public RevenueCaption GetRevenueCaption(int revenueCaptionId)
        {
            return Channel.GetRevenueCaption(revenueCaptionId);
        }

        public RevenueCaption[] GetAllRevenueCaptions()
        {
            return Channel.GetAllRevenueCaptions();
        }

        #endregion

        #region RevenueIncExp

        public RevenueIncExp UpdateRevenueIncExp(RevenueIncExp revenueIncExp)
        {
            return Channel.UpdateRevenueIncExp(revenueIncExp);
        }

        public void DeleteRevenueIncExp(int revenueIncExpId)
        {
            Channel.DeleteRevenueIncExp(revenueIncExpId);
        }

        public RevenueIncExp GetRevenueIncExp(int revenueIncExpId)
        {
            return Channel.GetRevenueIncExp(revenueIncExpId);
        }

        public RevenueIncExp[] GetAllRevenueIncExps()
        {
            return Channel.GetAllRevenueIncExps();
        }

        #endregion

        #region RevenuePool

        public RevenuePool UpdateRevenuePool(RevenuePool revenuePool)
        {
            return Channel.UpdateRevenuePool(revenuePool);
        }

        public void DeleteRevenuePool(int revenuePoolId)
        {
            Channel.DeleteRevenuePool(revenuePoolId);
        }

        public RevenuePool GetRevenuePool(int revenuePoolId)
        {
            return Channel.GetRevenuePool(revenuePoolId);
        }

        public RevenuePool[] GetAllRevenuePools()
        {
            return Channel.GetAllRevenuePools();
        }

        #endregion

        #region RevenueSetting

        public RevenueSetting UpdateRevenueSetting(RevenueSetting revenueSetting)
        {
            return Channel.UpdateRevenueSetting(revenueSetting);
        }

        public void DeleteRevenueSetting(int revenueSettingId)
        {
            Channel.DeleteRevenueSetting(revenueSettingId);
        }

        public RevenueSetting GetRevenueSetting(int revenueSettingId)
        {
            return Channel.GetRevenueSetting(revenueSettingId);
        }

        public RevenueSetting[] GetAllRevenueSettings()
        {
            return Channel.GetAllRevenueSettings();
        }

        #endregion

        

    }
}
