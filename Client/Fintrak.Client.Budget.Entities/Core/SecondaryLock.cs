using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class SecondaryLock : ObjectBase
    {
        int _SecondaryLockId;
        string _ModuleCode;
        string _MisCode;
        string _DefinitionCode;
        string _Note;
        string _Year;
        bool _Active;

        public int SecondaryLockId
        {
            get { return _SecondaryLockId; }
            set
            {
                if (_SecondaryLockId != value)
                {
                    _SecondaryLockId = value;
                    OnPropertyChanged(() => SecondaryLockId);
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


        public string MisCode
        {
            get { return _MisCode; }
            set
            {
                if (_MisCode != value)
                {
                    _MisCode = value;
                    OnPropertyChanged(() => MisCode);
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

        public string Note
        {
            get { return _Note; }
            set
            {
                if (_Note != value)
                {
                    _Note = value;
                    OnPropertyChanged(() => Note);
                }
            }
        }

        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
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
                return string.Format("{0} - {1}", _MisCode, _DefinitionCode);
            }
        }

        
        class SecondaryLockValidator : AbstractValidator<SecondaryLock>
        {
            public SecondaryLockValidator()
            {
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("DefinitionCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SecondaryLockValidator();
        }
    }
}
