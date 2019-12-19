using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class PlacementComputationResult : ObjectBase
    {
        int _Id;
        string _CustomerName;
        string _RefNo;
        string _Rating;
        DateTime _BookingDate;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        double _TransactionAmount;
        double _InterestRate;
        double _ExchangeRate;
        double _LCY_Amount;
        double _Days_in_Holding;
        double _Interest;
        double _AmortisedCost;
        DateTime _Rundate;
        bool _Active;


        public DateTime Rundate
        {
            get { return _Rundate; }
            set
            {
                if (_Rundate != value)
                {
                    _Rundate = value;
                    OnPropertyChanged(() => Rundate);
                }
            }
        }

        public double AmortisedCost
        {
            get { return _AmortisedCost; }
            set
            {
                if (_AmortisedCost != value)
                {
                    _AmortisedCost = value;
                    OnPropertyChanged(() => AmortisedCost);
                }
            }
        }

        public double Interest
        {
            get { return _Interest; }
            set
            {
                if (_Interest != value)
                {
                    _Interest = value;
                    OnPropertyChanged(() => Interest);
                }
            }
        }

        public string Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        public DateTime BookingDate
        {
            get { return _BookingDate; }
            set
            {
                if (_BookingDate != value)
                {
                    _BookingDate = value;
                    OnPropertyChanged(() => BookingDate);
                }
            }
        }

        public double Days_in_Holding
        {
            get { return _Days_in_Holding; }
            set
            {
                if (_Days_in_Holding != value)
                {
                    _Days_in_Holding = value;
                    OnPropertyChanged(() => Days_in_Holding);
                }
            }
        }

        public double LCY_Amount
        {
            get { return _LCY_Amount; }
            set
            {
                if (_LCY_Amount != value)
                {
                    _LCY_Amount = value;
                    OnPropertyChanged(() => LCY_Amount);
                }
            }
        }

        public double ExchangeRate
        {
            get { return _ExchangeRate; }
            set
            {
                if (_ExchangeRate != value)
                {
                    _ExchangeRate = value;
                    OnPropertyChanged(() => ExchangeRate);
                }
            }
        }

        public DateTime ValueDate
        {
            get { return _ValueDate; }
            set
            {
                if (_ValueDate != value)
                {
                    _ValueDate = value;
                    OnPropertyChanged(() => ValueDate);
                }
            }
        }

        public DateTime MaturityDate
        {
            get { return _MaturityDate; }
            set
            {
                if (_MaturityDate != value)
                {
                    _MaturityDate = value;
                    OnPropertyChanged(() => MaturityDate);
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

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }


        public double InterestRate
        {
            get { return _InterestRate; }
            set
            {
                if (_InterestRate != value)
                {
                    _InterestRate = value;
                    OnPropertyChanged(() => InterestRate);
                }
            }
        }

        public double TransactionAmount
        {
            get { return _TransactionAmount; }
            set
            {
                if (_TransactionAmount != value)
                {
                    _TransactionAmount = value;
                    OnPropertyChanged(() => TransactionAmount);
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


        class PlacementComputationResultValidator : AbstractValidator<PlacementComputationResult>
        {
            public PlacementComputationResultValidator()
            {
                //RuleFor(obj => obj._CustomerName).NotEmpty().WithMessage("CustomerName is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new PlacementComputationResultValidator();
        }
    }
}
