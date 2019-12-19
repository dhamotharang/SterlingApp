using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ForeignEADExchangeRate : ObjectBase
    {
        int _ForeignEADExchangeRateId;
        DateTime _IntRate_date;
        string _Currency;
        double _InterestRate;
        string _Reference;
        bool _Active;

        public int ForeignEADExchangeRateId
        {
            get { return _ForeignEADExchangeRateId; }
            set
            {
                if (_ForeignEADExchangeRateId != value)
                {
                    _ForeignEADExchangeRateId = value;
                    OnPropertyChanged(() => ForeignEADExchangeRateId);
                }
            }
        }


        public DateTime IntRate_date
        {
            get { return _IntRate_date; }
            set
            {
                if (_IntRate_date != value)
                {
                    _IntRate_date = value;
                    OnPropertyChanged(() => IntRate_date);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }


        public double InterestRate
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

        public string Reference
        {
            get { return _Reference; }
            set
            {
                if (_Reference != value)
                {
                    _Reference = value;
                    OnPropertyChanged(() => Reference);
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



        class ForeignEADExchangeRateValidator : AbstractValidator<ForeignEADExchangeRate>
        {
            public ForeignEADExchangeRateValidator()
            {
                RuleFor(obj => obj.ForeignEADExchangeRateId).NotEmpty().WithMessage("ForeignEADExchangeRateId is required.");
                RuleFor(obj => obj.InterestRate).NotEmpty().WithMessage("InterestRate is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ForeignEADExchangeRateValidator();
        }
    }
}
