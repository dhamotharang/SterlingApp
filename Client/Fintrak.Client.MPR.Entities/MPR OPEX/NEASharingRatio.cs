using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class NEASharingRatio : ObjectBase
    {
        int _NEASharingRatioId;
        string _Branch;
        string _SBUCode;
        decimal _Ratio;
        bool _Active;


        public int NEASharingRatioId
        {
            get { return _NEASharingRatioId; }
            set
            {
                if (_NEASharingRatioId != value)
                {
                    _NEASharingRatioId = value;
                    OnPropertyChanged(() => NEASharingRatioId);
                }
            }
        }

        public string Branch
        {
            get { return _Branch; }
            set
            {
                if (_Branch != value)
                {
                    _Branch = value;
                    OnPropertyChanged(() => Branch);
                }
            }
        }

        public string SBUCode
        {
            get { return _SBUCode; }
            set
            {
                if (_SBUCode != value)
                {
                    _SBUCode = value;
                    OnPropertyChanged(() => SBUCode);
                }
            }
        }


        public decimal Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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



        class NEASharingRatioValidator : AbstractValidator<NEASharingRatio>
        {
            public NEASharingRatioValidator()
            {
                RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
                RuleFor(obj => obj.SBUCode).NotEmpty().WithMessage("SBUCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new NEASharingRatioValidator();
        }
    }
}
