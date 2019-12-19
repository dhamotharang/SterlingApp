using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class AccountTransferPrice : ObjectBase
    {
        int _AccountTransferPriceId;
        string _AccountNo;
        AccountTypeEnum _Category;
        double _Rate;
        string _Year;
        string _Period;
        int _SolutionId;
        string _CompanyCode;
        bool _Active;

        public int AccountTransferPriceId
        {
            get { return _AccountTransferPriceId; }
            set
            {
                if (_AccountTransferPriceId != value)
                {
                    _AccountTransferPriceId = value;
                    OnPropertyChanged(() => AccountTransferPriceId);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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

        public string Period
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

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
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

        
        class AccountTransferPriceValidator : AbstractValidator<AccountTransferPrice>
        {
            public AccountTransferPriceValidator()
            {
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("Account No is required.");
                RuleFor(obj => obj.Category).NotEmpty().WithMessage("Category is required.");
                RuleFor(obj => obj.Rate).GreaterThan(0).WithMessage("Rate is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
                RuleFor(obj => obj.Period).NotEmpty().WithMessage("Period is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new AccountTransferPriceValidator();
        }
    }
}
