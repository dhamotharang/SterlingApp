using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OverdraftLGDComputation : ObjectBase
    {

        int _Id;
        string _AccountNo;
        string _Refno;
        string _Producttype;
        string _SubType;
        string _CustomerName;
        string _Currency;
        double _ExchangeRate;
        DateTime _date_pmt;
        double _AmortizedCost_FCY;
        double _AmortizedCost_LCY;
        double _EIR;
        int _Stage;
        DateTime _MaturityDate;
        double _TotalColRecAmt;
        double _RecoveryRate;
        double _UNSecuredRecovery;
        double _TotalRecoverableAmt;
        double _LGD;
        bool _Active;

        public int Id {
            get { return _Id; }
            set {
                if (_Id != value) {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        public string AccountNo {
            get { return _AccountNo; }
            set {
                if (_AccountNo != value) {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
                }
            }
        }

        public string Producttype {
            get { return _Producttype; }
            set {
                if (_Producttype != value) {
                    _Producttype = value;
                    OnPropertyChanged(() => Producttype);
                }
            }
        }


        public string SubType {
            get { return _SubType; }
            set {
                if (_SubType != value) {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
                }
            }
        }


        public string CustomerName {
            get { return _CustomerName; }
            set {
                if (_CustomerName != value) {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }


        public string Currency {
            get { return _Currency; }
            set {
                if (_Currency != value) {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public double ExchangeRate {
            get { return _ExchangeRate; }
            set {
                if (_ExchangeRate != value) {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
                }
            }
        }


        public DateTime date_pmt {
            get { return _date_pmt; }
            set {
                if (_date_pmt != value) {
                    _date_pmt = value;
                    OnPropertyChanged(() => date_pmt);
                }
            }
        }

        public double AmortizedCost_FCY {
            get { return _AmortizedCost_FCY; }
            set {
                if (_AmortizedCost_FCY != value) {
                    _AmortizedCost_FCY = value;
                    OnPropertyChanged(() => AmortizedCost_FCY);
                }
            }
        }

        public double AmortizedCost_LCY {
            get { return _AmortizedCost_LCY; }
            set {
                if (_AmortizedCost_LCY != value) {
                    _AmortizedCost_LCY = value;
                    OnPropertyChanged(() => AmortizedCost_LCY);
                }
            }
        }


        public double EIR {
            get { return _EIR; }
            set {
                if (_EIR != value) {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }

        public int Stage {
            get { return _Stage; }
            set {
                if (_Stage != value) {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
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


        public double TotalColRecAmt {
            get { return _TotalColRecAmt; }
            set {
                if (_TotalColRecAmt != value) {
                    _TotalColRecAmt = value;
                    OnPropertyChanged(() => TotalColRecAmt);
                }
            }
        }

        public double RecoveryRate {
            get { return _RecoveryRate; }
            set {
                if (_RecoveryRate != value) {
                    _RecoveryRate = value;
                    OnPropertyChanged(() => RecoveryRate);
                }
            }
        }

        public double UNSecuredRecovery {
            get { return _UNSecuredRecovery; }
            set {
                if (_UNSecuredRecovery != value) {
                    _UNSecuredRecovery = value;
                    OnPropertyChanged(() => UNSecuredRecovery);
                }
            }
        }

        public double TotalRecoverableAmt {
            get { return _TotalRecoverableAmt; }
            set {
                if (_TotalRecoverableAmt != value) {
                    _TotalRecoverableAmt = value;
                    OnPropertyChanged(() => TotalRecoverableAmt);
                }
            }
        }


        public double LGD {
            get { return _LGD; }
            set {
                if (_LGD != value) {
                    _LGD = value;
                    OnPropertyChanged(() => LGD);
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
        class OverdraftLGDComputationValidator : AbstractValidator<OverdraftLGDComputation>
        {
            public OverdraftLGDComputationValidator(){               
            }
        }

        protected override IValidator GetValidator()
        {
            return new OverdraftLGDComputationValidator();
        }

    }
}
