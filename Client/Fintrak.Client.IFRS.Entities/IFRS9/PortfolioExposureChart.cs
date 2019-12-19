using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PortfolioExposureChart : ObjectBase
    {
        int _PExposureId;
        string _Name;
        double _Exposure;
        int _RecCount;

        public int PExposureId
        {
            get { return _PExposureId; }
            set
            {
                if (_PExposureId != value)
                {
                    _PExposureId = value;
                    OnPropertyChanged(() => PExposureId);
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

        class PortfolioExposureChartValidator : AbstractValidator<PortfolioExposureChart>
        {
            public PortfolioExposureChartValidator()
            {
                RuleFor(obj => obj._Name).NotEmpty().WithMessage("Name is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new PortfolioExposureChartValidator();
        }
    }
}
