using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class AbcRatio : ObjectBase
    {
        int _AbcRatioId;
        string _Branch;
        double _Percentage;
        bool _Active;

        public int AbcRatioId
        {
            get { return _AbcRatioId; }
            set
            {
                if (_AbcRatioId != value)
                {
                    _AbcRatioId = value;
                    OnPropertyChanged(() => AbcRatioId);
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


        public double Percentage
        {
            get { return _Percentage; }
            set
            {
                if (_Percentage != value)
                {
                    _Percentage = value;
                    OnPropertyChanged(() => Percentage);
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


        class AbcRatioValidator : AbstractValidator<AbcRatio>
        {
            public AbcRatioValidator()
            {
                RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");




            }
        }

        protected override IValidator GetValidator()
        {
            return new AbcRatioValidator();
        }

    }
}
