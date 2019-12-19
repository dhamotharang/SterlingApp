using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MonthlyObeEadSTRLB : ObjectBase
    {
        int _ID;
        string _Refno;
        string _ProductType;
        string _SubType;
        double _Amount;
        string _Currency;
        double _ExchangeRate;
        string _OBEType;
        int _OriginYr;
        DateTime _date_pmt;
        double _MarginalCCF;
        int _Nodays;
        int _NoOfDayMonth;
        int _TTInDays;
        double _EAD;
        int _stage;
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

        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
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

        public string SubType {
            get { return _SubType; }
            set {
                if (_SubType != value) {
                    _SubType = value;
                    OnPropertyChanged(() => SubType);
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

        public string OBEType {
            get { return _OBEType; }
            set {
                if (_OBEType != value) {
                    _OBEType = value;
                    OnPropertyChanged(() => OBEType);
                }
            }
        }


        public int OriginYr {
            get { return _OriginYr; }
            set {
                if (_OriginYr != value) {
                    _OriginYr = value;
                    OnPropertyChanged(() => OriginYr);
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


        public double MarginalCCF {
            get { return _MarginalCCF; }
            set {
                if (_MarginalCCF != value) {
                    _MarginalCCF = value;
                    OnPropertyChanged(() => MarginalCCF);
                }
            }
        }


        public int Nodays {
            get { return _Nodays; }
            set {
                if (_Nodays != value) {
                    _Nodays = value;
                    OnPropertyChanged(() => Nodays);
                }
            }
        }


        public int NoOfDayMonth {
            get { return _NoOfDayMonth; }
            set {
                if (_NoOfDayMonth != value) {
                    _NoOfDayMonth = value;
                    OnPropertyChanged(() => NoOfDayMonth);
                }
            }
        }

        public int TTInDays {
            get { return _TTInDays; }
            set {
                if (_TTInDays != value) {
                    _TTInDays = value;
                    OnPropertyChanged(() => TTInDays);
                }
            }
        }

        public double EAD {
            get { return _EAD; }
            set {
                if (_EAD != value) {
                    _EAD = value;
                    OnPropertyChanged(() => EAD);
                }
            }
        }

        public int stage {
            get { return _stage; }
            set {
                if (_stage != value) {
                    _stage = value;
                    OnPropertyChanged(() => stage);
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


        class MonthlyObeEadSTRLBValidator : AbstractValidator<MonthlyObeEadSTRLB>
        {
            public MonthlyObeEadSTRLBValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new MonthlyObeEadSTRLBValidator();
        }
    }
}
