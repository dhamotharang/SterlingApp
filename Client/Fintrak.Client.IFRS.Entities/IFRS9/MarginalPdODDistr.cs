using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class MarginalPdODDistr : ObjectBase
    {


        int _ID;
        string _AccountNo;
        string _Refno;
        string _Producttype;
        string _subType;
        int _OriginYear;
        string _Currency;
        double _ExchangeRate;
        DateTime _date_pmt;
        int _TenorInDays;
        double _AmortizedCost;
        int _Stage;
        double _EIR;
        double _DiscountFactor;
        string _Scenerio;
        double _MarginalPD;
        DateTime _Rundate;
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


        public string subType {
            get { return _subType; }
            set {
                if (_subType != value) {
                    _subType = value;
                    OnPropertyChanged(() => subType);
                }
            }
        }


        public int OriginYear {
            get { return _OriginYear; }
            set {
                if (_OriginYear != value) {
                    _OriginYear = value;
                    OnPropertyChanged(() => OriginYear);
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



        public int TenorInDays {
            get { return _TenorInDays; }
            set {
                if (_TenorInDays != value) {
                    _TenorInDays = value;
                    OnPropertyChanged(() => TenorInDays);
                }
            }
        }



        public double AmortizedCost {
            get { return _AmortizedCost; }
            set {
                if (_AmortizedCost != value) {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
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


        public double EIR {
            get { return _EIR; }
            set {
                if (_EIR != value) {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }


        public double DiscountFactor {
            get { return _DiscountFactor; }
            set {
                if (_DiscountFactor != value) {
                    _DiscountFactor = value;
                    OnPropertyChanged(() => DiscountFactor);
                }
            }
        }


        public string Scenerio {
            get { return _Scenerio; }
            set {
                if (_Scenerio != value) {
                    _Scenerio = value;
                    OnPropertyChanged(() => Scenerio);
                }
            }
        }


        public double MarginalPD {
            get { return _MarginalPD; }
            set {
                if (_MarginalPD != value) {
                    _MarginalPD = value;
                    OnPropertyChanged(() => MarginalPD);
                }
            }
        }

        public DateTime Rundate {
            get { return _Rundate; }
            set {
                if (_Rundate != value) {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
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


        class MarginalPdODDistrValidator : AbstractValidator<MarginalPdODDistr>
        {
            public MarginalPdODDistrValidator(){
                //RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MarginalPdODDistrValidator();
        }

    }
}
