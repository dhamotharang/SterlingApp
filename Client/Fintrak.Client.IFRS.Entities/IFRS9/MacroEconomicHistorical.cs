using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomicHistorical : ObjectBase
    {
        int _MacroEconomicHistoricalId;
        string _Sector_Code;
        int _Year;
        int _Type;
        string _Variable;
        double? _Value;
        bool _Active;

        public int MacroEconomicHistoricalId
        {
            get { return _MacroEconomicHistoricalId; }
            set
            {
                if (_MacroEconomicHistoricalId != value)
                {
                    _MacroEconomicHistoricalId = value;
                    OnPropertyChanged(() => MacroEconomicHistoricalId);
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


        public int Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public string Variable
        {
            get { return _Variable; }
            set
            {
                if (_Variable != value)
                {
                    _Variable = value;
                    OnPropertyChanged(() => Variable);
                }
            }
        }

        public double? Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    OnPropertyChanged(() => Value);
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



        class MacroEconomicHistoricalValidator : AbstractValidator<MacroEconomicHistorical>
        {
            public MacroEconomicHistoricalValidator()
            {
                RuleFor(obj => obj.Sector_Code).NotEmpty().WithMessage("Sector is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicHistoricalValidator();
        }
    }
}
