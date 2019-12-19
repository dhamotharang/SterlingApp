using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SectorVariableMapping : ObjectBase
    {
        int _SectorVariableMappingId;
        string _Sector;
        int _Year;
        int _Type;
        string _Variable;
        double? _Value;
        bool _Active;

        public int SectorVariableMappingId
        {
            get { return _SectorVariableMappingId; }
            set
            {
                if (_SectorVariableMappingId != value)
                {
                    _SectorVariableMappingId = value;
                    OnPropertyChanged(() => SectorVariableMappingId);
                }
            }
        }

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
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


        class SectorVariableMappingValidator : AbstractValidator<SectorVariableMapping>
        {
            public SectorVariableMappingValidator()
            {
                RuleFor(obj => obj.Sector).NotEmpty().WithMessage("Sector is required.");
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SectorVariableMappingValidator();
        }
    }
}
