using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class PolicyLevel : ObjectBase
    {
        int _PolicyLevelId;
        string _PolicyCode;
        string _ModuleCode;
        string _DefinitionCode;
        CenterTypeEnum _Center;
        string _ReviewCode;  
        string _Year;
        bool _Active;

        public int PolicyLevelId
        {
            get { return _PolicyLevelId; }
            set
            {
                if (_PolicyLevelId != value)
                {
                    _PolicyLevelId = value;
                    OnPropertyChanged(() => PolicyLevelId);
                }
            }
        }


        public string PolicyCode
        {
            get { return _PolicyCode; }
            set
            {
                if (_PolicyCode != value)
                {
                    _PolicyCode = value;
                    OnPropertyChanged(() => PolicyCode);
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

        public CenterTypeEnum Center
        {
            get { return _Center; }
            set
            {
                if (_Center != value)
                {
                    _Center = value;
                    OnPropertyChanged(() => Center);
                }
            }
        }


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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
                return string.Format("{0} - {1}", _ModuleCode, _DefinitionCode);
            }
        }

        
        class PolicyLevelValidator : AbstractValidator<PolicyLevel>
        {
            public PolicyLevelValidator()
            {
                RuleFor(obj => obj.ModuleCode).NotEmpty().WithMessage("ModuleCode is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("DefinitionCode is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new PolicyLevelValidator();
        }
    }
}
