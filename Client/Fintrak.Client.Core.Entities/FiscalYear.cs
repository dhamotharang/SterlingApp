using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class FiscalYear : ObjectBase
    {
        int _FiscalYearId;
        string _Name;
        DateTime _StartDate;
        DateTime _EndDate;
        bool _Closed;
        bool _Active;

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

        class FiscalYearValidator : AbstractValidator<FiscalYear>
        {
            public FiscalYearValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.StartDate).NotEmpty().WithMessage("Start Date must not be empty.");
                RuleFor(obj => obj.EndDate).NotEmpty().WithMessage("End Date must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new FiscalYearValidator();
        }
    }
}
