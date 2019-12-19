using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class FiscalPeriod : ObjectBase
    {
        int _FiscalPeriodId;
        string _Name;
        DateTime _StartDate;
        DateTime _EndDate;
        int _FiscalYearId;
        bool _Closed;
        bool _Active;

        public int FiscalPeriodId
        {
            get { return _FiscalPeriodId; }
            set
            {
                if (_FiscalPeriodId != value)
                {
                    _FiscalPeriodId = value;
                    OnPropertyChanged(() => FiscalPeriodId);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }
        public int FiscalYearId
        {
            get { return _FiscalYearId; }
            set
            {
                if (_FiscalYearId != value)
                {
                    _FiscalYearId = value;
                    OnPropertyChanged(() => FiscalYearId);
                }
            }
        }


        public bool Closed
        {
            get { return _Closed; }
            set
            {
                if (_Closed != value)
                {
                    _Closed = value;
                    OnPropertyChanged(() => Closed);
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
                return string.Format("{0}", _Name );
            }
        }

        class FiscalPeriodValidator : AbstractValidator<FiscalPeriod>
        {
            public FiscalPeriodValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.StartDate).NotEmpty().WithMessage("Start Date must not be empty.");
                RuleFor(obj => obj.EndDate).NotEmpty().WithMessage("End Date must not be empty.");
                RuleFor(obj => obj.FiscalYearId).GreaterThan(0).WithMessage("Fiscal Year is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FiscalPeriodValidator();
        }
    }
}
