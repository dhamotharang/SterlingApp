using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Budget.Entities
{
    public class CurrencyRate : TransactionObjectDoubleBase
    {
        int _CurrencyRateId;
        int _CurrencyCode;
        int _RateType;
        string _ReviewCode; 
        string _Year;
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

        public int CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                if (_CurrencyCode != value)
                {
                    _CurrencyCode = value;
                    OnPropertyChanged(() => CurrencyCode);
                }
            }
        }

        public int RateType
        {
            get { return _RateType; }
            set
            {
                if (_RateType != value)
                {
                    _RateType = value;
                    OnPropertyChanged(() => RateType);
                }
            }
        }

        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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
                RuleFor(obj => obj.CurrencyCode).GreaterThan(0).WithMessage("Currency is required.");
                RuleFor(obj => obj.RateType).GreaterThan(0).WithMessage("Rate Type is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new CurrencyRateValidator();
        }
    }
}
