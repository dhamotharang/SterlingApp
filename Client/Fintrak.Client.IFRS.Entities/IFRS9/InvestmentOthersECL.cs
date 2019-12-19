using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class InvestmentOthersECL : ObjectBase
    {
        int _ecl_Id;
        string _RefNo;
        string _counterparty;
        string _asset_type;
        int _period;
        double _eclbest;
        double _ecloptimisitc;
        double _ecldownturn;
        double _unsecured_exposure;
        double _lgdbest;
        double _lgdoptimistic;
        double _lgdDown;
        double _lgd_macro_best;
        double _lgd_macro_optim;
        double _lgd_macro_down;
        double _pdbest;
        double _pdoptimistic;
        double _pd_down;
        double _monthly_int;
        double _interest_rate;
        double _eir;
        double _discount_factor;
        int _stage;
        string _rating;
        bool _Active;

        public int ecl_Id
        {
            get { return _ecl_Id; }
            set
            {
                if (_ecl_Id != value)
                {
                    _ecl_Id = value;
                    OnPropertyChanged(() => ecl_Id);
                }
            }
        }

        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public string counterparty
        {
            get { return _counterparty; }
            set
            {
                if (_counterparty != value)
                {
                    _counterparty = value;
                    OnPropertyChanged(() => counterparty);
                }
            }
        }

        public string asset_type
        {
            get { return _asset_type; }
            set
            {
                if (_asset_type != value)
                {
                    _asset_type = value;
                    OnPropertyChanged(() => asset_type);
                }
            }
        }

        public int period
        {
            get { return _period; }
            set
            {
                if (_period != value)
                {
                    _period = value;
                    OnPropertyChanged(() => period);
                }
            }
        }

        public double eclbest
        {
            get { return _eclbest; }
            set
            {
                if (_eclbest != value)
                {
                    _eclbest = value;
                    OnPropertyChanged(() => eclbest);
                }
            }
        }

        public double ecloptimisitc
        {
            get { return _ecloptimisitc; }
            set
            {
                if (_ecloptimisitc != value)
                {
                    _ecloptimisitc = value;
                    OnPropertyChanged(() => ecloptimisitc);
                }
            }
        }

        public double ecldownturn
        {
            get { return _ecldownturn; }
            set
            {
                if (_ecldownturn != value)
                {
                    _ecldownturn = value;
                    OnPropertyChanged(() => ecldownturn);
                }
            }
        }

        public double unsecured_exposure
        {
            get { return _unsecured_exposure; }
            set
            {
                if (_unsecured_exposure != value)
                {
                    _unsecured_exposure = value;
                    OnPropertyChanged(() => unsecured_exposure);
                }
            }
        }

        public double lgdbest
        {
            get { return _lgdbest; }
            set
            {
                if (_lgdbest != value)
                {
                    _lgdbest = value;
                    OnPropertyChanged(() => lgdbest);
                }
            }
        }

        public double lgdoptimistic
        {
            get { return _lgdoptimistic; }
            set
            {
                if (_lgdoptimistic != value)
                {
                    _lgdoptimistic = value;
                    OnPropertyChanged(() => lgdoptimistic);
                }
            }
        }

        public double lgdDown
        {
            get { return _lgdDown; }
            set
            {
                if (_lgdDown != value)
                {
                    _lgdDown = value;
                    OnPropertyChanged(() => lgdDown);
                }
            }
        }

        public double lgd_macro_best
        {
            get { return _lgd_macro_best; }
            set
            {
                if (_lgd_macro_best != value)
                {
                    _lgd_macro_best = value;
                    OnPropertyChanged(() => lgd_macro_best);
                }
            }
        }

        public double lgd_macro_optim
        {
            get { return _lgd_macro_optim; }
            set
            {
                if (_lgd_macro_optim != value)
                {
                    _lgd_macro_optim = value;
                    OnPropertyChanged(() => lgd_macro_optim);
                }
            }
        }

        public double lgd_macro_down
        {
            get { return _lgd_macro_down; }
            set
            {
                if (_lgd_macro_down != value)
                {
                    _lgd_macro_down = value;
                    OnPropertyChanged(() => lgd_macro_down);
                }
            }
        }

        public double pdbest
        {
            get { return _pdbest; }
            set
            {
                if (_pdbest != value)
                {
                    _pdbest = value;
                    OnPropertyChanged(() => pdbest);
                }
            }
        }

        public double pdoptimistic
        {
            get { return _pdoptimistic; }
            set
            {
                if (_pdoptimistic != value)
                {
                    _pdoptimistic = value;
                    OnPropertyChanged(() => pdoptimistic);
                }
            }
        }

        public double pd_down
        {
            get { return _pd_down; }
            set
            {
                if (_pd_down != value)
                {
                    _pd_down = value;
                    OnPropertyChanged(() => pd_down);
                }
            }
        }

        public double monthly_int
        {
            get { return _monthly_int; }
            set
            {
                if (_monthly_int != value)
                {
                    _monthly_int = value;
                    OnPropertyChanged(() => monthly_int);
                }
            }
        }

        public double interest_rate
        {
            get { return _interest_rate; }
            set
            {
                if (_interest_rate != value)
                {
                    _interest_rate = value;
                    OnPropertyChanged(() => interest_rate);
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

        public double discount_factor
        {
            get { return _discount_factor; }
            set
            {
                if (_discount_factor != value)
                {
                    _discount_factor = value;
                    OnPropertyChanged(() => discount_factor);
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

        public string rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(() => rating);
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


        class InvestmentOthersECLValidator : AbstractValidator<InvestmentOthersECL>
        {
            public InvestmentOthersECLValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new InvestmentOthersECLValidator();
        }
    }
}
