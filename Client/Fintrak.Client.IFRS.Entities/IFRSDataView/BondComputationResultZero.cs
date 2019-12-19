using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BondComputationResultZero : ObjectBase
    {
        int _Id;
        string _RefNo;
        int _Day;
        DateTime _Date;
        double _OpeningBalance;
        double _DailyCoupon;
        double _DailyYield;
        double _AmortizedPremiumDisc;
        double _ClosingBalance;
        double _IRR;
        int _Period;
        string _Year;
        DateTime _RunDate;
        string _CompanyCode;


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

        public double DailyYield
        {
            get { return _DailyYield; }
            set
            {
                if (_DailyYield != value)
                {
                    _DailyYield = value;
                    OnPropertyChanged(() => DailyYield);
                }
            }
        }

        public double AmortizedPremiumDisc
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

        public double IRR
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

        public int Period
        {
            get { return  _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
                }
            }
        }

        public string CompanyCode
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



        class BondComputationResultZeroValidator : AbstractValidator<BondComputationResultZero>
        {
            public BondComputationResultZeroValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BondComputationResultZeroValidator();
        }
    }
}
