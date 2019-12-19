using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class IFRSBonds : ObjectBase
    {
        int _BondId;
        string _RefNo;
        string _Currency;
        string _PortfolioID;
        Nullable<DateTime> _ValueDate;
        DateTime _MaturityDate;
        double _CleanPrice;
        Nullable<double> _FaceValue;
        decimal _CouponRate;
        decimal _CurrentMarketYield;
        Nullable<DateTime> _FirstCouponDate;
        string _Classification;
        string _Classification_Category;
        string _CompanyCode;
        string _Narration;
        string _Symbol;
        string _SandP_Rating;
        double _Price;
        Nullable<bool> _Split;
        bool _Active;
        int? _Stage;
        string _CollateralType;
        Nullable<double> _CollateralValue;


        //ACCESS START
        Nullable<double> _redemption;
        string _Coupon_payment_freg;
        string _Prin_Repayment_Freg;
        Nullable<double> _Current_Carrying_amount_GAAP;
        Nullable<double> _Current_Carrying_amount_IFRS;
        Nullable<double> _EIR;
        Nullable<double> _IAS39_Impairment;
        int? _Princ_Rep_Frq_int;
        int? _Interest_rep_frg_int;
        string _CounterParty;
        string _Previous_rating;
        Nullable<DateTime> _purchase_date;
        string _cust_id;
        //ACCESS END


        public int BondId

        {
            get { return _BondId; }
            set
            {
                if (_BondId != value)
                {
                    _BondId = value;
                    OnPropertyChanged(() => BondId);
                }
            }
        }
        public string SandP_Rating
        {
            get { return _SandP_Rating; }
            set
            {
                if (_SandP_Rating != value)
                {
                    _SandP_Rating = value;
                    OnPropertyChanged(() => SandP_Rating);
                }
            }
        }
        public string Classification_Category
        {
            get { return _Classification_Category; }
            set
            {
                if (_Classification_Category != value)
                {
                    _Classification_Category = value;
                    OnPropertyChanged(() => Classification_Category);
                }
            }
        }
        public double Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    OnPropertyChanged(() => Price);
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

        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                if (_Symbol != value)
                {
                    _Symbol = value;
                    OnPropertyChanged(() => Symbol);
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

        public string PortfolioID
        {
            get { return _PortfolioID; }
            set
            {
                if (_PortfolioID != value)
                {
                    _PortfolioID = value;
                    OnPropertyChanged(() => PortfolioID);
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

        public double CleanPrice
        {
            get { return _CleanPrice; }
            set
            {
                if (_CleanPrice != value)
                {
                    _CleanPrice = value;
                    OnPropertyChanged(() => CleanPrice);
                }
            }
        }


        public Nullable<double> FaceValue
        {
            get { return _FaceValue; }
            set
            {
                if (_FaceValue != value)
                {
                    _FaceValue = value;
                    OnPropertyChanged(() => FaceValue);
                }
            }
        }

        public decimal CouponRate
        {
            get { return _CouponRate; }
            set
            {
                if (_CouponRate != value)
                {
                    _CouponRate = value;
                    OnPropertyChanged(() => CouponRate);
                }
            }
        }

        public decimal CurrentMarketYield
        {
            get { return _CurrentMarketYield; }
            set
            {
                if (_CurrentMarketYield != value)
                {
                    _CurrentMarketYield = value;
                    OnPropertyChanged(() => CurrentMarketYield);
                }
            }
        }

        public Nullable<DateTime> FirstCouponDate
        {
            get { return _FirstCouponDate; }
            set
            {
                if (_FirstCouponDate != value)
                {
                    _FirstCouponDate = value;
                    OnPropertyChanged(() => FirstCouponDate);
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
        public string Narration
        {
            get { return _Narration; }
            set
            {
                if (_Narration != value)
                {
                    _Narration = value;
                    OnPropertyChanged(() => Narration);
                }
            }
        }

        public Nullable<bool> Split
        {
            get { return _Split; }
            set
            {
                if (_Split != value)
                {
                    _Split = value;
                    OnPropertyChanged(() => Split);
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


        //Access Start

        public Nullable<double> redemption
        {
            get { return _redemption; }
            set
            {
                if (_redemption != value)
                {
                    _redemption = value;
                    OnPropertyChanged(() => redemption);
                }
            }
        }

        public string Coupon_payment_freg
        {
            get { return _Coupon_payment_freg; }
            set
            {
                if (_Coupon_payment_freg != value)
                {
                    _Coupon_payment_freg = value;
                    OnPropertyChanged(() => Coupon_payment_freg);
                }
            }
        }

        public string Prin_Repayment_Freg
        {
            get { return _Prin_Repayment_Freg; }
            set
            {
                if (_Prin_Repayment_Freg != value)
                {
                    _Prin_Repayment_Freg = value;
                    OnPropertyChanged(() => Prin_Repayment_Freg);
                }
            }
        }

        public Nullable<double> Current_Carrying_amount_GAAP
        {
            get { return _Current_Carrying_amount_GAAP; }
            set
            {
                if (_Current_Carrying_amount_GAAP != value)
                {
                    _Current_Carrying_amount_GAAP = value;
                    OnPropertyChanged(() => Current_Carrying_amount_GAAP);
                }
            }
        }

        public Nullable<double> Current_Carrying_amount_IFRS
        {
            get { return _Current_Carrying_amount_IFRS; }
            set
            {
                if (_Current_Carrying_amount_IFRS != value)
                {
                    _Current_Carrying_amount_IFRS = value;
                    OnPropertyChanged(() => Current_Carrying_amount_IFRS);
                }
            }
        }

        public Nullable<double> EIR
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

        public Nullable<double> IAS39_Impairment
        {
            get { return _IAS39_Impairment; }
            set
            {
                if (_IAS39_Impairment != value)
                {
                    _IAS39_Impairment = value;
                    OnPropertyChanged(() => IAS39_Impairment);
                }
            }
        }

        public int? Princ_Rep_Frq_int
        {
            get { return _Princ_Rep_Frq_int; }
            set
            {
                if (_Princ_Rep_Frq_int != value)
                {
                    _Princ_Rep_Frq_int = value;
                    OnPropertyChanged(() => Princ_Rep_Frq_int);
                }
            }
        }

        public int? Interest_rep_frg_int
        {
            get { return _Interest_rep_frg_int; }
            set
            {
                if (_Interest_rep_frg_int != value)
                {
                    _Interest_rep_frg_int = value;
                    OnPropertyChanged(() => Interest_rep_frg_int);
                }
            }
        }

        public string CounterParty
        {
            get { return _CounterParty; }
            set
            {
                if (_CounterParty != value)
                {
                    _CounterParty = value;
                    OnPropertyChanged(() => CounterParty);
                }
            }
        }

        public string Previous_rating
        {
            get { return _Previous_rating; }
            set
            {
                if (_Previous_rating != value)
                {
                    _Previous_rating = value;
                    OnPropertyChanged(() => Previous_rating);
                }
            }
        }

        public Nullable<DateTime> purchase_date
        {
            get { return _purchase_date; }
            set
            {
                if (_purchase_date != value)
                {
                    _purchase_date = value;
                    OnPropertyChanged(() => purchase_date);
                }
            }
        }

        public string cust_id
        {
            get { return _cust_id; }
            set
            {
                if (_cust_id != value)
                {
                    _cust_id = value;
                    OnPropertyChanged(() => cust_id);
                }
            }
        }

        class IFRSBondsValidator : AbstractValidator<IFRSBonds>
        {
            public IFRSBondsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new IFRSBondsValidator();
        }
    }
}
