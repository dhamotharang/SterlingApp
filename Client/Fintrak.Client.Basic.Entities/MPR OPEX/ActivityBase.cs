using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ActivityBase : ObjectBase
    {
        int _ActivityBaseId;
        string _ServiceCode;
        string _ServiceDescription;
        string _ServiceCategory;
        decimal _Weight;
        bool _Active;


        public int ActivityBaseId
        {
            get { return _ActivityBaseId; }
            set
            {
                if (_ActivityBaseId != value)
                {
                    _ActivityBaseId = value;
                    OnPropertyChanged(() => ActivityBaseId);
                }
            }
        }

        public string ServiceCode
        {
            get { return _ServiceCode; }
            set
            {
                if (_ServiceCode != value)
                {
                    _ServiceCode = value;
                    OnPropertyChanged(() => ServiceCode);
                }
            }
        }

        public string ServiceDescription
        {
            get { return _ServiceDescription; }
            set
            {
                if (_ServiceDescription != value)
                {
                    _ServiceDescription = value;
                    OnPropertyChanged(() => ServiceDescription);
                }
            }
        }


        public string ServiceCategory
        {
            get { return _ServiceCategory; }
            set
            {
                if (_ServiceCategory != value)
                {
                    _ServiceCategory = value;
                    OnPropertyChanged(() => ServiceCategory);
                }
            }
        }

        public decimal Weight
        {
            get { return _Weight; }
            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged(() => Weight);
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


        
        class ActivityBaseValidator : AbstractValidator<ActivityBase>
        {
            public ActivityBaseValidator()
            {
                RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityBaseValidator();
        }
    }
}
