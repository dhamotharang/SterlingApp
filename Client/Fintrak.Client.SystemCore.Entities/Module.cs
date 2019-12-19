using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Module : ObjectBase
    {
        int _ModuleId;
        string _Name;
        string _Alias;
        int _SolutionId;
        bool _CanUpdate;
        bool _Active;

        public int ModuleId
        {
            get { return _ModuleId; }
            set
            {
                if (_ModuleId != value)
                {
                    _ModuleId = value;
                    OnPropertyChanged(() => ModuleId);
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

        public string Alias
        {
            get { return _Alias; }
            set
            {
                if (_Alias != value)
                {
                    _Alias = value;
                    OnPropertyChanged(() => Alias);
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

        public bool CanUpdate
        {
            get { return _CanUpdate; }
            set
            {
                if (_CanUpdate != value)
                {
                    _CanUpdate = value;
                    OnPropertyChanged(() => CanUpdate);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Name, _Alias );
            }
        }

        class ModuleValidator : AbstractValidator<Module>
        {
            public ModuleValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Alias).NotEmpty().WithMessage("Alias must not be empty.");
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ModuleValidator();
        }
    }
}
