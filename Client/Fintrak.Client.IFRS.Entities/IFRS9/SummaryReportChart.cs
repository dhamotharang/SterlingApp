using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SummaryReportChart : ObjectBase
    {
        int _SummaryReportChartId;
        string _Bucket;
        double _Individual;
        double _Collective;
        bool _Active;

        public int SummaryReportChartId
        {
            get { return _SummaryReportChartId; }
            set
            {
                if (_SummaryReportChartId != value)
                {
                    _SummaryReportChartId = value;
                    OnPropertyChanged(() => SummaryReportChartId);
                }
            }
        }

        public string Bucket
        {
            get { return _Bucket; }
            set
            {
                if (_Bucket != value)
                {
                    _Bucket = value;
                    OnPropertyChanged(() => Bucket);
                }
            }
        }


        public double Individual
        {
            get { return _Individual; }
            set
            {
                if (_Individual != value)
                {
                    _Individual = value;
                    OnPropertyChanged(() => Individual);
                }
            }
        }

        public double Collective
        {
            get { return _Collective; }
            set
            {
                if (_Collective != value)
                {
                    _Collective = value;
                    OnPropertyChanged(() => Collective);
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


        class SummaryReportValidator : AbstractValidator<SummaryReport>
        {
            public SummaryReportValidator()
            {
                RuleFor(obj => obj.Bucket).NotEmpty().WithMessage("Bucket is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new SummaryReportValidator();
        }
    }
}
