using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LifeTimePDClassification : ObjectBase
    {
        int _LifeTimePDClassificationId;
        string _RefNo;
        string _Sector;
        string _CustomerName;
        double _BaseLifetimePD;
        string _BaseRating;
        double _CurrentLifetimePD;
        string _CurrentRating;
        int? _NotchDiff;
        int _Probational_Period;
        string _Classification;
        string _Notes;
        bool _Active;


        public int LifeTimePDClassificationId
        {
            get { return _LifeTimePDClassificationId; }
            set
            {
                if (_LifeTimePDClassificationId != value)
                {
                    _LifeTimePDClassificationId = value;
                    OnPropertyChanged(() => LifeTimePDClassificationId);
                }
            }
        }

        public int? NotchDiff
        {
            get { return _NotchDiff; }
            set
            {
                if (_NotchDiff != value)
                {
                    _NotchDiff = value;
                    OnPropertyChanged(() => NotchDiff);
                }
            }
        }
        public int Probational_Period
        {
            get { return _Probational_Period; }
            set
            {
                if (_Probational_Period != value)
                {
                    _Probational_Period = value;
                    OnPropertyChanged(() => Probational_Period);
                }
            }
        }
        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }
        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }
        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }


        public double BaseLifetimePD
        {
            get { return _BaseLifetimePD; }
            set
            {
                if (_BaseLifetimePD != value)
                {
                    _BaseLifetimePD = value;
                    OnPropertyChanged(() => BaseLifetimePD);
                }
            }
        }


        public string BaseRating
        {
            get { return _BaseRating; }
            set
            {
                if (_BaseRating != value)
                {
                    _BaseRating = value;
                    OnPropertyChanged(() => BaseRating);
                }
            }
        }

        public double CurrentLifetimePD
        {
            get { return _CurrentLifetimePD; }
            set
            {
                if (_CurrentLifetimePD != value)
                {
                    _CurrentLifetimePD = value;
                    OnPropertyChanged(() => CurrentLifetimePD);
                }
            }
        }


        public string CurrentRating
        {
            get { return _CurrentRating; }
            set
            {
                if (_CurrentRating != value)
                {
                    _CurrentRating = value;
                    OnPropertyChanged(() => CurrentRating);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public string Notes
        {
            get { return _Notes; }
            set
            {
                if (_Notes != value)
                {
                    _Notes = value;
                    OnPropertyChanged(() => Notes);
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
        class LifeTimePDClassificationValidator : AbstractValidator<LifeTimePDClassification>
        {
            public LifeTimePDClassificationValidator()
            {
                RuleFor(obj => obj._Classification).NotEmpty().WithMessage("Classification is required.");
                }
        }

        protected override IValidator GetValidator()
        {
            return new LifeTimePDClassificationValidator();
        }
    }
}
