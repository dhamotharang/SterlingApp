using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class ActivityBaseRatio : ObjectBase
    {
        int _ActivityBaseRatioId;
        string _ServiceClass;
        double _Ratio;
        bool _Active;


        public int ActivityBaseRatioId
        {
            get { return _ActivityBaseRatioId; }
            set
            {
                if (_ActivityBaseRatioId != value)
                {
                    _ActivityBaseRatioId = value;
                    OnPropertyChanged(() => ActivityBaseRatioId);
                }
            }
        }

        public string ServiceClass
        {
            get { return _ServiceClass; }
            set
            {
                if (_ServiceClass != value)
                {
                    _ServiceClass = value;
                    OnPropertyChanged(() => ServiceClass);
                }
            }
        }

       

        public double Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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


        
        class ActivityBaseRatioValidator : AbstractValidator<ActivityBaseRatio>
        {
            public ActivityBaseRatioValidator()
            {
                RuleFor(obj => obj.ServiceClass).NotEmpty().WithMessage("ServiceClass is required.");               
             }
        }

        protected override IValidator GetValidator()
        {
            return new ActivityBaseRatioValidator();
        }
    }
}
