using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class ReportStatus : ObjectBase
    {
        int _StatusId;
        int _SolutionId;
        bool _Closed;
        bool _Active;

        public int StatusId
        {
            get { return _StatusId; }
            set
            {
                if (_StatusId != value)
                {
                    _StatusId = value;
                    OnPropertyChanged(() => StatusId);
                }
            }
        }

        public int SolutionId
        {
            get { return _SolutionId; }
            set
            {
                if (_SolutionId != value)
                {
                    _SolutionId = value;
                    OnPropertyChanged(() => SolutionId);
                }
            }
        }

        public bool Closed
        {
            get { return _Closed; }
            set
            {
                if (_Closed != value)
                {
                    _Closed = value;
                    OnPropertyChanged(() => Closed);
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

        class ReportStatusValidator : AbstractValidator<ReportStatus>
        {
            public ReportStatusValidator()
            {
                RuleFor(obj => obj._SolutionId).NotEmpty().WithMessage("Solution must not be empty.");
            
            }
        }

        protected override IValidator GetValidator()
        {
            return new ReportStatusValidator();
        }
    }
}
