using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomic : ObjectBase
    {
        int _MacroEconomicId;
        string _Sector_Code;
        int _Year;
        double? _Variable1;
        double? _Variable2;
        double? _Variable3;
        double? _Variable4;
        double? _Variable5;
        //string _CompanyCode;
        bool _Active;

        public int MacroEconomicId
        {
            get { return _MacroEconomicId; }
            set
            {
                if (_MacroEconomicId != value)
                {
                    _MacroEconomicId = value;
                    OnPropertyChanged(() => MacroEconomicId);
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


        public double? Variable1
        {
            get { return _Variable1; }
            set
            {
                if (_Variable1 != value)
                {
                    _Variable1 = value;
                    OnPropertyChanged(() => Variable1);
                }
            }
        }

        public double? Variable2
        {
            get { return _Variable2; }
            set
            {
                if (_Variable2 != value)
                {
                    _Variable2 = value;
                    OnPropertyChanged(() => Variable2);
                }
            }
        }

        public double? Variable3
        {
            get { return _Variable3; }
            set
            {
                if (_Variable3 != value)
                {
                    _Variable3 = value;
                    OnPropertyChanged(() => Variable3);
                }
            }
        }

        public double? Variable4
        {
            get { return _Variable4; }
            set
            {
                if (_Variable4 != value)
                {
                    _Variable4= value;
                    OnPropertyChanged(() => Variable4);
                }
            }
        }

        public double? Variable5
        {
            get { return _Variable5; }
            set
            {
                if (_Variable5 != value)
                {
                    _Variable5 = value;
                    OnPropertyChanged(() => Variable5);
                }
            }
        }


        //public string CompanyCode
        //{
        //    get { return _CompanyCode; }
        //    set
        //    {
        //        if (_CompanyCode != value)
        //        {
        //            _CompanyCode = value;
        //            OnPropertyChanged(() => CompanyCode);
        //        }
        //    }
        //}


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


        class MacroEconomicValidator : AbstractValidator<MacroEconomic>
        {
            public MacroEconomicValidator()
            {
                RuleFor(obj => obj.Sector_Code).NotEmpty().WithMessage("Sector Code is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicValidator();
        }
    }
}
