using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class NotchDifference : ObjectBase
    {
        int _NotchDifferenceId;
        string _InitialClassification;
        string _FinalClassification;
        int _StepMovement;
        bool _Active;

        public int NotchDifferenceId
        {
            get { return _NotchDifferenceId; }
            set
            {
                if (_NotchDifferenceId != value)
                {
                    _NotchDifferenceId = value;
                    OnPropertyChanged(() => NotchDifferenceId);
                }
            }
        }

        public string InitialClassification
        {
            get { return _InitialClassification; }
            set
            {
                if (_InitialClassification != value)
                {
                    _InitialClassification = value;
                    OnPropertyChanged(() => InitialClassification);
                }
            }
        }

        public string FinalClassification
        {
            get { return _FinalClassification; }
            set
            {
                if (_FinalClassification != value)
                {
                    _FinalClassification = value;
                    OnPropertyChanged(() => FinalClassification);
                }
            }
        }


        public int StepMovement
        {
            get { return _StepMovement; }
            set
            {
                if (_StepMovement != value)
                {
                    _StepMovement = value;
                    OnPropertyChanged(() => StepMovement);
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


        class NotchDifferenceValidator : AbstractValidator<NotchDifference>
        {
            public NotchDifferenceValidator()
            {
                RuleFor(obj => obj.InitialClassification).NotEmpty().WithMessage("InitialClassification is required.");
                RuleFor(obj => obj.FinalClassification).NotEmpty().WithMessage("FinalClassification is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new NotchDifferenceValidator();
        }
    }
}
