using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Basic.Entities
{
    public class GeneralTransferPrice : ObjectBase
    {
        int _GeneralTransferPriceId;
        AccountTypeEnum _Category;
        CurrencyType _CurrencyType;
        double _Rate;
        string _DefinitionCode;
        string _MISCode;
        string _Year;
        int _Period;
        string _CompanyCode;
        bool _Active;

        public int GeneralTransferPriceId
        {
            get { return _GeneralTransferPriceId; }
            set
            {
                if (_GeneralTransferPriceId != value)
                {
                    _GeneralTransferPriceId = value;
                    OnPropertyChanged(() => GeneralTransferPriceId);
                }
            }
        }

        public AccountTypeEnum Category
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

        public CurrencyType CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }
        public double Rate
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


        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
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

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
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


        class GeneralTransferPriceValidator : AbstractValidator<GeneralTransferPrice>
        {
            public GeneralTransferPriceValidator()
            {
                RuleFor(obj => obj.CurrencyType).NotEmpty().WithMessage("Currency Type No is required.");
                RuleFor(obj => obj.Category).NotEmpty().WithMessage("Category is required.");
                RuleFor(obj => obj.Rate).GreaterThan(0).WithMessage("Rate is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("DefinitionCode is required.");
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("FiscalYear is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("FiscalPeriod is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GeneralTransferPriceValidator();
        }
    }
}
