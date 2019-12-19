using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Transition : ObjectBase
    {
        int _TransitionId;
        string _Prudential_Classification;
        string _IFRS9_Classification;
        int _PDD_LowerBoundary;
        int _PDD_UpperBoundary;
        //string _CompanyCode;
        bool _Active;

        public int TransitionId
        {
            get { return _TransitionId; }
            set
            {
                if (_TransitionId != value)
                {
                    _TransitionId = value;
                    OnPropertyChanged(() => TransitionId);
                }
            }
        }

        public string Prudential_Classification
        {
            get { return _Prudential_Classification; }
            set
            {
                if (_Prudential_Classification != value)
                {
                    _Prudential_Classification = value;
                    OnPropertyChanged(() => Prudential_Classification);
                }
            }
        }

        public string IFRS9_Classification
        {
            get { return _IFRS9_Classification; }
            set
            {
                if (_IFRS9_Classification != value)
                {
                    _IFRS9_Classification = value;
                    OnPropertyChanged(() => IFRS9_Classification);
                }
            }
        }


        public int PDD_LowerBoundary
        {
            get { return _PDD_LowerBoundary; }
            set
            {
                if (_PDD_LowerBoundary != value)
                {
                    _PDD_LowerBoundary = value;
                    OnPropertyChanged(() => PDD_LowerBoundary);
                }
            }
        }

        public int PDD_UpperBoundary
        {
            get { return _PDD_UpperBoundary; }
            set
            {
                if (_PDD_UpperBoundary != value)
                {
                    _PDD_UpperBoundary = value;
                    OnPropertyChanged(() => PDD_UpperBoundary);
                }
            }
        }


        //public string CompanyCode
        //{
        //    get { return _CompanyCode; }
        //    set
        //    {
        //        if (_CompanyCode != value)
        //        {
        //            _CompanyCode = value;
        //            OnPropertyChanged(() => CompanyCode);
        //        }
        //    }
        //}


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


        class TransitionValidator : AbstractValidator<Transition>
        {
            public TransitionValidator()
            {
                RuleFor(obj => obj.Prudential_Classification).NotEmpty().WithMessage("Prudential Classification is required.");
                RuleFor(obj => obj.IFRS9_Classification).NotEmpty().WithMessage("IFRS9 Classification is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TransitionValidator();
        }
    }
}
