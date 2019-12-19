using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Client.Budget.Entities;

namespace Fintrak.Client.Budget.Entities
{
    public class DepreciationRate : TransactionObjectDoubleBase
    {
        int _DepreciationRateId;
        string _CategoryCode;
        string _ReviewCode;   
        string _Year;
        bool _Active;

        public int DepreciationRateId
        {
            get { return _DepreciationRateId; }
            set
            {
                if (_DepreciationRateId != value)
                {
                    _DepreciationRateId = value;
                    OnPropertyChanged(() => DepreciationRateId);
                }
            }
        }

        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                if (_CategoryCode != value)
                {
                    _CategoryCode = value;
                    OnPropertyChanged(() => CategoryCode);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _CategoryCode, _Year);
            }
        }

        
        class DepreciationRateValidator : AbstractValidator<DepreciationRate>
        {
            public DepreciationRateValidator()
            {
                RuleFor(obj => obj.CategoryCode).NotEmpty().WithMessage("CategoryCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new DepreciationRateValidator();
        }
    }
}
