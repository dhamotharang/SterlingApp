using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BondComputation : ObjectBase
    {
        int _Id;
        string _RefNo;
        int _Day;
        DateTime _PaymentDate;
        DateTime _Date;
        double _OpeningBalance;
        double _AmountPrincInit;
        double _DailyCoupon;
        double _DailyCouponLessFV;
        double _DailyInt;
        double _DailyPrinc;
        double _DailyPrincLessFV;
        double? _AmortizedPremiumDisc;
        double _ClosingBalance;
        double _AmountPrincEnd;
        double _AccruedInterest;
        double _AmortizedCost;
        double ? _DiscountPremium = null;
        double ? _UnAmortized =null;
        double ? _Amortized =null;
        decimal _CouponRate;
        decimal _IRR;
        int _NoOfPeriods;
        //DateTime _PostedDate;
        //DateTime _LastRunDate;
        string   _CompanyCode;


        public int Id

        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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

        public int Day
        {
            get { return _Day; }
            set
            {
                if (_Day != value)
                {
                    _Day = value;
                    OnPropertyChanged(() => Day);
                }
            }
        }

        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set
            {
                if (_PaymentDate != value)
                {
                    _PaymentDate = value;
                    OnPropertyChanged(() => PaymentDate);
                }
            }
        }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }

        public double OpeningBalance
        {
            get { return _OpeningBalance; }
            set
            {
                if (_OpeningBalance != value)
                {
                    _OpeningBalance = value;
                    OnPropertyChanged(() => OpeningBalance);
                }
            }
        }


        public double AmountPrincInit
        {
            get { return _AmountPrincInit; }
            set
            {
                if (_AmountPrincInit != value)
                {
                    _AmountPrincInit = value;
                    OnPropertyChanged(() => AmountPrincInit);
                }
            }
        }

        public double DailyCoupon
        {
            get { return _DailyCoupon; }
            set
            {
                if (_DailyCoupon != value)
                {
                    _DailyCoupon = value;
                    OnPropertyChanged(() => DailyCoupon);
                }
            }
        }

        public double DailyCouponLessFV
        {
            get { return _DailyCouponLessFV; }
            set
            {
                if (_DailyCouponLessFV != value)
                {
                    _DailyCouponLessFV = value;
                    OnPropertyChanged(() => DailyCouponLessFV);
                }
            }
        }

        public double DailyInt
        {
            get { return _DailyInt; }
            set
            {
                if (_DailyInt != value)
                {
                    _DailyInt = value;
                    OnPropertyChanged(() => DailyInt);
                }
            }
        }

        public double DailyPrinc
        {
            get { return _DailyPrinc; }
            set
            {
                if (_DailyPrinc != value)
                {
                    _DailyPrinc = value;
                    OnPropertyChanged(() => DailyPrinc);
                }
            }
        }

        public double DailyPrincLessFV
        {
            get { return _DailyPrincLessFV; }
            set
            {
                if (_DailyPrincLessFV != value)
                {
                    _DailyPrincLessFV = value;
                    OnPropertyChanged(() => DailyPrincLessFV);
                }
            }
        }

        public double? AmortizedPremiumDisc
        {
            get { return _AmortizedPremiumDisc; }
            set
            {
                if (_AmortizedPremiumDisc != value)
                {
                    _AmortizedPremiumDisc = value;
                    OnPropertyChanged(() => AmortizedPremiumDisc);
                }
            }
        }

        public double ClosingBalance
        {
            get { return _ClosingBalance; }
            set
            {
                if (_ClosingBalance != value)
                {
                    _ClosingBalance = value;
                    OnPropertyChanged(() => ClosingBalance);
                }
            }
        }

        public double AmountPrincEnd
        {
            get { return _AmountPrincEnd; }
            set
            {
                if (_AmountPrincEnd != value)
                {
                    _AmountPrincEnd = value;
                    OnPropertyChanged(() => AmountPrincEnd);
                }
            }
        }
        public double AccruedInterest
        {
            get { return _AccruedInterest; }
            set
            {
                if (_AccruedInterest != value)
                {
                    _AccruedInterest = value;
                    OnPropertyChanged(() => AccruedInterest);
                }
            }
        }


        public double AmortizedCost
        {
            get { return _AmortizedCost; }
            set
            {
                if (_AmortizedCost != value)
                {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

        public double? DiscountPremium
        {
            get { return _DiscountPremium; }
            set
            {
                if (_DiscountPremium != value)
                {
                    _DiscountPremium = value;
                    OnPropertyChanged(() => DiscountPremium);
                }
            }
        }

        public double? UnAmortized
        {
            get { return _UnAmortized; }
            set
            {
                if (_UnAmortized != value)
                {
                    _UnAmortized = value;
                    OnPropertyChanged(() => UnAmortized);
                }
            }
        }

        public double? Amortized
        {
            get { return _Amortized; }
            set
            {
                if (_Amortized != value)
                {
                    _Amortized = value;
                    OnPropertyChanged(() => Amortized);
                }
            }
        }

        public decimal CouponRate
        {
            get { return _CouponRate; }
            set
            {
                if (_CouponRate != value)
                {
                    _CouponRate = value;
                    OnPropertyChanged(() => CouponRate);
                }
            }
        }


        public decimal IRR
        {
            get { return _IRR; }
            set
            {
                if (_IRR != value)
                {
                    _IRR = value;
                    OnPropertyChanged(() => IRR);
                }
            }
        }

        public int NoOfPeriods
        {
            get { return _NoOfPeriods; }
            set
            {
                if (_NoOfPeriods != value)
                {
                    _NoOfPeriods = value;
                    OnPropertyChanged(() => NoOfPeriods);
                }
            }
        }


        public string  CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }



        class BondComputationValidator : AbstractValidator<BondComputation>
        {
            public BondComputationValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BondComputationValidator();
        }
    }
}
