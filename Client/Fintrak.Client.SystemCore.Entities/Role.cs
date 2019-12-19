using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.SystemCore.Framework;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Role : ObjectBase
    {
        int _RoleId;
        string _Name;
        string _Description;
        RoleType _Type;
        int _SolutionId;
        bool _Active;

        public int RoleId
        {
            get { return _RoleId; }
            set
            {
                if (_RoleId != value)
                {
                    _RoleId = value;
                    OnPropertyChanged(() => RoleId);
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

        public RoleType Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
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
                return string.Format("{0}", _Name );
            }
        }

        class RoleValidator : AbstractValidator<Role>
        {
            public RoleValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.SolutionId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new RoleValidator();
        }
    }
}
