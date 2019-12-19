using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDTarget : ObjectBase
    {
        int _TargetId;
        string _MisCode;
        string _Caption;
        decimal _Amount;
        DateTime _Date;
        int _Period;
        string _Year;
        bool _Active;

        public int TargetId
        {
            get { return _TargetId; }
            set
            {
                if (_TargetId != value)
                {
                    _TargetId = value;
                    OnPropertyChanged(() => TargetId);
                }
            }
        }

        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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

        
        class SCDTargetValidator : AbstractValidator<SCDTarget>
        {
            public SCDTargetValidator()
            {
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.Caption).NotEmpty().WithMessage("Caption is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDTargetValidator();
        }
    }
}
