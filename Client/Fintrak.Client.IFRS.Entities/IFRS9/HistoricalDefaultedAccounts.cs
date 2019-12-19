using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class HistoricalDefaultedAccounts : ObjectBase
    {
        int _DefaultedAccountId;
        string _RefNo;
        string _Sector;
        decimal _ODLimit;
        double _PrincipalOutstandingBal;
        double _BalanceOnRefDate;
        double _BalanceOnDefaultDate;
        //double _PastDueAmount;
        //int _PastDueDays;
        string _Currency;
        decimal _ExchangeRate;
        //decimal _Rate;
        //DateTime _ValueDate;
        //DateTime _MaturityDate;
        //int _TenorDays;
        //int _TenorMonth;
        //int _Period;
        //int _Year;
        DateTime _RunDate;
        //string _CompanyCode;
        string _Classification;
        //string _Segment;
        //string _SubSegment;
        bool _Active;

        public int DefaultedAccountId
        {
            get { return _DefaultedAccountId; }
            set
            {
                if (_DefaultedAccountId != value)
                {
                    _DefaultedAccountId = value;
                    OnPropertyChanged(() => DefaultedAccountId);
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
        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }
        public decimal ODLimit
        {
            get { return _ODLimit; }
            set
            {
                if (_ODLimit != value)
                {
                    _ODLimit = value;
                    OnPropertyChanged(() => ODLimit);
                }
            }
        }

        public double PrincipalOutstandingBal
        {
            get { return _PrincipalOutstandingBal; }
            set
            {
                if (_PrincipalOutstandingBal != value)
                {
                    _PrincipalOutstandingBal = value;
                    OnPropertyChanged(() => PrincipalOutstandingBal);
                }
            }
        }
        public double BalanceOnRefDate
        {
            get { return _BalanceOnRefDate; }
            set
            {
                if (_BalanceOnRefDate != value)
                {
                    _BalanceOnRefDate = value;
                    OnPropertyChanged(() => BalanceOnRefDate);
                }
            }
        }
        public double BalanceOnDefaultDate
        {
            get { return _BalanceOnDefaultDate; }
            set
            {
                if (_BalanceOnDefaultDate != value)
                {
                    _BalanceOnDefaultDate = value;
                    OnPropertyChanged(() => BalanceOnDefaultDate);
                }
            }
        }
        //public double PastDueAmount
        //{
        //    get { return _PastDueAmount; }
        //    set
        //    {
        //        if (_PastDueAmount != value)
        //        {
        //            _PastDueAmount = value;
        //            OnPropertyChanged(() => PastDueAmount);
        //        }
        //    }
        //}

        // public int PastDueDays
        //{
        //    get { return _PastDueDays; }
        //    set
        //    {
        //        if (_PastDueDays != value)
        //        {
        //            _PastDueDays = value;
        //            OnPropertyChanged(() => PastDueDays);
        //        }
        //    }
        //}
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
        public decimal ExchangeRate
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

        //public decimal Rate
        //{
        //    get { return _Rate; }
        //    set
        //    {
        //        if (_Rate != value)
        //        {
        //            _Rate = value;
        //            OnPropertyChanged(() => Rate);
        //        }
        //    }
        //}

        //public DateTime ValueDate
        //{
        //    get { return _ValueDate; }
        //    set
        //    {
        //        if (_ValueDate != value)
        //        {
        //            _ValueDate = value;
        //            OnPropertyChanged(() => ValueDate);
        //        }
        //    }
        //}


        //public DateTime MaturityDate
        //{
        //    get { return _MaturityDate; }
        //    set
        //    {
        //        if (_MaturityDate != value)
        //        {
        //            _MaturityDate = value;
        //            OnPropertyChanged(() => MaturityDate);
        //        }
        //    }
        //}
        //public int TenorDays
        //{
        //    get { return _TenorDays; }
        //    set
        //    {
        //        if (_TenorDays != value)
        //        {
        //            _TenorDays = value;
        //            OnPropertyChanged(() => TenorDays);
        //        }
        //    }
        //}


        //public int TenorMonth
        //{
        //    get { return _TenorMonth; }
        //    set
        //    {
        //        if (_TenorMonth != value)
        //        {
        //            _TenorMonth = value;
        //            OnPropertyChanged(() => TenorMonth);
        //        }
        //    }
        //}


        //public int Period
        //{
        //    get { return _Period; }
        //    set
        //    {
        //        if (_Period != value)
        //        {
        //            _Period = value;
        //            OnPropertyChanged(() => Period);
        //        }
        //    }
        //}

        //public int Year
        //{
        //    get { return _Year; }
        //    set
        //    {
        //        if (_Year != value)
        //        {
        //            _Year = value;
        //            OnPropertyChanged(() => Year);
        //        }
        //    }
        //}

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

        //public string CompanyCode
        //{
        //    get { return _CompanyCode; }
        //    set
        //    {
        //        if (_CompanyCode != value)
        //        {
        //            _CompanyCode = value;
        //            OnPropertyChanged(() => CompanyCode);
        //        }
        //    }
        //}

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        //public string Segment
        //{
        //    get { return _Segment; }
        //    set
        //    {
        //        if (_Segment != value)
        //        {
        //            _Segment = value;
        //            OnPropertyChanged(() => Segment);
        //        }
        //    }
        //}

        //public string SubSegment
        //{
        //    get { return _SubSegment; }
        //    set
        //    {
        //        if (_SubSegment != value)
        //        {
        //            _SubSegment = value;
        //            OnPropertyChanged(() => SubSegment);
        //        }
        //    }
        //}

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


        class HistoricalDefaultedAccountsValidator : AbstractValidator<HistoricalDefaultedAccounts>
        {
            public HistoricalDefaultedAccountsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
        
            }
        }

        protected override IValidator GetValidator()
        {
            return new HistoricalDefaultedAccountsValidator();
        }
    }
}
