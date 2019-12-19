using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ImpairmentInvestment : ObjectBase
    {
        int _Investment_Id;
        string _cust_id;
        string _counterparty;
        string _asset_name;
        string _rating;
        double _stage_allocation;
        double _outstanding_bal;
        double _best_ecl;
        double _Optimistic_ecl;
        double _downturn_ecl;
        double _impairment;
        bool _Active;

        public int Investment_Id
        {
            get { return _Investment_Id; }
            set
            {
                if (_Investment_Id != value)
                {
                    _Investment_Id = value;
                    OnPropertyChanged(() => Investment_Id);
                }
            }
        }

        public string cust_id
        {
            get { return _cust_id; }
            set
            {
                if (_cust_id != value)
                {
                    _cust_id = value;
                    OnPropertyChanged(() => cust_id);
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

        public string asset_name
        {
            get { return _asset_name; }
            set
            {
                if (_asset_name != value)
                {
                    _asset_name = value;
                    OnPropertyChanged(() => asset_name);
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

        public double stage_allocation
        {
            get { return _stage_allocation; }
            set
            {
                if (_stage_allocation != value)
                {
                    _stage_allocation = value;
                    OnPropertyChanged(() => stage_allocation);
                }
            }
        }

        public double outstanding_bal
        {
            get { return _outstanding_bal; }
            set
            {
                if (_outstanding_bal != value)
                {
                    _outstanding_bal = value;
                    OnPropertyChanged(() => outstanding_bal);
                }
            }
        }

        public double best_ecl
        {
            get { return _best_ecl; }
            set
            {
                if (_best_ecl != value)
                {
                    _best_ecl = value;
                    OnPropertyChanged(() => best_ecl);
                }
            }
        }


        public double Optimistic_ecl
        {
            get { return _Optimistic_ecl; }
            set
            {
                if (_Optimistic_ecl != value)
                {
                    _Optimistic_ecl = value;
                    OnPropertyChanged(() => Optimistic_ecl);
                }
            }
        }

        public double downturn_ecl
        {
            get { return _downturn_ecl; }
            set
            {
                if (_downturn_ecl != value)
                {
                    _downturn_ecl = value;
                    OnPropertyChanged(() => downturn_ecl);
                }
            }
        }

        public double impairment
        {
            get { return _impairment; }
            set
            {
                if (_impairment != value)
                {
                    _impairment = value;
                    OnPropertyChanged(() => impairment);
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


        class ImpairmentInvestmentValidator : AbstractValidator<ImpairmentInvestment>
        {
            public ImpairmentInvestmentValidator()
            {
                //RuleFor(obj => obj.Agency).NotEmpty().WithMessage("Agency is required.");
                //RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ImpairmentInvestmentValidator();
        }
    }
}
