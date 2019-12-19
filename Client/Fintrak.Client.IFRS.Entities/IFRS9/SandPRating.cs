using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SandPRating : ObjectBase
    {
        int _SandPRating_Id;
        int _Year;
        double _Rating;
        bool _Active;

        public int SandPRating_Id
        {
            get { return _SandPRating_Id; }
            set
            {
                if (_SandPRating_Id != value)
                {
                    _SandPRating_Id = value;
                    OnPropertyChanged(() => SandPRating_Id);
                }
            }
        }

        public int Year
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
        public double Rating
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


        class SandPRatingValidator : AbstractValidator<SandPRating>
        {
            public SandPRatingValidator()
            {
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new SandPRatingValidator();
        }
    }
}
