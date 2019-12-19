using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InterimQualitativeNote : ObjectBase
    {
        int _InterimQualitativeNoteId;
        string _report;
        string _TopNotes;
        string _BottomNotes;
        string _DisplayName;
        int _ReportType;
        DateTime _RunDate;
        bool _Active;

        public int InterimQualitativeNoteId
        {
            get { return _InterimQualitativeNoteId; }
            set
            {
                if (_InterimQualitativeNoteId != value)
                {
                    _InterimQualitativeNoteId = value;
                    OnPropertyChanged(() => InterimQualitativeNoteId);
                }
            }
        }

        public string report
        {
            get { return _report; }
            set
            {
                if (_report != value)
                {
                    _report = value;
                    OnPropertyChanged(() => report);
                }
            }
        }

        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                if (_DisplayName != value)
                {
                    _DisplayName = value;
                    OnPropertyChanged(() => DisplayName);
                }
            }
        }

        public int ReportType
        {
            get { return _ReportType; }
            set
            {
                if (_ReportType != value)
                {
                    _ReportType = value;
                    OnPropertyChanged(() => ReportType);
                }
            }
        }

        public string TopNotes
        {
            get { return _TopNotes; }
            set
            {
                if (_TopNotes != value)
                {
                    _TopNotes = value;
                    OnPropertyChanged(() => TopNotes);
                }
            }
        }

        public string BottomNotes
        {
            get { return _BottomNotes; }
            set
            {
                if (_BottomNotes != value)
                {
                    _BottomNotes = value;
                    OnPropertyChanged(() => BottomNotes);
                }
            }
        }

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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


        class InterimQualitativeNoteValidator : AbstractValidator<InterimQualitativeNote>
        {
            public InterimQualitativeNoteValidator()
            {
                RuleFor(obj => obj._report).NotEmpty().WithMessage("Report Name is required.");
                RuleFor(obj => obj._TopNotes).NotEmpty().WithMessage("Tope Note is required.");
                
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new InterimQualitativeNoteValidator();
        }
    }
}
