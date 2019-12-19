using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OdECLResult : ObjectBase
    {
        int _ID;
        string _AccountNo;
        string _RefNo;
        string _CustomerName;
        string _Producttype;
        string _SubType;
        string _Currency;
        int _Stage;
        double _AmortizedCost;

        double _EIR;
        double _PrincipalOutBal;

        double _FinalECLBest;
        double _FinalECLOptimistic;
        double _FinalECLDownTurn;
        double _FinalECLWeightAvg;
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

        public string AccountNo {
            get { return _AccountNo; }
            set {
                if (_AccountNo != value) {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
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

        public string CustomerName {
            get { return _CustomerName; }
            set {
                if (_CustomerName != value) {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
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

        public string Currency {
            get { return _Currency; }
            set {
                if (_Currency != value) {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
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





        public double EIR {
            get { return _EIR; }
            set {
                if (_EIR != value) {
                    _EIR = value;
                    OnPropertyChanged(() => EIR);
                }
            }
        }


        public double PrincipalOutBal {
            get { return _PrincipalOutBal; }
            set {
                if (_PrincipalOutBal != value) {
                    _PrincipalOutBal = value;
                    OnPropertyChanged(() => PrincipalOutBal);
                }
            }
        }




        public double FinalECLBest {
            get { return _FinalECLBest; }
            set {
                if (_FinalECLBest != value) {
                    _FinalECLBest = value;
                    OnPropertyChanged(() => FinalECLBest);
                }
            }
        }

        public double FinalECLOptimistic {
            get { return _FinalECLOptimistic; }
            set {
                if (_FinalECLOptimistic != value) {
                    _FinalECLOptimistic = value;
                    OnPropertyChanged(() => FinalECLOptimistic);
                }
            }
        }

        public double FinalECLDownTurn {
            get { return _FinalECLDownTurn; }
            set {
                if (_FinalECLDownTurn != value) {
                    _FinalECLDownTurn = value;
                    OnPropertyChanged(() => FinalECLDownTurn);
                }
            }
        }

        public double FinalECLWeightAvg {
            get { return _FinalECLWeightAvg; }
            set {
                if (_FinalECLWeightAvg != value) {
                    _FinalECLWeightAvg = value;
                    OnPropertyChanged(() => FinalECLWeightAvg);
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


        class OdECLResultValidator : AbstractValidator<OdECLResult>
        {
            public OdECLResultValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new OdECLResultValidator();
        }
    }
}
