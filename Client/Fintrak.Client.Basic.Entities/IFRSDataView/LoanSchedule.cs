using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class LoanSchedule : ObjectBase
    {
        int _Id;
        string _RefNo;
        int _Sequence;
        DateTime _PaymentDate;
        DateTime _Date;
        double _OpeningBalance;
        double _AmountPrincInit;
        double _DailyInt;
        double _DailyPrinc;
        double _ClosingBalance;
        double _AmountPrincEnd;
        double _AccruedInterest;
        double _AmortizedCost;
        double _Amortized;
        decimal _NorminalRate;
        int _AMSequence;
        DateTime _AMPaymentDate;
        DateTime _AMDate;
        double _AMOpeningBalance;
        double _AMAmountPrincInit;
        double _AMDailyInt;
        double _AMDailyPrinc;
        double _AMClosingBalance;
        double _AMAmountPrincEnd;
        double _AMAccruedInterest;
        double _AMAmortizedCost;
        double _AMAmortized;
        decimal _EffectiveRate;
        double _BalloonAmt;
        double _DiscountPremium;
        double _UnearnedFee;
        double _EarnedFee;
        int _NoOfPeriods;
        DateTime _PostedDate;
        DateTime _LastRunDate;
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

        public int Sequence
        {
            get { return _Sequence; }
            set
            {
                if (_Sequence != value)
                {
                    _Sequence = value;
                    OnPropertyChanged(() => Sequence);
                }
            }
        }

        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set
            {
                if (_PaymentDate != value)
                {
                    _PaymentDate = value;
                    OnPropertyChanged(() => PaymentDate);
                }
            }
        }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    OnPropertyChanged(() => Date);
                }
            }
        }

        public double OpeningBalance
        {
            get { return _OpeningBalance; }
            set
            {
                if (_OpeningBalance != value)
                {
                    _OpeningBalance = value;
                    OnPropertyChanged(() => OpeningBalance);
                }
            }
        }


        public double AmountPrincInit
        {
            get { return _AmountPrincInit; }
            set
            {
                if (_AmountPrincInit != value)
                {
                    _AmountPrincInit = value;
                    OnPropertyChanged(() => AmountPrincInit);
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



        public double ClosingBalance
        {
            get { return _ClosingBalance; }
            set
            {
                if (_ClosingBalance != value)
                {
                    _ClosingBalance = value;
                    OnPropertyChanged(() => ClosingBalance);
                }
            }
        }

        public double AmountPrincEnd
        {
            get { return _AmountPrincEnd; }
            set
            {
                if (_AmountPrincEnd != value)
                {
                    _AmountPrincEnd = value;
                    OnPropertyChanged(() => AmountPrincEnd);
                }
            }
        }
        public double AccruedInterest
        {
            get { return _AccruedInterest; }
            set
            {
                if (_AccruedInterest != value)
                {
                    _AccruedInterest = value;
                    OnPropertyChanged(() => AccruedInterest);
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


        public double Amortized
        {
            get { return _Amortized; }
            set
            {
                if (_Amortized != value)
                {
                    _Amortized = value;
                    OnPropertyChanged(() => Amortized);
                }
            }
        }

        public decimal NorminalRate
        {
            get { return _NorminalRate; }
            set
            {
                if (_NorminalRate != value)
                {
                    _NorminalRate = value;
                    OnPropertyChanged(() => NorminalRate);
                }
            }
        }
        public int AMSequence
        {
            get { return _AMSequence; }
            set
            {
                if (_AMSequence != value)
                {
                    _AMSequence = value;
                    OnPropertyChanged(() => AMSequence);
                }
            }
        }

        public DateTime AMPaymentDate
        {
            get { return _AMPaymentDate; }
            set
            {
                if (_AMPaymentDate != value)
                {
                    _AMPaymentDate = value;
                    OnPropertyChanged(() => AMPaymentDate);
                }
            }
        }

        public DateTime AMDate
        {
            get { return _AMDate; }
            set
            {
                if (_AMDate != value)
                {
                    _AMDate = value;
                    OnPropertyChanged(() => AMDate);
                }
            }
        }

        public double AMOpeningBalance
        {
            get { return _AMOpeningBalance; }
            set
            {
                if (_AMOpeningBalance != value)
                {
                    _AMOpeningBalance = value;
                    OnPropertyChanged(() => AMOpeningBalance);
                }
            }
        }


        public double AMAmountPrincInit
        {
            get { return _AMAmountPrincInit; }
            set
            {
                if (_AMAmountPrincInit != value)
                {
                    _AMAmountPrincInit = value;
                    OnPropertyChanged(() => AMAmountPrincInit);
                }
            }
        }



        public double AMDailyInt
        {
            get { return _AMDailyInt; }
            set
            {
                if (_AMDailyInt != value)
                {
                    _AMDailyInt = value;
                    OnPropertyChanged(() => AMDailyInt);
                }
            }
        }

        public double AMDailyPrinc
        {
            get { return _AMDailyPrinc; }
            set
            {
                if (_AMDailyPrinc != value)
                {
                    _AMDailyPrinc = value;
                    OnPropertyChanged(() => AMDailyPrinc);
                }
            }
        }



        public double AMClosingBalance
        {
            get { return _AMClosingBalance; }
            set
            {
                if (_AMClosingBalance != value)
                {
                    _AMClosingBalance = value;
                    OnPropertyChanged(() => AMClosingBalance);
                }
            }
        }

        public double AMAmountPrincEnd
        {
            get { return _AMAmountPrincEnd; }
            set
            {
                if (_AMAmountPrincEnd != value)
                {
                    _AMAmountPrincEnd = value;
                    OnPropertyChanged(() => AMAmountPrincEnd);
                }
            }
        }
        public double AMAccruedInterest
        {
            get { return _AMAccruedInterest; }
            set
            {
                if (_AMAccruedInterest != value)
                {
                    _AMAccruedInterest = value;
                    OnPropertyChanged(() => AMAccruedInterest);
                }
            }
        }


        public double AMAmortizedCost
        {
            get { return _AMAmortizedCost; }
            set
            {
                if (_AMAmortizedCost != value)
                {
                    _AMAmortizedCost = value;
                    OnPropertyChanged(() => AMAmortizedCost);
                }
            }
        }


        public double AMAmortized
        {
            get { return _AMAmortized; }
            set
            {
                if (_AMAmortized != value)
                {
                    _AMAmortized = value;
                    OnPropertyChanged(() => AMAmortized);
                }
            }
        }

        public double BalloonAmt
        {
            get { return _BalloonAmt; }
            set
            {
                if (_BalloonAmt != value)
                {
                    _BalloonAmt = value;
                    OnPropertyChanged(() => BalloonAmt);
                }
            }
        }

        public double DiscountPremium
        {
            get { return _DiscountPremium; }
            set
            {
                if (_DiscountPremium != value)
                {
                    _DiscountPremium = value;
                    OnPropertyChanged(() => DiscountPremium);
                }
            }
        }


        public double UnearnedFee
        {
            get { return _UnearnedFee; }
            set
            {
                if (_UnearnedFee != value)
                {
                    _UnearnedFee = value;
                    OnPropertyChanged(() => UnearnedFee);
                }
            }
        }

        public double EarnedFee
        {
            get { return _EarnedFee; }
            set
            {
                if (_EarnedFee != value)
                {
                    _EarnedFee = value;
                    OnPropertyChanged(() => EarnedFee);
                }
            }
        }



        public decimal EffectiveRate
        {
            get { return _EffectiveRate; }
            set
            {
                if (_EffectiveRate != value)
                {
                    _EffectiveRate = value;
                    OnPropertyChanged(() => EffectiveRate);
                }
            }
        }

        public int NoOfPeriods
        {
            get { return _NoOfPeriods; }
            set
            {
                if (_NoOfPeriods != value)
                {
                    _NoOfPeriods = value;
                    OnPropertyChanged(() => NoOfPeriods);
                }
            }
        }

        public DateTime PostedDate
        {
            get { return _PostedDate; }
            set
            {
                if (_PostedDate != value)
                {
                    _PostedDate = value;
                    OnPropertyChanged(() => PostedDate);
                }
            }
        }

        public DateTime LastRunDate
        {
            get { return _LastRunDate; }
            set
            {
                if (_LastRunDate != value)
                {
                    _LastRunDate = value;
                    OnPropertyChanged(() => LastRunDate);
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






        class LoanScheduleValidator : AbstractValidator<LoanSchedule>
        {
            public LoanScheduleValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanScheduleValidator();
        }
    }
}
