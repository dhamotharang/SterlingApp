using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ExpenseMapping : ObjectBase
    {
        int _ExpenseMappingId;
        string _BasisCode;
        string _ItemCode;
        string _MISCode;
        bool _Active;


        public int ExpenseMappingId
        {
            get { return _ExpenseMappingId; }
            set
            {
                if (_ExpenseMappingId != value)
                {
                    _ExpenseMappingId = value;
                    OnPropertyChanged(() => ExpenseMappingId);
                }
            }
        }

        public string BasisCode
        {
            get { return _BasisCode; }
            set
            {
                if (_BasisCode != value)
                {
                    _BasisCode = value;
                    OnPropertyChanged(() => BasisCode);
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

        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
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


        
        class ExpenseMappingValidator : AbstractValidator<ExpenseMapping>
        {
            public ExpenseMappingValidator()
            {
                RuleFor(obj => obj.BasisCode).NotEmpty().WithMessage("BasisCode is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MisCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ExpenseMappingValidator();
        }
    }
}
