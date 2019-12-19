using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class Staffs : ObjectBase
    {
        int _StaffId;
        string _StaffCode;
        string _Name;
        string _Email;
        string _Phone;
        bool _Active;

        public int StaffId
        {
            get { return _StaffId; }
            set
            {
                if (_StaffId != value)
                {
                    _StaffId = value;
                    OnPropertyChanged(() => StaffId);
                }
            }
        }

        public string StaffCode
        {
            get { return _StaffCode; }
            set
            {
                if (_StaffCode != value)
                {
                    _StaffCode = value;
                    OnPropertyChanged(() => StaffCode);
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

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _Name, _Email );
            }
        }

        class StaffsValidator : AbstractValidator<Staffs>
        {
            public StaffsValidator()
            {
                RuleFor(obj => obj.StaffCode).NotEmpty().WithMessage("StaffCode must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
               
            }
        }

        protected override IValidator GetValidator()
        {
            return new StaffsValidator();
        }
    }
}
