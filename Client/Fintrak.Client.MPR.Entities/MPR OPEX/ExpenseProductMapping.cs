using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ExpenseProductMapping : ObjectBase
    {
        int _ExpenseProductId;
        string _BasisCode;
        string _ProductCode;      
        bool _Active;


        public int ExpenseProductId
        {
            get { return _ExpenseProductId; }
            set
            {
                if (_ExpenseProductId != value)
                {
                    _ExpenseProductId = value;
                    OnPropertyChanged(() => ExpenseProductId);
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

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
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


        
        class ExpenseProductMappingValidator : AbstractValidator<ExpenseProductMapping>
        {
            public ExpenseProductMappingValidator()
            {
                RuleFor(obj => obj.BasisCode).NotEmpty().WithMessage("BasisCode is required.");
                RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("ProductCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ExpenseProductMappingValidator();
        }
    }
}
