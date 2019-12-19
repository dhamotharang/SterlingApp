using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class CompanyUser : ObjectBase
    {
        int _CompanyUserId;
        int _UserId;
        string _CompanyCode;
        bool _Active;

        public int CompanyUserId
        {
            get { return _CompanyUserId; }
            set
            {
                if (_CompanyUserId != value)
                {
                    _CompanyUserId = value;
                    OnPropertyChanged(() => CompanyUserId);
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

        public int UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    OnPropertyChanged(() => UserId);
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

        class CompanyUserValidator : AbstractValidator<CompanyUser>
        {
            public CompanyUserValidator()
            {
                RuleFor(obj => obj.UserId).NotEmpty().WithMessage("UserId must not be empty.");
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("CompanyCode must not be empty.");
                           }
        }

        protected override IValidator GetValidator()
        {
            return new CompanyUserValidator();
        }
    }
}
