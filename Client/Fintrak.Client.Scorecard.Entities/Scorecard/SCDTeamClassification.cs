using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDTeamClassification : ObjectBase
    {
        int _TeamClassificationId;
        string _Code;
        string _Name;
        int _Period;
        string _Year;
        bool _Active;

        public int TeamClassificationId
        {
            get { return _TeamClassificationId; }
            set
            {
                if (_TeamClassificationId != value)
                {
                    _TeamClassificationId = value;
                    OnPropertyChanged(() => TeamClassificationId);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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

        
        class SCDTeamClassificationValidator : AbstractValidator<SCDTeamClassification>
        {
            public SCDTeamClassificationValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDTeamClassificationValidator();
        }
    }
}
