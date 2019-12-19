using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class RateType : ObjectBase
    {
        int _RateTypeId;
        string _Name;
        bool _Active;

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

        class RateTypeValidator : AbstractValidator<RateType>
        {
            public RateTypeValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
            
            }
        }

        protected override IValidator GetValidator()
        {
            return new RateTypeValidator();
        }
    }
}
