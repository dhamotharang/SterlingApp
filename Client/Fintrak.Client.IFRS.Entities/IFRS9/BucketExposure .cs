using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BucketExposure : ObjectBase
    {
        int _BucketExposureId;
        string _Name;
        double _Exposure;
        double _TotalECL;
        double _RecCount;
        bool _Active;

        public int BucketExposureId
        {
            get { return _BucketExposureId; }
            set
            {
                if (_BucketExposureId != value)
                {
                    _BucketExposureId = value;
                    OnPropertyChanged(() => BucketExposureId);
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


        public double RecCount
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
        public double TotalECL
        {
            get { return _TotalECL; }
            set
            {
                if (_TotalECL != value)
                {
                    _TotalECL = value;
                    OnPropertyChanged(() => TotalECL);
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


        class BucketExposureValidator : AbstractValidator<BucketExposure>
        {
            public BucketExposureValidator()
            {
                RuleFor(obj => obj._Name).NotEmpty().WithMessage("Name is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new BucketExposureValidator();
        }
    }
}
