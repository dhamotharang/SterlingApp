using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class CompanySecurity : ObjectBase
    {
        int _CompanySecurityId;
        string _CompanyCode;
        string _Root;
        string _Filter;
        string _Attributes;
        string _Scope;
        bool _Active;

        public int CompanySecurityId
        {
            get { return _CompanySecurityId; }
            set
            {
                if (_CompanySecurityId != value)
                {
                    _CompanySecurityId = value;
                    OnPropertyChanged(() => CompanySecurityId);
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
                    _CompanyCode = value.Trim();
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public string Root
        {
            get { return _Root; }
            set
            {
                if (_Root != value)
                {
                    _Root = value.Trim();
                    OnPropertyChanged(() => Root);
                }
            }
        }

        public string Filter
        {
            get { return _Filter; }
            set
            {
                if (_Filter != value)
                {
                    _Filter = value.Trim();
                    OnPropertyChanged(() => Filter);
                }
            }
        }

        public string Attributes
        {
            get { return _Attributes; }
            set
            {
                if (_Attributes != value)
                {
                    _Attributes = value.Trim();
                    OnPropertyChanged(() => Attributes);
                }
            }
        }

        public string Scope
        {
            get { return _Scope; }
            set
            {
                if (_Scope != value)
                {
                    _Scope = value.Trim();
                    OnPropertyChanged(() => Scope);
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

       

        class CompanySecurityValidator : AbstractValidator<CompanySecurity>
        {
            public CompanySecurityValidator()
            {
                RuleFor(obj => obj.Root).NotEmpty().WithMessage("Root must not be empty.");
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("CompanyCode must not be empty.");
                RuleFor(obj => obj.Attributes).NotEmpty().WithMessage("Attributes must not be empty.");
                RuleFor(obj => obj.Filter).NotEmpty().WithMessage("Filter must not be empty.");
                RuleFor(obj => obj.Scope).NotEmpty().WithMessage("Scope must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CompanySecurityValidator();
        }
    }
}
