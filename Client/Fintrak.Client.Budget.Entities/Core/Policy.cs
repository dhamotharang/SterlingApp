using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class Policy : ObjectBase
    {
        int _PolicyId;
        string _Code;
        string _ModuleCode;
        string _Name;
        bool _Active;

        public int PolicyId
        {
            get { return _PolicyId; }
            set
            {
                if (_PolicyId != value)
                {
                    _PolicyId = value;
                    OnPropertyChanged(() => PolicyId);
                }
            }
        }


        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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
                return string.Format("{0} - {1}", _ModuleCode, _Name);
            }
        }

        
        class PolicyValidator : AbstractValidator<Policy>
        {
            public PolicyValidator()
            {
                RuleFor(obj => obj.ModuleCode).NotEmpty().WithMessage("ModuleCode is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new PolicyValidator();
        }
    }
}
