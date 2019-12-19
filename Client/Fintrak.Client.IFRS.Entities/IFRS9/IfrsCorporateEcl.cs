using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsCorporateEcl : ObjectBase
    {
        int _ecl_id;
        string _refno;
        int _period;
        double _eclbest;
        double _ecloptimisitc;
        double _ecldownturn;
        double _unsecured_exposure;
        double _prob_wighted_opt;
        double _probwighted_best;
        double _probwighted_down;
        double _lgdbest;
        double _lgdoptimistic;
        double _lgdDown;
        double _lgd_macro_best;
        double _lgd_macro_optim;
        double _lgd_macro_down;
        double _pdbest;
        double _pdoptimistic;
        double _pd_down;
        double _interestfactor;
        double _interest_rate;
        double _eir;
        double _discount_factor;
        string _rating;
        string _staging;
        double _Exposure_net_impairment;
        bool _Active;

        public int ecl_id
        {
            get { return _ecl_id; }
            set
            {
                if (_ecl_id != value)
                {
                    _ecl_id = value;
                    OnPropertyChanged(() => ecl_id);
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

        public double prob_wighted_opt
        {
            get { return _prob_wighted_opt; }
            set
            {
                if (_prob_wighted_opt != value)
                {
                    _prob_wighted_opt = value;
                    OnPropertyChanged(() => prob_wighted_opt);
                }
            }
        }

        public double probwighted_best
        {
            get { return _probwighted_best; }
            set
            {
                if (_probwighted_best != value)
                {
                    _probwighted_best = value;
                    OnPropertyChanged(() => probwighted_best);
                }
            }
        }

        public double probwighted_down
        {
            get { return _probwighted_down; }
            set
            {
                if (_probwighted_down != value)
                {
                    _probwighted_down = value;
                    OnPropertyChanged(() => probwighted_down);
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

        public double interestfactor
        {
            get { return _interestfactor; }
            set
            {
                if (_interestfactor != value)
                {
                    _interestfactor = value;
                    OnPropertyChanged(() => interestfactor);
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

        public string staging
        {
            get { return _staging; }
            set
            {
                if (_staging != value)
                {
                    _staging = value;
                    OnPropertyChanged(() => staging);
                }
            }
        }

        public double Exposure_net_impairment
        {
            get { return _Exposure_net_impairment; }
            set
            {
                if (_Exposure_net_impairment != value)
                {
                    _Exposure_net_impairment = value;
                    OnPropertyChanged(() => Exposure_net_impairment);
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

        class IfrsCorporateEclValidator : AbstractValidator<IfrsCorporateEcl>
        {
            public IfrsCorporateEclValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsCorporateEclValidator();
        }
    }
}
