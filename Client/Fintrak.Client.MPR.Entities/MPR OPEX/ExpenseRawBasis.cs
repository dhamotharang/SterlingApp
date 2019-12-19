using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ExpenseRawBasis : ObjectBase
    {
        int _ExpenseRawBasisId;
        string _BasisCode;
        string _MISCode;
        double _Weight;
        bool _Active;


        public int ExpenseRawBasisId
        {
            get { return _ExpenseRawBasisId; }
            set
            {
                if (_ExpenseRawBasisId != value)
                {
                    _ExpenseRawBasisId = value;
                    OnPropertyChanged(() => ExpenseRawBasisId);
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


        public double Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged(() => Weight);
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


        
        class ExpenseRawBasisValidator : AbstractValidator<ExpenseRawBasis>
        {
            public ExpenseRawBasisValidator()
            {
                RuleFor(obj => obj.BasisCode).NotEmpty().WithMessage("BasisCode is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ExpenseRawBasisValidator();
        }
    }
}
