using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorialExposureChart : ObjectBase
    {
        int _SectorialExposureChartId;
        string _Name;
        double _Exposure;
        int _RecCount;

        public int SectorialExposureChartId
        {
            get { return _SectorialExposureChartId; }
            set
            {
                if (_SectorialExposureChartId != value)
                {
                    _SectorialExposureChartId = value;
                    OnPropertyChanged(() => SectorialExposureChartId);
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

        class SectorialExposureChartValidator : AbstractValidator<SectorialExposureChart>
        {
            public SectorialExposureChartValidator()
            {
                RuleFor(obj => obj._Name).NotEmpty().WithMessage("Name is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorialExposureChartValidator();
        }
    }
}
