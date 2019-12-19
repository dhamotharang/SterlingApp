using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ECLComparism : ObjectBase
    {
        int _ECLComparismId;
        string _RefNo;
        string _Bucket;
        string _Stressed_Bucket;
        double _LifeTimePD;
        double _Stressed_LifeTimePD;
        double _AnnualPD;
        double _EAD;
        double _UndrawnAmount;
        double _BalanceOutstanding;
        double _CashShortFall;
        double _Stressed_CashShortFall;
        double _ImpairmentCharge;
        double _Stressed_ImpairmentCharge;
        double _ExpectedLoss;
        double _Stressed_ExpectedLoss;
        double _DiscountedValue;
        double _Stressed_DiscountedValue;
        bool _Active;

        public int ECLComparismId
        {
            get { return _ECLComparismId; }
            set
            {
                if (_ECLComparismId != value)
                {
                    _ECLComparismId = value;
                    OnPropertyChanged(() => ECLComparismId);
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

        public string Bucket
        {
            get { return _Bucket; }
            set
            {
                if (_Bucket != value)
                {
                    _Bucket = value;
                    OnPropertyChanged(() => Bucket);
                }
            }
        }


        public string Stressed_Bucket
        {
            get { return _Stressed_Bucket; }
            set
            {
                if (_Stressed_Bucket != value)
                {
                    _Stressed_Bucket = value;
                    OnPropertyChanged(() => Stressed_Bucket);
                }
            }
        }

        public double LifeTimePD
        {
            get { return _LifeTimePD; }
            set
            {
                if (_LifeTimePD != value)
                {
                    _LifeTimePD = value;
                    OnPropertyChanged(() => LifeTimePD);
                }
            }
        }


        public double Stressed_LifeTimePD
        {
            get { return _Stressed_LifeTimePD; }
            set
            {
                if (_Stressed_LifeTimePD != value)
                {
                    _Stressed_LifeTimePD = value;
                    OnPropertyChanged(() => Stressed_LifeTimePD);
                }
            }
        }

        public double AnnualPD
        {
            get { return _AnnualPD; }
            set
            {
                if (_AnnualPD != value)
                {
                    _AnnualPD = value;
                    OnPropertyChanged(() => AnnualPD);
                }
            }
        }


        public double EAD
        {
            get { return _EAD; }
            set
            {
                if (_EAD != value)
                {
                    _EAD = value;
                    OnPropertyChanged(() => EAD);
                }
            }
        }

        public double UndrawnAmount
        {
            get { return _UndrawnAmount; }
            set
            {
                if (_UndrawnAmount != value)
                {
                    _UndrawnAmount = value;
                    OnPropertyChanged(() => UndrawnAmount);
                }
            }
        }


        public double BalanceOutstanding
        {
            get { return _BalanceOutstanding; }
            set
            {
                if (_BalanceOutstanding != value)
                {
                    _BalanceOutstanding = value;
                    OnPropertyChanged(() => BalanceOutstanding);
                }
            }
        }

        public double CashShortFall
        {
            get { return _CashShortFall; }
            set
            {
                if (_CashShortFall != value)
                {
                    _CashShortFall = value;
                    OnPropertyChanged(() => CashShortFall);
                }
            }
        }


        public double Stressed_CashShortFall
        {
            get { return _Stressed_CashShortFall; }
            set
            {
                if (_Stressed_CashShortFall != value)
                {
                    _Stressed_CashShortFall = value;
                    OnPropertyChanged(() => Stressed_CashShortFall);
                }
            }
        }

        public double ImpairmentCharge
        {
            get { return _ImpairmentCharge; }
            set
            {
                if (_ImpairmentCharge != value)
                {
                    _ImpairmentCharge = value;
                    OnPropertyChanged(() => ImpairmentCharge);
                }
            }
        }


        public double Stressed_ImpairmentCharge
        {
            get { return _Stressed_ImpairmentCharge; }
            set
            {
                if (_Stressed_ImpairmentCharge != value)
                {
                    _Stressed_ImpairmentCharge = value;
                    OnPropertyChanged(() => Stressed_ImpairmentCharge);
                }
            }
        }

        public double ExpectedLoss
        {
            get { return _ExpectedLoss; }
            set
            {
                if (_ExpectedLoss != value)
                {
                    _ExpectedLoss = value;
                    OnPropertyChanged(() => ExpectedLoss);
                }
            }
        }


        public double Stressed_ExpectedLoss
        {
            get { return _Stressed_ExpectedLoss; }
            set
            {
                if (_Stressed_ExpectedLoss != value)
                {
                    _Stressed_ExpectedLoss = value;
                    OnPropertyChanged(() => Stressed_ExpectedLoss);
                }
            }
        }

        public double DiscountedValue
        {
            get { return _DiscountedValue; }
            set
            {
                if (_DiscountedValue != value)
                {
                    _DiscountedValue = value;
                    OnPropertyChanged(() => DiscountedValue);
                }
            }
        }


        public double Stressed_DiscountedValue
        {
            get { return _Stressed_DiscountedValue; }
            set
            {
                if (_Stressed_DiscountedValue != value)
                {
                    _Stressed_DiscountedValue = value;
                    OnPropertyChanged(() => Stressed_DiscountedValue);
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


        class ECLComparismValidator : AbstractValidator<ECLComparism>
        {
            public ECLComparismValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
                RuleFor(obj => obj.Bucket).NotEmpty().WithMessage("Bucket is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ECLComparismValidator();
        }
    }
}
