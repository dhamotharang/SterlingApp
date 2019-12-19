using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HCClassification : ObjectBase
    {
        public int _ID;
        public string _HC1;
        public string _HC2;
        public bool _Active;



        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }

        public string HC1
        {
            get { return _HC1; }
            set
            {
                if (_HC1 != value)
                {
                    _HC1 = value;
                    OnPropertyChanged(() => HC1);
                }
            }
        }
        public string HC2
        {
            get { return _HC2; }
            set
            {
                if (_HC2 != value)
                {
                    _HC2 = value;
                    OnPropertyChanged(() => HC2);
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


        class HCClassificationValidator : AbstractValidator<HCClassification>
        {
            public HCClassificationValidator()
            {
                //RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new HCClassificationValidator();
        }
    }
}