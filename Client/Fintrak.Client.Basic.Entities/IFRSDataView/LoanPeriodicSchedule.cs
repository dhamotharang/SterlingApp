using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class LoanPeriodicSchedule : ObjectBase
    {
        int _Id;
        string _RefNo;
        int _Num_Pmt;
        DateTime _Date_Pmt;
        double _Amt_Prin_Init;
        double _Amt_Pmt;
        double _Amt_Int_Pay;
        double _Amt_Prin_Pay;
        double _Amt_Prin_End;
        decimal _Rate;
        string _AMRefNo;
        int _AMnum_Pmt;
        DateTime _AMDate_Pmt;
        double _AMamt_Prin_Init;
        double _AMAmt_Pmt;
        double _AMAmt_Int_Pay;
        double _AMAmt_Prin_Pay;
        double _AMAmt_Prin_End;
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

        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (_Rate != value)
                {
                    _Rate = value;
                    OnPropertyChanged(() => Rate);
                }
            }
        }

        public string AMRefNo
        {
            get { return _AMRefNo; }
            set
            {
                if (_AMRefNo != value)
                {
                    _AMRefNo = value;
                    OnPropertyChanged(() => AMRefNo);
                }
            }
        }

        public int AMnum_Pmt
        {
            get { return _AMnum_Pmt; }
            set
            {
                if (_AMnum_Pmt != value)
                {
                    _AMnum_Pmt = value;
                    OnPropertyChanged(() => AMnum_Pmt);
                }
            }
        }


        public DateTime AMDate_Pmt
        {
            get { return _AMDate_Pmt; }
            set
            {
                if (_AMDate_Pmt != value)
                {
                    _AMDate_Pmt = value;
                    OnPropertyChanged(() => AMDate_Pmt);
                }
            }
        }

        public double AMamt_Prin_Init
        {
            get { return _AMamt_Prin_Init; }
            set
            {
                if (_AMamt_Prin_Init != value)
                {
                    _AMamt_Prin_Init = value;
                    OnPropertyChanged(() => AMamt_Prin_Init);
                }
            }
        }
        public double AMAmt_Pmt
        {
            get { return _AMAmt_Pmt; }
            set
            {
                if (_AMAmt_Pmt != value)
                {
                    _AMAmt_Pmt = value;
                    OnPropertyChanged(() => AMAmt_Pmt);
                }
            }
        }


        public double AMAmt_Int_Pay
        {
            get { return _AMAmt_Int_Pay; }
            set
            {
                if (_AMAmt_Int_Pay != value)
                {
                    _AMAmt_Int_Pay = value;
                    OnPropertyChanged(() => AMAmt_Int_Pay);
                }
            }
        }



        public double AMAmt_Prin_Pay
        {
            get { return _AMAmt_Prin_Pay; }
            set
            {
                if (_AMAmt_Prin_Pay != value)
                {
                    _AMAmt_Prin_Pay = value;
                    OnPropertyChanged(() => AMAmt_Prin_Pay);
                }
            }
        }


        public double AMAmt_Prin_End
        {
            get { return _AMAmt_Prin_End; }
            set
            {
                if (_AMAmt_Prin_End != value)
                {
                    _AMAmt_Prin_End = value;
                    OnPropertyChanged(() => AMAmt_Prin_End);
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



        class LoanPeriodicScheduleValidator : AbstractValidator<LoanPeriodicSchedule>
        {
            public LoanPeriodicScheduleValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanPeriodicScheduleValidator();
        }
    }
}
