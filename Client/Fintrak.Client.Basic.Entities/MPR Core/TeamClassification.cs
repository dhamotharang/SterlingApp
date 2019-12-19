using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class TeamClassification : ObjectBase
    {
        int _TeamClassificationId;
        string _Code;
        string _Name;
        string _ClassificationTypeCode;
        string _Year;
        string _CompanyCode;
        bool _Active;
        Nullable<int> _Period;

        public Nullable<int> Period
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


        public string ClassificationTypeCode
        {
            get { return _ClassificationTypeCode; }
            set
            {
                if (_ClassificationTypeCode != value)
                {
                    _ClassificationTypeCode = value;
                    OnPropertyChanged(() => ClassificationTypeCode);
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


        class TeamClassificationValidator : AbstractValidator<TeamClassification>
        {
            public TeamClassificationValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.ClassificationTypeCode).NotEmpty().WithMessage("Team Classification Type is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamClassificationValidator();
        }
    }
}
