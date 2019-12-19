using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class BondPeriodicSchedule : ObjectBase
    {
        int _Id;
        string _RefNo;
        int _Num_Pmt;
        DateTime _Date_Pmt;
        double _Amt_Prin_Init;
        double _Amt_Int_Pay;
        double _Amt_Prin_Pay;
        double _Amt_Prin_PayLessFV;
        double _DailyInt;
        double _DailyPrinc;
        double _DailyPrincLessFV;
      //  double _ClosingBalance;
        double _Amt_Cashflow;
        double _Coupon_Cashflow;
        double _Amt_Prin_End;
        decimal _Coupon_Rate;
        decimal _IRR;     
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

        public int Num_Pmt
        {
            get { return _Num_Pmt; }
            set
            {
                if (_Num_Pmt != value)
                {
                    _Num_Pmt = value;
                    OnPropertyChanged(() => Num_Pmt);
                }
            }
        }

        public DateTime Date_Pmt
        {
            get { return _Date_Pmt; }
            set
            {
                if (_Date_Pmt != value)
                {
                    _Date_Pmt = value;
                    OnPropertyChanged(() => Date_Pmt);
                }
            }
        }

        public double Amt_Prin_Init
        {
            get { return _Amt_Prin_Init; }
            set
            {
                if (_Amt_Prin_Init != value)
                {
                    _Amt_Prin_Init = value;
                    OnPropertyChanged(() => Amt_Prin_Init);
                }
            }
        }


        public double Amt_Int_Pay
        {
            get { return _Amt_Int_Pay; }
            set
            {
                if (_Amt_Int_Pay != value)
                {
                    _Amt_Int_Pay = value;
                    OnPropertyChanged(() => Amt_Int_Pay);
                }
            }
        }

        public double Amt_Prin_Pay
        {
            get { return _Amt_Prin_Pay; }
            set
            {
                if (_Amt_Prin_Pay != value)
                {
                    _Amt_Prin_Pay = value;
                    OnPropertyChanged(() => Amt_Prin_Pay);
                }
            }
        }

        public double Amt_Prin_PayLessFV
        {
            get { return _Amt_Prin_PayLessFV; }
            set
            {
                if (_Amt_Prin_PayLessFV != value)
                {
                    _Amt_Prin_PayLessFV = value;
                    OnPropertyChanged(() => Amt_Prin_PayLessFV);
                }
            }
        }

        public double DailyInt
        {
            get { return _DailyInt; }
            set
            {
                if (_DailyInt != value)
                {
                    _DailyInt = value;
                    OnPropertyChanged(() => DailyInt);
                }
            }
        }

        public double DailyPrinc
        {
            get { return _DailyPrinc; }
            set
            {
                if (_DailyPrinc != value)
                {
                    _DailyPrinc = value;
                    OnPropertyChanged(() => DailyPrinc);
                }
            }
        }

        public double DailyPrincLessFV
        {
            get { return _DailyPrincLessFV; }
            set
            {
                if (_DailyPrincLessFV != value)
                {
                    _DailyPrincLessFV = value;
                    OnPropertyChanged(() => DailyPrincLessFV);
                }
            }
        }


        //public double ClosingBalance
        //{
        //    get { return _ClosingBalance; }
        //    set
        //    {
        //        if (_ClosingBalance != value)
        //        {
        //            _ClosingBalance = value;
        //            OnPropertyChanged(() => ClosingBalance);
        //        }
        //    }
        //}

        public double Amt_Cashflow
        {
            get { return _Amt_Cashflow; }
            set
            {
                if (_Amt_Cashflow != value)
                {
                    _Amt_Cashflow = value;
                    OnPropertyChanged(() => Amt_Cashflow);
                }
            }
        }
        public double Coupon_Cashflow
        {
            get { return _Coupon_Cashflow; }
            set
            {
                if (_Coupon_Cashflow != value)
                {
                    _Coupon_Cashflow = value;
                    OnPropertyChanged(() => Coupon_Cashflow);
                }
            }
        }


        public double Amt_Prin_End
        {
            get { return _Amt_Prin_End; }
            set
            {
                if (_Amt_Prin_End != value)
                {
                    _Amt_Prin_End = value;
                    OnPropertyChanged(() => Amt_Prin_End);
                }
            }
        }



        public decimal Coupon_Rate
        {
            get { return _Coupon_Rate; }
            set
            {
                if (_Coupon_Rate != value)
                {
                    _Coupon_Rate = value;
                    OnPropertyChanged(() => Coupon_Rate);
                }
            }
        }


        public decimal IRR
        {
            get { return _IRR; }
            set
            {
                if (_IRR != value)
                {
                    _IRR = value;
                    OnPropertyChanged(() => IRR);
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



        class BondPeriodicScheduleValidator : AbstractValidator<BondPeriodicSchedule>
        {
            public BondPeriodicScheduleValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new BondPeriodicScheduleValidator();
        }
    }
}
