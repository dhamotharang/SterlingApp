using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.MPR.Proxies
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

        public BSCaption[] GetBSCaptions()
        {
            return Channel.GetBSCaptions();
        }

        public BSCaptionData[] GetAllBSCaptions()
        {
            var result = Channel.GetAllBSCaptions();
            return result;
        }

        public BSCaptionData[] GetAllMPRBSCaptions()
        {
            var result = Channel.GetAllMPRBSCaptions();
            return result;
        }

        public BSCaptionData[] GetAllBudgetBSCaptions()
        {
            var result = Channel.GetAllBudgetBSCaptions();
            return result;
        }


        public BSCaptionData[] GetAllMPRBSCaptionsByCaptionName(string CaptionName)
        {
            var result = Channel.GetAllMPRBSCaptionsByCaptionName(CaptionName);
            return result;
        }

        public BSCaptionData[] GetAllBudgetBSCaptionsByCaptionName(string CaptionName)
        {
            var result = Channel.GetAllBudgetBSCaptionsByCaptionName(CaptionName);
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

        public MPRProductData[] GetAllMPRProductsByProductCode(string productCode)
        {
            return Channel.GetAllMPRProductsByProductCode(productCode);
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

        public MPRBalanceSheet[] GetmprBalanceSheets(int number)
        {
            return Channel.GetmprBalanceSheets(number);
        }

        public MPRBalanceSheet[] GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate)
        {
            return Channel.GetAllBalanceSheets(searchType, searchValue, number, fromDate);
        }

        public MPRBalanceSheet[] GetAllMPRBalanceSheets()
        {
            return Channel.GetAllMPRBalanceSheets();
        }

        public MPRBalanceSheet[] GetRunDate()
        {
            return Channel.GetRunDate();
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

        public MPRBalanceSheetAdjustment[] GetCodebyUsers(string userName)
        {
            return Channel.GetCodebyUsers(userName);
        }

        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            return Channel.GetBalanceSheetAdjustmentByCode(code, userName);
        }

        public void DeleteMPRBalanceSheetAdjustment(string code, string userName)
        {
            Channel.DeleteMPRBalanceSheetAdjustment(code, userName);
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

        public BalanceSheetBudgetOfficer[] GetBalanceSheetBudgetOfficers(string searchValue)
        {
            return Channel.GetBalanceSheetBudgetOfficers(searchValue);
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

        public BalanceSheetBudget[] GetBalanceSheetBudgets(string searchValue)
        {
            return Channel.GetBalanceSheetBudgets(searchValue);
        }

        public void DeleteBSBSelectedIds(string selectedIds)
        {
            Channel.DeleteBSBSelectedIds(selectedIds);
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

        #region BSINOtherInformation

        public BSINOtherInformation UpdateBSINOtherInformation(BSINOtherInformation bSINOtherInformation)
        {
            return Channel.UpdateBSINOtherInformation(bSINOtherInformation);
        }

        public void DeleteBSINOtherInformation(int bSINOtherInformationId)
        {
            Channel.DeleteBSINOtherInformation(bSINOtherInformationId);
        }

        public BSINOtherInformation GetBSINOtherInformation(int bSINOtherInformationId)
        {
            return Channel.GetBSINOtherInformation(bSINOtherInformationId);
        }

        public BSINOtherInformation[] GetAllBSINOtherInformations()
        {
            var result = Channel.GetAllBSINOtherInformations();
            return result;
        }

        public IEnumerable<BSCaption> GetAllBsPlCaptions()
        {

            return Channel.GetAllBsPlCaptions();
        }

        #endregion

        #region BSINOtherInformationTotalLine

        public BSINOtherInformationTotalLine UpdateBSINOtherInformationTotalLine(BSINOtherInformationTotalLine bSINOtherInformationTotalLine)
        {
            return Channel.UpdateBSINOtherInformationTotalLine(bSINOtherInformationTotalLine);
        }

        public void DeleteBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            Channel.DeleteBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);
        }

        public BSINOtherInformationTotalLine GetBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            return Channel.GetBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);
        }

        public BSINOtherInformationTotalLine[] GetAllBSINOtherInformationTotalLines()
        {
            var result = Channel.GetAllBSINOtherInformationTotalLines();
            return result;
        }

        public IEnumerable<BSCaption> GetAllBsPlOtherInfoCaptions()
        {

            return Channel.GetAllBsPlOtherInfoCaptions();
        }

        #endregion



    }
}
