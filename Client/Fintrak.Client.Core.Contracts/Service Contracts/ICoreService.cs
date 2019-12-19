using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Client.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Core.Framework;
using audit = Fintrak.Shared.AuditService;
using Fintrak.Shared.Common.Services.QueryService;

namespace Fintrak.Client.Core.Contracts
{
    [ServiceContract]
    public interface ICoreService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        [OperationContract]
        UInt64 GetTotalRecordsCount(string tableName, string columnName, string searchParamS, Double? searchParamN);

        #region FiscalYear

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FiscalYear UpdateFiscalYear(FiscalYear fiscalYear);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFiscalYear(int fiscalYearId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FiscalYear GetFiscalYear(int fiscalYearId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FiscalYear GetOpenFiscalYear();

        [OperationContract]
        FiscalYear[] GetAllFiscalYears();

        #endregion

        #region FiscalPeriod

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FiscalPeriod UpdateFiscalPeriod(FiscalPeriod fiscalPeriod);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFiscalPeriod(int fiscalPeriodId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FiscalPeriod GetFiscalPeriod(int fiscalPeriodId);

        [OperationContract]
        FiscalPeriod[] GetAllFiscalPeriods();

        [OperationContract]
        FiscalPeriodData GetOpenFiscalPeriod();

        [OperationContract]
        FiscalPeriodData[] GetFiscalPeriodByYear(int fiscalYearId);

        #endregion

        #region ProductType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductType UpdateProductType(ProductType productType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductType(int productTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductType GetProductType(int productTypeId);

        [OperationContract]
        ProductType[] GetAllProductTypes();

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

        [OperationContract]
        Product[] GetAvailableProduct(QueryOptions queryOptions);

        [OperationContract]
        UInt64 GetTotalRecordsCountProduct(string tableName, string searchParam);

        #endregion

        #region ProductTypeMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductTypeMapping UpdateProductTypeMapping(ProductTypeMapping productTypeMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductTypeMapping(int productTypeMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductTypeMapping GetProductTypeMapping(int productTypeMappingId);

        [OperationContract]
        ProductTypeMappingData[] GetProductTypeMappingByProduct(string productCode);

        #endregion

        #region ChartOfAccount

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ChartOfAccount UpdateChartOfAccount(ChartOfAccount chartOfAccount);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteChartOfAccount(int chartOfAccountId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ChartOfAccount GetChartOfAccount(int chartOfAccountId);

        [OperationContract]
        ChartOfAccount[] GetAllChartOfAccounts();

        [OperationContract]
        ChartOfAccountData[] GetChartOfAccounts();

        #endregion

        #region Currency

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Currency UpdateCurrency(Currency currency);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCurrency(int currencyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Currency GetCurrency(int currencyId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Currency[] GetBaseCurrency();

        [OperationContract]
        Currency[] GetAllCurrencies();

        #endregion

        #region RateType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RateType UpdateRateType(RateType rateType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRateType(int rateTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RateType GetRateType(int rateTypeId);

        [OperationContract]
        RateType[] GetAllRateTypes();

        #endregion

        #region CurrencyRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CurrencyRate UpdateCurrencyRate(CurrencyRate currencyRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCurrencyRate(int currencyRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CurrencyRate GetCurrencyRate(int currencyRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CurrencyRate[] GetCurrencyRateByDate(string curSymbol);

        [OperationContract]
        CurrencyRateData[] GetCurrencyRates(int currencyId);

        #endregion

        #region Branch

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Branch UpdateBranch(Branch branch);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBranch(int branchId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Branch GetBranch(int branchId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Branch GetBranchByCode(string code);

        [OperationContract]
        Branch[] GetAllBranches();

        #endregion

        #region DefaultUser

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DefaultUser UpdateDefaultUser(DefaultUser defaultUser);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDefaultUser(int defaultUserId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DefaultUser GetDefaultUser(int defaultUserId);

        [OperationContract]
        DefaultUserData[] GetAllDefaultUsers();

        #endregion DefaultUser

        #region FinancialType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FinancialType UpdateFinancialType(FinancialType financialType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFinancialType(int financialTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FinancialType GetFinancialType(int financialTypeId);

        [OperationContract]
        FinancialTypeData[] GetFinancialTypes();

        #endregion

        #region GLDefinition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GLDefinition UpdateGLDefinition(GLDefinition glDefinition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGLDefinition(int glDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GLDefinition GetGLDefinition(int glDefinitionId);

        [OperationContract]
        GLDefinition[] GetAllGLDefinitions();

        #endregion

        #region Staff

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Staff UpdateStaff(Staff staff);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaff(int staffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Staff GetStaff(int staffId);

        [OperationContract]
        Staff[] GetAllStaffs();

        #endregion

        #region PayGrade

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PayGrade UpdatePayGrade(PayGrade payGrade);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePayGrade(int payGradeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PayGrade GetPayGrade(int payGradeId);

        [OperationContract]
        PayGrade[] GetAllPayGrades();

        #endregion

        #region AuditTrail

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        audit.AuditTrail GetAuditTrail(long auditTrailId);

        [OperationContract]
        audit.AuditTrail[] GetAllAuditTrails();

        [OperationContract]
        audit.AuditTrail[] GetAuditTrails(DateTime fromDate, DateTime toDate);

        [OperationContract]
        audit.AuditTrail[] GetAuditTrailByTable(string tableName, DateTime fromDate, DateTime toDate);

        [OperationContract]
        audit.AuditTrail[] GetAuditTrailByLoginID(string userName, DateTime fromDate, DateTime toDate);

        [OperationContract]
        audit.AuditTrail[] GetAuditTrailByAction(string action, DateTime fromDate, DateTime toDate);

        [OperationContract]
        audit.AuditTrail[] GetAuditTrailByTab(audit.AuditAction action);

        #endregion

        #region ReportStatus

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ReportStatus UpdateReportStatus(ReportStatus reportStatus);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteReportStatus(int statusId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ReportStatus GetReportStatus(int statusId);

        [OperationContract]
        ReportStatusData[] GetAllReportStatus();

        #endregion ReportStatus

    }
}
