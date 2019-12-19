using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SummaryReport : ObjectBase
    {
        int _SummaryReportId;
        string _Bucket;
        double _Individual;
        double _IndividualLoss;
        double _Collective;
        double _CollectiveLoss;
        bool _Active;

        public int SummaryReportId
        {
            get { return _SummaryReportId; }
            set
            {
                if (_SummaryReportId != value)
                {
                    _SummaryReportId = value;
                    OnPropertyChanged(() => SummaryReportId);
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

        public double CollectiveLoss
        {
            get { return _CollectiveLoss; }
            set
            {
                if (_CollectiveLoss != value)
                {
                    _CollectiveLoss = value;
                    OnPropertyChanged(() => CollectiveLoss);
                }
            }
        }

        public double IndividualLoss
        {
            get { return _IndividualLoss; }
            set
            {
                if (_IndividualLoss != value)
                {
                    _IndividualLoss = value;
                    OnPropertyChanged(() => IndividualLoss);
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
