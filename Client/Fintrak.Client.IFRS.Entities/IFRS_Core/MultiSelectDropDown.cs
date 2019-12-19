using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MultiSelectDropDown : ObjectBase
    {
        string _icon;
        string _name;
        string _maker;
        bool _ticked;
        bool _Active;

        public string icon
        {
            get { return _icon; }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged(() => icon);
                }
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => name);
                }
            }
        }

        public string maker
        {
            get { return _maker; }
            set
            {
                if (_maker != value)
                {
                    _maker = value;
                    OnPropertyChanged(() => maker);
                }
            }
        }

        public bool ticked
        {
            get { return _ticked; }
            set
            {
                if (_ticked != value)
                {
                    _ticked = value;
                    OnPropertyChanged(() => ticked);
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


        class MultiSelectDropDownValidator : AbstractValidator<MultiSelectDropDown>
        {
            public MultiSelectDropDownValidator()
            {
                RuleFor(obj => obj.name).NotEmpty().WithMessage("name is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new MultiSelectDropDownValidator();
        }
    }
}
