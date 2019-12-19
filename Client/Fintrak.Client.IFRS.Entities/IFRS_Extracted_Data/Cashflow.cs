using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Cashflow : ObjectBase
    {
        int _CashflowId;
        string _RefNo;
        DateTime _datepmt;
        double _amt_prin_pay;
        double _amt_int_pay;
        double _amt_fee_pay;
        bool _Active;

        public int CashflowId {
            get { return _CashflowId; }
            set {
                if (_CashflowId != value) {
                    _CashflowId = value;
                    OnPropertyChanged(() => CashflowId);
                }
            }
        }


        public string RefNo {
            get { return _RefNo; }
            set {
                if (_RefNo != value) {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public DateTime datepmt {
            get { return _datepmt; }
            set {
                if (_datepmt != value) {
                    _datepmt = value;
                    OnPropertyChanged(() => datepmt);
                }
            }
        }


        public double amt_prin_pay {
            get { return _amt_prin_pay; }
            set {
                if (_amt_prin_pay != value) {
                    _amt_prin_pay = value;
                    OnPropertyChanged(() => amt_prin_pay);
                }
            }
        }

        public double amt_int_pay {
            get { return _amt_int_pay; }
            set {
                if (_amt_int_pay != value) {
                    _amt_int_pay = value;
                    OnPropertyChanged(() => amt_int_pay);
                }
            }
        }


        public double amt_fee_pay {
            get { return _amt_fee_pay; }
            set {
                if (_amt_fee_pay != value) {
                    _amt_fee_pay = value;
                    OnPropertyChanged(() => amt_fee_pay);
                }
            }
        }



        public bool Active {
            get { return _Active; }
            set {
                if (_Active != value) {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }


        class CashflowValidator : AbstractValidator<Cashflow>
        {
            public CashflowValidator(){
                //RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new CashflowValidator();
        }
    }
}
