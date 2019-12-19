using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroeconomicVDisplay : ObjectBase
    {
        int _MacroVariableDisplayId;
        string _Sector_Code;
        string _Sector_Name;
        int _Year;
        double _Exchange_Rate;
        double _GDP;
        double _Oil_Price;
        double _Unemployment_Rate;
        double _Inflation_Rate;
        string _VType;
        bool _Active;

        public int MacroVariableDisplayId
        {
            get { return _MacroVariableDisplayId; }
            set
            {
                if (_MacroVariableDisplayId != value)
                {
                    _MacroVariableDisplayId = value;
                    OnPropertyChanged(() => MacroVariableDisplayId);
                }
            }
        }

        public string Sector_Code
        {
            get { return _Sector_Code; }
            set
            {
                if (_Sector_Code != value)
                {
                    _Sector_Code = value;
                    OnPropertyChanged(() => Sector_Code);
                }
            }
        }

        public string VType
        {
            get { return _VType; }
            set
            {
                if (_VType != value)
                {
                    _VType = value;
                    OnPropertyChanged(() => VType);
                }
            }
        }

        public string Sector_Name
        {
            get { return _Sector_Name; }
            set
            {
                if (_Sector_Name != value)
                {
                    _Sector_Name = value;
                    OnPropertyChanged(() => Sector_Name);
                }
            }
        }


        public int Year
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

        public double Exchange_Rate
        {
            get { return _Exchange_Rate; }
            set
            {
                if (_Exchange_Rate != value)
                {
                    _Exchange_Rate = value;
                    OnPropertyChanged(() => Exchange_Rate);
                }
            }
        }

        public double GDP
        {
            get { return _GDP; }
            set
            {
                if (_GDP != value)
                {
                    _GDP = value;
                    OnPropertyChanged(() => GDP);
                }
            }
        }

        public double Oil_Price
        {
            get { return _Oil_Price; }
            set
            {
                if (_Oil_Price != value)
                {
                    _Oil_Price = value;
                    OnPropertyChanged(() => Oil_Price);
                }
            }
        }

        public double Unemployment_Rate
        {
            get { return _Unemployment_Rate; }
            set
            {
                if (_Unemployment_Rate != value)
                {
                    _Unemployment_Rate = value;
                    OnPropertyChanged(() => Unemployment_Rate);
                }
            }
        }


        public double Inflation_Rate
        {
            get { return _Inflation_Rate; }
            set
            {
                if (_Inflation_Rate != value)
                {
                    _Inflation_Rate = value;
                    OnPropertyChanged(() => Inflation_Rate);
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


        class MacroeconomicVDisplayValidator : AbstractValidator<MacroeconomicVDisplay>
        {
            public MacroeconomicVDisplayValidator()
            {
                RuleFor(obj => obj.Sector_Code).NotEmpty().WithMessage("Sector Code is required.");
                RuleFor(obj => obj.Sector_Name).NotEmpty().WithMessage("Sector  Name is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroeconomicVDisplayValidator();
        }
    }
}
