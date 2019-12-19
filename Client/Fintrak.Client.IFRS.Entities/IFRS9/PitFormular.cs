using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PitFormular : ObjectBase
    {
        int _PitFormularId;
        string _Sector_code;
        string _Equation;
        double _ComputedPd;
        string _Type;
        DateTime _Rundate;
        bool _Active;

        public int PitFormularId
        {
            get { return _PitFormularId; }
            set
            {
                if (_PitFormularId != value)
                {
                    _PitFormularId = value;
                    OnPropertyChanged(() => PitFormularId);
                }
            }
        }

        public string Sector_code
        {
            get { return _Sector_code; }
            set
            {
                if (_Sector_code != value)
                {
                    _Sector_code = value;
                    OnPropertyChanged(() => Sector_code);
                }
            }
        }

        public string Equation
        {
            get { return _Equation; }
            set
            {
                if (_Equation != value)
                {
                    _Equation = value;
                    OnPropertyChanged(() => Equation);
                }
            }
        }


        public double ComputedPd
        {
            get { return _ComputedPd; }
            set
            {
                if (_ComputedPd != value)
                {
                    _ComputedPd = value;
                    OnPropertyChanged(() => ComputedPd);
                }
            }
        }

        public string Type
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


        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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


        class PitFormularValidator : AbstractValidator<PitFormular>
        {
            public PitFormularValidator()
            {
                RuleFor(obj => obj.Sector_code).NotEmpty().WithMessage("Sector_code is required.");
                RuleFor(obj => obj.Equation).NotEmpty().WithMessage("Equation is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new PitFormularValidator();
        }
    }
}
