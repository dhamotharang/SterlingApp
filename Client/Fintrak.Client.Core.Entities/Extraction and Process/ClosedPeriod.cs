using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class ClosedPeriod : ObjectBase
    {
        int _ClosedPeriodId;
        int _SolutionId;
        DateTime _Date;
        bool _Status;
        bool _Active;
        bool _Deleted;

        public int ClosedPeriodId
        {
            get { return _ClosedPeriodId; }
            set
            {
                if (_ClosedPeriodId != value)
                {
                    _ClosedPeriodId = value;
                    OnPropertyChanged(() => ClosedPeriodId);
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

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }

        public bool Status
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
        public new bool Deleted
        {
            get { return _Deleted; }
            set
            {
                if (_Deleted != value)
                {
                    _Deleted = value;
                    OnPropertyChanged(() => Deleted);
                }
            }
        }

        //public string LongDescription
        //{
        //    get
        //    {
        //        return string.Format("{0}", _Title );
        //    }
        //}

        class ClosedPeriodValidator : AbstractValidator<ClosedPeriod>
        {
            public ClosedPeriodValidator()
            {
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ClosedPeriodValidator();
        }
    }
}
