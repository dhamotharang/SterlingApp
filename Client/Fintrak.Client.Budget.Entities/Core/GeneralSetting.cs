using System;
using System.Linq;
using Fintrak.Shared.Budget.Framework.Enums;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Budget.Entities
{
    public class GeneralSetting : ObjectBase
    {
        int _GeneralSettingId;
        Months _StartMonth;
        Months _EndMonth;
        Months _CurrentMonth;
        string _ReviewCode; 
        string _Year;
        bool _Active;

        public int GeneralSettingId
        {
            get { return _GeneralSettingId; }
            set
            {
                if (_GeneralSettingId != value)
                {
                    _GeneralSettingId = value;
                    OnPropertyChanged(() => GeneralSettingId);
                }
            }
        }


        public Months StartMonth
        {
            get { return _StartMonth; }
            set
            {
                if (_StartMonth != value)
                {
                    _StartMonth = value;
                    OnPropertyChanged(() => StartMonth);
                }
            }
        }

        public Months EndMonth
        {
            get { return _EndMonth; }
            set
            {
                if (_EndMonth != value)
                {
                    _EndMonth = value;
                    OnPropertyChanged(() => EndMonth);
                }
            }
        }

        public Months CurrentMonth
        {
            get { return _CurrentMonth; }
            set
            {
                if (_CurrentMonth != value)
                {
                    _CurrentMonth = value;
                    OnPropertyChanged(() => CurrentMonth);
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
                return string.Format("{0} - {1}", _StartMonth, _EndMonth);
            }
        }

        
        class GeneralSettingValidator : AbstractValidator<GeneralSetting>
        {
            public GeneralSettingValidator()
            {
                RuleFor(obj => obj.StartMonth).NotEmpty().WithMessage("StartMonth is required.");
                RuleFor(obj => obj.EndMonth).NotEmpty().WithMessage("EndMonth is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new GeneralSettingValidator();
        }
    }
}
