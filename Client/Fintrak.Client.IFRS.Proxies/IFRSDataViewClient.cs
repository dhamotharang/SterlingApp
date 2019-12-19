using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IIFRSDataViewService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRSDataViewClient : UserClientBase<IIFRSDataViewService>, IIFRSDataViewService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region BondComputationResult


        public BondComputation[] GetBondComputationResultDistinctRefNo()
        {
            return Channel.GetBondComputationResultDistinctRefNo();
        }

        public BondComputation[] GetBondComputationResultbyRefNo(string refNo, DateTime? Date, string path)
        {
            return Channel.GetBondComputationResultbyRefNo(refNo, Date, path);
        }

        public BondComputation[] GetAllBondComputations()
        {
            return Channel.GetAllBondComputations();
        }

        public BondComputation[] GetRefNoBondComputation()
        {
            return Channel.GetRefNoBondComputation();
        }

        public string[] GetDistinctRefNo()
        {
            return Channel.GetDistinctRefNo();
        }
       

        #endregion

        #region BondPeriodicSchedule


        public BondPeriodicSchedule[] GetBondPeriodicScheduleDistinctRefNo()
        {
            return Channel.GetBondPeriodicScheduleDistinctRefNo();
        }

        public IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos()
        {
            return Channel.GetDistinctBondPeriodicScheduleRefNos();
        }

        public BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return Channel.GetBondPeriodicSchedulebyRefNo(refNo, path);
        }

        public BondPeriodicSchedule[] GetAllBondPeriodicSchedules()
        {
            return Channel.GetAllBondPeriodicSchedules();
        }



        #endregion

        #region BondComputationResultZero


        public IEnumerable<string> GetBondComputationResultZeroDistinctRefNo()
        {
            return Channel.GetBondComputationResultZeroDistinctRefNo();
        }

        public BondComputationResultZero[] GetBondComputationResultZerobyRefNo(string refNo)
        {
            return Channel.GetBondComputationResultZerobyRefNo(refNo);
        }

        public BondComputationResultZero[] GetBondComputationResultZeros()
        {
            return Channel.GetBondComputationResultZeros();
        }

        public BondComputationResultZero[] GetRefNoBondComputationResultZero()
        {
            return Channel.GetRefNoBondComputationResultZero();
        }



        #endregion


        #region LoanDetailedInfo

        public LoanPry[] GetPryLoanBySearch(string searchParam)
        {
            return Channel.GetPryLoanBySearch(searchParam);
        }

        public RawLoanDetails[] GetAllLoanDetailsBySearch(string searchParam)
        {
            return Channel.GetAllLoanDetailsBySearch(searchParam);
        }
        public CollateralDetails[] GetCollateralDetailsBySearch(string searchParam)
        {
            return Channel.GetCollateralDetailsBySearch(searchParam);
        }


        public LoanECLResult[] GetLoanECLResultBySearch(string searchParam)
        {
            return Channel.GetLoanECLResultBySearch(searchParam);
        }

        public LoansECLComputationResult[] GetLoansECLComputationResultBySearch(string searchParam, string path)
        {
            return Channel.GetLoansECLComputationResultBySearch(searchParam, path);
        }

        public Cashflow[] GetCashflowBySearch(string searchParam)
        {
            return Channel.GetCashflowBySearch(searchParam);
        }

        public IfrsMonthlyEAD[] GetIfrsMonthlyEADBySearch(string searchParam)
        {
            return Channel.GetIfrsMonthlyEADBySearch(searchParam);
        }


        public string[] GetDistinctLoanDetailsRefNos(int count)
        {
            return Channel.GetDistinctLoanDetailsRefNos(count);
        }

        #endregion

        

        #region LoanPeriodicSchedule


        public LoanPeriodicSchedule[] GetLoanPeriodicScheduleDistinctRefNo()
        {
            return Channel.GetLoanPeriodicScheduleDistinctRefNo();
        }

        public LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return Channel.GetLoanPeriodicSchedulebyRefNo(refNo, path);
        }

        public LoanPeriodicSchedule[] GetAllLoanPeriodicSchedules()
        {
            return Channel.GetAllLoanPeriodicSchedules();
        }

        public LoanPeriodicSchedule[] GetRefNoLoanPeriodicSchedule()
        {
            return Channel.GetRefNoLoanPeriodicSchedule();
        }
        public void DeleteLoanPeriodicSchedulebyRefNo(string refNo)
        {
             Channel.DeleteLoanPeriodicSchedulebyRefNo(refNo);
        }

       public string[] GetLoanPeriodicRefNo()
        {
            return Channel.GetLoanPeriodicRefNo();
        }

        #endregion

        #region LoanSchedule


        public string[] GetDistinctLoanScheduleRefNos()
        {
            return Channel.GetDistinctLoanScheduleRefNos();
        }

        public MultiSelectDropDown[] GetLoanScheduleDistinctRefNo()
        {
            return Channel.GetLoanScheduleDistinctRefNo();
        }

        public LoanSchedule[] GetLoanSchedulebyRefNo(string refNo)
        {
            return Channel.GetLoanSchedulebyRefNo(refNo);
        }

        public LoanSchedule[] GetAllLoanSchedules()
        {
            return Channel.GetAllLoanSchedules();
        }

        //public LoanSchedule[] GetRefNo()
        //{
        //    return Channel.GetRefNo();
        //}

        public LoanSchedule[] GetScheduleRange(string refNo, DateTime? rangeDate, string path)
        {
            return Channel.GetScheduleRange(refNo, rangeDate, path);
        }



        #endregion

        #region LoansImpairmentResult

        public LoansImpairmentResult[] GetAllLoansImpairmentResults()
        {
            return Channel.GetAllLoansImpairmentResults();
        }



        #endregion

        #region TreasuryBills

        public TBillsComputationResult[] GetTBillsByClassification(string classification)
        {
            return Channel.GetTBillsByClassification(classification);
        }

        public TBillsComputationResult[] GetAllTBillsComputationResults()
        {
            return Channel.GetAllTBillsComputationResults();
        }



        #endregion

        #region EquityStocks

        public EquityStockComputationResult[] GetEquityStockByClassification(string classification)
        {
            return Channel.GetEquityStockByClassification(classification);
        }

        public EquityStockComputationResult[] GetAllEquityStocks()
        {
            return Channel.GetAllEquityStocks();
        }



        #endregion

        #region BondConsolidatedData

        public BondConsolidatedData[] GetAllBondConsolidatedData()
        {
            return Channel.GetAllBondConsolidatedData();
        }



        #endregion

        #region LoanConsolidatedData

        public LoanConsolidatedData[] GetAllLoanConsolidatedData()
        {
            return Channel.GetAllLoanConsolidatedData();
        }



        #endregion

        #region LoanConsolidatedDataFSDH

        public LoanConsolidatedDataFSDH[] GetAllLoanConsolidatedDataFSDH()
        {
            return Channel.GetAllLoanConsolidatedDataFSDH();
        }



        #endregion

        #region TbillConsolidatedData

        public TbillConsolidatedData[] GetAllTbillConsolidatedData()
        {
            return Channel.GetAllTbillConsolidatedData();
        }



        #endregion

        #region BondConsolidatedDataOthers

        public BondConsolidatedDataOthers[] GetAllBondConsolidatedDataOthers()
        {
            return Channel.GetAllBondConsolidatedDataOthers();
        }



        #endregion


        #region BorrowingPeriodicSchedule


        public BorrowingPeriodicSchedule[] GetBorrowingPeriodicScheduleDistinctRefNo()
        {
            return Channel.GetBorrowingPeriodicScheduleDistinctRefNo();
        }

        public IEnumerable<string> GetDistinctBorrowingScheduleRefNos()
        {
            return Channel.GetDistinctBorrowingScheduleRefNos();
        }

        public string[] GetDistinctBorrowingPeriodicScheduleRefNos()
        {
            return Channel.GetDistinctBorrowingPeriodicScheduleRefNos();
        }

        public BorrowingPeriodicSchedule[] GetBorrowingPeriodicSchedulebyRefNo(string refNo, string path)
        {
            return Channel.GetBorrowingPeriodicSchedulebyRefNo(refNo, path);
        }

        public BorrowingPeriodicSchedule[] GetAllBorrowingPeriodicSchedules()
        {
            return Channel.GetAllBorrowingPeriodicSchedules();
        }

        public BorrowingPeriodicSchedule[] GetRefNoBorrowingPeriodicSchedule()
        {
            return Channel.GetRefNoBorrowingPeriodicSchedule();
        }
        public void DeleteBorrowingPeriodicSchedulebyRefNo(string refNo)
        {
            Channel.DeleteBorrowingPeriodicSchedulebyRefNo(refNo);
        }

        public string[] GetBorrowingPeriodicRefNo()
        {
            return Channel.GetBorrowingPeriodicRefNo();
        }

        #endregion

        #region BorrowingSchedule


        public BorrowingSchedule[] GetBorrowingScheduleDistinctRefNo()
        {
            return Channel.GetBorrowingScheduleDistinctRefNo();
        }

        public BorrowingSchedule[] GetBorrowingSchedulebyRefNo(string refNo, DateTime? Date, string path)
        {
            return Channel.GetBorrowingSchedulebyRefNo(refNo, Date, path);
        }

        public BorrowingSchedule[] GetAllBorrowingSchedules()
        {
            return Channel.GetAllBorrowingSchedules();
        }

        //public BorrowingSchedule[] GetRefNo()
        //{
        //    return Channel.GetRefNo();
        //}



        #endregion

    }
}
