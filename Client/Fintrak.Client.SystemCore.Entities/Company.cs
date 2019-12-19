using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class Company : ObjectBase
    {
        int _CompanyId;
        string _Code;
        string _Name;
        string _Email;
        bool _Active;

        public int CompanyId
        {
            get { return _CompanyId; }
            set
            {
                if (_CompanyId != value)
                {
                    _CompanyId = value;
                    OnPropertyChanged(() => CompanyId);
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

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(() => Email);
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
                return string.Format("{0} - {1}", _Name, _Email );
            }
        }

        class CompanyValidator : AbstractValidator<Company>
        {
            public CompanyValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.Email).NotEmpty().WithMessage("Email must not be empty.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CompanyValidator();
        }
    }
}
