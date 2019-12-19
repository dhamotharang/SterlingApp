using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDKPIActualMap : ObjectBase
    {
        int _MapId;
        string _KPICode;
        string _Formula;
        int _Period;
        string _Year;
        bool _Active;

        public int MapId
        {
            get { return _MapId; }
            set
            {
                if (_MapId != value)
                {
                    _MapId = value;
                    OnPropertyChanged(() => MapId);
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

        public string Formula
        {
            get { return _Formula; }
            set
            {
                if (_Formula != value)
                {
                    _Formula = value;
                    OnPropertyChanged(() => Formula);
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

        
        class SCDKPIActualMapValidator : AbstractValidator<SCDKPIActualMap>
        {
            public SCDKPIActualMapValidator()
            {
                RuleFor(obj => obj.Formula).NotEmpty().WithMessage("Formula is required.");
                RuleFor(obj => obj.KPICode).NotEmpty().WithMessage("KPICode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDKPIActualMapValidator();
        }
    }
}
