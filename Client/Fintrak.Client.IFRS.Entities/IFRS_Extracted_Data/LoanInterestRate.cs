using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanInterestRate : ObjectBase
    {
        int _LoanInterestRate_Id;
        string _RefNo;
        string _CustomerName;
        DateTime _Date_PMT;
        DateTime _FPD_Date;
        int _Repayment_Freq;
        double _Rate;
        bool _Active;

        public int LoanInterestRate_Id
        {
            get { return _LoanInterestRate_Id; }
            set
            {
                if (_LoanInterestRate_Id != value)
                {
                    _LoanInterestRate_Id = value;
                    OnPropertyChanged(() => LoanInterestRate_Id);
                }
            }
        }

        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }


        public DateTime Date_PMT
        {
            get { return _Date_PMT; }
            set
            {
                if (_Date_PMT != value)
                {
                    _Date_PMT = value;
                    OnPropertyChanged(() => Date_PMT);
                }
            }
        }

        public DateTime FPD_Date
        {
            get { return _FPD_Date; }
            set
            {
                if (_FPD_Date != value)
                {
                    _FPD_Date = value;
                    OnPropertyChanged(() => FPD_Date);
                }
            }
        }

        public int Repayment_Freq
        {
            get { return _Repayment_Freq; }
            set
            {
                if (_Repayment_Freq != value)
                {
                    _Repayment_Freq = value;
                    OnPropertyChanged(() => Repayment_Freq);
                }
            }
        }

        public double Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class LoanInterestRateValidator : AbstractValidator<LoanInterestRate>
        {
            public LoanInterestRateValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanInterestRateValidator();
        }
    }
}
