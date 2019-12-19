using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanDetailedInfo : ObjectBase
    {
        LoanPry _loanpry;
        RawLoanDetails _loandetails;
        CollateralDetails _collateraldetails;
        LoanSchedule[] _loanschedule;
        LoanECLResult _loaneclresult;
        LoansECLComputationResult[] _loaneclcomputationresult;
        IfrsMonthlyEAD[] _loanmonthlyead;
        Cashflow[] _loancashflow;



        public LoanPry loanpry

        {
            get { return _loanpry; }
            set
            {
                if (_loanpry != value)
                {
                    _loanpry = value;
                    OnPropertyChanged(() => loanpry);
                }
            }
        }



        public RawLoanDetails loandetails
        {
            get { return _loandetails; }
            set
            {
                if (_loandetails != value)
                {
                    _loandetails = value;
                    OnPropertyChanged(() => loandetails);
                }
            }
        }


        public CollateralDetails collateraldetails
        {
            get { return _collateraldetails; }
            set
            {
                if (_collateraldetails != value)
                {
                    _collateraldetails = value;
                    OnPropertyChanged(() => collateraldetails);
                }
            }
        }



        public Cashflow[] loancashflow

        {
            get { return _loancashflow; }
            set
            {
                if (_loancashflow != value)
                {
                    _loancashflow = value;
                    OnPropertyChanged(() => loancashflow);
                }
            }
        }



        public LoanSchedule[] loanschedule

        {
            get { return _loanschedule; }
            set
            {
                if (_loanschedule != value)
                {
                    _loanschedule = value;
                    OnPropertyChanged(() => loanschedule);
                }
            }
        }



        public IfrsMonthlyEAD[] loanmonthlyead

        {
            get { return _loanmonthlyead; }
            set
            {
                if (_loanmonthlyead != value)
                {
                    _loanmonthlyead = value;
                    OnPropertyChanged(() => loanmonthlyead);
                }
            }
        }



        public LoanECLResult loaneclresult

        {
            get { return _loaneclresult; }
            set
            {
                if (_loaneclresult != value)
                {
                    _loaneclresult = value;
                    OnPropertyChanged(() => loaneclresult);
                }
            }
        }



        public LoansECLComputationResult[] loaneclcomputationresult

        {
            get { return _loaneclcomputationresult; }
            set
            {
                if (_loaneclcomputationresult != value)
                {
                    _loaneclcomputationresult = value;
                    OnPropertyChanged(() => loaneclcomputationresult);
                }
            }
        }




        class LoanDetailedInfoValidator : AbstractValidator<LoanDetailedInfo>
        {
            public LoanDetailedInfoValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanDetailedInfoValidator();
        }
    }
}
