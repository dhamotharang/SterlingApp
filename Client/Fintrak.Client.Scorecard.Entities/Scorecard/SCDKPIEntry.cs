using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDKPIEntry : ObjectBase
    {
        int _EntryId;
        string _KPICode;
        string _MISCode;
        string _StaffCode;
        decimal _Actual;
        decimal _Target;
        double _Score;
        DateTime _Date;
        int _Period;
        string _Year;
        bool _Active;

        public int EntryId
        {
            get { return _EntryId; }
            set
            {
                if (_EntryId != value)
                {
                    _EntryId = value;
                    OnPropertyChanged(() => EntryId);
                }
            }
        }

        public string KPICode
        {
            get { return _KPICode; }
            set
            {
                if (_KPICode != value)
                {
                    _KPICode = value;
                    OnPropertyChanged(() => KPICode);
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

        public string StaffCode
        {
            get { return _StaffCode; }
            set
            {
                if (_StaffCode != value)
                {
                    _StaffCode = value;
                    OnPropertyChanged(() => StaffCode);
                }
            }
        }

        public decimal Actual
        {
            get { return _Actual; }
            set
            {
                if (_Actual != value)
                {
                    _Actual = value;
                    OnPropertyChanged(() => Actual);
                }
            }
        }


        public decimal Target
        {
            get { return _Target; }
            set
            {
                if (_Target != value)
                {
                    _Target = value;
                    OnPropertyChanged(() => Target);
                }
            }
        }



        public double Score
        {
            get { return _Score; }
            set
            {
                if (_Score != value)
                {
                    _Score = value;
                    OnPropertyChanged(() => Score);
                }
            }
        }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
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

        
        class SCDKPIEntryValidator : AbstractValidator<SCDKPIEntry>
        {
            public SCDKPIEntryValidator()
            {
                RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
                RuleFor(obj => obj.KPICode).NotEmpty().WithMessage("KPICode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDKPIEntryValidator();
        }
    }
}
