using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class ClosedPeriodTemplate : ObjectBase
    {
        int _ClosedPeriodTemplateId;
        int _SolutionId;
        string _Action;
        bool _Active;

        public int ClosedPeriodTemplateId
        {
            get { return _ClosedPeriodTemplateId; }
            set
            {
                if (_ClosedPeriodTemplateId != value)
                {
                    _ClosedPeriodTemplateId = value;
                    OnPropertyChanged(() => ClosedPeriodTemplateId);
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

         public string Action
        {
            get { return _Action; }
            set
            {
                if (_Action != value)
                {
                    _Action = value;
                    OnPropertyChanged(() => Action);
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

        class ClosedPeriodTemplateValidator : AbstractValidator<ClosedPeriodTemplate>
        {
            public ClosedPeriodTemplateValidator()
            {
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ClosedPeriodTemplateValidator();
        }
    }
}
