using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class CurrencyRate : ObjectBase
    {
        int _CurrencyRateId;
        int _CurrencyId;
        int _RateTypeId;
        string _Frequency;
        double _Rate;
        DateTime _Date;
        DateTime? _Rundate;
        bool _Active;

        public int CurrencyRateId
        {
            get { return _CurrencyRateId; }
            set
            {
                if (_CurrencyRateId != value)
                {
                    _CurrencyRateId = value;
                    OnPropertyChanged(() => CurrencyRateId);
                }
            }
        }

        public int CurrencyId
        {
            get { return _CurrencyId; }
            set
            {
                if (_CurrencyId != value)
                {
                    _CurrencyId = value;
                    OnPropertyChanged(() => CurrencyId);
                }
            }
        }

        public int RateTypeId
        {
            get { return _RateTypeId; }
            set
            {
                if (_RateTypeId != value)
                {
                    _RateTypeId = value;
                    OnPropertyChanged(() => RateTypeId);
                }
            }
        }

        public string Frequency
        {
            get { return _Frequency; }
            set
            {
                if (_Frequency != value)
                {
                    _Frequency = value;
                    OnPropertyChanged(() => Frequency);
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

        public DateTime? Rundate
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

        class CurrencyRateValidator : AbstractValidator<CurrencyRate>
        {
            public CurrencyRateValidator()
            {
                RuleFor(obj => obj.CurrencyId).GreaterThan(0).WithMessage("Currency is required.");
                RuleFor(obj => obj.RateTypeId).GreaterThan(0).WithMessage("Rate Type is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new CurrencyRateValidator();
        }
    }
}
