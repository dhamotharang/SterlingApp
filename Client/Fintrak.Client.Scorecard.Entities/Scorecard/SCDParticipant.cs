using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDParticipant : ObjectBase
    {
        int _ParticipantId;
        string _KPICode;
        string _StaffCode;
        string _TeamClassificationCode;
        ParticipantStatus _Status;
        int _Period;
        string _Year;
        bool _Active;

        public int ParticipantId
        {
            get { return _ParticipantId; }
            set
            {
                if (_ParticipantId != value)
                {
                    _ParticipantId = value;
                    OnPropertyChanged(() => ParticipantId);
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

        public ParticipantStatus Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
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

        
        class SCDParticipantValidator : AbstractValidator<SCDParticipant>
        {
            public SCDParticipantValidator()
            {              
                RuleFor(obj => obj.KPICode).NotEmpty().WithMessage("KPICode is required."); 
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDParticipantValidator();
        }
    }
}
