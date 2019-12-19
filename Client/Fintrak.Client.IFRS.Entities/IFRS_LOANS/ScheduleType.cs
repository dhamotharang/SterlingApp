using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ScheduleType : ObjectBase
    {
        int _ScheduleTypeId;
        string _Code;
        string _Name;
        string _ActionName;
        bool _IsDefault;
        string _CompanyCode;
        bool _Active;

        public int ScheduleTypeId
        {
            get { return _ScheduleTypeId; }
            set
            {
                if (_ScheduleTypeId != value)
                {
                    _ScheduleTypeId = value;
                    OnPropertyChanged(() => ScheduleTypeId);
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

        public string ActionName
        {
            get { return _ActionName; }
            set
            {
                if (_ActionName != value)
                {
                    _ActionName = value;
                    OnPropertyChanged(() => ActionName);
                }
            }
        }

        public bool IsDefault
        {
            get { return _IsDefault; }
            set
            {
                if (_IsDefault != value)
                {
                    _IsDefault = value;
                    OnPropertyChanged(() => IsDefault);
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


        class ScheduleTypeValidator : AbstractValidator<ScheduleType>
        {
            public ScheduleTypeValidator()
            {
                RuleFor(obj => obj.Code).NotEmpty().WithMessage("Code is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ScheduleTypeValidator();
        }
    }
}
