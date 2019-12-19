using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Core.Entities
{
    public class ExtractionRole : ObjectBase
    {
        int _ExtractionRoleId;
        int _ExtractionId;
        int _RoleId;
        bool _Active;

        public int ExtractionRoleId
        {
            get { return _ExtractionRoleId; }
            set
            {
                if (_ExtractionRoleId != value)
                {
                    _ExtractionRoleId = value;
                    OnPropertyChanged(() => ExtractionRoleId);
                }
            }
        }

        public int ExtractionId
        {
            get { return _ExtractionId; }
            set
            {
                if (_ExtractionId != value)
                {
                    _ExtractionId = value;
                    OnPropertyChanged(() => ExtractionId);
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

        class ExtractionRoleValidator : AbstractValidator<ExtractionRole>
        {
            public ExtractionRoleValidator()
            {
                RuleFor(obj => obj.ExtractionId).NotEmpty().WithMessage("Extraction is require.");
                RuleFor(obj => obj.RoleId).GreaterThan(0).WithMessage("Role is require.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ExtractionRoleValidator();
        }
    }
}
