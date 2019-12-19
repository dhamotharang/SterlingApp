using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class Placement : ObjectBase
    {
        int _Placement_Id;
        string _RefNo;
        string _CustomerName;
        string _Rating;
        Nullable<DateTime> _BookingDate;
        Nullable<DateTime> _ValueDate;
        DateTime _MaturityDate;
        double _Amount;
        double _Rate;
        string _Currency;
        double _ExchangeRate;
        double _LCY_Amount;
        string _CollateralType;
        Nullable<double> _CollateralValue;
        Nullable<double> _CollateralHaircut;
        Nullable<DateTime> _RunDate;
        bool _Active;
        int? _Stage;

        //Access
        string _asset_classification;
        string _repayment_term;
        string _previous_rating;
        string _asset_type;
        string _asset_desc;
        string _rating_agency;
        Nullable<double> _days_pass_due;
        string _prudential_classification;
        string _forebearance_flag;

        public int Placement_Id
        {
            get { return _Placement_Id; }
            set
            {
                if (_Placement_Id != value)
                {
                    _Placement_Id = value;
                    OnPropertyChanged(() => Placement_Id);
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

        public Nullable<DateTime> BookingDate
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

        public Nullable<DateTime> ValueDate
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

        public double Rate
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

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
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

        public string CollateralType
        {
            get { return _CollateralType; }
            set
            {
                if (_CollateralType != value)
                {
                    _CollateralType = value;
                    OnPropertyChanged(() => CollateralType);
                }
            }
        }

        public Nullable<double> CollateralValue
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

        public Nullable<double> CollateralHaircut
        {
            get { return _CollateralHaircut; }
            set
            {
                if (_CollateralHaircut != value)
                {
                    _CollateralHaircut = value;
                    OnPropertyChanged(() => CollateralHaircut);
                }
            }
        }

        public Nullable<DateTime> RunDate
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

        public int? Stage
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




        //Access

        public string asset_classification
        {
            get { return _asset_classification; }
            set
            {
                if (_asset_classification != value)
                {
                    _asset_classification = value;
                    OnPropertyChanged(() => asset_classification);
                }
            }
        }

        public string repayment_term
        {
            get { return _repayment_term; }
            set
            {
                if (_repayment_term != value)
                {
                    _repayment_term = value;
                    OnPropertyChanged(() => repayment_term);
                }
            }
        }

        public string previous_rating
        {
            get { return _previous_rating; }
            set
            {
                if (_previous_rating != value)
                {
                    _previous_rating = value;
                    OnPropertyChanged(() => previous_rating);
                }
            }
        }

        public string asset_type
        {
            get { return _asset_type; }
            set
            {
                if (_asset_type != value)
                {
                    _asset_type = value;
                    OnPropertyChanged(() => asset_type);
                }
            }
        }

        public string asset_desc
        {
            get { return _asset_desc; }
            set
            {
                if (_asset_desc != value)
                {
                    _asset_desc = value;
                    OnPropertyChanged(() => asset_desc);
                }
            }
        }

        public string rating_agency
        {
            get { return _rating_agency; }
            set
            {
                if (_rating_agency != value)
                {
                    _rating_agency = value;
                    OnPropertyChanged(() => rating_agency);
                }
            }
        }

        public Nullable<double> days_pass_due
        {
            get { return _days_pass_due; }
            set
            {
                if (_days_pass_due != value)
                {
                    _days_pass_due = value;
                    OnPropertyChanged(() => days_pass_due);
                }
            }
        }

        public string prudential_classification
        {
            get { return _prudential_classification; }
            set
            {
                if (_prudential_classification != value)
                {
                    _prudential_classification = value;
                    OnPropertyChanged(() => prudential_classification);
                }
            }
        }

        public string forebearance_flag
        {
            get { return _forebearance_flag; }
            set
            {
                if (_forebearance_flag != value)
                {
                    _forebearance_flag = value;
                    OnPropertyChanged(() => forebearance_flag);
                }
            }
        }


        class PlacementValidator : AbstractValidator<Placement>
        {
            public PlacementValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
              
            }
        }

        protected override IValidator GetValidator()
        {
            return new PlacementValidator();
        }
    }
}
