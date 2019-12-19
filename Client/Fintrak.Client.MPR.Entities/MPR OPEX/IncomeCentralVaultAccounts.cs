using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCentralVaultAccounts : ObjectBase
    {
        int _IncomeCentralVaultAccountsId;
        string _Account;
        bool _Active;


        public int IncomeCentralVaultAccountsId
        {
            get { return _IncomeCentralVaultAccountsId; }
            set
            {
                if (_IncomeCentralVaultAccountsId != value)
                {
                    _IncomeCentralVaultAccountsId = value;
                    OnPropertyChanged(() => IncomeCentralVaultAccountsId);
                }
            }
        }

        public string Account
        {
            get { return _Account; }
            set
            {
                if (_Account != value)
                {
                    _Account = value;
                    OnPropertyChanged(() => Account);
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



        class IncomeCentralVaultAccountsValidator : AbstractValidator<IncomeCentralVaultAccounts>
        {
            public IncomeCentralVaultAccountsValidator()
            {
                RuleFor(obj => obj.Account).NotEmpty().WithMessage("Account is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCentralVaultAccountsValidator();
        }
    }
}
