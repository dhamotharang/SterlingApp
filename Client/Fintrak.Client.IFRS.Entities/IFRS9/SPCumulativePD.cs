using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SPCumulativePD : ObjectBase
    {
        int _SPCumulative_Id;
        string _Rating;
        int _Years;
        double _Value;
        bool _Active;

        public int SPCumulative_Id
        {
            get { return _SPCumulative_Id; }
            set
            {
                if (_SPCumulative_Id != value)
                {
                    _SPCumulative_Id = value;
                    OnPropertyChanged(() => SPCumulative_Id);
                }
            }
        }

        public int Years
        {
            get { return _Years; }
            set
            {
                if (_Years != value)
                {
                    _Years = value;
                    OnPropertyChanged(() => Years);
                }
            }
        }

        public double Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged(() => Value);
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


        class SPCumulativePDValidator : AbstractValidator<SPCumulativePD>
        {
            public SPCumulativePDValidator()
            {
                RuleFor(obj => obj.Years).NotEmpty().WithMessage("Year is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new SPCumulativePDValidator();
        }
    }
}
