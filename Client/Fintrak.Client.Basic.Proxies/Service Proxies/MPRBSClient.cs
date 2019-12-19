using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.Basic.Proxies
{
    [Export(typeof(IMPRBSService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRBSClient : UserClientBase<IMPRBSService>, IMPRBSService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region BalanceSheetThreshold

        public BalanceSheetThreshold UpdateBalanceSheetThreshold(BalanceSheetThreshold balanceSheetThreshold)
        {
            return Channel.UpdateBalanceSheetThreshold(balanceSheetThreshold);
        }

        public void DeleteBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            Channel.DeleteBalanceSheetThreshold(balanceSheetThresholdId);
        }

        public BalanceSheetThreshold GetBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            return Channel.GetBalanceSheetThreshold(balanceSheetThresholdId);
        }

        public BalanceSheetThresholdData[] GetAllBalanceSheetThresholds()
        {
            return Channel.GetAllBalanceSheetThresholds();
        }


        #endregion

        #region BSCaption

        public BSCaption UpdateBSCaption(BSCaption bsCaption)
        {
            return Channel.UpdateBSCaption(bsCaption);
        }

        public void DeleteBSCaption(int bsCaptionId)
        {
            Channel.DeleteBSCaption(bsCaptionId);
        }

        public BSCaption GetBSCaption(int bsCaptionId)
        {
            return Channel.GetBSCaption(bsCaptionId);
        }

        public BSCaptionData[] GetAllBSCaptions()
        {
            var result = Channel.GetAllBSCaptions();
            return result;
        }

        

        #endregion

        #region MPRProduct

        public MPRProduct UpdateMPRProduct(MPRProduct product)
        {
            return Channel.UpdateMPRProduct(product);
        }

        public void DeleteMPRProduct(int productId)
        {
            Channel.DeleteMPRProduct(productId);
        }

        public MPRProduct GetMPRProduct(int productId)
        {
            return Channel.GetMPRProduct(productId);
        }

        public MPRProductData[] GetAllMPRProducts()
        {
            return Channel.GetAllMPRProducts();
        }

        public MPRProductData[] GetMPRProductByType(BalanceSheetType type)
        {
            return Channel.GetMPRProductByType(type);
        }

        public MPRProductData[] GetMPRProductByNotional(bool notional)
        {
            return Channel.GetMPRProductByNotional(notional);
        }

        public KeyValueData[] GetUnMappedProducts()
        {
            return Channel.GetUnMappedProducts();
        }

        #endregion

        #region NonProductMap

        public NonProductMap UpdateNonProductMap(NonProductMap nonProductMap)
        {
            return Channel.UpdateNonProductMap(nonProductMap);
        }

        public void DeleteNonProductMap(int nonProductMapId)
        {
            Channel.DeleteNonProductMap(nonProductMapId);
        }

        public NonProductMap GetNonProductMap(int nonProductMapId)
        {
            return Channel.GetNonProductMap(nonProductMapId);
        }

        public NonProductMapData[] GetAllNonProductMaps()
        {
            return Channel.GetAllNonProductMaps();
        }

      
        #endregion

        #region NonProductRate

        public NonProductRate UpdateNonProductRate(NonProductRate nonProductRate)
        {
            return Channel.UpdateNonProductRate(nonProductRate);
        }

        public void DeleteNonProductRate(int nonProductRateId)
        {
            Channel.DeleteNonProductRate(nonProductRateId);
        }

        public NonProductRate GetNonProductRate(int nonProductRateId)
        {
            return Channel.GetNonProductRate(nonProductRateId);
        }

        public NonProductRate[] GetAllNonProductRates()
        {
            return Channel.GetAllNonProductRates();
        }


        #endregion

        #region ProductMIS

        public ProductMIS UpdateProductMIS(ProductMIS productMIS)
        {
            return Channel.UpdateProductMIS(productMIS);
        }

        public void DeleteProductMIS(int productMISId)
        {
            Channel.DeleteProductMIS(productMISId);
        }

        public ProductMIS GetProductMIS(int productMISId)
        {
            return Channel.GetProductMIS(productMISId);
        }

        public ProductMISData[] GetAllProductMISs()
        {
            return Channel.GetAllProductMISs();
        }

   

        #endregion

        #region MPRBalanceSheet

        public MPRBalanceSheet UpdateBalanceSheet(MPRBalanceSheet balanceSheet)
        {
            return Channel.UpdateBalanceSheet(balanceSheet);
        }

        public void DeleteBalanceSheet(int balanceSheetId)
        {
            Channel.DeleteBalanceSheet(balanceSheetId);
        }

        public MPRBalanceSheet GetBalanceSheet(int balanceSheetId)
        {
            return Channel.GetBalanceSheet(balanceSheetId);
        }

        public MPRBalanceSheet[] GetAllBalanceSheets()
        {
            return Channel.GetAllBalanceSheets();
        }

        public MPRBalanceSheet[] GetBalanceSheets(string searchType, string searchValue, int number)
        {
            return Channel.GetBalanceSheets(searchType, searchValue, number);
        }

        #endregion

        #region BalanceSheetAdjustment

        public MPRBalanceSheetAdjustment UpdateBalanceSheetAdjustment(MPRBalanceSheetAdjustment balanceSheetAdjustment)
        {
            return Channel.UpdateBalanceSheetAdjustment(balanceSheetAdjustment);
        }

        public void DeleteBalanceSheetAdjustment(int balanceSheetAdjustmentId)
        {
            Channel.DeleteBalanceSheetAdjustment(balanceSheetAdjustmentId);
        }

        public MPRBalanceSheetAdjustment GetBalanceSheetAdjustment(int balanceSheetAdjustmentId)
        {
            return Channel.GetBalanceSheetAdjustment(balanceSheetAdjustmentId);
        }

        public MPRBalanceSheetAdjustment[] GetAllBalanceSheetAdjustments()
        {
            return Channel.GetAllBalanceSheetAdjustments();
        }


        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustments(string searchType, string searchValue, int number)
        {
            return Channel.GetBalanceSheetAdjustments(searchType, searchValue, number);
        }



        #endregion

        #region BalanceSheetBudgetOfficer

        public BalanceSheetBudgetOfficer UpdateBalanceSheetBudgetOfficer(BalanceSheetBudgetOfficer balanceSheetBudgetOfficer)
        {
            return Channel.UpdateBalanceSheetBudgetOfficer(balanceSheetBudgetOfficer);
        }

        public void DeleteBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            Channel.DeleteBalanceSheetBudgetOfficer(balanceSheetBudgetOffId);
        }

        public BalanceSheetBudgetOfficer GetBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            return Channel.GetBalanceSheetBudgetOfficer(balanceSheetBudgetOffId);
        }

        public BalanceSheetBudgetOfficer[] GetAllBalanceSheetBudgetOfficers(string year)
        {
            return Channel.GetAllBalanceSheetBudgetOfficers(year);
        }


        #endregion

        #region BalanceSheetBudget

        public BalanceSheetBudget UpdateBalanceSheetBudget(BalanceSheetBudget balanceSheetBudget)
        {
            return Channel.UpdateBalanceSheetBudget(balanceSheetBudget);
        }

        public void DeleteBalanceSheetBudget(int balanceSheetBudgetId)
        {
            Channel.DeleteBalanceSheetBudget(balanceSheetBudgetId);
        }

        public BalanceSheetBudget GetBalanceSheetBudget(int balanceSheetBudgetId)
        {
            return Channel.GetBalanceSheetBudget(balanceSheetBudgetId);
        }

        public BalanceSheetBudget[] GetAllBalanceSheetBudgets(string year)
        {
            return Channel.GetAllBalanceSheetBudgets(year);
        }


        #endregion

        #region BSGLMapping

        public BSGLMapping UpdateBSGLMapping(BSGLMapping bsGLMapping)
        {
            return Channel.UpdateBSGLMapping(bsGLMapping);
        }

        public void DeleteBSGLMapping(int bsGLMappingId)
        {
            Channel.DeleteBSGLMapping(bsGLMappingId);
        }

        public BSGLMapping GetBSGLMapping(int bsGLMappingId)
        {
            return Channel.GetBSGLMapping(bsGLMappingId);
        }

        public BSGLMappingData[] GetAllBSGLMappings()
        {
            var result = Channel.GetAllBSGLMappings();
            return result;
        }



        #endregion


    }
}
