using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class LoanSetup : ObjectBase
    {
        int _LoanSetupId;
        decimal _SignificantLoanMarkUp;
        RiskRatingTypes _RatingType;
        bool _EPOption;
        int _EPDefault;
        string _CompanyCode;
        bool _Active;

        public int LoanSetupId
        {
            get { return _LoanSetupId; }
            set
            {
                if (_LoanSetupId != value)
                {
                    _LoanSetupId = value;
                    OnPropertyChanged(() => LoanSetupId);
                }
            }
        }

        public decimal SignificantLoanMarkUp
        {
            get { return _SignificantLoanMarkUp; }
            set
            {
                if (_SignificantLoanMarkUp != value)
                {
                    _SignificantLoanMarkUp = value;
                    OnPropertyChanged(() => SignificantLoanMarkUp);
                }
            }
        }

       
        public RiskRatingTypes RatingType
        {
            get { return _RatingType; }
            set
            {
                if (_RatingType != value)
                {
                    _RatingType = value;
                    OnPropertyChanged(() => RatingType);
                }
            }
        }

        public bool EPOption
        {
            get { return _EPOption; }
            set
            {
                if (_EPOption != value)
                {
                    _EPOption = value;
                    OnPropertyChanged(() => EPOption);
                }
            }
        }

        public int EPDefault
        {
            get { return _EPDefault; }
            set
            {
                if (_EPDefault != value)
                {
                    _EPDefault = value;
                    OnPropertyChanged(() => EPDefault);
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


        class LoanSetupValidator : AbstractValidator<LoanSetup>
        {
            public LoanSetupValidator()
            {
                RuleFor(obj => obj.SignificantLoanMarkUp).NotEmpty().WithMessage("SignificantLoanMarkUp is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanSetupValidator();
        }
    }
}
