using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Assumption : ObjectBase
    {
        int _InstrumentID;
        string _Instrument;
        int _Highest_Level_of_Speculative;
        int _Notches_for_sicr;
        string _Default_Rating;
        string _Assumed_DefaultRating;
        double _Assumed_EIR;
        int _Assumed_Tenor;
        DateTime _Assumed_MaturityDate;
        DateTime _Assumed_StartDate;
        double _Assumed_CCF_Guarantee;
        double _Assumed_CCF_LCs;
        bool _Active;

        public int InstrumentID
        {
            get { return _InstrumentID; }
            set
            {
                if (_InstrumentID != value)
                {
                    _InstrumentID = value;
                    OnPropertyChanged(() => InstrumentID);
                }
            }
        }

        public string Instrument
        {
            get { return _Instrument; }
            set
            {
                if (_Instrument != value)
                {
                    _Instrument = value;
                    OnPropertyChanged(() => Instrument);
                }
            }
        }

        public int Highest_Level_of_Speculative
        {
            get { return _Highest_Level_of_Speculative; }
            set
            {
                if (_Highest_Level_of_Speculative != value)
                {
                    _Highest_Level_of_Speculative = value;
                    OnPropertyChanged(() => Highest_Level_of_Speculative);
                }
            }
        }

        public int Notches_for_sicr
        {
            get { return _Notches_for_sicr; }
            set
            {
                if (_Notches_for_sicr != value)
                {
                    _Notches_for_sicr = value;
                    OnPropertyChanged(() => Notches_for_sicr);
                }
            }
        }

        public string Default_Rating
        {
            get { return _Default_Rating; }
            set
            {
                if (_Default_Rating != value)
                {
                    _Default_Rating = value;
                    OnPropertyChanged(() => Default_Rating);
                }
            }
        }

        public string Assumed_DefaultRating
        {
            get { return _Assumed_DefaultRating; }
            set
            {
                if (_Assumed_DefaultRating != value)
                {
                    _Assumed_DefaultRating = value;
                    OnPropertyChanged(() => Assumed_DefaultRating);
                }
            }
        }

        public double Assumed_EIR
        {
            get { return _Assumed_EIR; }
            set
            {
                if (_Assumed_EIR != value)
                {
                    _Assumed_EIR = value;
                    OnPropertyChanged(() => Assumed_EIR);
                }
            }
        }

        public int Assumed_Tenor
        {
            get { return _Assumed_Tenor; }
            set
            {
                if (_Assumed_Tenor != value)
                {
                    _Assumed_Tenor = value;
                    OnPropertyChanged(() => Assumed_Tenor);
                }
            }
        }

        public DateTime Assumed_MaturityDate
        {
            get { return _Assumed_MaturityDate; }
            set
            {
                if (_Assumed_MaturityDate != value)
                {
                    _Assumed_MaturityDate = value;
                    OnPropertyChanged(() => Assumed_MaturityDate);
                }
            }
        }

        public DateTime Assumed_StartDate
        {
            get { return _Assumed_StartDate; }
            set
            {
                if (_Assumed_StartDate != value)
                {
                    _Assumed_StartDate = value;
                    OnPropertyChanged(() => Assumed_StartDate);
                }
            }
        }

        public double Assumed_CCF_Guarantee
        {
            get { return _Assumed_CCF_Guarantee; }
            set
            {
                if (_Assumed_CCF_Guarantee != value)
                {
                    _Assumed_CCF_Guarantee = value;
                    OnPropertyChanged(() => Assumed_CCF_Guarantee);
                }
            }
        }

        public double Assumed_CCF_LCs
        {
            get { return _Assumed_CCF_LCs; }
            set
            {
                if (_Assumed_CCF_LCs != value)
                {
                    _Assumed_CCF_LCs = value;
                    OnPropertyChanged(() => Assumed_CCF_LCs);
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


        class AssumptionValidator : AbstractValidator<Assumption>
        {
            public AssumptionValidator()
            {
                RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new AssumptionValidator();
        }
    }
}
