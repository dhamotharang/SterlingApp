using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class CheckDataAvailability : ObjectBase
    {
        int _CheckDataId;
        int _RecCount;
        string _Package;
        int _Status;
        string _ColorCode;
        DateTime _ExtractionDate;
        bool _Active;

        public int CheckDataId
        {
            get { return _CheckDataId; }
            set
            {
                if (_CheckDataId != value)
                {
                    _CheckDataId = value;
                    OnPropertyChanged(() => CheckDataId);
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

        public string Package
        {
            get { return _Package; }
            set
            {
                if (_Package != value)
                {
                    _Package = value;
                    OnPropertyChanged(() => Package);
                }
            }
        }


        public int Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
                }
            }
        }

        public string ColorCode
        {
            get { return _ColorCode; }
            set
            {
                if (_ColorCode != value)
                {
                    _ColorCode = value;
                    OnPropertyChanged(() => ColorCode);
                }
            }
        }

        public DateTime ExtractionDate
        {
            get { return _ExtractionDate; }
            set
            {
                if (_ExtractionDate != value)
                {
                    _ExtractionDate = value;
                    OnPropertyChanged(() => ExtractionDate);
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

        class CheckDataAvailabilityValidator : AbstractValidator<CheckDataAvailability>
        {
            public CheckDataAvailabilityValidator()
            {
                RuleFor(obj => obj.CheckDataId).GreaterThan(0).WithMessage("CheckDataId is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CheckDataAvailabilityValidator();
        }
    }
}
