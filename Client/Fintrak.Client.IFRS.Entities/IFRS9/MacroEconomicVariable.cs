using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomicVariable : ObjectBase
    {
        int _MacroEconomicVariableId;
        string _Name;
        string _Description;
        bool _IsGeneric;
        bool _Active;

        public int MacroEconomicVariableId
        {
            get { return _MacroEconomicVariableId; }
            set
            {
                if (_MacroEconomicVariableId != value)
                {
                    _MacroEconomicVariableId = value;
                    OnPropertyChanged(() => MacroEconomicVariableId);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }



        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public bool IsGeneric
        {
            get { return _IsGeneric; }
            set
            {
                if (_IsGeneric != value)
                {
                    _IsGeneric = value;
                    OnPropertyChanged(() => IsGeneric);
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


        class MacroEconomicVariableValidator : AbstractValidator<MacroEconomicVariable>
        {
            public MacroEconomicVariableValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicVariableValidator();
        }
    }
}
