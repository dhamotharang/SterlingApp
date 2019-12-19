using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeMovement : ObjectBase
    {
        int _FeeMovementId;
        string _Name;
        bool _Active;

        public int FeeMovementId
        {
            get { return _FeeMovementId; }
            set
            {
                if (_FeeMovementId != value)
                {
                    _FeeMovementId = value;
                    OnPropertyChanged(() => FeeMovementId);
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

        class FeeMovementValidator : AbstractValidator<FeeMovement>
        {
            public FeeMovementValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeMovementValidator();
        }
    }
}
