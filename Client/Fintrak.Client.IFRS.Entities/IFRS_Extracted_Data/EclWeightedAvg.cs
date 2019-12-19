using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class EclWeightedAvg : ObjectBase
    {
        int _ECLWATempID;
        string _Refno;
        string _CustomerName;
        Nullable<double> _OldFinalECLWeightAvg;
        Nullable<double> _FinalECLWeightAvg;
        bool _Active;


        public int ECLWATempID

        {
            get { return _ECLWATempID; }
            set
            {
                if (_ECLWATempID != value)
                {
                    _ECLWATempID = value;
                    OnPropertyChanged(() => ECLWATempID);
                }
            }
        }
        public string Refno
        {
            get { return _Refno; }
            set
            {
                if (_Refno != value)
                {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        public Nullable<double> OldFinalECLWeightAvg
        {
            get { return _OldFinalECLWeightAvg; }
            set
            {
                if (_OldFinalECLWeightAvg != value)
                {
                    _OldFinalECLWeightAvg = value;
                    OnPropertyChanged(() => OldFinalECLWeightAvg);
                }
            }
        }

        public Nullable<double> FinalECLWeightAvg
        {
            get { return _FinalECLWeightAvg; }
            set
            {
                if (_FinalECLWeightAvg != value)
                {
                    _FinalECLWeightAvg = value;
                    OnPropertyChanged(() => FinalECLWeightAvg);
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
        class EclWeightedAvgValidator : AbstractValidator<EclWeightedAvg>
        {
            public EclWeightedAvgValidator()
            {
                //RuleFor(obj => obj.Refno).NotEmpty().WithMessage("Refno is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new EclWeightedAvgValidator();
        }
    }
}
