using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ExpenseGLMapping : ObjectBase
    {
        int _ExpenseGLId;
        string _BasisCode;
        string _GLCode;      
        bool _Active;


        public int ExpenseGLId
        {
            get { return _ExpenseGLId; }
            set
            {
                if (_ExpenseGLId != value)
                {
                    _ExpenseGLId = value;
                    OnPropertyChanged(() => ExpenseGLId);
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

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
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


        
        class ExpenseGLMappingValidator : AbstractValidator<ExpenseGLMapping>
        {
            public ExpenseGLMappingValidator()
            {
                RuleFor(obj => obj.BasisCode).NotEmpty().WithMessage("BasisCode is required.");
                RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ExpenseGLMappingValidator();
        }
    }
}
