using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class CompanyModule : ObjectBase
    {
        int _CompanyModuleId;
        string _CompanyCode;
        string _ModuleName;      
        bool _Active;

        public int CompanyModuleId
        {
            get { return _CompanyModuleId; }
            set
            {
                if (_CompanyModuleId != value)
                {
                    _CompanyModuleId = value;
                    OnPropertyChanged(() => CompanyModuleId);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public string ModuleName
        {
            get { return _ModuleName; }
            set
            {
                if (_ModuleName != value)
                {
                    _ModuleName = value;
                    OnPropertyChanged(() => ModuleName);
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

   
        class CompanyModuleValidator : AbstractValidator<CompanyModule>
        {
            public CompanyModuleValidator()
            {
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("CompanyCode must not be empty.");
                RuleFor(obj => obj.ModuleName).NotEmpty().WithMessage("MoludeName must not be empty.");             
            }
        }

        protected override IValidator GetValidator()
        {
            return new CompanyModuleValidator();
        }
    }
}
