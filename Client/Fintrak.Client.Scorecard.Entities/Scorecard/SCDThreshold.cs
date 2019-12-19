using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDThreshold : ObjectBase
    {
        int _ThresholdId;
        string _KPICode;
        string _TeamClassificationCode;
        string _StaffCode;
        double _Minimum;
        double _Maximum;
        string _Color;
        int _Period;
        string _Year;
        bool _Active;

        public int ThresholdId
        {
            get { return _ThresholdId; }
            set
            {
                if (_ThresholdId != value)
                {
                    _ThresholdId = value;
                    OnPropertyChanged(() => ThresholdId);
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

        public string TeamClassificationCode
        {
            get { return _TeamClassificationCode; }
            set
            {
                if (_TeamClassificationCode != value)
                {
                    _TeamClassificationCode = value;
                    OnPropertyChanged(() => TeamClassificationCode);
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

        public double Minimum
        {
            get { return _Minimum; }
            set
            {
                if (_Minimum != value)
                {
                    _Minimum = value;
                    OnPropertyChanged(() => Minimum);
                }
            }
        }


        public double Maximum
        {
            get { return _Maximum; }
            set
            {
                if (_Maximum != value)
                {
                    _Maximum = value;
                    OnPropertyChanged(() => Maximum);
                }
            }
        }

        public string Color
        {
            get { return _Color; }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged(() => Color);
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

        
        class SCDThresholdValidator : AbstractValidator<SCDThreshold>
        {
            public SCDThresholdValidator()
            {
                RuleFor(obj => obj.TeamClassificationCode).NotEmpty().WithMessage("TeamClassificationCode is required.");
                RuleFor(obj => obj.KPICode).NotEmpty().WithMessage("KPICode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDThresholdValidator();
        }
    }
}
