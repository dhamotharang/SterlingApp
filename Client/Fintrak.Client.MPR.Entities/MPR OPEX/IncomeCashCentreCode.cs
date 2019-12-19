using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCashCentreCode : ObjectBase
    {
        int _IncomeCashCentreCodeId;
        string _CashCentreCode;
        string _BranchCode;
        bool _Active;


        public int IncomeCashCentreCodeId
        {
            get { return _IncomeCashCentreCodeId; }
            set
            {
                if (_IncomeCashCentreCodeId != value)
                {
                    _IncomeCashCentreCodeId = value;
                    OnPropertyChanged(() => IncomeCashCentreCodeId);
                }
            }
        }

        public string CashCentreCode
        {
            get { return _CashCentreCode; }
            set
            {
                if (_CashCentreCode != value)
                {
                    _CashCentreCode = value;
                    OnPropertyChanged(() => CashCentreCode);
                }
            }
        }



        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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



        class IncomeCashCentreCodeValidator : AbstractValidator<IncomeCashCentreCode>
        {
            public IncomeCashCentreCodeValidator()
            {
                RuleFor(obj => obj.BranchCode).NotEmpty().WithMessage("Branch Code is required.");
                RuleFor(obj => obj.CashCentreCode).NotEmpty().WithMessage("Cash Centre Code is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeCashCentreCodeValidator();
        }
    }
}
