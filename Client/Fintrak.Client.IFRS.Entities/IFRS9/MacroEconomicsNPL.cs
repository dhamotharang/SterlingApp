using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacroEconomicsNPL : ObjectBase
    {
        int _macroeconomicnplId;
        int _Seq;
        int _Year;
        double _NPL;
        string _Scenerio;
        double _HistoricalAvg;
        bool _Active;

        public int macroeconomicnplId
        {
            get { return _macroeconomicnplId; }
            set
            {
                if (_macroeconomicnplId != value)
                {
                    _macroeconomicnplId = value;
                    OnPropertyChanged(() => macroeconomicnplId);
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

        public double NPL
        {
            get { return _NPL; }
            set
            {
                if (_NPL != value)
                {
                    _NPL = value;
                    OnPropertyChanged(() => NPL);
                }
            }
        }

        public string Scenerio
        {
            get { return _Scenerio; }
            set
            {
                if (_Scenerio != value)
                {
                    _Scenerio = value;
                    OnPropertyChanged(() => Scenerio);
                }
            }
        }

        public double HistoricalAvg
        {
            get { return _HistoricalAvg; }
            set
            {
                if (_HistoricalAvg != value)
                {
                    _HistoricalAvg = value;
                    OnPropertyChanged(() => HistoricalAvg);
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


        class MacroEconomicsNPLValidator : AbstractValidator<MacroEconomicsNPL>
        {
            public MacroEconomicsNPLValidator()
            {
                RuleFor(obj => obj.Year).NotEmpty().WithMessage("Year is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacroEconomicsNPLValidator();
        }
    }
}
