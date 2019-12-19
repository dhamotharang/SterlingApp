using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class DepositRepaymentSchedule : ObjectBase
    {
        int _DepositRepayId;
        double? _INT_DUE;
        double? _INT_PAID;
        double? _PRINCIPAL_PAID;
        double? _PRINCIPAL_AMOUNT_DUE;
        double? _AmountDiff;
        DateTime _DUEDT;
        DateTime _RUNDATE;
        string _REFNO;
        bool _Active;


        public int DepositRepayId

        {
            get { return _DepositRepayId; }
            set
            {
                if (_DepositRepayId != value)
                {
                    _DepositRepayId = value;
                    OnPropertyChanged(() => DepositRepayId);
                }
            }
        }
        public double? AmountDiff
        {
            get { return _AmountDiff; }
            set
            {
                if (_AmountDiff != value)
                {
                    _AmountDiff = value;
                    OnPropertyChanged(() => AmountDiff);
                }
            }
        }

        public double? INT_DUE
        {
            get { return _INT_DUE; }
            set
            {
                if (_INT_DUE != value)
                {
                    _INT_DUE = value;
                    OnPropertyChanged(() => INT_DUE);
                }
            }
        }
        public double? INT_PAID
        {
            get { return _INT_PAID; }
            set
            {
                if (_INT_PAID != value)
                {
                    _INT_PAID = value;
                    OnPropertyChanged(() => INT_PAID);
                }
            }
        }
        public double? PRINCIPAL_PAID
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
        public double? PRINCIPAL_AMOUNT_DUE
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
        public DateTime DUEDT
        {
            get { return _DUEDT; }
            set
            {
                if (_DUEDT != value)
                {
                    _DUEDT = value;
                    OnPropertyChanged(() => DUEDT);
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
       
        public string REFNO
        {
            get { return _REFNO; }
            set
            {
                if (_REFNO != value)
                {
                    _REFNO = value;
                    OnPropertyChanged(() => REFNO);
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
        class DepositRepaymentScheduleValidator : AbstractValidator<DepositRepaymentSchedule>
        {
            public DepositRepaymentScheduleValidator()
            {
                RuleFor(obj => obj.INT_DUE).NotEmpty().WithMessage("INT_DUE is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new DepositRepaymentScheduleValidator();
        }
    }
}
