using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class EuroBondSpread : ObjectBase
    {
        int _ID;
        DateTime _ConDate;
        double _ZeroRate;
        string _Classification;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public DateTime ConDate
        {
            get { return _ConDate; }
            set
            {
                if (_ConDate != value)
                {
                    _ConDate = value;
                    OnPropertyChanged(() => ConDate);
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

        public double ZeroRate
        {
            get { return _ZeroRate; }
            set
            {
                if (_ZeroRate != value)
                {
                    _ZeroRate = value;
                    OnPropertyChanged(() => ZeroRate);
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


        class EuroBondSpreadValidator : AbstractValidator<EuroBondSpread>
        {
            public EuroBondSpreadValidator()
            {
                //RuleFor(obj => obj._ConDate).NotEmpty().WithMessage("ConDate is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new EuroBondSpreadValidator();
        }
    }
}
