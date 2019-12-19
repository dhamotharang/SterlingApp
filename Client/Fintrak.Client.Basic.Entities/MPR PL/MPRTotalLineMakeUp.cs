using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRTotalLineMakeUp : ObjectBase
    {
        int _MPRTotalLineMakeUpId;
        string _TotalLine;
        string _CaptionCode;
        string _CompanyCode;
        bool _Active;

        public int MPRTotalLineMakeUpId
        {
            get { return _MPRTotalLineMakeUpId; }
            set
            {
                if (_MPRTotalLineMakeUpId != value)
                {
                    _MPRTotalLineMakeUpId = value;
                    OnPropertyChanged(() => MPRTotalLineMakeUpId);
                }
            }
        }

        public string TotalLine
        {
            get { return _TotalLine; }
            set
            {
                if (_TotalLine != value)
                {
                    _TotalLine = value;
                    OnPropertyChanged(() => TotalLine);
                }
            }
        }

        public string CaptionCode
        {
            get { return _CaptionCode; }
            set
            {
                if (_CaptionCode != value)
                {
                    _CaptionCode = value;
                    OnPropertyChanged(() => CaptionCode);
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

        
        class MPRTotalLineMakeUpValidator : AbstractValidator<MPRTotalLineMakeUp>
        {
            public MPRTotalLineMakeUpValidator()
            {
                RuleFor(obj => obj.TotalLine).NotEmpty().WithMessage("TotalLine is required.");
                RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MPRTotalLineMakeUpValidator();
        }
    }
}
