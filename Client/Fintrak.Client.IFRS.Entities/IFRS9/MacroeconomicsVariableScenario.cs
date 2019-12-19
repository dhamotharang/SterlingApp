using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroeconomicsVariableScenario : ObjectBase
    {
        int _MacroeconomicsId;
        string _Variable;
        string _Frequency;
        DateTime _StartDate;
        double _Optimistic;
        double _Best;
        double _Downturn;
        DateTime _Rundate;
        int _Flag;
        bool _Active;

        public int MacroeconomicsId
        {
            get { return _MacroeconomicsId; }
            set
            {
                if (_MacroeconomicsId != value)
                {
                    _MacroeconomicsId = value;
                    OnPropertyChanged(() => MacroeconomicsId);
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

        public string Frequency
        {
            get { return _Frequency; }
            set
            {
                if (_Frequency != value)
                {
                    _Frequency = value;
                    OnPropertyChanged(() => Frequency);
                }
            }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged(() => StartDate);
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

        public double Best
        {
            get { return _Best; }
            set
            {
                if (_Best != value)
                {
                    _Best = value;
                    OnPropertyChanged(() => Best);
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

        public int Flag
        {
            get { return _Flag; }
            set
            {
                if (_Flag != value)
                {
                    _Flag = value;
                    OnPropertyChanged(() => Flag);
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


        class MacroeconomicsVariableScenarioValidator : AbstractValidator<MacroeconomicsVariableScenario>
        {
            public MacroeconomicsVariableScenarioValidator()
            {
                //RuleFor(obj => obj.type).NotEmpty().WithMessage("Type is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroeconomicsVariableScenarioValidator();
        }
    }
}
