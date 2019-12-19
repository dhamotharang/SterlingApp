using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsLgdScenarioByInstrument : ObjectBase
    {
        int _InstrumentId;
        string _InstrumentType;
        double _LGD_BEST;
        double _LGD_DOWNTURN;
        double _LGD_OPTIMISTIC;
        bool _Active;

        public int InstrumentId
        {
            get { return _InstrumentId; }
            set
            {
                if (_InstrumentId != value)
                {
                    _InstrumentId = value;
                    OnPropertyChanged(() => InstrumentId);
                }
            }
        }

        public string InstrumentType
        {
            get { return _InstrumentType; }
            set
            {
                if (_InstrumentType != value)
                {
                    _InstrumentType = value;
                    OnPropertyChanged(() => InstrumentType);
                }
            }
        }

        public double LGD_BEST
        {
            get { return _LGD_BEST; }
            set
            {
                if (_LGD_BEST != value)
                {
                    _LGD_BEST = value;
                    OnPropertyChanged(() => LGD_BEST);
                }
            }
        }

        public double LGD_DOWNTURN
        {
            get { return _LGD_DOWNTURN; }
            set
            {
                if (_LGD_DOWNTURN != value)
                {
                    _LGD_DOWNTURN = value;
                    OnPropertyChanged(() => LGD_DOWNTURN);
                }
            }
        }

        public double LGD_OPTIMISTIC
        {
            get { return _LGD_OPTIMISTIC; }
            set
            {
                if (_LGD_OPTIMISTIC != value)
                {
                    _LGD_OPTIMISTIC = value;
                    OnPropertyChanged(() => LGD_OPTIMISTIC);
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

        class IfrsLgdScenarioByInstrumentValidator : AbstractValidator<IfrsLgdScenarioByInstrument>
        {
            public IfrsLgdScenarioByInstrumentValidator()
            {
                RuleFor(obj => obj.InstrumentType).NotEmpty().WithMessage("InstrumentType is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsLgdScenarioByInstrumentValidator();
        }
    }
}
