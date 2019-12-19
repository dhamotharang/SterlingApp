using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ProbabilityWeight : ObjectBase
    {
        int _ProbabilityWeighId;
        Nullable<double> _Mean;
        Nullable<double> _StandardDeviation;
        Nullable<double> _LowerLimit;
        Nullable<double> _UpperLimit;
        Nullable<double> _DownTurn;
        Nullable<double> _Upturn;
        Nullable<double> _Base;
        double _Loc;
        bool _Active;


        public int ProbabilityWeighId

        {
            get { return _ProbabilityWeighId; }
            set
            {
                if (_ProbabilityWeighId != value)
                {
                    _ProbabilityWeighId = value;
                    OnPropertyChanged(() => ProbabilityWeighId);
                }
            }
        }

        public Nullable<double> Mean
        {
            get { return _Mean; }
            set
            {
                if (_Mean != value)
                {
                    _Mean = value;
                    OnPropertyChanged(() => Mean);
                }
            }
        }

        public Nullable<double> StandardDeviation
        {
            get { return _StandardDeviation; }
            set
            {
                if (_StandardDeviation != value)
                {
                    _StandardDeviation = value;
                    OnPropertyChanged(() => StandardDeviation);
                }
            }
        }

        public Nullable<double> LowerLimit
        {
            get { return _LowerLimit; }
            set
            {
                if (_LowerLimit != value)
                {
                    _LowerLimit = value;
                    OnPropertyChanged(() => LowerLimit);
                }
            }
        }

        public Nullable<double> UpperLimit
        {
            get { return _UpperLimit; }
            set
            {
                if (_UpperLimit != value)
                {
                    _UpperLimit = value;
                    OnPropertyChanged(() => UpperLimit);
                }
            }
        }

        public Nullable<double> DownTurn
        {
            get { return _DownTurn; }
            set
            {
                if (_DownTurn != value)
                {
                    _DownTurn = value;
                    OnPropertyChanged(() => DownTurn);
                }
            }
        }

        public Nullable<double> Upturn
        {
            get { return _Upturn; }
            set
            {
                if (_Upturn != value)
                {
                    _Upturn = value;
                    OnPropertyChanged(() => Upturn);
                }
            }
        }

        public Nullable<double> Base
        {
            get { return _Base; }
            set
            {
                if (_Base != value)
                {
                    _Base = value;
                    OnPropertyChanged(() => Base);
                }
            }
        }

        public double Loc
        {
            get { return _Loc; }
            set
            {
                if (_Loc != value)
                {
                    _Loc = value;
                    OnPropertyChanged(() => Loc);
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
        class ProbabilityWeightValidator : AbstractValidator<ProbabilityWeight>
        {
            public ProbabilityWeightValidator()
            {
                //RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Refno is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProbabilityWeightValidator();
        }
    }
}
