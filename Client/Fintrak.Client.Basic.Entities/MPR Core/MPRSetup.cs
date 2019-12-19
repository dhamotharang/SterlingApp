using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRSetup : ObjectBase
    {
        int _MPRSetupId;
        string _ExcoDefinitionCode;
        string _ExcoTeamCode;
        int _AccountLenght;
        string _Year;
        string _CompanyCode;
        bool _Active;
        Nullable<int> _Period;


        public Nullable<int> Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }


        public int MPRSetupId
        {
            get { return _MPRSetupId; }
            set
            {
                if (_MPRSetupId != value)
                {
                    _MPRSetupId = value;
                    OnPropertyChanged(() => MPRSetupId);
                }
            }
        }

        public string ExcoDefinitionCode
        {
            get { return _ExcoDefinitionCode; }
            set
            {
                if (_ExcoDefinitionCode != value)
                {
                    _ExcoDefinitionCode = value;
                    OnPropertyChanged(() => ExcoDefinitionCode);
                }
            }
        }

        public string ExcoTeamCode
        {
            get { return _ExcoTeamCode; }
            set
            {
                if (_ExcoTeamCode != value)
                {
                    _ExcoTeamCode = value;
                    OnPropertyChanged(() => ExcoTeamCode);
                }
            }
        }

        public int AccountLenght
        {
            get { return _AccountLenght; }
            set
            {
                if (_AccountLenght != value)
                {
                    _AccountLenght = value;
                    OnPropertyChanged(() => AccountLenght);
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


        class MPRSetupValidator : AbstractValidator<MPRSetup>
        {
            public MPRSetupValidator()
            {
                //RuleFor(obj => obj.ExcoTeamCode).GreaterThan(0).WithMessage("Account is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MPRSetupValidator();
        }
    }
}
