using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsFinalRetailOutput : ObjectBase
    {
        int _FinalRetailId;
        string _Account_No;
        string _CustomerName;
        string _InternalRating;
        int _STAGE;
        int _YTM;
        string _FacilityType;
        double _OutstandingBalance;
        int _Seq;
        double? _EAD;
        double _LifetimePDBest;
        double _LifetimePDOptimistic;
        double _LifetimePDDownturn;
        double _DiscountFactor;
        double _LGD_Best;
        double _LGD_Optimistic;
        double _LGD_Downturn;
        double _ECL_MonthlyBest;
        double _ECL_MonthlyOptimistic;
        double _ECL_MonthlyDownturn;
        double _ECL_Best;
        double _ECL_Optimistic;
        double _ECL_Downturn;
        double _Macroeconomic_Best;
        double _Macroeconomic_Optimistic;
        double _Macroeconomic_Downturn;
        double _Impairment;
        DateTime _RunDate;
        bool _Active;

        public int FinalRetailId
        {
            get { return _FinalRetailId; }
            set
            {
                if (_FinalRetailId != value)
                {
                    _FinalRetailId = value;
                    OnPropertyChanged(() => FinalRetailId);
                }
            }
        }

        public string Account_No
        {
            get { return _Account_No; }
            set
            {
                if (_Account_No != value)
                {
                    _Account_No = value;
                    OnPropertyChanged(() => Account_No);
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

        public string InternalRating
        {
            get { return _InternalRating; }
            set
            {
                if (_InternalRating != value)
                {
                    _InternalRating = value;
                    OnPropertyChanged(() => InternalRating);
                }
            }
        }

        public int STAGE
        {
            get { return _STAGE; }
            set
            {
                if (_STAGE != value)
                {
                    _STAGE = value;
                    OnPropertyChanged(() => STAGE);
                }
            }
        }

        public int YTM
        {
            get { return _YTM; }
            set
            {
                if (_YTM != value)
                {
                    _YTM = value;
                    OnPropertyChanged(() => YTM);
                }
            }
        }

        public string FacilityType
        {
            get { return _FacilityType; }
            set
            {
                if (_FacilityType != value)
                {
                    _FacilityType = value;
                    OnPropertyChanged(() => FacilityType);
                }
            }
        }

        public double OutstandingBalance
        {
            get { return _OutstandingBalance; }
            set
            {
                if (_OutstandingBalance != value)
                {
                    _OutstandingBalance = value;
                    OnPropertyChanged(() => OutstandingBalance);
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

        public double? EAD
        {
            get { return _EAD; }
            set
            {
                if (_EAD != value)
                {
                    _EAD = value;
                    OnPropertyChanged(() => EAD);
                }
            }
        }

        public double LifetimePDBest
        {
            get { return _LifetimePDBest; }
            set
            {
                if (_LifetimePDBest != value)
                {
                    _LifetimePDBest = value;
                    OnPropertyChanged(() => LifetimePDBest);
                }
            }
        }

        public double LifetimePDOptimistic
        {
            get { return _LifetimePDOptimistic; }
            set
            {
                if (_LifetimePDOptimistic != value)
                {
                    _LifetimePDOptimistic = value;
                    OnPropertyChanged(() => LifetimePDOptimistic);
                }
            }
        }

        public double LifetimePDDownturn
        {
            get { return _LifetimePDDownturn; }
            set
            {
                if (_LifetimePDDownturn != value)
                {
                    _LifetimePDDownturn = value;
                    OnPropertyChanged(() => LifetimePDDownturn);
                }
            }
        }

        public double DiscountFactor
        {
            get { return _DiscountFactor; }
            set
            {
                if (_DiscountFactor != value)
                {
                    _DiscountFactor = value;
                    OnPropertyChanged(() => DiscountFactor);
                }
            }
        }

        public double LGD_Best
        {
            get { return _LGD_Best; }
            set
            {
                if (_LGD_Best != value)
                {
                    _LGD_Best = value;
                    OnPropertyChanged(() => LGD_Best);
                }
            }
        }

        public double LGD_Optimistic
        {
            get { return _LGD_Optimistic; }
            set
            {
                if (_LGD_Optimistic != value)
                {
                    _LGD_Optimistic = value;
                    OnPropertyChanged(() => LGD_Optimistic);
                }
            }
        }

        public double LGD_Downturn
        {
            get { return _LGD_Downturn; }
            set
            {
                if (_LGD_Downturn != value)
                {
                    _LGD_Downturn = value;
                    OnPropertyChanged(() => LGD_Downturn);
                }
            }
        }

        public double ECL_MonthlyBest
        {
            get { return _ECL_MonthlyBest; }
            set
            {
                if (_ECL_MonthlyBest != value)
                {
                    _ECL_MonthlyBest = value;
                    OnPropertyChanged(() => ECL_MonthlyBest);
                }
            }
        }

        public double ECL_MonthlyOptimistic
        {
            get { return _ECL_MonthlyOptimistic; }
            set
            {
                if (_ECL_MonthlyOptimistic != value)
                {
                    _ECL_MonthlyOptimistic = value;
                    OnPropertyChanged(() => ECL_MonthlyOptimistic);
                }
            }
        }

        public double ECL_MonthlyDownturn
        {
            get { return _ECL_MonthlyDownturn; }
            set
            {
                if (_ECL_MonthlyDownturn != value)
                {
                    _ECL_MonthlyDownturn = value;
                    OnPropertyChanged(() => ECL_MonthlyDownturn);
                }
            }
        }

        public double ECL_Best
        {
            get { return _ECL_Best; }
            set
            {
                if (_ECL_Best != value)
                {
                    _ECL_Best = value;
                    OnPropertyChanged(() => ECL_Best);
                }
            }
        }

        public double ECL_Optimistic
        {
            get { return _ECL_Optimistic; }
            set
            {
                if (_ECL_Optimistic != value)
                {
                    _ECL_Optimistic = value;
                    OnPropertyChanged(() => ECL_Optimistic);
                }
            }
        }

        public double ECL_Downturn
        {
            get { return _ECL_Downturn; }
            set
            {
                if (_ECL_Downturn != value)
                {
                    _ECL_Downturn = value;
                    OnPropertyChanged(() => ECL_Downturn);
                }
            }
        }

        public double Macroeconomic_Best
        {
            get { return _Macroeconomic_Best; }
            set
            {
                if (_Macroeconomic_Best != value)
                {
                    _Macroeconomic_Best = value;
                    OnPropertyChanged(() => Macroeconomic_Best);
                }
            }
        }

        public double Macroeconomic_Optimistic
        {
            get { return _Macroeconomic_Optimistic; }
            set
            {
                if (_Macroeconomic_Optimistic != value)
                {
                    _Macroeconomic_Optimistic = value;
                    OnPropertyChanged(() => Macroeconomic_Optimistic);
                }
            }
        }

        public double Macroeconomic_Downturn
        {
            get { return _Macroeconomic_Downturn; }
            set
            {
                if (_Macroeconomic_Downturn != value)
                {
                    _Macroeconomic_Downturn = value;
                    OnPropertyChanged(() => Macroeconomic_Downturn);
                }
            }
        }

        double Impairment
        {
            get { return _Impairment; }
            set
            {
                if (_Impairment != value)
                {
                    _Impairment = value;
                    OnPropertyChanged(() => Impairment);
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

        class IfrsFinalRetailOutputValidator : AbstractValidator<IfrsFinalRetailOutput>
        {
            public IfrsFinalRetailOutputValidator()
            {
                RuleFor(obj => obj._Account_No).NotEmpty().WithMessage("Account No. is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsFinalRetailOutputValidator();
        }
    }
}
