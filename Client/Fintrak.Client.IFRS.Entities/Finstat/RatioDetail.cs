using System;
using System.Linq; 
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RatioDetail : ObjectBase
    {
        int _RatioID;
        string _RatioCaption;
        string _ReportCaption;
        string _ReportSubCaption;
        string _ReportSubSubCaption;
        string _DivisorType;
        decimal _Multiplier;
        string _PreviousType;
        bool _Annualised;
        int _ReportType;
        int _Position;

        bool _Active;

        public int RatioID
        {
            get { return _RatioID; }
            set
            {
                if (_RatioID != value)
                {
                    _RatioID = value;
                    OnPropertyChanged(() => RatioID);
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

        public string ReportSubCaption
        {
            get { return _ReportSubCaption; }
            set
            {
                if (_ReportSubCaption != value)
                {
                    _ReportSubCaption = value;
                    OnPropertyChanged(() => ReportSubCaption);
                }
            }
        }

        public string ReportSubSubCaption
        {
            get { return _ReportSubSubCaption; }
            set
            {
                if (_ReportSubSubCaption != value)
                {
                    _ReportSubSubCaption = value;
                    OnPropertyChanged(() => ReportSubSubCaption);
                }
            }
        }

        public string DivisorType
        {
            get { return _DivisorType; }
            set
            {
                if (_DivisorType != value)
                {
                    _DivisorType = value;
                    OnPropertyChanged(() => DivisorType);
                }
            }
        }

        public decimal Multiplier
        {
            get { return _Multiplier; }
            set
            {
                if (_Multiplier != value)
                {
                    _Multiplier = value;
                    OnPropertyChanged(() => Multiplier);
                }
            }
        }

        public string PreviousType
        {
            get { return _PreviousType; }
            set
            {
                if (_PreviousType != value)
                {
                    _PreviousType = value;
                    OnPropertyChanged(() => PreviousType);
                }
            }
        }

        public bool Annualised
        {
            get { return _Annualised; }
            set
            {
                if (_Annualised != value)
                {
                    _Annualised = value;
                    OnPropertyChanged(() => Annualised);
                }
            }
        }

        public int ReportType
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

        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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


        class RatioDetailValidator : AbstractValidator<RatioDetail>
        {
            public RatioDetailValidator()
            {
                // RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("RatioDetail is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new RatioDetailValidator();
        }
    }
}
