using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class FeeSharedRatio : TransactionObjectDoubleBase
    {
        int _FeeSharedRatioId;
        string _DefintionCode;
        string _MisCode;
        string _ItemCode;
        string _Year;
        string _ReviewCode; 
        bool _Active;

        public int FeeSharedRatioId
        {
            get { return _FeeSharedRatioId; }
            set
            {
                if (_FeeSharedRatioId != value)
                {
                    _FeeSharedRatioId = value;
                    OnPropertyChanged(() => FeeSharedRatioId);
                }
            }
        }

        public string DefintionCode
        {
            get { return _DefintionCode; }
            set
            {
                if (_DefintionCode != value)
                {
                    _DefintionCode = value;
                    OnPropertyChanged(() => DefintionCode);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
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

        class FeeSharedRatioValidator : AbstractValidator<FeeSharedRatio>
        {
            public FeeSharedRatioValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");              
            }
        }

        protected override IValidator GetValidator()
        {
            return new FeeSharedRatioValidator();
        }
    }
}
