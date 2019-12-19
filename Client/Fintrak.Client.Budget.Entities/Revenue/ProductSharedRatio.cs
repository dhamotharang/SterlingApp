using System;
using System.Linq;
using Fintrak.Client.Budget.Entities;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class ProductSharedRatio : TransactionObjectDoubleBase
    {
        int _ProductSharedRatioId;
        string _DefintionCode;
        string _MisCode;       
        string _Year;
        string _ProductCode;     
        string _ReviewCode; 
        bool _Active;

        public int ProductSharedRatioId
        {
            get { return _ProductSharedRatioId; }
            set
            {
                if (_ProductSharedRatioId != value)
                {
                    _ProductSharedRatioId = value;
                    OnPropertyChanged(() => ProductSharedRatioId);
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

        class ProductSharedRatioValidator : AbstractValidator<ProductSharedRatio>
        {
            public ProductSharedRatioValidator()
            {
                RuleFor(obj => obj.DefintionCode).NotEmpty().WithMessage("DefintionCode is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductSharedRatioValidator();
        }
    }
}
