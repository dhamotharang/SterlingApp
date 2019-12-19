using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorialExposure : ObjectBase
    {
        int _SectorialExposureId;
        string _Name;
        double _Exposure;
        int _RecCount;
        bool _Active;

        public int SectorialExposureId
        {
            get { return _SectorialExposureId; }
            set
            {
                if (_SectorialExposureId != value)
                {
                    _SectorialExposureId = value;
                    OnPropertyChanged(() => SectorialExposureId);
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


        public int RecCount
        {
            get { return _RecCount; }
            set
            {
                if (_RecCount != value)
                {
                    _RecCount = value;
                    OnPropertyChanged(() => RecCount);
                }
            }
        }

        public double Exposure
        {
            get { return _Exposure; }
            set
            {
                if (_Exposure != value)
                {
                    _Exposure = value;
                    OnPropertyChanged(() => Exposure);
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


        class SectorialExposureValidator : AbstractValidator<SectorialExposure>
        {
            public SectorialExposureValidator()
            {
                RuleFor(obj => obj._Name).NotEmpty().WithMessage("Name is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorialExposureValidator();
        }
    }
}
