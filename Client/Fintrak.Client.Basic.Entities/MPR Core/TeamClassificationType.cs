using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class TeamClassificationType : ObjectBase
    {
        int _TeamClassificationTypeId;
        string _Code;
        string _Name;
        string _Description;
        string _Year;
        string _CompanyCode;
        bool _Active;

        public int TeamClassificationTypeId
        {
            get { return _TeamClassificationTypeId; }
            set
            {
                if (_TeamClassificationTypeId != value)
                {
                    _TeamClassificationTypeId = value;
                    OnPropertyChanged(() => TeamClassificationTypeId);
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


        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
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


        class TeamClassificationTypeValidator : AbstractValidator<TeamClassificationType>
        {
            public TeamClassificationTypeValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("FiscalYear is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamClassificationTypeValidator();
        }
    }
}
