using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsRetailPdSeries : ObjectBase
    {
        int _PdSeriesId;
        string _refno;
        int _ClassificationStage;
        int _Month;
        string _Category;
        double _LifeTimePD_BEST;
        double _LifeTimePD_Downturn;
        double _LifeTimePD_Optimistic;
        bool _Active;

        public int PdSeriesId
        {
            get { return _PdSeriesId; }
            set
            {
                if (_PdSeriesId != value)
                {
                    _PdSeriesId = value;
                    OnPropertyChanged(() => PdSeriesId);
                }
            }
        }

        public string refno
        {
            get { return _refno; }
            set
            {
                if (_refno != value)
                {
                    _refno = value;
                    OnPropertyChanged(() => refno);
                }
            }
        }

        public int ClassificationStage
        {
            get { return _ClassificationStage;}
            set
            {
                if (_ClassificationStage != value)
                {
                    _ClassificationStage = value;
                    OnPropertyChanged(() => ClassificationStage);
                }
            }
        }

        public int Month
        {
            get { return _Month; }
            set
            {
                if (_Month != value)
                {
                    _Month = value;
                    OnPropertyChanged(() => Month);
                }
            }
        }

        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
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

        class IfrsRetailPdSeriesValidator : AbstractValidator<IfrsRetailPdSeries>
        {
            public IfrsRetailPdSeriesValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsRetailPdSeriesValidator();
        }
    }
}
