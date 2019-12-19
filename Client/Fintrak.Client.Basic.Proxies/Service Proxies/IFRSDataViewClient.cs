using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.Basic.Contracts;
using Fintrak.Client.Basic.Entities;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.Basic.Proxies
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

        public BondComputation[] GetBondComputationResultbyRefNo(string refNo)
        {
            return Channel.GetBondComputationResultbyRefNo(refNo);
        }

        public BondComputation[] GetAllBondComputations()
        {
            return Channel.GetAllBondComputations();
        }

       

        #endregion

        #region BondPeriodicSchedule


        public BondPeriodicSchedule[] GetBondPeriodicScheduleDistinctRefNo()
        {
            return Channel.GetBondPeriodicScheduleDistinctRefNo();
        }

        public BondPeriodicSchedule[] GetBondPeriodicSchedulebyRefNo(string refNo)
        {
            return Channel.GetBondPeriodicSchedulebyRefNo(refNo);
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



        #endregion

        #region LoanPeriodicSchedule


        public LoanPeriodicSchedule[] GetLoanPeriodicScheduleDistinctRefNo()
        {
            return Channel.GetLoanPeriodicScheduleDistinctRefNo();
        }

        public LoanPeriodicSchedule[] GetLoanPeriodicSchedulebyRefNo(string refNo)
        {
            return Channel.GetLoanPeriodicSchedulebyRefNo(refNo);
        }

        public LoanPeriodicSchedule[] GetAllLoanPeriodicSchedules()
        {
            return Channel.GetAllLoanPeriodicSchedules();
        }



        #endregion

        #region LoanSchedule


        public LoanSchedule[] GetLoanScheduleDistinctRefNo()
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



       

    }
}
