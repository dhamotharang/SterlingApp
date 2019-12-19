using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDKPIClassification : ObjectBase
    {
        int _ClassificationId;
        string _KPICode;
        string _TeamClassificationCode;
        string _CategoryCode;
        int _Period;
        string _Year;
        bool _Active;

        public int ClassificationId
        {
            get { return _ClassificationId; }
            set
            {
                if (_ClassificationId != value)
                {
                    _ClassificationId = value;
                    OnPropertyChanged(() => ClassificationId);
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


        public string CategoryCode
        {
            get { return _CategoryCode; }
            set
            {
                if (_CategoryCode != value)
                {
                    _CategoryCode = value;
                    OnPropertyChanged(() => CategoryCode);
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

        
        class SCDKPIClassificationValidator : AbstractValidator<SCDKPIClassification>
        {
            public SCDKPIClassificationValidator()
            {
                RuleFor(obj => obj.TeamClassificationCode).NotEmpty().WithMessage("Collection is required.");
                RuleFor(obj => obj.CategoryCode).NotEmpty().WithMessage("Category is required.");
                RuleFor(obj => obj.KPICode).NotEmpty().WithMessage("KPICode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDKPIClassificationValidator();
        }
    }
}
