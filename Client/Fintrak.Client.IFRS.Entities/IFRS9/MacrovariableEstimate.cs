using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MacrovariableEstimate : ObjectBase
    {
        int _MacrovariableEstimate_Id;
        int _Seq;
        double _Optimistic;
        double _Best;
        double _Downturn;
        DateTime _Date;
        string _Category;
        bool _Active;

        public int MacrovariableEstimate_Id
        {
            get { return _MacrovariableEstimate_Id; }
            set
            {
                if (_MacrovariableEstimate_Id != value)
                {
                    _MacrovariableEstimate_Id = value;
                    OnPropertyChanged(() => MacrovariableEstimate_Id);
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


        class MacrovariableEstimateValidator : AbstractValidator<MacrovariableEstimate>
        {
            public MacrovariableEstimateValidator()
            {
                RuleFor(obj => obj.Seq).NotEmpty().WithMessage("Seq is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MacrovariableEstimateValidator();
        }
    }
}
