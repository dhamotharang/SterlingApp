using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class MenuRole : ObjectBase
    {
        int _UserRoleId;
        int _MenuId;
        int _RoleId;
        bool _Active;

        public int UserRoleId
        {
            get { return _UserRoleId; }
            set
            {
                if (_UserRoleId != value)
                {
                    _UserRoleId = value;
                    OnPropertyChanged(() => UserRoleId);
                }
            }
        }

        public int MenuId
        {
            get { return _MenuId; }
            set
            {
                if (_MenuId != value)
                {
                    _MenuId = value;
                    OnPropertyChanged(() => MenuId);
                }
            }
        }

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

        class MenuRoleValidator : AbstractValidator<MenuRole>
        {
            public MenuRoleValidator()
            {
                RuleFor(obj => obj.MenuId).GreaterThan(0).WithMessage("Menu is require.");
                RuleFor(obj => obj.RoleId).GreaterThan(0).WithMessage("Role is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MenuRoleValidator();
        }
    }
}
