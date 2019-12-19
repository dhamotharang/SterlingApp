using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class SolutionRunDate : ObjectBase
    {
        int _SolutionRunDateId;
        int _SolutionId;
        string _SolutionName;
       DateTime _RunDate;
        bool _Active;

        public int SolutionRunDateId
        {
            get { return _SolutionRunDateId; }
            set
            {
                if (_SolutionRunDateId != value)
                {
                    _SolutionRunDateId = value;
                    OnPropertyChanged(() => SolutionRunDateId);
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

        public string SolutionName
        {
            get { return _SolutionName; }
            set
            {
                if (_SolutionName != value)
                {
                    _SolutionName = value;
                    OnPropertyChanged(() => SolutionName);
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

        //public string LongDescription
        //{
        //    get
        //    {
        //        return string.Format("{0}", _Title );
        //    }
        //}

        class SolutionRunDateValidator : AbstractValidator<SolutionRunDate>
        {
            public SolutionRunDateValidator()
            {
                RuleFor(obj => obj.SolutionRunDateId).GreaterThan(0).WithMessage("SolutionRunDateId is required.");
                //RuleFor(obj => obj.SolutionName).NotEmpty().WithMessage("Solution Name must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SolutionRunDateValidator();
        }
    }
}
