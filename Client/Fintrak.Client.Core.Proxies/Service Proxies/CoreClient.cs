using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using coreEntities = Fintrak.Client.Core.Entities;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;
using audit = Fintrak.Shared.AuditService;
using Fintrak.Shared.Common.Services.QueryService;

namespace Fintrak.Client.Core.Proxies
{
    [Export(typeof(ICoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoreClient : UserClientBase<ICoreService>, ICoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        public UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN)
        {
            return Channel.GetTotalRecordsCount(tableName, columnName, searchParamS, searchParamN);
        }

       
        #region FiscalYear

        public FiscalYear UpdateFiscalYear(FiscalYear fiscalYear)
        {
            return Channel.UpdateFiscalYear(fiscalYear);
        }

        public void DeleteFiscalYear(int fiscalYearId)
        {
            Channel.DeleteFiscalYear(fiscalYearId);
        }

        public FiscalYear GetFiscalYear(int fiscalYearId)
        {
            return Channel.GetFiscalYear(fiscalYearId);
        }

        public FiscalYear GetOpenFiscalYear()
        {
            return Channel.GetOpenFiscalYear();
        }

        public FiscalYear[] GetAllFiscalYears()
        {
            return Channel.GetAllFiscalYears();
        }


        #endregion

        #region FiscalPeriod

        public FiscalPeriod UpdateFiscalPeriod(FiscalPeriod fiscalPeriod)
        {
            return Channel.UpdateFiscalPeriod(fiscalPeriod);
        }

        public void DeleteFiscalPeriod(int fiscalPeriodId)
        {
            Channel.DeleteFiscalPeriod(fiscalPeriodId);
        }

        public FiscalPeriod GetFiscalPeriod(int fiscalPeriodId)
        {
            return Channel.GetFiscalPeriod(fiscalPeriodId);
        }

        public FiscalPeriod[] GetAllFiscalPeriods()
        {
            return Channel.GetAllFiscalPeriods();
        }

        public FiscalPeriodData GetOpenFiscalPeriod()
        {
            return Channel.GetOpenFiscalPeriod();
        }

        public FiscalPeriodData[] GetFiscalPeriodByYear(int fiscalYearId)
        {
            return Channel.GetFiscalPeriodByYear(fiscalYearId);
        }


        #endregion

        #region ProductType

        public ProductType UpdateProductType(ProductType productType)
        {
            return Channel.UpdateProductType(productType);
        }

        public void DeleteProductType(int productTypeId)
        {
            Channel.DeleteProductType(productTypeId);
        }

        public ProductType GetProductType(int productTypeId)
        {
            return Channel.GetProductType(productTypeId);
        }

        public ProductType[] GetAllProductTypes()
        {
            return Channel.GetAllProductTypes();
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

        public Product[] GetAvailableProduct(QueryOptions queryOptions)
        {
            return Channel.GetAvailableProduct(queryOptions);
        }

        public UInt64 GetTotalRecordsCountProduct(string tableName, string searchParam)
        {
            return Channel.GetTotalRecordsCountProduct(tableName, searchParam);
        }


        #endregion

        #region ProductTypeMapping

        public ProductTypeMapping UpdateProductTypeMapping(ProductTypeMapping productTypeMapping)
        {
            return Channel.UpdateProductTypeMapping(productTypeMapping);
        }

        public void DeleteProductTypeMapping(int productTypeMappingId)
        {
            Channel.DeleteProductTypeMapping(productTypeMappingId);
        }

        public ProductTypeMapping GetProductTypeMapping(int productTypeMappingId)
        {
            return Channel.GetProductTypeMapping(productTypeMappingId);
        }

        public ProductTypeMappingData[] GetProductTypeMappingByProduct(string productCode)
        {
            return Channel.GetProductTypeMappingByProduct(productCode);
        }


        #endregion

        #region ChartOfAccount

        public ChartOfAccount UpdateChartOfAccount(ChartOfAccount chartOfAccount)
        {
            return Channel.UpdateChartOfAccount(chartOfAccount);
        }

        public void DeleteChartOfAccount(int chartOfAccountId)
        {
            Channel.DeleteChartOfAccount(chartOfAccountId);
        }

        public ChartOfAccount GetChartOfAccount(int chartOfAccountId)
        {
            return Channel.GetChartOfAccount(chartOfAccountId);
        }

        public ChartOfAccount[] GetAllChartOfAccounts()
        {
            return Channel.GetAllChartOfAccounts();
        }

        public ChartOfAccountData[] GetChartOfAccounts()
        {
            return Channel.GetChartOfAccounts();
        }

        #endregion

        #region RateType

        public RateType UpdateRateType(RateType RateType)
        {
            return Channel.UpdateRateType(RateType);
        }

        public void DeleteRateType(int RateTypeId)
        {
            Channel.DeleteRateType(RateTypeId);
        }

        public RateType GetRateType(int RateTypeId)
        {
            return Channel.GetRateType(RateTypeId);
        }

        public RateType[] GetAllRateTypes()
        {
            return Channel.GetAllRateTypes();
        }


        #endregion

        #region Currency

        public Currency UpdateCurrency(Currency currency)
        {
            return Channel.UpdateCurrency(currency);
        }

        public void DeleteCurrency(int currencyId)
        {
            Channel.DeleteCurrency(currencyId);
        }

        public Currency GetCurrency(int currencyId)
        {
            return Channel.GetCurrency(currencyId);
        }

        public Currency[] GetBaseCurrency()
        {
            return Channel.GetBaseCurrency();
        }

        public Currency[] GetAllCurrencies()
        {
            return Channel.GetAllCurrencies();
        }


        #endregion

        #region CurrencyRate

        public CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate)
        {
            return Channel.UpdateCurrencyRate(currencyRate);
        }

        public void DeleteCurrencyRate(int currencyRateId)
        {
            Channel.DeleteCurrencyRate(currencyRateId);
        }

        public CurrencyRate GetCurrencyRate(int currencyRateId)
        {
            return Channel.GetCurrencyRate(currencyRateId);
        }

        public CurrencyRateData[] GetCurrencyRates(int currencyId)
        {
            return Channel.GetCurrencyRates(currencyId);
        }

        public CurrencyRate[] GetCurrencyRateByDate(string curSymbol)
        {
            return Channel.GetCurrencyRateByDate(curSymbol);
        }
        
        #endregion

        #region Branch

        public Branch UpdateBranch(Branch branch)
        {
            return Channel.UpdateBranch(branch);
        }

        public void DeleteBranch(int branchId)
        {
            Channel.DeleteBranch(branchId);
        }

        public Branch GetBranch(int branchId)
        {
            return Channel.GetBranch(branchId);
        }

        public Branch GetBranchByCode(string code)
        {
            return Channel.GetBranchByCode(code);
        }

        public Branch[] GetAllBranches()
        {
            return Channel.GetAllBranches();
        }


        #endregion

        #region DefaultUser

        public DefaultUser UpdateDefaultUser(DefaultUser defaultUser)
        {
            return Channel.UpdateDefaultUser(defaultUser);
        }

        public void DeleteDefaultUser(int defaultUserId)
        {
            Channel.DeleteDefaultUser(defaultUserId);
        }

        public DefaultUser GetDefaultUser(int defaultUserId)
        {
            return Channel.GetDefaultUser(defaultUserId);
        }

        public DefaultUserData[] GetAllDefaultUsers()
        {
            return Channel.GetAllDefaultUsers();
        }


        #endregion

        #region FinancialType

        public FinancialType UpdateFinancialType(FinancialType financialType)
        {
            return Channel.UpdateFinancialType(financialType);
        }

        public void DeleteFinancialType(int financialTypeId)
        {
            Channel.DeleteFinancialType(financialTypeId);
        }

        public FinancialType GetFinancialType(int financialTypeId)
        {
            return Channel.GetFinancialType(financialTypeId);
        }

        public FinancialTypeData[] GetFinancialTypes()
        {
            return Channel.GetFinancialTypes();
        }

        #endregion

        #region GLDefinition

        public GLDefinition UpdateGLDefinition(GLDefinition glDefinition)
        {
            return Channel.UpdateGLDefinition(glDefinition);
        }

        public void DeleteGLDefinition(int glDefinitionId)
        {
            Channel.DeleteGLDefinition(glDefinitionId);
        }

        public GLDefinition GetGLDefinition(int glDefinitionId)
        {
            return Channel.GetGLDefinition(glDefinitionId);
        }

        public GLDefinition[] GetAllGLDefinitions()
        {
            return Channel.GetAllGLDefinitions();
        }



        #endregion

        #region Staff

        public Staff UpdateStaff(Staff staff)
        {
            return Channel.UpdateStaff(staff);
        }

        public void DeleteStaff(int staffId)
        {
            Channel.DeleteStaff(staffId);
        }

        public Staff GetStaff(int staffId)
        {
            return Channel.GetStaff(staffId);
        }

        public Staff[] GetAllStaffs()
        {
            return Channel.GetAllStaffs();
        }


        #endregion

        #region PayGrade

        public PayGrade UpdatePayGrade(PayGrade payGrade)
        {
            return Channel.UpdatePayGrade(payGrade);
        }

        public void DeletePayGrade(int payGradeId)
        {
            Channel.DeletePayGrade(payGradeId);
        }

        public PayGrade GetPayGrade(int payGradeId)
        {
            return Channel.GetPayGrade(payGradeId);
        }

        public PayGrade[] GetAllPayGrades()
        {
            return Channel.GetAllPayGrades();
        }


        #endregion

        #region AuditTrail

        public audit.AuditTrail GetAuditTrail(long auditTrailId)
        {
            return Channel.GetAuditTrail(auditTrailId);
        }

        public audit.AuditTrail[] GetAllAuditTrails()
        {
            return Channel.GetAllAuditTrails();
        }

        public audit.AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrails(fromDate, toDate);
        }

        public audit.AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByTable(tableName, fromDate, toDate);
        }

        public audit.AuditTrail[] GetAuditTrailByLoginID(string userName, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByLoginID(userName, fromDate, toDate);
        }

        public audit.AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate)
        {
            return Channel.GetAuditTrailByAction(action, fromDate, toDate);
        }

        public audit.AuditTrail[] GetAuditTrailByTab(audit.AuditAction action)
        {
            return Channel.GetAuditTrailByTab(action);
        }

        #endregion

        #region ReportStatus

        public ReportStatus UpdateReportStatus(ReportStatus reportStatus)
        {
            return Channel.UpdateReportStatus(reportStatus);
        }

        public void DeleteReportStatus(int reportStatusId)
        {
            Channel.DeleteReportStatus(reportStatusId);
        }

        public ReportStatus GetReportStatus(int reportStatusId)
        {
            return Channel.GetReportStatus(reportStatusId);
        }
        public ReportStatusData[] GetAllReportStatus()
        {
            return Channel.GetAllReportStatus();
        }


        #endregion
    }
}
