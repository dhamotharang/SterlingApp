using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ImpairmentCorporate : ObjectBase
    {
        int _Corporate_Id;
        string _cust_id;
        string _refno;
        double _stage_allocation;
        double _best_ecl;
        double _Optimistic_ecl;
        double _downturn_ecl;
        double _impairment;
        string _credit_rating;
        bool _Active;

        public int Corporate_Id
        {
            get { return _Corporate_Id; }
            set
            {
                if (_Corporate_Id != value)
                {
                    _Corporate_Id = value;
                    OnPropertyChanged(() => Corporate_Id);
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


        public string credit_rating
        {
            get { return _credit_rating; }
            set
            {
                if (_credit_rating != value)
                {
                    _credit_rating = value;
                    OnPropertyChanged(() => credit_rating);
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


        class ImpairmentCorporateValidator : AbstractValidator<ImpairmentCorporate>
        {
            public ImpairmentCorporateValidator()
            {
                //RuleFor(obj => obj.Agency).NotEmpty().WithMessage("Agency is required.");
                //RuleFor(obj => obj.Rating).NotEmpty().WithMessage("Rating is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new ImpairmentCorporateValidator();
        }
    }
}
