using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class RatioCaptionMapping : ObjectBase
    {
        int _RatioCaptionMappingId;
        string _RatioCaption;
        string _ReportType;
        string _ReportCaption;

        bool _Active;

        public int RatioCaptionMappingId
        {
            get { return _RatioCaptionMappingId; }
            set
            {
                if (_RatioCaptionMappingId != value)
                {
                    _RatioCaptionMappingId = value;
                    OnPropertyChanged(() => RatioCaptionMappingId);
                }
            }
        }

        public string RatioCaption
        {
            get { return _RatioCaption; }
            set
            {
                if (_RatioCaption != value)
                {
                    _RatioCaption = value;
                    OnPropertyChanged(() => RatioCaption);
                }
            }
        }


        public string ReportType
        {
            get { return _ReportType; }
            set
            {
                if (_ReportType != value)
                {
                    _ReportType = value;
                    OnPropertyChanged(() => ReportType);
                }
            }
        }


        public string ReportCaption
        {
            get { return _ReportCaption; }
            set
            {
                if (_ReportCaption != value)
                {
                    _ReportCaption = value;
                    OnPropertyChanged(() => ReportCaption);
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


        class RatioCaptionMappingValidator : AbstractValidator<RatioCaptionMapping>
        {
            public RatioCaptionMappingValidator()
            {
                RuleFor(obj => obj.RatioCaption).NotEmpty().WithMessage("Ratio Caption is required.");
                RuleFor(obj => obj.ReportType).NotEmpty().WithMessage("Report Type is required.");
                RuleFor(obj => obj.ReportCaption).NotEmpty().WithMessage("Report Caption is required.");



            }
        }

        protected override IValidator GetValidator()
        {
            return new RatioCaptionMappingValidator();
        }
    }
}
