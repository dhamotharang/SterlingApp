using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IfrsMonthlyEAD : ObjectBase
    {
        int _Id;
        string _AccountNo;
        string _RefNo;
        string _Producttype;
        string _SubType;
        string _currency;
        int _OriginYear;
        int _seq;
        DateTime _date_pmt;
        double _AmortizedCost;
        double _EAD;
        double _InterestAccrued;
        double _EIR;
        int _TenorInDays;
        int _Stage;
        DateTime _RunDate;
        bool _Active;


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

        public string currency
        {
            get { return _currency; }
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    OnPropertyChanged(() => currency);
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
        

        public string Producttype
        {
            get { return _Producttype; }
            set
            {
                if (_Producttype != value)
                {
                    _Producttype = value;
                    OnPropertyChanged(() => Producttype);
                }
            }
        }

        public string SubType
        {
            get { return _SubType; }
            set
            {
                if (_SubType != value)
                {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }

        public double EIR
        {
            get { return _EIR; }
            set
            {
                if (_EIR != value)
                {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }


        public double InterestAccrued
        {
            get { return _InterestAccrued; }
            set
            {
                if (_InterestAccrued != value)
                {
                    _InterestAccrued = value;
                    OnPropertyChanged(() => InterestAccrued);
                }
            }
        }
        public double AmortizedCost
        {
            get { return _AmortizedCost; }
            set
            {
                if (_AmortizedCost != value)
                {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
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

        public int TenorInDays
        {
            get { return _TenorInDays; }
            set
            {
                if (_TenorInDays != value)
                {
                    _TenorInDays = value;
                    OnPropertyChanged(() => TenorInDays);
                }
            }
        }

        public int OriginYear
        {
            get { return _OriginYear; }
            set
            {
                if (_OriginYear != value)
                {
                    _OriginYear = value;
                    OnPropertyChanged(() => OriginYear);
                }
            }
        }

        public int Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage != value)
                {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
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


        class IfrsMonthlyEADValidator : AbstractValidator<IfrsMonthlyEAD>
        {
            public IfrsMonthlyEADValidator()
            {
                //RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("Account Code is required.");
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IfrsMonthlyEADValidator();
        }
    }
}
