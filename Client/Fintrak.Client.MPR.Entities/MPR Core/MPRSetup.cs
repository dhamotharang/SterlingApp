using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class Setup : ObjectBase
    {
        int _SetupId;
        string _ExcoDefinitionCode;
        string _ExcoTeamCode;
        int _AccountLenght;
        string _Year;
        Nullable<int> _Period;
        string _CompanyCode;
        bool _Active;
        PoolOption _PoolOption;
        string _SwithMode;
        Nullable<int> _LevelId;

        public int SetupId
        {
            get { return _SetupId; }
            set
            {
                if (_SetupId != value)
                {
                    _SetupId = value;
                    OnPropertyChanged(() => SetupId);
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

        public PoolOption PoolOption
        {
            get { return _PoolOption; }
            set
            {
                if (_PoolOption != value)
                {
                    _PoolOption = value;
                    OnPropertyChanged(() => PoolOption);
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

        public string SwithMode
        {
            get { return _SwithMode; }
            set
            {
                if (_SwithMode != value)
                {
                    _SwithMode = value;
                    OnPropertyChanged(() => SwithMode);
                }
            }
        }

        public Nullable<int> LevelId
        {
            get { return _LevelId; }
            set
            {
                if (_LevelId != value)
                {
                    _LevelId = value;
                    OnPropertyChanged(() => LevelId);
                }
            }

        }
        class SetupValidator : AbstractValidator<Setup>
        {
            public SetupValidator()
            {
                //RuleFor(obj => obj.ExcoTeamCode).GreaterThan(0).WithMessage("Account is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SetupValidator();
        }
    }
}
