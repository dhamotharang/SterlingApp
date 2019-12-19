using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class StaffLoansComputationResult : ObjectBase
    {
        int _StaffLoan_Id;
        string _RefNo;
        string _Name;
        string _ProductType;
        DateTime _ValueDate;
        double _Amount;
        double _Rate;
        double _PrimeLendingRate;
        double _TenorYear;
        double _TenorMonth;
        double _PeriodPaid;
        double _OutstandPeriod;
        double _Payments;
        double _DiscountMarketValue;
        double _DiscountDifference;
        double _AmountRecoMonthly;
        double _PayRecoTotal;
        double _StraightLineExpense;
        double _InterestRecoIFRS;
        double _InterestRecoNGAAP;
        double _InterestDifferential;
        DateTime _Rundate;
        bool _Active;

        public int StaffLoan_Id
        {
            get { return _StaffLoan_Id; }
            set
            {
                if (_StaffLoan_Id != value)
                {
                    _StaffLoan_Id = value;
                    OnPropertyChanged(() => StaffLoan_Id);
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

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }

        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }

        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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

        public double PrimeLendingRate
        {
            get { return _PrimeLendingRate; }
            set
            {
                if (_PrimeLendingRate != value)
                {
                    _PrimeLendingRate = value;
                    OnPropertyChanged(() => PrimeLendingRate);
                }
            }
        }

        public double TenorYear
        {
            get { return _TenorYear; }
            set
            {
                if (_TenorYear != value)
                {
                    _TenorYear = value;
                    OnPropertyChanged(() => TenorYear);
                }
            }
        }

        public double TenorMonth
        {
            get { return _TenorMonth; }
            set
            {
                if (_TenorMonth != value)
                {
                    _TenorMonth = value;
                    OnPropertyChanged(() => TenorMonth);
                }
            }
        }

        public double PeriodPaid
        {
            get { return _PeriodPaid; }
            set
            {
                if (_PeriodPaid != value)
                {
                    _PeriodPaid = value;
                    OnPropertyChanged(() => PeriodPaid);
                }
            }
        }

        public double OutstandPeriod
        {
            get { return _OutstandPeriod; }
            set
            {
                if (_OutstandPeriod != value)
                {
                    _OutstandPeriod = value;
                    OnPropertyChanged(() => OutstandPeriod);
                }
            }
        }

        public double Payments
        {
            get { return _Payments; }
            set
            {
                if (_Payments != value)
                {
                    _Payments = value;
                    OnPropertyChanged(() => Payments);
                }
            }
        }

        public double DiscountMarketValue
        {
            get { return _DiscountMarketValue; }
            set
            {
                if (_DiscountMarketValue != value)
                {
                    _DiscountMarketValue = value;
                    OnPropertyChanged(() => DiscountMarketValue);
                }
            }
        }

        public double DiscountDifference
        {
            get { return _DiscountDifference; }
            set
            {
                if (_DiscountDifference != value)
                {
                    _DiscountDifference = value;
                    OnPropertyChanged(() => DiscountDifference);
                }
            }
        }

        public double AmountRecoMonthly
        {
            get { return _AmountRecoMonthly; }
            set
            {
                if (_AmountRecoMonthly != value)
                {
                    _AmountRecoMonthly = value;
                    OnPropertyChanged(() => AmountRecoMonthly);
                }
            }
        }

        public double PayRecoTotal
        {
            get { return _PayRecoTotal; }
            set
            {
                if (_PayRecoTotal != value)
                {
                    _PayRecoTotal = value;
                    OnPropertyChanged(() => PayRecoTotal);
                }
            }
        }

        public double StraightLineExpense
        {
            get { return _StraightLineExpense; }
            set
            {
                if (_StraightLineExpense != value)
                {
                    _StraightLineExpense = value;
                    OnPropertyChanged(() => StraightLineExpense);
                }
            }
        }

        public double InterestRecoIFRS
        {
            get { return _InterestRecoIFRS; }
            set
            {
                if (_InterestRecoIFRS != value)
                {
                    _InterestRecoIFRS = value;
                    OnPropertyChanged(() => InterestRecoIFRS);
                }
            }
        }

        public double InterestRecoNGAAP
        {
            get { return _InterestRecoNGAAP; }
            set
            {
                if (_InterestRecoNGAAP != value)
                {
                    _InterestRecoNGAAP = value;
                    OnPropertyChanged(() => InterestRecoNGAAP);
                }
            }
        }

        public double InterestDifferential
        {
            get { return _InterestDifferential; }
            set
            {
                if (_InterestDifferential != value)
                {
                    _InterestDifferential = value;
                    OnPropertyChanged(() => InterestDifferential);
                }
            }
        }

        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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

        class StaffLoansComputationResultValidator : AbstractValidator<StaffLoansComputationResult>
        {
            public StaffLoansComputationResultValidator()
            {
                //RuleFor(obj => obj.Agency).NotEmpty().WithMessage("Agency is required.");
                //RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new StaffLoansComputationResultValidator();
        }
    }
}
