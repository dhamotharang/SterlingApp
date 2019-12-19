using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDTeamMap : ObjectBase
    {
        int _TeamMapId;
        OfficeType _Centre;
        string _TeamDefinitionCode;
        string _StaffCode;
        string _MISCode;
        string _MISName;
        string _ParentCode;
        string _Grade;
        string _TeamClassificationCode;   
        int _Period;
        string _Year;
        bool _Active;

        public int TeamMapId
        {
            get { return _TeamMapId; }
            set
            {
                if (_TeamMapId != value)
                {
                    _TeamMapId = value;
                    OnPropertyChanged(() => TeamMapId);
                }
            }
        }

        public OfficeType Centre
        {
            get { return _Centre; }
            set
            {
                if (_Centre != value)
                {
                    _Centre = value;
                    OnPropertyChanged(() => Centre);
                }
            }
        }

        public string TeamDefinitionCode
        {
            get { return _TeamDefinitionCode; }
            set
            {
                if (_TeamDefinitionCode != value)
                {
                    _TeamDefinitionCode = value;
                    OnPropertyChanged(() => TeamDefinitionCode);
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

        public string MISName
        {
            get { return _MISName; }
            set
            {
                if (_MISName != value)
                {
                    _MISName = value;
                    OnPropertyChanged(() => MISName);
                }
            }
        }

        public string ParentCode
        {
            get { return _ParentCode; }
            set
            {
                if (_ParentCode != value)
                {
                    _ParentCode = value;
                    OnPropertyChanged(() => ParentCode);
                }
            }
        }


        public string Grade
        {
            get { return _Grade; }
            set
            {
                if (_Grade != value)
                {
                    _Grade = value;
                    OnPropertyChanged(() => Grade);
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

        
        class SCDTeamMapValidator : AbstractValidator<SCDTeamMap>
        {
            public SCDTeamMapValidator()
            {
                RuleFor(obj => obj.TeamDefinitionCode).NotEmpty().WithMessage("TeamDefinitionCode is required.");
                RuleFor(obj => obj.Centre).NotEmpty().WithMessage("Centre is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDTeamMapValidator();
        }
    }
}
