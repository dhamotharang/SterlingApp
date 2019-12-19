using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class SecondaryLockLevel : ObjectBase
    {
        int _SecondaryLockLevelId;
        string _ModuleCode;   
        string _DefinitionCode;
        bool _Active;

        public int SecondaryLockLevelId
        {
            get { return _SecondaryLockLevelId; }
            set
            {
                if (_SecondaryLockLevelId != value)
                {
                    _SecondaryLockLevelId = value;
                    OnPropertyChanged(() => SecondaryLockLevelId);
                }
            }
        }

        public string ModuleCode
        {
            get { return _ModuleCode; }
            set
            {
                if (_ModuleCode != value)
                {
                    _ModuleCode = value;
                    OnPropertyChanged(() => ModuleCode);
                }
            }
        }


        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
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
                return string.Format("{0} - {1}", _ModuleCode, _DefinitionCode);
            }
        }

        
        class SecondaryLockLevelValidator : AbstractValidator<SecondaryLockLevel>
        {
            public SecondaryLockLevelValidator()
            {
                RuleFor(obj => obj.ModuleCode).NotEmpty().WithMessage("ModuleCode is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("DefinitionCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SecondaryLockLevelValidator();
        }
    }
}
