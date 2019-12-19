using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Calendar : ObjectBase
    {
        int _CalId;
        DateTime _ThisDate;
        string _Day;
        string _FullDescription;
     
        bool _Active;

        public int CalId
        {
            get { return _CalId; }
            set
            {
                if (_CalId != value)
                {
                    _CalId = value;
                    OnPropertyChanged(() => CalId);
                }
            }
        }

        public DateTime ThisDate
        {
            get { return _ThisDate; }
            set
            {
                if (_ThisDate != value)
                {
                    _ThisDate = value;
                    OnPropertyChanged(() => ThisDate);
                }
            }
        }

        public string Day
        {
            get { return _Day; }
            set
            {
                if (_Day != value)
                {
                    _Day = value;
                    OnPropertyChanged(() => Day);
                }
            }
        }

        public string FullDescription
        {
            get { return _FullDescription; }
            set
            {
                if (_FullDescription != value)
                {
                    _FullDescription = value;
                    OnPropertyChanged(() => FullDescription);
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


        class CalendarValidator : AbstractValidator<Calendar>
        {
            public CalendarValidator()
            {
               // RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new CalendarValidator();
        }
    }
}
