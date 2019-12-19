using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class UserRole : ObjectBase
    {
        int _UserRoleId;
        int _UserSetupId;
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

        public int UserSetupId
        {
            get { return _UserSetupId; }
            set
            {
                if (_UserSetupId != value)
                {
                    _UserSetupId = value;
                    OnPropertyChanged(() => UserSetupId);
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

        class UserRoleValidator : AbstractValidator<UserRole>
        {
            public UserRoleValidator()
            {
                RuleFor(obj => obj.UserSetupId).GreaterThan(0).WithMessage("User Setup is require.");
                RuleFor(obj => obj.RoleId).GreaterThan(0).WithMessage("Role is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserRoleValidator();
        }
    }
}
