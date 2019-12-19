using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ImpairmentResultOBE : ObjectBase
    {
        int _Impairment_Id;
        string _Account_No;
        string _CustomerName;
        string _InternalRating;
        int _stage;
        int _MonthToMaturity;
        string _FacilityType;
        double _OutstandingBalance;
        double _EAD;
        double _LGD_Best;
        double _LGD_Optimistic;
        double _LGD_Downturn;
        double _ECL_Best;
        double _ECL_Optimistic;
        double _ECL_Downturn;
        double _Macroeconomic_Best;
        double _Macroeconomic_Optimistic;
        double _Macroeconomic_Downturn;
        double _Impairment;
        bool _Active;

        public int Impairment_Id
        {
            get { return _Impairment_Id; }
            set
            {
                if (_Impairment_Id != value)
                {
                    _Impairment_Id = value;
                    OnPropertyChanged(() => Impairment_Id);
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

        public int stage
        {
            get { return _stage; }
            set
            {
                if (_stage != value)
                {
                    _stage = value;
                    OnPropertyChanged(() => stage);
                }
            }
        }

        public int MonthToMaturity
        {
            get { return _MonthToMaturity; }
            set
            {
                if (_MonthToMaturity != value)
                {
                    _MonthToMaturity = value;
                    OnPropertyChanged(() => MonthToMaturity);
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

        public double EAD
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

        public double Impairment
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

        class ImpairmentResultOBEValidator : AbstractValidator<ImpairmentResultOBE>
        {
            public ImpairmentResultOBEValidator()
            {
                //RuleFor(obj => obj.Agency).NotEmpty().WithMessage("Agency is required.");
                //RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ImpairmentResultOBEValidator();
        }
    }
}
