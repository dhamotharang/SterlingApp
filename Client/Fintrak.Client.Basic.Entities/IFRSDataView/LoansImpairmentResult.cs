using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class LoansImpairmentResult : ObjectBase
    {
        int _Id;
        string _RefNo;
        string _AccountNo;
        string _ProductCategory;
        string _ProductCode;
        string _ProductName;
        string _ProductType;
        string _Classification;
        string _Performing;
        bool _WatchListed;
        bool _Significant;
        int _AgedBaseOnLastCr;
        double _Amount;
        double _PrincipalOutstandingBal;
        double _Interest_Receiv_Pay_UnEarn;
        double _InterestInSuspense;
        double _AmortizedBalance;
        double _TotalAmortizedCost;
        string _ImpairmentTrigger;
        string _InitialSelection;
        int _DaysToMaturity;
        int _ExpiredDays;
        double _PeriodicInterestRepayment;
        double _PeriodicCFPerPrincRepayment;
        decimal _RecoverableRate;
        double _PMT;
        double _RecoverableAmount;
        double _CollateralRecoverableAmt;
        double _TotalRecoverableAmount;
        double _ImpairmentSwitchTest;
        string _FinalSelection;
        double _SpecificImpairment;
        double _CollectiveImpairment;
        double _TotalImpairment;
        double _FairValueGain;


        double _CollateralValue;
        double _CollateralHairCut;
        string _CollateralCategory;
        int _NPER;
        int _Period;
        int _Year;
        DateTime _RunDate;
        double _StaffFairValueAmount;
        double _StaffFairValueGain;

        string _CompanyCode;


        public int Id

        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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

        public string ProductCategory
        {
            get { return _ProductCategory; }
            set
            {
                if (_ProductCategory != value)
                {
                    _ProductCategory = value;
                    OnPropertyChanged(() => ProductCategory);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }

        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }


        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType != value)
                {
                    _ProductType = value;
                    OnPropertyChanged(() => ProductType);
                }
            }
        }



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

        public string Performing
        {
            get { return _Performing; }
            set
            {
                if (_Performing != value)
                {
                    _Performing = value;
                    OnPropertyChanged(() => Performing);
                }
            }
        }



        public bool WatchListed
        {
            get { return _WatchListed; }
            set
            {
                if (_WatchListed != value)
                {
                    _WatchListed = value;
                    OnPropertyChanged(() => WatchListed);
                }
            }
        }

        public bool Significant
        {
            get { return _Significant; }
            set
            {
                if (_Significant != value)
                {
                    _Significant = value;
                    OnPropertyChanged(() => Significant);
                }
            }
        }
        public int AgedBaseOnLastCr
        {
            get { return _AgedBaseOnLastCr; }
            set
            {
                if (_AgedBaseOnLastCr != value)
                {
                    _AgedBaseOnLastCr = value;
                    OnPropertyChanged(() => AgedBaseOnLastCr);
                }
            }
        }


        public double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
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

        public double Interest_Receiv_Pay_UnEarn
        {
            get { return _Interest_Receiv_Pay_UnEarn; }
            set
            {
                if (_Interest_Receiv_Pay_UnEarn != value)
                {
                    _Interest_Receiv_Pay_UnEarn = value;
                    OnPropertyChanged(() => Interest_Receiv_Pay_UnEarn);
                }
            }
        }
        public double InterestInSuspense
        {
            get { return _InterestInSuspense; }
            set
            {
                if (_InterestInSuspense != value)
                {
                    _InterestInSuspense = value;
                    OnPropertyChanged(() => InterestInSuspense);
                }
            }
        }

        public double AmortizedBalance
        {
            get { return _AmortizedBalance; }
            set
            {
                if (_AmortizedBalance != value)
                {
                    _AmortizedBalance = value;
                    OnPropertyChanged(() => AmortizedBalance);
                }
            }
        }

        public double TotalAmortizedCost
        {
            get { return _TotalAmortizedCost; }
            set
            {
                if (_TotalAmortizedCost != value)
                {
                    _TotalAmortizedCost = value;
                    OnPropertyChanged(() => TotalAmortizedCost);
                }
            }
        }

        public string ImpairmentTrigger
        {
            get { return _ImpairmentTrigger; }
            set
            {
                if (_ImpairmentTrigger != value)
                {
                    _ImpairmentTrigger = value;
                    OnPropertyChanged(() => ImpairmentTrigger);
                }
            }
        }


        public string InitialSelection
        {
            get { return _InitialSelection; }
            set
            {
                if (_InitialSelection != value)
                {
                    _InitialSelection = value;
                    OnPropertyChanged(() => InitialSelection);
                }
            }
        }



        public int DaysToMaturity
        {
            get { return _DaysToMaturity; }
            set
            {
                if (_DaysToMaturity != value)
                {
                    _DaysToMaturity = value;
                    OnPropertyChanged(() => DaysToMaturity);
                }
            }
        }

        public int ExpiredDays
        {
            get { return _ExpiredDays; }
            set
            {
                if (_ExpiredDays != value)
                {
                    _ExpiredDays = value;
                    OnPropertyChanged(() => ExpiredDays);
                }
            }
        }



        public double PeriodicInterestRepayment
        {
            get { return _PeriodicInterestRepayment; }
            set
            {
                if (_PeriodicInterestRepayment != value)
                {
                    _PeriodicInterestRepayment = value;
                    OnPropertyChanged(() => PeriodicInterestRepayment);
                }
            }
        }

        public double PeriodicCFPerPrincRepayment
        {
            get { return _PeriodicCFPerPrincRepayment; }
            set
            {
                if (_PeriodicCFPerPrincRepayment != value)
                {
                    _PeriodicCFPerPrincRepayment = value;
                    OnPropertyChanged(() => PeriodicCFPerPrincRepayment);
                }
            }
        }
        public decimal RecoverableRate
        {
            get { return _RecoverableRate; }
            set
            {
                if (_RecoverableRate != value)
                {
                    _RecoverableRate = value;
                    OnPropertyChanged(() => RecoverableRate);
                }
            }
        }


        public double PMT
        {
            get { return _PMT; }
            set
            {
                if (_PMT != value)
                {
                    _PMT = value;
                    OnPropertyChanged(() => PMT);
                }
            }
        }


        public double RecoverableAmount
        {
            get { return _RecoverableAmount; }
            set
            {
                if (_RecoverableAmount != value)
                {
                    _RecoverableAmount = value;
                    OnPropertyChanged(() => RecoverableAmount);
                }
            }
        }

        public double TotalRecoverableAmount
        {
            get { return _TotalRecoverableAmount; }
            set
            {
                if (_TotalRecoverableAmount != value)
                {
                    _TotalRecoverableAmount = value;
                    OnPropertyChanged(() => TotalRecoverableAmount);
                }
            }
        }

        public double ImpairmentSwitchTest
        {
            get { return _ImpairmentSwitchTest; }
            set
            {
                if (_ImpairmentSwitchTest != value)
                {
                    _ImpairmentSwitchTest = value;
                    OnPropertyChanged(() => ImpairmentSwitchTest);
                }
            }
        }


        public string FinalSelection
        {
            get { return _FinalSelection; }
            set
            {
                if (_FinalSelection != value)
                {
                    _FinalSelection = value;
                    OnPropertyChanged(() => FinalSelection);
                }
            }
        }

        public double SpecificImpairment
        {
            get { return _SpecificImpairment; }
            set
            {
                if (_SpecificImpairment != value)
                {
                    _SpecificImpairment = value;
                    OnPropertyChanged(() => SpecificImpairment);
                }
            }
        }



        public double CollateralRecoverableAmt
        {
            get { return _CollateralRecoverableAmt; }
            set
            {
                if (_CollateralRecoverableAmt != value)
                {
                    _CollateralRecoverableAmt = value;
                    OnPropertyChanged(() => CollateralRecoverableAmt);
                }
            }
        }

        public double CollectiveImpairment
        {
            get { return _CollectiveImpairment; }
            set
            {
                if (_CollectiveImpairment != value)
                {
                    _CollectiveImpairment = value;
                    OnPropertyChanged(() => CollectiveImpairment);
                }
            }
        }

        public double TotalImpairment
        {
            get { return _TotalImpairment; }
            set
            {
                if (_TotalImpairment != value)
                {
                    _TotalImpairment = value;
                    OnPropertyChanged(() => TotalImpairment);
                }
            }
        }

        public double FairValueGain
        {
            get { return _FairValueGain; }
            set
            {
                if (_FairValueGain != value)
                {
                    _FairValueGain = value;
                    OnPropertyChanged(() => FairValueGain);
                }
            }
        }

        public double CollateralValue
        {
            get { return _CollateralValue; }
            set
            {
                if (_CollateralValue != value)
                {
                    _CollateralValue = value;
                    OnPropertyChanged(() => CollateralValue);
                }
            }
        }


        public double CollateralHairCut
        {
            get { return _CollateralHairCut; }
            set
            {
                if (_CollateralHairCut != value)
                {
                    _CollateralHairCut = value;
                    OnPropertyChanged(() => CollateralHairCut);
                }
            }
        }

        public string CollateralCategory
        {
            get { return _CollateralCategory; }
            set
            {
                if (_CollateralCategory != value)
                {
                    _CollateralCategory = value;
                    OnPropertyChanged(() => CollateralCategory);
                }
            }
        }

        public int NPER
        {
            get { return _NPER; }
            set
            {
                if (_NPER != value)
                {
                    _NPER = value;
                    OnPropertyChanged(() => NPER);
                }
            }
        }


        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }



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

        public double StaffFairValueAmount
        {
            get { return _StaffFairValueAmount; }
            set
            {
                if (_StaffFairValueAmount != value)
                {
                    _StaffFairValueAmount = value;
                    OnPropertyChanged(() => StaffFairValueAmount);
                }
            }
        }

        public double StaffFairValueGain
        {
            get { return _StaffFairValueGain; }
            set
            {
                if (_StaffFairValueGain != value)
                {
                    _StaffFairValueGain = value;
                    OnPropertyChanged(() => StaffFairValueGain);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }



       


        class LoansImpairmentResultValidator : AbstractValidator<LoansImpairmentResult>
        {
            public LoansImpairmentResultValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoansImpairmentResultValidator();
        }
    }
}
