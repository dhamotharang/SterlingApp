using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class BondsECLComputationResult : ObjectBase
    {
        int _ID;
        string _AccountNo;
        string _Refno;
        string _CustomerName;
        DateTime _date_pmt;
        string _Producttype;
        string _SubType;
        int _Stage;
        string _Currency;
        double _ExchangeRate;
        double _AmortizedCost;
        double _AmortizedCost_Trans;
        double _PrincipalOutBal;
        double _EIR;
        double _TotalRecoverableAmt;
        double _LGD;
        double _DiscountFactor;
        double _PDBest;
        double _PDOptimistic;
        double _PDDownTurn;
        double _FinalECLBest;
        double _FinalECLOptimistic;
        double _FinalECLDownTurn;
        double _FinalECLWeightAvg;
        DateTime _Rundate;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
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

        public string Refno {
            get { return _Refno; }
            set {
                if (_Refno != value) {
                    _Refno = value;
                    OnPropertyChanged(() => Refno);
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

        public int Stage {
            get { return _Stage; }
            set {
                if (_Stage != value) {
                    _Stage = value;
                    OnPropertyChanged(() => Stage);
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

        public double AmortizedCost {
            get { return _AmortizedCost; }
            set {
                if (_AmortizedCost != value) {
                    _AmortizedCost = value;
                    OnPropertyChanged(() => AmortizedCost);
                }
            }
        }

        public double AmortizedCost_Trans {
            get { return _AmortizedCost_Trans; }
            set {
                if (_AmortizedCost_Trans != value) {
                    _AmortizedCost_Trans = value;
                    OnPropertyChanged(() => AmortizedCost_Trans);
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

        public double PrincipalOutBal
        {
            get { return _PrincipalOutBal; }
            set
            {
                if (_PrincipalOutBal != value)
                {
                    _PrincipalOutBal = value;
                    OnPropertyChanged(() => PrincipalOutBal);
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

        public double DiscountFactor {
            get { return _DiscountFactor; }
            set {
                if (_DiscountFactor != value) {
                    _DiscountFactor = value;
                    OnPropertyChanged(() => DiscountFactor);
                }
            }
        }

        public double PDBest {
            get { return _PDBest; }
            set {
                if (_PDBest != value) {
                    _PDBest = value;
                    OnPropertyChanged(() => PDBest);
                }
            }
        }

        public double PDOptimistic {
            get { return _PDOptimistic; }
            set {
                if (_PDOptimistic != value) {
                    _PDOptimistic = value;
                    OnPropertyChanged(() => PDOptimistic);
                }
            }
        }

        public double PDDownTurn {
            get { return _PDDownTurn; }
            set {
                if (_PDDownTurn != value) {
                    _PDDownTurn = value;
                    OnPropertyChanged(() => PDDownTurn);
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


        class BondsECLComputationResultValidator : AbstractValidator<BondsECLComputationResult>
        {
            public BondsECLComputationResultValidator()
            {
                //RuleFor(obj => obj.Instrument).NotEmpty().WithMessage("Instrument is required.");        
            }
        }

        protected override IValidator GetValidator()
        {
            return new BondsECLComputationResultValidator();
        }
    }
}
