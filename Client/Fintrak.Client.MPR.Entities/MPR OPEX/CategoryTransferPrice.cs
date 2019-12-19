using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class CategoryTransferPrice : ObjectBase
    {
        int _CategoryTransferPriceId;
        string _Category;
        int _Period;
        int _Year;
        decimal _Rate;
        string _CurrencyType;
        bool _Active;


        public int CategoryTransferPriceId
        {
            get { return _CategoryTransferPriceId; }
            set
            {
                if (_CategoryTransferPriceId != value)
                {
                    _CategoryTransferPriceId = value;
                    OnPropertyChanged(() => CategoryTransferPriceId);
                }
            }
        }

        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }



        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int Year
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


        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public string CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => CurrencyType);
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



        class CategoryTransferPriceValidator : AbstractValidator<CategoryTransferPrice>
        {
            public CategoryTransferPriceValidator()
            {
                RuleFor(obj => obj.Category).NotEmpty().WithMessage("Category is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CategoryTransferPriceValidator();
        }
    }
}
