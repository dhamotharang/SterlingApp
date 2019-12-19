using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class ChartOfAccount : ObjectBase
    {
        int _ChartOfAccountId;
        string _AccountCode;
        string _AccountName;
        AccountTypeEnum _AccountType;
        int? _ParentId;
        string _IFRS;
        int _Position;
        int _FinancialTypeId;
        bool _Active;

        public int ChartOfAccountId
        {
            get { return _ChartOfAccountId; }
            set
            {
                if (_ChartOfAccountId != value)
                {
                    _ChartOfAccountId = value;
                    OnPropertyChanged(() => ChartOfAccountId);
                }
            }
        }

        public string AccountCode
        {
            get { return _AccountCode; }
            set
            {
                if (_AccountCode != value)
                {
                    _AccountCode = value;
                    OnPropertyChanged(() => AccountCode);
                }
            }
        }

        public string AccountName
        {
            get { return _AccountName; }
            set
            {
                if (_AccountName != value)
                {
                    _AccountName = value;
                    OnPropertyChanged(() => AccountName);
                }
            }
        }

        public AccountTypeEnum AccountType
        {
            get { return _AccountType; }
            set
            {
                if (_AccountType != value)
                {
                    _AccountType = value;
                    OnPropertyChanged(() => AccountType);
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

        public string IFRS
        {
            get { return _IFRS; }
            set
            {
                if (_IFRS != value)
                {
                    _IFRS = value;
                    OnPropertyChanged(() => IFRS);
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

        public int FinancialTypeId
        {
            get { return _FinancialTypeId; }
            set
            {
                if (_FinancialTypeId != value)
                {
                    _FinancialTypeId = value;
                    OnPropertyChanged(() => FinancialTypeId);
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
                return string.Format("{0} - {1}", _AccountName, _AccountCode);
            }
        }

        public string AccountTypeName
        {
            get
            {
                return string.Format("{0}", _AccountType.ToString());
            }
        }


        class ChartOfAccountValidator : AbstractValidator<ChartOfAccount>
        {
            public ChartOfAccountValidator()
            {
                RuleFor(obj => obj.AccountCode).NotEmpty().WithMessage("Code must not be empty.");
                RuleFor(obj => obj.AccountName).NotEmpty().WithMessage("Name must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new ChartOfAccountValidator();
        }
    }
}
