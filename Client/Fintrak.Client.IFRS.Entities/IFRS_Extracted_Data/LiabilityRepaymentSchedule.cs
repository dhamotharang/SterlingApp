using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LiabilityRepaymentSchedule : ObjectBase
    {
        int _LiabilityRepayId;
        string _CONTRACT_REF_NO;
        string _CONTRACT_STATUS;
        string _COUNTERPARTY;
        double _PRINCIPAL_PAID;
        string _PRODUCT_DESCRIPTION;
        double _PRINCIPAL_AMOUNT_DUE;
        DateTime _VALUE_DATE;
        string _CURRENCY;
        decimal _EXCHANGE_RATE;
        decimal _MAIN_COMP_RATE;
        DateTime _LAST_INTEREST_PAYDATE;
        DateTime _NEXT_INTEREST_PAYDATE;
        DateTime _MATURITY_DATE;
        double _CF_AMT;
        string _CF_CONTRACT_STATUS;
        DateTime? _CONTRACT_END_DATE;
        string _GL_CODE;
        DateTime _BOOKING_DATE;
        DateTime _RUNDATE;
        double? _AMOUNT;
        bool _Active;


        public int LiabilityRepayId

        {
            get { return _LiabilityRepayId; }
            set
            {
                if (_LiabilityRepayId != value)
                {
                    _LiabilityRepayId = value;
                    OnPropertyChanged(() => LiabilityRepayId);
                }
            }
        }

        public string GL_CODE
        {
            get { return _GL_CODE; }
            set
            {
                if (_GL_CODE != value)
                {
                    _GL_CODE = value;
                    OnPropertyChanged(() => GL_CODE);
                }
            }
        }
        public string CF_CONTRACT_STATUS
        {
            get { return _CF_CONTRACT_STATUS; }
            set
            {
                if (_CF_CONTRACT_STATUS != value)
                {
                    _CF_CONTRACT_STATUS = value;
                    OnPropertyChanged(() => CF_CONTRACT_STATUS);
                }
            }
        }
        public decimal EXCHANGE_RATE
        {
            get { return _EXCHANGE_RATE; }
            set
            {
                if (_EXCHANGE_RATE != value)
                {
                    _EXCHANGE_RATE = value;
                    OnPropertyChanged(() => EXCHANGE_RATE);
                }
            }
        }
        public double CF_AMT
        {
            get { return _CF_AMT; }
            set
            {
                if (_CF_AMT != value)
                {
                    _CF_AMT = value;
                    OnPropertyChanged(() => CF_AMT);
                }
            }
        }
        public DateTime? CONTRACT_END_DATE
        {
            get { return _CONTRACT_END_DATE; }
            set
            {
                if (_CONTRACT_END_DATE != value)
                {
                    _CONTRACT_END_DATE = value;
                    OnPropertyChanged(() => CONTRACT_END_DATE);
                }
            }
        }
        public DateTime NEXT_INTEREST_PAYDATE
        {
            get { return _NEXT_INTEREST_PAYDATE; }
            set
            {
                if (_NEXT_INTEREST_PAYDATE != value)
                {
                    _NEXT_INTEREST_PAYDATE = value;
                    OnPropertyChanged(() => NEXT_INTEREST_PAYDATE);
                }
            }
        }
        public DateTime MATURITY_DATE
        {
            get { return _MATURITY_DATE; }
            set
            {
                if (_MATURITY_DATE != value)
                {
                    _MATURITY_DATE = value;
                    OnPropertyChanged(() => MATURITY_DATE);
                }
            }
        }
        public DateTime LAST_INTEREST_PAYDATE
        {
            get { return _LAST_INTEREST_PAYDATE; }
            set
            {
                if (_LAST_INTEREST_PAYDATE != value)
                {
                    _LAST_INTEREST_PAYDATE = value;
                    OnPropertyChanged(() => LAST_INTEREST_PAYDATE);
                }
            }
        }
        public DateTime VALUE_DATE
        {
            get { return _VALUE_DATE; }
            set
            {
                if (_VALUE_DATE != value)
                {
                    _VALUE_DATE = value;
                    OnPropertyChanged(() => VALUE_DATE);
                }
            }
        }
        public string CURRENCY
        {
            get { return _CURRENCY; }
            set
            {
                if (_CURRENCY != value)
                {
                    _CURRENCY = value;
                    OnPropertyChanged(() => CURRENCY);
                }
            }
        }
        public decimal MAIN_COMP_RATE
        {
            get { return _MAIN_COMP_RATE; }
            set
            {
                if (_MAIN_COMP_RATE != value)
                {
                    _MAIN_COMP_RATE = value;
                    OnPropertyChanged(() => MAIN_COMP_RATE);
                }
            }
        }
        public string CONTRACT_REF_NO
        {
            get { return _CONTRACT_REF_NO; }
            set
            {
                if (_CONTRACT_REF_NO != value)
                {
                    _CONTRACT_REF_NO = value;
                    OnPropertyChanged(() => CONTRACT_REF_NO);
                }
            }
        }
        public string PRODUCT_DESCRIPTION
        {
            get { return _PRODUCT_DESCRIPTION; }
            set
            {
                if (_PRODUCT_DESCRIPTION != value)
                {
                    _PRODUCT_DESCRIPTION = value;
                    OnPropertyChanged(() => PRODUCT_DESCRIPTION);
                }
            }
        }

        public string CONTRACT_STATUS
        {
            get { return _CONTRACT_STATUS; }
            set
            {
                if (_CONTRACT_STATUS != value)
                {
                    _CONTRACT_STATUS = value;
                    OnPropertyChanged(() => CONTRACT_STATUS);
                }
            }
        }
        public string COUNTERPARTY
        {
            get { return _COUNTERPARTY; }
            set
            {
                if (_COUNTERPARTY != value)
                {
                    _COUNTERPARTY = value;
                    OnPropertyChanged(() => COUNTERPARTY);
                }
            }
        }
        public double PRINCIPAL_PAID
        {
            get { return _PRINCIPAL_PAID; }
            set
            {
                if (_PRINCIPAL_PAID != value)
                {
                    _PRINCIPAL_PAID = value;
                    OnPropertyChanged(() => PRINCIPAL_PAID);
                }
            }
        }
        public double PRINCIPAL_AMOUNT_DUE
        {
            get { return _PRINCIPAL_AMOUNT_DUE; }
            set
            {
                if (_PRINCIPAL_AMOUNT_DUE != value)
                {
                    _PRINCIPAL_AMOUNT_DUE = value;
                    OnPropertyChanged(() => PRINCIPAL_AMOUNT_DUE);
                }
            }
        }
        public DateTime BOOKING_DATE
        {
            get { return _BOOKING_DATE; }
            set
            {
                if (_BOOKING_DATE != value)
                {
                    _BOOKING_DATE = value;
                    OnPropertyChanged(() => BOOKING_DATE);
                }
            }
        }
        public DateTime RUNDATE
        {
            get { return _RUNDATE; }
            set
            {
                if (_RUNDATE != value)
                {
                    _RUNDATE = value;
                    OnPropertyChanged(() => RUNDATE);
                }
            }
        }
       
        public double? AMOUNT
        {
            get { return _AMOUNT; }
            set
            {
                if (_AMOUNT != value)
                {
                    _AMOUNT = value;
                    OnPropertyChanged(() => AMOUNT);
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
        class LiabilityRepaymentScheduleValidator : AbstractValidator<LiabilityRepaymentSchedule>
        {
            public LiabilityRepaymentScheduleValidator()
            {
                RuleFor(obj => obj._PRINCIPAL_AMOUNT_DUE).NotEmpty().WithMessage("_PRINCIPAL_AMOUNT_DUE is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LiabilityRepaymentScheduleValidator();
        }
    }
}
