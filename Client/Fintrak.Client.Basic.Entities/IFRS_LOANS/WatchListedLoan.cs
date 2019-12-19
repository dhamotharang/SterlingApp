using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class WatchListedLoan : ObjectBase
    {
        int _WatchListedLoanId;
        string _RefNo;
        string _CompanyCode;
        bool _Active;

        public int WatchListedLoanId
        {
            get { return _WatchListedLoanId; }
            set
            {
                if (_WatchListedLoanId != value)
                {
                    _WatchListedLoanId = value;
                    OnPropertyChanged(() => WatchListedLoanId);
                }
            }
        }

        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
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


        class WatchListedLoanValidator : AbstractValidator<WatchListedLoan>
        {
            public WatchListedLoanValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new WatchListedLoanValidator();
        }
    }
}
