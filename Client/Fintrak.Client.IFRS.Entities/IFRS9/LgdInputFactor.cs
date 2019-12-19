using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LgdInputFactor : ObjectBase
    {
        int _LgdInputFactorId;
        string _Business;
        string _Value;
        double _Beststimate;
        double _Optimistic;
        double _Downturn;
        DateTime _Rundate;
        bool _Active;

        public int LgdInputFactorId
        {
            get { return _LgdInputFactorId; }
            set
            {
                if (_LgdInputFactorId != value)
                {
                    _LgdInputFactorId = value;
                    OnPropertyChanged(() => LgdInputFactorId);
                }
            }
        }

        public string Business
        {
            get { return _Business; }
            set
            {
                if (_Business != value)
                {
                    _Business = value;
                    OnPropertyChanged(() => Business);
                }
            }
        }

        public string Value
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

        public double Beststimate
        {
            get { return _Beststimate; }
            set
            {
                if (_Beststimate != value)
                {
                    _Beststimate = value;
                    OnPropertyChanged(() => Beststimate);
                }
            }
        }

        public double Optimistic
        {
            get { return _Optimistic; }
            set
            {
                if (_Optimistic != value)
                {
                    _Optimistic = value;
                    OnPropertyChanged(() => Optimistic);
                }
            }
        }

        public double Downturn
        {
            get { return _Downturn; }
            set
            {
                if (_Downturn != value)
                {
                    _Downturn = value;
                    OnPropertyChanged(() => Downturn);
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


        class LgdInputFactorValidator : AbstractValidator<LgdInputFactor>
        {
            public LgdInputFactorValidator()
            {
                //RuleFor(obj => obj.type).NotEmpty().WithMessage("Type is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new LgdInputFactorValidator();
        }
    }
}
