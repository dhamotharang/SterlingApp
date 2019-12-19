using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class BSCaption : ObjectBase
    {
        int _CaptionId;
        string _CaptionCode;
        string _CaptionName;
        AccountTypeEnum _Category;
        CurrencyType _CurrencyType;
        BalanceSheetType _BalanceSheetType;
        int _Position;
        string _PLCaption;
        int? _ParentId;
        string _CompanyCode; 
        bool _Active;

        public int CaptionId
        {
            get { return _CaptionId; }
            set
            {
                if (_CaptionId != value)
                {
                    _CaptionId = value;
                    OnPropertyChanged(() => CaptionId);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
                }
            }
        }

        public string CaptionName
        {
            get { return _CaptionName; }
            set
            {
                if (_CaptionName != value)
                {
                    _CaptionName = value;
                    OnPropertyChanged(() => CaptionName);
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
                    OnPropertyChanged(() => CurrencyType);
                }
            }
        }

        public BalanceSheetType BalanceSheetType
        {
            get { return _BalanceSheetType; }
            set
            {
                if (_BalanceSheetType != value)
                {
                    _BalanceSheetType = value;
                    OnPropertyChanged(() => BalanceSheetType);
                }
            }
        }


        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
                }
            }
        }

        public string PLCaption
        {
            get { return _PLCaption; }
            set
            {
                if (_PLCaption != value)
                {
                    _PLCaption = value;
                    OnPropertyChanged(() => PLCaption);
                }
            }
        }

        public int? ParentId
        {
            get { return _ParentId; }
            set
            {
                if (_ParentId != value)
                {
                    _ParentId = value;
                    OnPropertyChanged(() => ParentId);
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

        
        class BSCaptionValidator : AbstractValidator<BSCaption>
        {
            public BSCaptionValidator()
            {
                RuleFor(obj => obj.CaptionName).NotEmpty().WithMessage("Caption Name is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption Code is required.");
                RuleFor(obj => obj.CurrencyType).NotEmpty().WithMessage("Currency Type is required.");
                RuleFor(obj => obj.BalanceSheetType).NotEmpty().WithMessage("BalanceSheet Type is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new BSCaptionValidator();
        }
    }
}
