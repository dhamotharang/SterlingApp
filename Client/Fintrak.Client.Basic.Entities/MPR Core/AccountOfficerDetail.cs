using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class AccountOfficerDetail : ObjectBase
    {
        int _AccountOfficerDetailId;
        string _MisCode;
        string _StaffID;
        string _Email;
        string _Phone;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int AccountOfficerDetailId
        {
            get { return _AccountOfficerDetailId; }
            set
            {
                if (_AccountOfficerDetailId != value)
                {
                    _AccountOfficerDetailId = value;
                    OnPropertyChanged(() => AccountOfficerDetailId);
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

        public string StaffID
        {
            get { return _StaffID; }
            set
            {
                if (_StaffID != value)
                {
                    _StaffID = value;
                    OnPropertyChanged(() => StaffID);
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

        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (_Phone != value)
                {
                    _Phone = value;
                    OnPropertyChanged(() => Phone);
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

        
        class AccountOfficerDetailValidator : AbstractValidator<AccountOfficerDetail>
        {
            public AccountOfficerDetailValidator()
            {

                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("AccountOfficer is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new AccountOfficerDetailValidator();
        }
    }
}
