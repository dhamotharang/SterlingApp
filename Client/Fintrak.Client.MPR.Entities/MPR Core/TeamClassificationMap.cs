using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class TeamClassificationMap : ObjectBase
    {
        int _TeamClassificationMapId;
        string _DefinitionCode;
        string _MisCode;
        string _ClassificationTypeCode;
        string _ClassificationCode;
        string _Year;
        string _CompanyCode;
        bool _Active;


        public int TeamClassificationMapId
        {
            get { return _TeamClassificationMapId; }
            set
            {
                if (_TeamClassificationMapId != value)
                {
                    _TeamClassificationMapId = value;
                    OnPropertyChanged(() => TeamClassificationMapId);
                }
            }
        }

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
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

        public string ClassificationCode
        {
            get { return _ClassificationCode; }
            set
            {
                if (_ClassificationCode != value)
                {
                    _ClassificationCode = value;
                    OnPropertyChanged(() => ClassificationCode);
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


        class TeamClassificationMapValidator : AbstractValidator<TeamClassificationMap>
        {
            public TeamClassificationMapValidator()
            {
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("Team is required.");
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.ClassificationTypeCode).NotEmpty().WithMessage("Classification Type is required.");
                RuleFor(obj => obj.ClassificationCode).NotEmpty().WithMessage("Classification is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamClassificationMapValidator();
        }
    }
}
