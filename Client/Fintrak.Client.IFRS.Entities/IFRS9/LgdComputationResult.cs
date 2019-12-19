using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LgdComputationResult : ObjectBase
    {
        int _Id;
        string _AccountNo;
        string _Currency;
        double _ExchangeRate;
        DateTime _date_pmt;
        double _Amt_LCY;
        double _AMT_FCY;
        double _eir;
        double _CollateralValue;
        double _CollateralgrowthRate;
        double _CollateralgrowthValue;
        DateTime _Rundate;
        DateTime _Collateral_Realiazation_Date;
        double _CollateralHaircut;
        double _Realization_period;
        double _Cost_of_Recovery_Rate;
        double _Hair_cut_Cost;
        double _Cost_of_recovery;
        double _Values_at_rundate;
        double _Net_Cash_Flow;
        double _DiscountedValue;
        double _LGD;
        string _CollateralType;
        bool _Active;



        public string CollateralType
        {
            get { return _CollateralType; }
            set
            {
                if (_CollateralType != value)
                {
                    _CollateralType = value;
                    OnPropertyChanged(() => CollateralType);
                }
            }
        }

        public double LGD
        {
            get { return _LGD; }
            set
            {
                if (_LGD != value)
                {
                    _LGD = value;
                    OnPropertyChanged(() => LGD);
                }
            }
        }

        public double DiscountedValue
        {
            get { return _DiscountedValue; }
            set
            {
                if (_DiscountedValue != value)
                {
                    _DiscountedValue = value;
                    OnPropertyChanged(() => DiscountedValue);
                }
            }
        }

        public double Net_Cash_Flow
        {
            get { return _Net_Cash_Flow; }
            set
            {
                if (_Net_Cash_Flow != value)
                {
                    _Net_Cash_Flow = value;
                    OnPropertyChanged(() => Net_Cash_Flow);
                }
            }
        }

        public double Values_at_rundate
        {
            get { return _Values_at_rundate; }
            set
            {
                if (_Values_at_rundate != value)
                {
                    _Values_at_rundate = value;
                    OnPropertyChanged(() => Values_at_rundate);
                }
            }
        }

        public double Cost_of_recovery
        {
            get { return _Cost_of_recovery; }
            set
            {
                if (_Cost_of_recovery != value)
                {
                    _Cost_of_recovery = value;
                    OnPropertyChanged(() => Cost_of_recovery);
                }
            }
        }

        public double Hair_cut_Cost
        {
            get { return _Hair_cut_Cost; }
            set
            {
                if (_Hair_cut_Cost != value)
                {
                    _Hair_cut_Cost = value;
                    OnPropertyChanged(() => Hair_cut_Cost);
                }
            }
        }

        public double Cost_of_Recovery_Rate
        {
            get { return _Cost_of_Recovery_Rate; }
            set
            {
                if (_Cost_of_Recovery_Rate != value)
                {
                    _Cost_of_Recovery_Rate = value;
                    OnPropertyChanged(() => Cost_of_Recovery_Rate);
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

        public double Realization_period
        {
            get { return _Realization_period; }
            set
            {
                if (_Realization_period != value)
                {
                    _Realization_period = value;
                    OnPropertyChanged(() => Realization_period);
                }
            }
        }

        public double CollateralHaircut
        {
            get { return _CollateralHaircut; }
            set
            {
                if (_CollateralHaircut != value)
                {
                    _CollateralHaircut = value;
                    OnPropertyChanged(() => CollateralHaircut);
                }
            }
        }

        public double ExchangeRate
        {
            get { return _ExchangeRate; }
            set
            {
                if (_ExchangeRate != value)
                {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
                }
            }
        }

        public DateTime Collateral_Realiazation_Date
        {
            get { return _Collateral_Realiazation_Date; }
            set
            {
                if (_Collateral_Realiazation_Date != value)
                {
                    _Collateral_Realiazation_Date = value;
                    OnPropertyChanged(() => Collateral_Realiazation_Date);
                }
            }
        }

        public double CollateralgrowthValue
        {
            get { return _CollateralgrowthValue; }
            set
            {
                if (_CollateralgrowthValue != value)
                {
                    _CollateralgrowthValue = value;
                    OnPropertyChanged(() => CollateralgrowthValue);
                }
            }
        }

        public double CollateralgrowthRate
        {
            get { return _CollateralgrowthRate; }
            set
            {
                if (_CollateralgrowthRate != value)
                {
                    _CollateralgrowthRate = value;
                    OnPropertyChanged(() => CollateralgrowthRate);
                }
            }
        }

        public double CollateralValue
        {
            get { return _CollateralValue; }
            set
            {
                if (_CollateralValue != value)
                {
                    _CollateralValue = value;
                    OnPropertyChanged(() => CollateralValue);
                }
            }
        }

        public DateTime date_pmt
        {
            get { return _date_pmt; }
            set
            {
                if (_date_pmt != value)
                {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }

        public double Amt_LCY
        {
            get { return _Amt_LCY; }
            set
            {
                if (_Amt_LCY != value)
                {
                    _Amt_LCY = value;
                    OnPropertyChanged(() => Amt_LCY);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public double eir
        {
            get { return _eir; }
            set
            {
                if (_eir != value)
                {
                    _eir = value;
                    OnPropertyChanged(() => eir);
                }
            }
        }

        public double AMT_FCY
        {
            get { return _AMT_FCY; }
            set
            {
                if (_AMT_FCY != value)
                {
                    _AMT_FCY = value;
                    OnPropertyChanged(() => AMT_FCY);
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


        class LgdComputationResultValidator : AbstractValidator<LgdComputationResult>
        {
            public LgdComputationResultValidator()
            {
                //RuleFor(obj => obj._AccountNo).NotEmpty().WithMessage("AccountNo is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new LgdComputationResultValidator();
        }
    }
}
