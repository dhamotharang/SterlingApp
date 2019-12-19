using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroVarRecoveryRates : ObjectBase
    {
        int _RecoveryRatesId;
        int _Seq;
        double? _BestEstimate;
        double? _Optimistic;
        double? _Downturn;
        DateTime _Date;
        bool _Active;

        public int RecoveryRatesId
        {
            get { return _RecoveryRatesId; }
            set
            {
                if (_RecoveryRatesId != value)
                {
                    _RecoveryRatesId = value;
                    OnPropertyChanged(() => RecoveryRatesId);
                }
            }
        }

        public int Seq
        {
            get { return _Seq; }
            set
            {
                if (_Seq != value)
                {
                    _Seq = value;
                    OnPropertyChanged(() => Seq);
                }
            }
        }

        public double? BestEstimate
        {
            get { return _BestEstimate; }
            set
            {
                if (_BestEstimate != value)
                {
                    _BestEstimate = value;
                    OnPropertyChanged(() => BestEstimate);
                }
            }
        }

        public double? Optimistic
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

        public double? Downturn
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

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
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

        class MacroVarRecoveryRatesValidator : AbstractValidator<MacroVarRecoveryRates>
        {
            public MacroVarRecoveryRatesValidator()
            {
                RuleFor(obj => obj.BestEstimate).NotEmpty().WithMessage("BestEstimate is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroVarRecoveryRatesValidator();
        }
    }
}
