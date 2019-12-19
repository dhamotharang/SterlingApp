using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class UploadRole : ObjectBase
    {
        int _UploadRoleId;
        int _UploadId;
        int _RoleId;
        bool _Active;

        public int UploadRoleId
        {
            get { return _UploadRoleId; }
            set
            {
                if (_UploadRoleId != value)
                {
                    _UploadRoleId = value;
                    OnPropertyChanged(() => UploadRoleId);
                }
            }
        }

        public int UploadId
        {
            get { return _UploadId; }
            set
            {
                if (_UploadId != value)
                {
                    _UploadId = value;
                    OnPropertyChanged(() => UploadId);
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

        class UploadRoleValidator : AbstractValidator<UploadRole>
        {
            public UploadRoleValidator()
            {
                RuleFor(obj => obj.UploadId).NotEmpty().WithMessage("Upload is require.");
                RuleFor(obj => obj.RoleId).GreaterThan(0).WithMessage("Role is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new UploadRoleValidator();
        }
    }
}
