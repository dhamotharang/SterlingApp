using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class QualitativeNote : ObjectBase
    {
        int _QualitativeNoteId;
        string _RefNote;
        string _TopNotes;
        string _BottomNotes;
        string _DisplayName;
        int _ReportType;
        DateTime _RunDate;
        bool _Active;

        public int QualitativeNoteId
        {
            get { return _QualitativeNoteId; }
            set
            {
                if (_QualitativeNoteId != value)
                {
                    _QualitativeNoteId = value;
                    OnPropertyChanged(() => QualitativeNoteId);
                }
            }
        }

        public string RefNote
        {
            get { return _RefNote; }
            set
            {
                if (_RefNote != value)
                {
                    _RefNote = value;
                    OnPropertyChanged(() => RefNote);
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


        class QualitativeNoteValidator : AbstractValidator<QualitativeNote>
        {
            public QualitativeNoteValidator()
            {
                RuleFor(obj => obj._RefNote).NotEmpty().WithMessage("RefNote is required.");
                RuleFor(obj => obj._TopNotes).NotEmpty().WithMessage("Tope Note is required.");
                
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new QualitativeNoteValidator();
        }
    }
}
