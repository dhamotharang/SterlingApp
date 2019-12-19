using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class BSExemption : ObjectBase
    {
        int _BSExemptionId;
        string _AccountNo;     
        bool _Active;

        public int BSExemptionId
        {
            get { return _BSExemptionId; }
            set
            {
                if (_BSExemptionId != value)
                {
                    _BSExemptionId = value;
                    OnPropertyChanged(() => BSExemptionId);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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

        
        class BSExemptionValidator : AbstractValidator<BSExemption>
        {
            public BSExemptionValidator()
            {
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("Account is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BSExemptionValidator();
        }
    }
}
