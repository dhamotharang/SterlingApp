using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Scorecard.Entities
{
    public class SCDConfiguration : ObjectBase
    {
        int _ConfigurationId;
        OperationMode _Mode;
        string _TeamClassificationTypeCode;
        PeriodType _PeriodType;
        string _CompanyCode;
        bool _Active;

        public int ConfigurationId
        {
            get { return _ConfigurationId; }
            set
            {
                if (_ConfigurationId != value)
                {
                    _ConfigurationId = value;
                    OnPropertyChanged(() => ConfigurationId);
                }
            }
        }

        public OperationMode Mode
        {
            get { return _Mode; }
            set
            {
                if (_Mode != value)
                {
                    _Mode = value;
                    OnPropertyChanged(() => Mode);
                }
            }
        }

        public string TeamClassificationTypeCode
        {
            get { return _TeamClassificationTypeCode; }
            set
            {
                if (_TeamClassificationTypeCode != value)
                {
                    _TeamClassificationTypeCode = value;
                    OnPropertyChanged(() => TeamClassificationTypeCode);
                }
            }
        }

        public PeriodType PeriodType
        {
            get { return _PeriodType; }
            set
            {
                if (_PeriodType != value)
                {
                    _PeriodType = value;
                    OnPropertyChanged(() => PeriodType);
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

        
        class SCDConfigurationValidator : AbstractValidator<SCDConfiguration>
        {
            public SCDConfigurationValidator()
            {
                RuleFor(obj => obj.TeamClassificationTypeCode).NotEmpty().WithMessage("TeamClassificationTypeCode is required.");
                RuleFor(obj => obj.Mode).NotEmpty().WithMessage("Mode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SCDConfigurationValidator();
        }
    }
}
