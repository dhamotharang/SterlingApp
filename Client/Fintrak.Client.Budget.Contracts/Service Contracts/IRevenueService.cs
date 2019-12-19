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
    public interface IRevenueService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region ProductCategory

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductCategory UpdateProductCategory(ProductCategory productCategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductCategory(int productCategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductCategory GetProductCategory(int productCategoryId);

        [OperationContract]
        ProductCategory[] GetAllProductCategorys();

        #endregion

        #region Product

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Product UpdateProduct(Product product);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProduct(int productId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Product GetProduct(int productId);

        [OperationContract]
        Product[] GetAllProducts();

        #endregion

        #region ProductClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductClassification UpdateProductClassification(ProductClassification productClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductClassification(int productClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductClassification GetProductClassification(int productClassificationId);

        [OperationContract]
        ProductClassification[] GetAllProductClassifications();

        #endregion

        #region ProductGroup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductGroup UpdateProductGroup(ProductGroup productGroup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductGroup(int productGroupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductGroup GetProductGroup(int productGroupId);

        [OperationContract]
        ProductGroup[] GetAllProductGroups();

        #endregion

        #region ProductGroupEntry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductGroupEntry UpdateProductGroupEntry(ProductGroupEntry productGroupEntry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductGroupEntry(int productGroupEntryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductGroupEntry GetProductGroupEntry(int productGroupEntryId);

        [OperationContract]
        ProductGroupEntry[] GetAllProductGroupEntrys();

        #endregion

        #region ProductInterestRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductInterestRate UpdateProductInterestRate(ProductInterestRate productInterestRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductInterestRate(int productInterestRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductInterestRate GetProductInterestRate(int productInterestRateId);

        [OperationContract]
        ProductInterestRate[] GetAllProductInterestRates();

        #endregion

        #region ProductPoolRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductPoolRate UpdateProductPoolRate(ProductPoolRate productPoolRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductPoolRate(int productPoolRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductPoolRate GetProductPoolRate(int productPoolRateId);

        [OperationContract]
        ProductPoolRate[] GetAllProductPoolRates();

        #endregion

        #region ProductSharedRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductSharedRatio UpdateProductSharedRatio(ProductSharedRatio productSharedRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductSharedRatio(int productSharedRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductSharedRatio GetProductSharedRatio(int productSharedRatioId);

        [OperationContract]
        ProductSharedRatio[] GetAllProductSharedRatios();

        #endregion

        #region ProductVolumeBasedRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductVolumeBasedRate UpdateProductVolumeBasedRate(ProductVolumeBasedRate productVolumeBasedRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductVolumeBasedRate(int productVolumeBasedRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductVolumeBasedRate GetProductVolumeBasedRate(int productVolumeBasedRateId);

        [OperationContract]
        ProductVolumeBasedRate[] GetAllProductVolumeBasedRates();

        #endregion

        #region ProductVolumeBasedSetup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductVolumeBasedSetup UpdateProductVolumeBasedSetup(ProductVolumeBasedSetup productVolumeBasedSetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductVolumeBasedSetup(int productVolumeBasedSetupId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductVolumeBasedSetup GetProductVolumeBasedSetup(int productVolumeBasedSetupId);

        [OperationContract]
        ProductVolumeBasedSetup[] GetAllProductVolumeBasedSetups();

        #endregion

        #region RevenueCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueCaption UpdateRevenueCaption(RevenueCaption revenueCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueCaption(int revenueCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueCaption GetRevenueCaption(int revenueCaptionId);

        [OperationContract]
        RevenueCaption[] GetAllRevenueCaptions();

        #endregion

        #region RevenueIncExp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueIncExp UpdateRevenueIncExp(RevenueIncExp revenueIncExp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueIncExp(int revenueIncExpId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueIncExp GetRevenueIncExp(int revenueIncExpId);

        [OperationContract]
        RevenueIncExp[] GetAllRevenueIncExps();

        #endregion

        #region RevenuePool

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenuePool UpdateRevenuePool(RevenuePool revenuePool);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenuePool(int revenuePoolId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenuePool GetRevenuePool(int revenuePoolId);

        [OperationContract]
        RevenuePool[] GetAllRevenuePools();


        #endregion

        #region RevenueSetting

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RevenueSetting UpdateRevenueSetting(RevenueSetting revenueSetting);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRevenueSetting(int revenueSettingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RevenueSetting GetRevenueSetting(int revenueSettingId);

        [OperationContract]
        RevenueSetting[] GetAllRevenueSettings();

        #endregion

    

    }
}
