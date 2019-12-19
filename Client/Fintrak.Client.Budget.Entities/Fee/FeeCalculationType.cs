using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeCalculationType : ObjectBase
    {
        int _FeeCalculationTypeId;
        string _VolumeBase;
        string _Name;
        bool _Active;

        public int FeeCalculationTypeId
        {
            get { return _FeeCalculationTypeId; }
            set
            {
                if (_FeeCalculationTypeId != value)
                {
                    _FeeCalculationTypeId = value;
                    OnPropertyChanged(() => FeeCalculationTypeId);
                }
            }
        }

        public string VolumeBase
        {
            get { return _VolumeBase; }
            set
            {
                if (_VolumeBase != value)
                {
                    _VolumeBase = value;
                    OnPropertyChanged(() => VolumeBase);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
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

        class FeeCalculationTypeValidator : AbstractValidator<FeeCalculationType>
        {
            public FeeCalculationTypeValidator()
            {
                RuleFor(obj => obj.VolumeBase).NotEmpty().WithMessage("VolumeBase is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
             
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeCalculationTypeValidator();
        }
    }
}
