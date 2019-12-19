using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class ModificationLevel : ObjectBase
    {
        int _ModificationLevelId;
        string _ModuleCode;
        string _DefinitionCode;
        bool _Status;
        bool _Active;

        public int ModificationLevelId
        {
            get { return _ModificationLevelId; }
            set
            {
                if (_ModificationLevelId != value)
                {
                    _ModificationLevelId = value;
                    OnPropertyChanged(() => ModificationLevelId);
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


        public bool Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
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

        
        class ModificationLevelValidator : AbstractValidator<ModificationLevel>
        {
            public ModificationLevelValidator()
            {
                RuleFor(obj => obj.ModuleCode).NotEmpty().WithMessage("ModuleCode is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("DefinitionCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ModificationLevelValidator();
        }
    }
}
