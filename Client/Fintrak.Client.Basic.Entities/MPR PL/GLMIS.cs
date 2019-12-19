using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class GLMIS : ObjectBase
    {
        int _GLMISId;
        string _GLAccount;
        string _TeamDefinitionCode;
        string _AccountOfficerDefinitionCode;
        string _AccountOfficerCode;
        string _TeamCode;
        string _CompanyCode;
        bool _Active;

        public int GLMISId
        {
            get { return _GLMISId; }
            set
            {
                if (_GLMISId != value)
                {
                    _GLMISId = value;
                    OnPropertyChanged(() => GLMISId);
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

        public string TeamDefinitionCode
        {
            get { return _TeamDefinitionCode; }
            set
            {
                if (_TeamDefinitionCode != value)
                {
                    _TeamDefinitionCode = value;
                    OnPropertyChanged(() => TeamDefinitionCode);
                }
            }
        }


        public string AccountOfficerDefinitionCode
        {
            get { return _AccountOfficerDefinitionCode; }
            set
            {
                if (_AccountOfficerDefinitionCode != value)
                {
                    _AccountOfficerDefinitionCode = value;
                    OnPropertyChanged(() => AccountOfficerDefinitionCode);
                }
            }
        }



        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }


        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
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

        
        class GLMISValidator : AbstractValidator<GLMIS>
        {
            public GLMISValidator()
            {
                RuleFor(obj => obj.GLAccount).NotEmpty().WithMessage("GLAccount is required.");
                RuleFor(obj => obj.AccountOfficerDefinitionCode).NotEmpty().WithMessage("AccountOfficerDefinitionCode is required.");
                RuleFor(obj => obj.AccountOfficerCode).NotEmpty().WithMessage("AccountOfficerCode is required.");
                RuleFor(obj => obj.TeamDefinitionCode).NotEmpty().WithMessage("TeamDefinitionCode is required.");
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("TeamCode is required.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new GLMISValidator();
        }
    }
}
