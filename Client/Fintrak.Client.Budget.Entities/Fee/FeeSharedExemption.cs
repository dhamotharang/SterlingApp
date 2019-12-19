using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeSharedExemption : ObjectBase
    {
        int _FeeSharedExemptionId;
        string _ItemCode;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeSharedExemptionId
        {
            get { return _FeeSharedExemptionId; }
            set
            {
                if (_FeeSharedExemptionId != value)
                {
                    _FeeSharedExemptionId = value;
                    OnPropertyChanged(() => FeeSharedExemptionId);
                }
            }
        }

        public string ItemCode
        {
            get { return _ItemCode; }
            set
            {
                if (_ItemCode != value)
                {
                    _ItemCode = value;
                    OnPropertyChanged(() => ItemCode);
                }
            }
        }


        public string Year
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


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class FeeSharedExemptionValidator : AbstractValidator<FeeSharedExemption>
        {
            public FeeSharedExemptionValidator()
            {
                RuleFor(obj => obj.ItemCode).NotEmpty().WithMessage("ItemCode is required.");
          
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeSharedExemptionValidator();
        }
    }
}
