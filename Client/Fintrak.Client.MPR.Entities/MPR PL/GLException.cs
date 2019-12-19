using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class GLException : ObjectBase
    {
        int _GLExceptionId;
        string _GLAccount;
        string _CompanyCode;
        bool _Active;


        public int GLExceptionId
        {
            get { return _GLExceptionId; }
            set
            {
                if (_GLExceptionId != value)
                {
                    _GLExceptionId = value;
                    OnPropertyChanged(() => GLExceptionId);
                }
            }
        }

        public string GLAccount
        {
            get { return _GLAccount; }
            set
            {
                if (_GLAccount != value)
                {
                    _GLAccount = value;
                    OnPropertyChanged(() => GLAccount);
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


        
        class GLExceptionValidator : AbstractValidator<GLException>
        {
            public GLExceptionValidator()
            {
                RuleFor(obj => obj.GLAccount).NotEmpty().WithMessage("GLAccount is required.");
                RuleFor(obj => obj.CompanyCode).NotEmpty().WithMessage("Company Code is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new GLExceptionValidator();
        }
    }
}
