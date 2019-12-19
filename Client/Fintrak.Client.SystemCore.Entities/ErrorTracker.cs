using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class ErrorTracker : ObjectBase
    {
        int _ErrorTrackerId;
        string _ErrMessage;
        string _StoredProcedureName;
        string _UserId;
        bool _Active;

        public int ErrorTrackerId
        {
            get { return _ErrorTrackerId; }
            set
            {
                if (_ErrorTrackerId != value)
                {
                    _ErrorTrackerId = value;
                    OnPropertyChanged(() => ErrorTrackerId);
                }
            }
        }

        public string ErrMessage
        {
            get { return _ErrMessage; }
            set
            {
                if (_ErrMessage != value)
                {
                    _ErrMessage = value.Trim();
                    OnPropertyChanged(() => ErrMessage);
                }
            }
        }

        public string StoredProcedureName
        {
            get { return _StoredProcedureName; }
            set
            {
                if (_StoredProcedureName != value)
                {
                    _StoredProcedureName = value.Trim();
                    OnPropertyChanged(() => StoredProcedureName);
                }
            }
        }


        public string UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value.Trim();
                    OnPropertyChanged(() => UserId);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _StoredProcedureName, _ErrMessage );
            }
        }

        class ErrorTrackerValidator : AbstractValidator<ErrorTracker>
        {
            public ErrorTrackerValidator()
            {
                RuleFor(obj => obj.ErrMessage).NotEmpty().WithMessage("ErrMessage must not be empty.");
                RuleFor(obj => obj.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ErrorTrackerValidator();
        }
    }
}
