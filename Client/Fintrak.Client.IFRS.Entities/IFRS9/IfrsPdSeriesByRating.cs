using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsPdSeriesByRating : ObjectBase
    {
        int _Sno;
        string _Rating;
        int _seq;
        int _Year;
        string _Period;
        double _PDYear;
        double _MarginalDefaultPD;
        double _MarginalPD_BEST;
        double _MarginalPD_Downturn;
        double _MarginalPD_Optimistic;
        double _SurvivalPD_Downturn;
        double _SurvivalPD_Optimistic;
        double _LifeTimePD_BEST;
        double _LifeTimePD_Downturn;
        double _LifeTimePD_Optimistic;
        DateTime _RunDate;
        DateTime _EndDate;
        bool _Active;

        public int Sno
        {
            get { return _Sno; }
            set
            {
                if (_Sno != value)
                {
                    _Sno = value;
                    OnPropertyChanged(() => Sno);
                }
            }
        }

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        public int seq
        {
            get { return _seq;}
            set
            {
                if (_seq != value)
                {
                    _seq = value;
                    OnPropertyChanged(() => seq);
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

        public string Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public double PDYear
        {
            get { return _PDYear; }
            set
            {
                if (_PDYear != value)
                {
                    _PDYear = value;
                    OnPropertyChanged(() => PDYear);
                }
            }
        }

        public double MarginalDefaultPD
        {
            get { return _MarginalDefaultPD; }
            set
            {
                if (_MarginalDefaultPD != value)
                {
                    _MarginalDefaultPD = value;
                    OnPropertyChanged(() => MarginalDefaultPD);
                }
            }
        }

        public double MarginalPD_BEST
        {
            get { return _MarginalPD_BEST; }
            set
            {
                if (_MarginalPD_BEST != value)
                {
                    _MarginalPD_BEST = value;
                    OnPropertyChanged(() => MarginalPD_BEST);
                }
            }
        }

        public double MarginalPD_Downturn
        {
            get { return _MarginalPD_Downturn; }
            set
            {
                if (_MarginalPD_Downturn != value)
                {
                    _MarginalPD_Downturn = value;
                    OnPropertyChanged(() => MarginalPD_Downturn);
                }
            }
        }

        public double MarginalPD_Optimistic
        {
            get { return _MarginalPD_Optimistic; }
            set
            {
                if (_MarginalPD_Optimistic != value)
                {
                    _MarginalPD_Optimistic = value;
                    OnPropertyChanged(() => MarginalPD_Optimistic);
                }
            }
        }

        public double SurvivalPD_Downturn
        {
            get { return _SurvivalPD_Downturn; }
            set
            {
                if (_SurvivalPD_Downturn != value)
                {
                    _SurvivalPD_Downturn = value;
                    OnPropertyChanged(() => SurvivalPD_Downturn);
                }
            }
        }

        public double SurvivalPD_Optimistic
        {
            get { return _SurvivalPD_Optimistic; }
            set
            {
                if (_SurvivalPD_Optimistic != value)
                {
                    _SurvivalPD_Optimistic = value;
                    OnPropertyChanged(() => SurvivalPD_Optimistic);
                }
            }
        }

        public double LifeTimePD_BEST
        {
            get { return _LifeTimePD_BEST; }
            set
            {
                if (_LifeTimePD_BEST != value)
                {
                    _LifeTimePD_BEST = value;
                    OnPropertyChanged(() => LifeTimePD_BEST);
                }
            }
        }

        public double LifeTimePD_Downturn
        {
            get { return _LifeTimePD_Downturn; }
            set
            {
                if (_LifeTimePD_Downturn != value)
                {
                    _LifeTimePD_Downturn = value;
                    OnPropertyChanged(() => LifeTimePD_Downturn);
                }
            }
        }

        public double LifeTimePD_Optimistic
        {
            get { return _LifeTimePD_Optimistic; }
            set
            {
                if (_LifeTimePD_Optimistic != value)
                {
                    _LifeTimePD_Optimistic = value;
                    OnPropertyChanged(() => LifeTimePD_Optimistic);
                }
            }
        }

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged(() => EndDate);
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

        class IfrsPdSeriesByRatingValidator : AbstractValidator<IfrsPdSeriesByRating>
        {
            public IfrsPdSeriesByRatingValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsPdSeriesByRatingValidator();
        }
    }
}
