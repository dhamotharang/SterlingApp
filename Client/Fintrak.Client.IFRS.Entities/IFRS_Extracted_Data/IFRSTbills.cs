using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IFRSTbills : ObjectBase
    {
        int _TbillId;
        string _RefNo;
        DateTime _EffectiveDate;
        DateTime _MaturityDate;
        double _CleanPrice;
        double _FaceValue;
        decimal _InterestRate;
        decimal _CurrentMarketYield;
        string _Classification;
        string _Description;
        int _Flag;
        bool _Active;


        public int TbillId

        {
            get { return _TbillId; }
            set
            {
                if (_TbillId != value)
                {
                    _TbillId = value;
                    OnPropertyChanged(() => TbillId);
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

        public DateTime EffectiveDate
        {
            get { return _EffectiveDate; }
            set
            {
                if (_EffectiveDate != value)
                {
                    _EffectiveDate = value;
                    OnPropertyChanged(() => EffectiveDate);
                }
            }
        }

        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
                }
            }
        }

        public double CleanPrice
        {
            get { return _CleanPrice; }
            set
            {
                if (_CleanPrice != value)
                {
                    _CleanPrice = value;
                    OnPropertyChanged(() => CleanPrice);
                }
            }
        }


        public double FaceValue
        {
            get { return _FaceValue; }
            set
            {
                if (_FaceValue != value)
                {
                    _FaceValue = value;
                    OnPropertyChanged(() => FaceValue);
                }
            }
        }

        public decimal InterestRate
        {
            get { return _InterestRate; }
            set
            {
                if (_InterestRate != value)
                {
                    _InterestRate = value;
                    OnPropertyChanged(() => InterestRate);
                }
            }
        }

        public decimal CurrentMarketYield
        {
            get { return _CurrentMarketYield; }
            set
            {
                if (_CurrentMarketYield != value)
                {
                    _CurrentMarketYield = value;
                    OnPropertyChanged(() => CurrentMarketYield);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public int Flag
        {
            get { return _Flag; }
            set
            {
                if (_Flag != value)
                {
                    _Flag = value;
                    OnPropertyChanged(() => Flag);
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
        class IFRSTbillsValidator : AbstractValidator<IFRSTbills>
        {
            public IFRSTbillsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSTbillsValidator();
        }
    }
}
