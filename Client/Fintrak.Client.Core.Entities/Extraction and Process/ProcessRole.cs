using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class ProcessRole : ObjectBase
    {
        int _ProcessRoleId;
        int _ProcessId;
        int _RoleId;
        bool _Active;

        public int ProcessRoleId
        {
            get { return _ProcessRoleId; }
            set
            {
                if (_ProcessRoleId != value)
                {
                    _ProcessRoleId = value;
                    OnPropertyChanged(() => ProcessRoleId);
                }
            }
        }

        public int ProcessId
        {
            get { return _ProcessId; }
            set
            {
                if (_ProcessId != value)
                {
                    _ProcessId = value;
                    OnPropertyChanged(() => ProcessId);
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

        class ProcessRoleValidator : AbstractValidator<ProcessRole>
        {
            public ProcessRoleValidator()
            {
                RuleFor(obj => obj.ProcessId).NotEmpty().WithMessage("Process is require.");
                RuleFor(obj => obj.RoleId).GreaterThan(0).WithMessage("Solution is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProcessRoleValidator();
        }
    }
}
