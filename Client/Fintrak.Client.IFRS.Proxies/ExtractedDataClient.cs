using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IExtractedDataService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtratedDataClient : UserClientBase<IExtractedDataService>, IExtractedDataService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region IFRSBonds

        public IFRSBonds UpdateIFRSBonds(IFRSBonds IFRSBonds)
        {
            return Channel.UpdateIFRSBonds(IFRSBonds);
        }

        public void DeleteIFRSBonds(int bondId)
        {
            Channel.DeleteIFRSBonds(bondId);
        }

        public IFRSBonds GetIFRSBonds(int bondId)
        {
            return Channel.GetIFRSBonds(bondId);
        }

        public IFRSBonds[] GetAllIFRSBonds()
        {
            return Channel.GetAllIFRSBonds();
        }

        public IFRSBonds[] GetBondsByClassification(string classification)
        {
            return Channel.GetBondsByClassification(classification);
        }

        public IFRSBonds[] GetbondsByMaturityDate(DateTime matureDate)
        {
            return Channel.GetbondsByMaturityDate(matureDate);
        }

       public  void UpdatebondsByMaturityDate(DateTime matureDate, decimal cmprice)
        {
             Channel.UpdatebondsByMaturityDate(matureDate, cmprice);
        }
        
        
        #endregion

        #region IFRSTbills

        public IFRSTbills UpdateIFRSTbills(IFRSTbills IFRSTbills)
        {
            return Channel.UpdateIFRSTbills(IFRSTbills);
        }

        public void DeleteIFRSTbills(int tbillId)
        {
            Channel.DeleteIFRSTbills(tbillId);
        }

        public IFRSTbills GetIFRSTbills(int tbillId)
        {
            return Channel.GetIFRSTbills(tbillId);
        }

        public IFRSTbills[] GetAllIFRSTbills()
        {
            return Channel.GetAllIFRSTbills();
        }

        public IFRSTbills[] GetTbillsByClassification(string classification, int type)
        {
            return Channel.GetTbillsByClassification(classification, type);
        }

        public IFRSTbills[] GetTbillsByMaturityDate(DateTime matureDate, int type)
        {
            return Channel.GetTbillsByMaturityDate(matureDate, type);
        }

        public void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice)
        {
            Channel.UpdateTbillsByMaturityDate(matureDate, cmprice);
        }

        public IFRSTbills[] GetListByType(int Type)
        {
            return Channel.GetListByType(Type);
        }

        #endregion

        #region LoanPry

        public LoanPry UpdateLoanPry(LoanPry loanPryMoratorium)
        {
            return Channel.UpdateLoanPry(loanPryMoratorium);
        }

        public void DeleteLoanPry(int pryId)
        {
            Channel.DeleteLoanPry(pryId);
        }

        public LoanPry GetLoanPry(int pryId)
        {
            return Channel.GetLoanPry(pryId);
        }

        public LoanPry[] GetAllLoanPry()
        {
            return Channel.GetAllLoanPry();
        }

        public LoanPry[] GetLoanPryByScheduleType(string schType)
        {
            return Channel.GetLoanPryByScheduleType(schType);
        }
        public LoanPry[] GetPryLoanBySearch(string searchParam)
        {
            return Channel.GetPryLoanBySearch(searchParam);
        }

        public LoanPry[] GetPryLoans(int defaultCount, string path)
        {
            return Channel.GetPryLoans(defaultCount, path);
        }


        #endregion

        #region RawLoanDetails

        public RawLoanDetails UpdateRawLoanDetails(RawLoanDetails loanDetails)
        {
            return Channel.UpdateRawLoanDetails(loanDetails);
        }

        public void DeleteRawLoanDetails(int loanDetailld)
        {
            Channel.DeleteRawLoanDetails(loanDetailld);
        }

        public RawLoanDetails GetRawLoanDetails(int loanDetailld)
        {
            return Channel.GetRawLoanDetails(loanDetailld);
        }

        public RawLoanDetails[] GetAllRawLoanDetails(int defaultCount,string path)
        {
            return Channel.GetAllRawLoanDetails(defaultCount,path);
        }

        public RawLoanDetails[] GetAllLoanDetailsBySearch(string searchParam)
        {
            return Channel.GetAllLoanDetailsBySearch(searchParam);
        }

        public void UpdateLoanClassNotch(string refNo, string rating, int stage, string notes)
        {
            Channel.UpdateLoanClassNotch(refNo, rating, stage, notes);
        }

        public void DeleteLoanDetailsNotch(string refNo)
        {
            Channel.DeleteLoanDetailsNotch(refNo);
        }

        public CollateralRecov[] ComputeRecovAmt(string refNo, string collateralType, double collateralValue)
        {
            return Channel.ComputeRecovAmt(refNo, collateralType, collateralValue);
        }

        public RawLoanDetails[] GetAllLoanDetails(int defaultCount, string path)
        {
            return Channel.GetAllLoanDetails(defaultCount, path);
        }

        #endregion

        #region IntegralFee

        public IntegralFee UpdateIntegralFee(IntegralFee integralFee)
        {
            return Channel.UpdateIntegralFee(integralFee);
        }

        public void DeleteIntegralFee(int integralFeeId)
        {
            Channel.DeleteIntegralFee(integralFeeId);
        }

        public IntegralFee GetIntegralFee(int integralFeeId)
        {
            return Channel.GetIntegralFee(integralFeeId);
        }

        public IntegralFee[] GetAllIntegralFee()
        {
            return Channel.GetAllIntegralFee();
        }

        #endregion

        #region IfrsCustomer

        public IfrsCustomer UpdateIfrsCustomer(IfrsCustomer ifrsCustomer)
        {
            return Channel.UpdateIfrsCustomer(ifrsCustomer);
        }

        public void DeleteIfrsCustomer(int customerId)
        {
            Channel.DeleteIfrsCustomer(customerId);
        }

        public IfrsCustomer GetIfrsCustomer(int customerId)
        {
            return Channel.GetIfrsCustomer(customerId);
        }

        public IfrsCustomer[] GetAllIfrsCustomer()
        {
            return Channel.GetAllIfrsCustomer();
        }

        public IfrsCustomer[] GetIfrsCustomerByRating(string rating)
        {
            return Channel.GetIfrsCustomerByRating(rating);
        }

        public IfrsCustomer[] GetCustomerInfoBySearch(string searchParam)
        {
            return Channel.GetCustomerInfoBySearch(searchParam);
        }

        public IfrsCustomer[] GetCustomers(int defaultCount)
        {
            return Channel.GetCustomers(defaultCount);
        }

        #endregion

        #region IfrsCustomerAccount

        public IfrsCustomerAccount UpdateIfrsCustomerAccount(IfrsCustomerAccount ifrsCustomerAccount)
        {
            return Channel.UpdateIfrsCustomerAccount(ifrsCustomerAccount);
        }

        public void DeleteIfrsCustomerAccount(int custAccountId)
        {
            Channel.DeleteIfrsCustomerAccount(custAccountId);
        }

        public IfrsCustomerAccount GetIfrsCustomerAccount(int custAccountId)
        {
            return Channel.GetIfrsCustomerAccount(custAccountId);
        }

        public IfrsCustomerAccount[] GetAllIfrsCustomerAccount()
        {
            return Channel.GetAllIfrsCustomerAccount();
        }
        public string[] GetDistinctSector()
        {
            return Channel.GetDistinctSector();
        }

        #endregion

        #region UnMappedProduct

        public UnMappedProduct[] GetAllUnMappedProducts()
        {
            return Channel.GetAllUnMappedProducts();
        }



        #endregion

        #region LoanPryMoratorium

        public LoanPryMoratorium UpdateLoanPryMoratorium(LoanPryMoratorium loanPryMoratorium)
        {
            return Channel.UpdateLoanPryMoratorium(loanPryMoratorium);
        }

        public void DeleteLoanPryMoratorium(int loanPryMoratoriumId)
        {
            Channel.DeleteLoanPryMoratorium(loanPryMoratoriumId);
        }

        public LoanPryMoratorium GetLoanPryMoratorium(int loanPryMoratoriumId)
        {
            return Channel.GetLoanPryMoratorium(loanPryMoratoriumId);
        }

        public LoanPryMoratorium[] GetAllLoanPryMoratorium()
        {
            return Channel.GetAllLoanPryMoratorium();
        }

       

        #endregion
        
        #region Borrowings

        public Borrowings UpdateBorrowings(Borrowings borrowing)
        {
            return Channel.UpdateBorrowings(borrowing);
        }

        public void DeleteBorrowings(int borrowingId)
        {
            Channel.DeleteBorrowings(borrowingId);
        }

        public Borrowings GetBorrowings(int borrowingId)
        {
            return Channel.GetBorrowings(borrowingId);
        }

        public Borrowings[] GetAllBorrowings()
        {
            return Channel.GetAllBorrowings();
        }
        
        #endregion

        #region OffBalanceSheetExposure

        public OffBalanceSheetExposure UpdateOffBalanceSheetExposure(OffBalanceSheetExposure offBalanceSheetExposure)
        {
            return Channel.UpdateOffBalanceSheetExposure(offBalanceSheetExposure);
        }

        public void DeleteOffBalanceSheetExposure(int obeId)
        {
            Channel.DeleteOffBalanceSheetExposure(obeId);
        }

        public OffBalanceSheetExposure GetOffBalanceSheetExposure(int obeId)
        {
            return Channel.GetOffBalanceSheetExposure(obeId);
        }

        public OffBalanceSheetExposure[] GetAllOffBalanceSheetExposure()
        {
            return Channel.GetAllOffBalanceSheetExposure();
        }

        public OffBalanceSheetExposure[] GetOffBalanceSheetExposureByPortfolio(string portfolio)
        {
            return Channel.GetOffBalanceSheetExposureByPortfolio(portfolio);
        }

        #endregion

        #region Placement

        public Placement UpdatePlacement(Placement placement)
        {
            return Channel.UpdatePlacement(placement);
        }

        public void DeletePlacement(int Placement_Id)
        {
            Channel.DeletePlacement(Placement_Id);
        }

        public Placement GetPlacement(int Placement_Id)
        {
            return Channel.GetPlacement(Placement_Id);
        }

        public Placement[] GetAllPlacements()
        {
            return Channel.GetAllPlacements();
        }

        //public Placement[] GetPlacementByRefNo(string RefNo)
        //{
        //    return Channel.GetPlacementByRefNo(RefNo);
        //}


        #endregion

        #region LoanInterestRate

        public LoanInterestRate UpdateLoanInterestRate(LoanInterestRate loanInterestRate)
        {
            return Channel.UpdateLoanInterestRate(loanInterestRate);
        }

        public void DeleteLoanInterestRate(int LoanInterestRate_Id)
        {
            Channel.DeleteLoanInterestRate(LoanInterestRate_Id);
        }

        public LoanInterestRate GetLoanInterestRate(int LoanInterestRate_Id)
        {
            return Channel.GetLoanInterestRate(LoanInterestRate_Id);
        }

        public LoanInterestRate[] GetAllLoanInterestRates()
        {
            return Channel.GetAllLoanInterestRates();
        }

        //public LoanInterestRate[] GetLoanInterestRateByRefNo(string RefNo)
        //{
        //    return Channel.GetLoanInterestRateByRefNo(RefNo);
        //}


        #endregion

        #region DepositRepaymentSchedule

        public DepositRepaymentSchedule UpdateDepositRepaymentSchedule(DepositRepaymentSchedule depositRepaymentSchedule)
        {
            return Channel.UpdateDepositRepaymentSchedule(depositRepaymentSchedule);
        }

        public void DeleteDepositRepaymentSchedule(int depositRepayId)
        {
            Channel.DeleteDepositRepaymentSchedule(depositRepayId);
        }

        public DepositRepaymentSchedule GetDepositRepaymentSchedule(int depositRepayId)
        {
            return Channel.GetDepositRepaymentSchedule(depositRepayId);
        }

        public DepositRepaymentSchedule[] GetAllDepositRepaymentSchedule()
        {
            return Channel.GetAllDepositRepaymentSchedule();
        }

        public DepositRepaymentSchedule[] GetVarianceData()
        {
            return Channel.GetVarianceData();
        }

        #endregion

        #region LiabilityRepaymentSchedule

        public LiabilityRepaymentSchedule UpdateLiabilityRepaymentSchedule(LiabilityRepaymentSchedule liabilityRepaymentSchedule)
        {
            return Channel.UpdateLiabilityRepaymentSchedule(liabilityRepaymentSchedule);
        }

        public void DeleteLiabilityRepaymentSchedule(int liabilityRepayId)
        {
            Channel.DeleteLiabilityRepaymentSchedule(liabilityRepayId);
        }

        public LiabilityRepaymentSchedule GetLiabilityRepaymentSchedule(int liabilityRepayId)
        {
            return Channel.GetLiabilityRepaymentSchedule(liabilityRepayId);
        }

        public LiabilityRepaymentSchedule[] GetAllLiabilityRepaymentSchedule()
        {
            return Channel.GetAllLiabilityRepaymentSchedule();
        }

        #endregion

        #region LoanCommitment

        public LoanCommitment UpdateLoanCommitment(LoanCommitment loanCommitment)
        {
            return Channel.UpdateLoanCommitment(loanCommitment);
        }

        public void DeleteLoanCommitment(int LoanCommitmentId)
        {
            Channel.DeleteLoanCommitment(LoanCommitmentId);
        }

        public LoanCommitment GetLoanCommitment(int LoanCommitmentId)
        {
            return Channel.GetLoanCommitment(LoanCommitmentId);
        }

        public LoanCommitment[] GetAllLoanCommitments()
        {
            return Channel.GetAllLoanCommitments();
        }

        //public LoanCommitment[] GetLoanCommitmentByRefNo(string RefNo)
        //{
        //    return Channel.GetLoanCommitmentByRefNo(RefNo);
        //}


        #endregion

        #region InputDetail

        public InputDetail UpdateInputDetail(InputDetail inputDetail)
        {
            return Channel.UpdateInputDetail(inputDetail);
        }

        public void DeleteInputDetail(int InputDetailId)
        {
            Channel.DeleteInputDetail(InputDetailId);
        }

        public InputDetail GetInputDetail(int InputDetailId)
        {
            return Channel.GetInputDetail(InputDetailId);
        }

        public InputDetail[] GetAllInputDetails()
        {
            return Channel.GetAllInputDetails();
        }

        public EclWeightedAvg[] GetAllEclWeightedAvgs()
        {
            return Channel.GetAllEclWeightedAvgs();
        }

        public int InsertByRefno(string refNo)
        {
            return Channel.InsertByRefno(refNo);
        }

        public void ComputeEcl()
        {
            Channel.ComputeEcl();
        }

        #endregion

        #region NseIndex

        public NseIndex UpdateNseIndex(NseIndex nseIndex)
        {
            return Channel.UpdateNseIndex(nseIndex);
        }

        public void DeleteNseIndex(int NseIndexId)
        {
            Channel.DeleteNseIndex(NseIndexId);
        }

        public NseIndex GetNseIndex(int NseIndexId)
        {
            return Channel.GetNseIndex(NseIndexId);
        }

        public NseIndex[] GetAllNseIndexs()
        {
            return Channel.GetAllNseIndexs();
        }

        public ProbabilityWeight[] GetAllProbabilityWeights()
        {
            return Channel.GetAllProbabilityWeights();
        }

        public void ComputeProbabilityWeight(double lOC)
        {
            Channel.ComputeProbabilityWeight(lOC);
        }

        #endregion

        #region OBExposure

        public OBExposure UpdateOBExposure(OBExposure OBExposure)
        {
            return Channel.UpdateOBExposure(OBExposure);
        }

        public void DeleteOBExposure(int obe_Id)
        {
            Channel.DeleteOBExposure(obe_Id);
        }

        public OBExposure GetOBExposurebyId(int obeId)
        {
            return Channel.GetOBExposurebyId(obeId);
        }
        public OBExposure[] GetOBExposure(int flag, int defaultCount, string path)
        {
            return Channel.GetOBExposure(flag, defaultCount, path);
        }

        public OBExposure[] GetOBExposureBySearch(int flag, string searchParam)
        {
            return Channel.GetOBExposureBySearch(flag, searchParam);
        }

        public string[] GetProductTypes()
        {
            return Channel.GetProductTypes();
        }

        public string[] GetsubTypes(string productType)
        {
            return Channel.GetsubTypes(productType);
        }
        #endregion

        #region OBExposureCCF

        public OBExposureCCF UpdateOBExposureCCF(OBExposureCCF OBExposureCCF)
        {
            return Channel.UpdateOBExposureCCF(OBExposureCCF);
        }

        public void DeleteOBExposureCCF(int obe_Id)
        {
            Channel.DeleteOBExposureCCF(obe_Id);
        }

        public OBExposureCCF GetOBExposureCCFbyId(int obeId)
        {
            return Channel.GetOBExposureCCFbyId(obeId);
        }
        public OBExposureCCF[] GetOBExposureCCF(int flag, int defaultCount, string path)
        {
            return Channel.GetOBExposureCCF(flag, defaultCount, path);
        }

        public OBExposureCCF[] GetOBExposureCCFBySearch(int flag, string searchParam)
        {
            return Channel.GetOBExposureCCFBySearch(flag, searchParam);
        }
        #endregion

        #region CollateralDetails

        public CollateralDetails UpdateCollateralDetails(CollateralDetails CollateralDetails)
        {
            return Channel.UpdateCollateralDetails(CollateralDetails);
        }

        public void DeleteCollateralDetails(int obe_Id)
        {
            Channel.DeleteCollateralDetails(obe_Id);
        }

        public CollateralDetails[] GetCollateralDetails(int defaultCount, string path)
        {
            return Channel.GetCollateralDetails(defaultCount, path);
        }

        public CollateralDetails GetCollateralDetailsById(int colDetsId)
        {
            return Channel.GetCollateralDetailsById(colDetsId);
        }
        public CollateralDetails[] GetCollateralDetailsBySearch(string searchParam)
        {
            return Channel.GetCollateralDetailsBySearch(searchParam);
        }
        #endregion

        #region FacClassConsolidated

        public FacClassConsolidated UpdateFacClassConsolidated(FacClassConsolidated FacClassConsolidated)
        {
            return Channel.UpdateFacClassConsolidated(FacClassConsolidated);
        }

        public void DeleteFacClassConsolidated(int obe_Id)
        {
            Channel.DeleteFacClassConsolidated(obe_Id);
        }

        public FacClassConsolidated[] GetFacClassConsolidated(int defaultCount, string path)
        {
            return Channel.GetFacClassConsolidated(defaultCount, path);
        }

        public FacClassConsolidated GetFacClassConsolidatedById(int colDetsId)
        {
            return Channel.GetFacClassConsolidatedById(colDetsId);
        }
        public FacClassConsolidated[] GetFacClassConsolidatedBySearch(string searchParam)
        {
            return Channel.GetFacClassConsolidatedBySearch(searchParam);
        }
        #endregion


        #region HCClassification

        public HCClassification[] GetAllHCClassification()
        {
            return Channel.GetAllHCClassification();
        }


        public HCClassification UpdateHCClassification(HCClassification HCClassification)
        {
            return Channel.UpdateHCClassification(HCClassification);
        }

        public void DeleteHCClassification(int obe_Id)
        {
            Channel.DeleteHCClassification(obe_Id);
        }

        public HCClassification GetHCClassificationById(int Id)
        {
            return Channel.GetHCClassificationById(Id);
        }

        public HCClassification[] GetHCClassificationBySearch(string searchParam)
        {
            return Channel.GetHCClassificationBySearch(searchParam);
        }
        #endregion

        #region FacRating

        public FacRating UpdateFacRating(FacRating FacRating)
        {
            return Channel.UpdateFacRating(FacRating);
        }

        public void DeleteFacRating(int obe_Id)
        {
            Channel.DeleteFacRating(obe_Id);
        }

        public FacRating[] GetFacRating(int defaultCount, string path)
        {
            return Channel.GetFacRating(defaultCount, path);
        }

        public FacRating GetFacRatingById(int facId)
        {
            return Channel.GetFacRatingById(facId);
        }
        public FacRating[] GetFacRatingBySearch(string searchParam)
        {
            return Channel.GetFacRatingBySearch(searchParam);
        }
        #endregion

        #region FacStaging

        public FacStaging UpdateFacStaging(FacStaging FacStaging)
        {
            return Channel.UpdateFacStaging(FacStaging);
        }

        public void DeleteFacStaging(int obe_Id)
        {
            Channel.DeleteFacStaging(obe_Id);
        }

        public FacStaging[] GetFacStaging(int defaultCount, string path)
        {
            return Channel.GetFacStaging(defaultCount, path);
        }

        public FacStaging GetFacStagingById(int facId)
        {
            return Channel.GetFacStagingById(facId);
        }
        public FacStaging[] GetFacStagingBySearch(string searchParam)
        {
            return Channel.GetFacStagingBySearch(searchParam);
        }
        #endregion

        #region FacOBEStaging

        public FacOBEStaging UpdateFacOBEStaging(FacOBEStaging FacOBEStaging)
        {
            return Channel.UpdateFacOBEStaging(FacOBEStaging);
        }

        public void DeleteFacOBEStaging(int obe_Id)
        {
            Channel.DeleteFacOBEStaging(obe_Id);
        }

        public FacOBEStaging[] GetFacOBEStaging(int defaultCount, string path)
        {
            return Channel.GetFacOBEStaging(defaultCount, path);
        }

        public FacOBEStaging GetFacOBEStagingById(int facId)
        {
            return Channel.GetFacOBEStagingById(facId);
        }
        public FacOBEStaging[] GetFacOBEStagingBySearch(string searchParam)
        {
            return Channel.GetFacOBEStagingBySearch(searchParam);
        }
        #endregion


        #region Cashflow

        public Cashflow UpdateCashflow(Cashflow loanPryMoratorium)
        {
            return Channel.UpdateCashflow(loanPryMoratorium);
        }

        public void DeleteCashflow(int cashflowId)
        {
            Channel.DeleteCashflow(cashflowId);
        }

        public Cashflow GetCashflow(int cashflowId)
        {
            return Channel.GetCashflow(cashflowId);
        }

        public Cashflow[] GetAllCashflow()
        {
            return Channel.GetAllCashflow();
        }

        public Cashflow[] GetCashflowBySearch(string searchParam)
        {
            return Channel.GetCashflowBySearch(searchParam);
        }

        public Cashflow[] GetCashflows(int defaultCount)
        {
            return Channel.GetCashflows(defaultCount);
        }

        #endregion

    }
}
