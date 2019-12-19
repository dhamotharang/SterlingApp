using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class OfficerDetail : ObjectBase
    {
        int _OfficerDetailId;
        string _MisCode;
        string _Name;
        string _StaffID;
        string _DefinitionCode;
        string _Year;
        string _Email;
        string _Mobile;
        string _ReviewCode; 
        bool _Active;

        public int OfficerDetailId
        {
            get { return _OfficerDetailId; }
            set
            {
                if (_OfficerDetailId != value)
                {
                    _OfficerDetailId = value;
                    OnPropertyChanged(() => OfficerDetailId);
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

        public string DefinitionCode
        {
            get { return _DefinitionCode; }
            set
            {
                if (_DefinitionCode != value)
                {
                    _DefinitionCode = value;
                    OnPropertyChanged(() => DefinitionCode);
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

        public string Mobile
        {
            get { return _Mobile; }
            set
            {
                if (_Mobile != value)
                {
                    _Mobile = value;
                    OnPropertyChanged(() => Mobile);
                }
            }
        }


        public string ReviewCode
        {
            get { return _ReviewCode; }
            set
            {
                if (_ReviewCode != value)
                {
                    _ReviewCode = value;
                    OnPropertyChanged(() => ReviewCode);
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

        class OfficerDetailValidator : AbstractValidator<OfficerDetail>
        {
            public OfficerDetailValidator()
            {
                RuleFor(obj => obj.MisCode).NotEmpty().WithMessage("MisCode is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.DefinitionCode).NotEmpty().WithMessage("Definition is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new OfficerDetailValidator();
        }
    }
}
