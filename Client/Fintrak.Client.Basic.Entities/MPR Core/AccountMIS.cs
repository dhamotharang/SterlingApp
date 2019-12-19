using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class AccountMIS : ObjectBase
    {
        int _AccountMISId;
        string _AccountNo;
        string _TeamDefinitionCode;
        string _TeamCode;
        string _AccountOfficerDefinitionCode;
        string _AccountOfficerCode;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int AccountMISId
        {
            get { return _AccountMISId; }
            set
            {
                if (_AccountMISId != value)
                {
                    _AccountMISId = value;
                    OnPropertyChanged(() => AccountMISId);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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

        
        class AccountMISValidator : AbstractValidator<AccountMIS>
        {
            public AccountMISValidator()
            {
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("Account is required.");
                RuleFor(obj => obj.AccountOfficerCode).NotEmpty().WithMessage("AccountOfficer is required.");
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
       
             
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new AccountMISValidator();
        }
    }
}
