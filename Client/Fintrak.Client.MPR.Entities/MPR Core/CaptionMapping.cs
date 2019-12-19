using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class CaptionMapping : ObjectBase
    {
        int _CaptionMappingId;
        string _MPRCaptionCode;
        string _MPRCaptionName;
        string _BudgetCaptionCode;
        string _BudgetCaptionName;
        string _CaptionIndicator;

        bool _Active;

        public int CaptionMappingId
        {
            get { return _CaptionMappingId; }
            set
            {
                if (_CaptionMappingId != value)
                {
                    _CaptionMappingId = value;
                    OnPropertyChanged(() => CaptionMappingId);
                }
            }
        }

        public string MPRCaptionCode
        {
            get { return _MPRCaptionCode; }
            set
            {
                if (_MPRCaptionCode != value)
                {
                    _MPRCaptionCode = value;
                    OnPropertyChanged(() => MPRCaptionCode);
                }
            }
        }


        public string MPRCaptionName
        {
            get { return _MPRCaptionName; }
            set
            {
                if (_MPRCaptionName != value)
                {
                    _MPRCaptionName = value;
                    OnPropertyChanged(() => MPRCaptionName);
                }
            }
        }


        public string BudgetCaptionCode
        {
            get { return _BudgetCaptionCode; }
            set
            {
                if (_BudgetCaptionCode != value)
                {
                    _BudgetCaptionCode = value;
                    OnPropertyChanged(() => BudgetCaptionCode);
                }
            }
        }

        public string BudgetCaptionName
        {
            get { return _BudgetCaptionName; }
            set
            {
                if (_BudgetCaptionName != value)
                {
                    _BudgetCaptionName = value;
                    OnPropertyChanged(() => BudgetCaptionName);
                }
            }
        }

        public string CaptionIndicator
        {
            get { return _CaptionIndicator; }
            set
            {
                if (_CaptionIndicator != value)
                {
                    _CaptionIndicator = value;
                    OnPropertyChanged(() => CaptionIndicator);
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


        class CaptionMappingValidator : AbstractValidator<CaptionMapping>
        {
            public CaptionMappingValidator()
            {
                RuleFor(obj => obj.MPRCaptionCode).NotEmpty().WithMessage("MPR Caption Code is required.");
                RuleFor(obj => obj.CaptionIndicator).NotEmpty().WithMessage("Caption Indicator is required.");
                RuleFor(obj => obj.BudgetCaptionCode).NotEmpty().WithMessage("Budget Caption Code is required.");



            }
        }

        protected override IValidator GetValidator()
        {
            return new CaptionMappingValidator();
        }
    }
}
