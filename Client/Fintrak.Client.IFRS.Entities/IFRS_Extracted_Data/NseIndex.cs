using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class NseIndex : ObjectBase
    {
        int _NseIndexId;
        DateTime _Date;
        Nullable<double> _NSEIndex;
        bool _Active;


        public int NseIndexId

        {
            get { return _NseIndexId; }
            set
            {
                if (_NseIndexId != value)
                {
                    _NseIndexId = value;
                    OnPropertyChanged(() => NseIndexId);
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

        public Nullable<double> NSEIndex
        {
            get { return _NSEIndex; }
            set
            {
                if (_NSEIndex != value)
                {
                    _NSEIndex = value;
                    OnPropertyChanged(() => NSEIndex);
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
        class NseIndexValidator : AbstractValidator<NseIndex>
        {
            public NseIndexValidator()
            {
                //RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Refno is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new NseIndexValidator();
        }
    }
}
