using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SICRParameters : ObjectBase
    {
        int _ID;
        string _SICR_Param;
        string _SICR_Desc;
        bool _Active;

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

        public string SICR_Param
        {
            get { return _SICR_Param; }
            set
            {
                if (_SICR_Param != value)
                {
                    _SICR_Param = value;
                    OnPropertyChanged(() => SICR_Param);
                }
            }
        }


        public string SICR_Desc
        {
            get { return _SICR_Desc; }
            set
            {
                if (_SICR_Desc != value)
                {
                    _SICR_Desc = value;
                    OnPropertyChanged(() => SICR_Desc);
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


        class SICRParametersValidator : AbstractValidator<SICRParameters>
        {
            public SICRParametersValidator()
            {
                RuleFor(obj => obj.SICR_Param).NotEmpty().WithMessage("Parameter is required.");
                RuleFor(obj => obj.SICR_Desc).NotEmpty().WithMessage("Description is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SICRParametersValidator();
        }
    }
}
