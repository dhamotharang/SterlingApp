using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.Core.Entities
{
    public class Branch : ObjectBase
    {
        int _BranchId;
        string _Code;
        string _Name;
        string _Address;
        int _CompanyId;
        string _Email;
        string _Contact;
        string _Phone;
        bool _Active;

        public int BranchId
        {
            get { return _BranchId; }
            set
            {
                if (_BranchId != value)
                {
                    _BranchId = value;
                    OnPropertyChanged(() => BranchId);
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

        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }

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

        public string Contact
        {
            get { return _Contact; }
            set
            {
                if (_Contact != value)
                {
                    _Contact = value;
                    OnPropertyChanged(() => Contact);
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

       
        class BranchValidator : AbstractValidator<Branch>
        {
            public BranchValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
                RuleFor(obj => obj.CompanyId).NotEmpty().WithMessage("Company must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new BranchValidator();
        }
    }
}
