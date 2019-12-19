using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MonthlyDiscountFactorBond : ObjectBase
    {
        int _MonthlyDiscountFactorBond_Id;
        string _RefNo;
        int _Seq;
        double _EIR;
        double _DF;
        string _FacilityType;
        DateTime _RunDate;
        bool _Active;

        public int MonthlyDiscountFactorBond_Id
        {
            get { return _MonthlyDiscountFactorBond_Id; }
            set
            {
                if (_MonthlyDiscountFactorBond_Id != value)
                {
                    _MonthlyDiscountFactorBond_Id = value;
                    OnPropertyChanged(() => MonthlyDiscountFactorBond_Id);
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


        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (_Seq != value)
                {
                    _Seq = value;
                    OnPropertyChanged(() => Seq);
                }
            }
        }

        public double EIR
        {
            get { return _EIR; }
            set
            {
                if (_EIR != value)
                {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }

        public double DF
        {
            get { return _DF; }
            set
            {
                if (_DF != value)
                {
                    _DF = value;
                    OnPropertyChanged(() => DF);
                }
            }
        }

        public string FacilityType
        {
            get { return _FacilityType; }
            set
            {
                if (_FacilityType != value)
                {
                    _FacilityType = value;
                    OnPropertyChanged(() => FacilityType);
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


        class MonthlyDiscountFactorBondValidator : AbstractValidator<MonthlyDiscountFactorBond>
        {
            public MonthlyDiscountFactorBondValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new MonthlyDiscountFactorBondValidator();
        }
    }
}
