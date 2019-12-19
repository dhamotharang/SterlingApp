using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanSpreadSensitivity : ObjectBase
    {
        int _LoanSpreadSensitivityId;
        string _RefNo;
        string _AccountNo;
        string _Sector;
        string _Rating;
        string _Bucket;
        string _SpreadBy;
        int _Tenor;
        bool _Significant;
        double _DiscountedValue;
        //double _BalanceOutstanding;
        //double _IRR;
        //string _AssessmentType;
        double _LifeTimePD;
        double _PVofCollateral;
        double _CashShortFall;
        double _AnnualPD;
        double _Lgd;
        //double _UndrawnAmount;
        double _EAD;
        double _ImpairmentCharge;
        double _ExpectedLoss;
        bool _Active;

        public int LoanSpreadSensitivityId
        {
            get { return _LoanSpreadSensitivityId; }
            set
            {
                if (_LoanSpreadSensitivityId != value)
                {
                    _LoanSpreadSensitivityId = value;
                    OnPropertyChanged(() => LoanSpreadSensitivityId);
                }
            }
        }

        //public double UndrawnAmount
        //{
        //    get { return _UndrawnAmount; }
        //    set
        //    {
        //        if (_UndrawnAmount != value)
        //        {
        //            _UndrawnAmount = value;
        //            OnPropertyChanged(() => UndrawnAmount);
        //        }
        //    }
        //}

        //public double BalanceOutstanding
        //{
        //    get { return _BalanceOutstanding; }
        //    set
        //    {
        //        if (_BalanceOutstanding != value)
        //        {
        //            _BalanceOutstanding = value;
        //            OnPropertyChanged(() => BalanceOutstanding);
        //        }
        //    }
        //}
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
        public double PVofCollateral
        {
            get { return _PVofCollateral; }
            set
            {
                if (_PVofCollateral != value)
                {
                    _PVofCollateral = value;
                    OnPropertyChanged(() => PVofCollateral);
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
        //public string AssessmentType
        //{
        //    get { return _AssessmentType; }
        //    set
        //    {
        //        if (_AssessmentType != value)
        //        {
        //            _AssessmentType = value;
        //            OnPropertyChanged(() => AssessmentType);
        //        }
        //    }
        //}

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }
        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }

        public string SpreadBy
        {
            get { return _SpreadBy; }
            set
            {
                if (_SpreadBy != value)
                {
                    _SpreadBy = value;
                    OnPropertyChanged(() => SpreadBy);
                }
            }
        }
         public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
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

         public int Tenor
        {
            get { return _Tenor; }
            set
            {
                if (_Tenor != value)
                {
                    _Tenor = value;
                    OnPropertyChanged(() => Tenor);
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

        //public double IRR
        //{
        //    get { return _IRR; }
        //    set
        //    {
        //        if (_IRR != value)
        //        {
        //            _IRR = value;
        //            OnPropertyChanged(() => IRR);
        //        }
        //    }
        //}

         public double Lgd
        {
            get { return _Lgd; }
            set
            {
                if (_Lgd != value)
                {
                    _Lgd = value;
                    OnPropertyChanged(() => Lgd);
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
        public bool Significant
        {
            get { return _Significant; }
            set
            {
                if (_Significant != value)
                {
                    _Significant = value;
                    OnPropertyChanged(() => Significant);
                }
            }
        }

        class LoanSpreadSensitivityValidator : AbstractValidator<LoanSpreadSensitivity>
        {
            public LoanSpreadSensitivityValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanSpreadSensitivityValidator();
        }
    }
}
