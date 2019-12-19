using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class ConsolidatedLoans : ObjectBase
    {
        int _ID;
        string _AcctNo;
        string _Classification;
        string _ProductName;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        string _Sector;
        string _SubSector;
        string _ProductType;
        double _Amount;
        double _CurrentBalance;
        double _AdjustedBalances;
        string _LoanType;
        double _Rate;
        string _HC1;
        string _HC2;
        string _PAY1;
        double _OB1;
        string _RR1;
        string _PAY2;
        double _OB2;
        string _RR2;
        string _PAY3;
        double _OB3;
        string _RR3;
        string _PAY4;
        double _OB4;
        string _RR4;
        string _PAY5;
        double _OB5;
        string _RR5;
        string _PAY6;
        double _OB6;
        string _RR6;
        DateTime _RunDate;
        bool _Active;

        public int ID {
            get { return _ID; }
            set {
                if (_ID != value) {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }
        public string AcctNo {
            get { return _AcctNo; }
            set {
                if (_AcctNo != value) {
                    _AcctNo = value;
                    OnPropertyChanged(() => AcctNo);
                }
            }
        }
        public string Classification {
            get { return _Classification; }
            set {
                if (_Classification != value) {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }
        public string ProductName {
            get { return _ProductName; }
            set {
                if (_ProductName != value) {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }
        public DateTime ValueDate {
            get { return _ValueDate; }
            set {
                if (_ValueDate != value) {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }
        public DateTime MaturityDate {
            get { return _MaturityDate; }
            set {
                if (_MaturityDate != value) {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
                }
            }
        }
        public string Sector {
            get { return _Sector; }
            set {
                if (_Sector != value) {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }
        public string SubSector {
            get { return _SubSector; }
            set {
                if (_SubSector != value) {
                    _SubSector = value;
                    OnPropertyChanged(() => SubSector);
                }
            }
        }
        public string ProductType {
            get { return _ProductType; }
            set {
                if (_ProductType != value) {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }
        public double Amount {
            get { return _Amount; }
            set {
                if (_Amount != value) {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }
        public double CurrentBalance {
            get { return _CurrentBalance; }
            set {
                if (_CurrentBalance != value) {
                    _CurrentBalance = value;
                    OnPropertyChanged(() => CurrentBalance);
                }
            }
        }
        public double AdjustedBalances {
            get { return _AdjustedBalances; }
            set {
                if (_AdjustedBalances != value) {
                    _AdjustedBalances = value;
                    OnPropertyChanged(() => AdjustedBalances);
                }
            }
        }
        public string LoanType {
            get { return _LoanType; }
            set {
                if (_LoanType != value) {
                    _LoanType = value;
                    OnPropertyChanged(() => LoanType);
                }
            }
        }
        public double Rate {
            get { return _Rate; }
            set {
                if (_Rate != value) {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }
        public string HC1 {
            get { return _HC1; }
            set {
                if (_HC1 != value) {
                    _HC1 = value;
                    OnPropertyChanged(() => HC1);
                }
            }
        }
        public string HC2 {
            get { return _HC2; }
            set {
                if (_HC2 != value) {
                    _HC2 = value;
                    OnPropertyChanged(() => HC2);
                }
            }
        }
        public string PAY1 {
            get { return _PAY1; }
            set {
                if (_PAY1 != value) {
                    _PAY1 = value;
                    OnPropertyChanged(() => PAY1);
                }
            }
        }
        public double OB1 {
            get { return _OB1; }
            set {
                if (_OB1 != value) {
                    _OB1 = value;
                    OnPropertyChanged(() => OB1);
                }
            }
        }
        public string RR1 {
            get { return _RR1; }
            set {
                if (_RR1 != value) {
                    _RR1 = value;
                    OnPropertyChanged(() => RR1);
                }
            }
        }
        public string PAY2 {
            get { return _PAY2; }
            set {
                if (_PAY2 != value) {
                    _PAY2 = value;
                    OnPropertyChanged(() => PAY2);
                }
            }
        }
        public double OB2 {
            get { return _OB2; }
            set {
                if (_OB2 != value) {
                    _OB2 = value;
                    OnPropertyChanged(() => OB2);
                }
            }
        }
        public string RR2 {
            get { return _RR2; }
            set {
                if (_RR2 != value) {
                    _RR2 = value;
                    OnPropertyChanged(() => RR2);
                }
            }
        }
        public string PAY3 {
            get { return _PAY3; }
            set {
                if (_PAY3 != value) {
                    _PAY3 = value;
                    OnPropertyChanged(() => PAY3);
                }
            }
        }
        public double OB3 {
            get { return _OB3; }
            set {
                if (_OB3 != value) {
                    _OB3 = value;
                    OnPropertyChanged(() => OB3);
                }
            }
        }
        public string RR3 {
            get { return _RR3; }
            set {
                if (_RR3 != value) {
                    _RR3 = value;
                    OnPropertyChanged(() => RR3);
                }
            }
        }
        public string PAY4 {
            get { return _PAY4; }
            set {
                if (_PAY4 != value) {
                    _PAY4 = value;
                    OnPropertyChanged(() => PAY4);
                }
            }
        }
        public double OB4 {
            get { return _OB4; }
            set {
                if (_OB4 != value) {
                    _OB4 = value;
                    OnPropertyChanged(() => OB4);
                }
            }
        }
        public string RR4 {
            get { return _RR4; }
            set {
                if (_RR4 != value) {
                    _RR4 = value;
                    OnPropertyChanged(() => RR4);
                }
            }
        }
        public string PAY5 {
            get { return _PAY5; }
            set {
                if (_PAY5 != value) {
                    _PAY5 = value;
                    OnPropertyChanged(() => PAY5);
                }
            }
        }
        public double OB5 {
            get { return _OB5; }
            set {
                if (_OB5 != value) {
                    _OB5 = value;
                    OnPropertyChanged(() => OB5);
                }
            }
        }
        public string RR5 {
            get { return _RR5; }
            set {
                if (_RR5 != value) {
                    _RR5 = value;
                    OnPropertyChanged(() => RR5);
                }
            }
        }
        public string PAY6 {
            get { return _PAY6; }
            set {
                if (_PAY6 != value) {
                    _PAY6 = value;
                    OnPropertyChanged(() => PAY6);
                }
            }
        }
        public double OB6 {
            get { return _OB6; }
            set {
                if (_OB6 != value) {
                    _OB6 = value;
                    OnPropertyChanged(() => OB6);
                }
            }
        }
        public string RR6 {
            get { return _RR6; }
            set {
                if (_RR6 != value) {
                    _RR6 = value;
                    OnPropertyChanged(() => RR6);
                }
            }
        }
        public DateTime RunDate {
            get { return _RunDate; }
            set {
                if (_RunDate != value) {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
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


        class ConsolidatedLoansValidator : AbstractValidator<ConsolidatedLoans>
        {
            public ConsolidatedLoansValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new ConsolidatedLoansValidator();
        }
    }
}
