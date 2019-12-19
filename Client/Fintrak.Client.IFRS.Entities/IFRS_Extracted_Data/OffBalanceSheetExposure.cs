using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class OffBalanceSheetExposure : ObjectBase
    {
        int _ObeId;
        string _RefNo;
        string _CUSTOMER_NAME;
        string _CUR;
        string _CollateralType;
        double _CollateralValue;
        DateTime _TRNX_DATE;
        string _Maturity_profile;
        DateTime _MATURITY_DATE;
        double _Amount_FCY;
        double _Amount_NGN;
        Nullable<decimal> _Ex_Rate;
        int? _TenorMonths;
        string _Portfolio;
        string _RATING;
        string _SUB_PORTFOLIO;
        Nullable<bool> _CanCrystallize;
        bool _Active;
        int? _Staging;
        double _EIR;
        string _Type;

        //Access Start
        string _ACCOUNT_NUMBER;
        string _SETTLEMENT_ACCOUNT;
        string _Forbearance_flag;
        string _CBN_Sector;
        string _Initial_Credit_Rating;
        int? _Days_past_due;
        string _Classification;
        string _Principal_payment_structure;
        string _Interest_payment_structure;
        int? _CUST_ID;
        string _Secured_Unsecured;
        Nullable<double> _Forced_sale_value;
        Nullable<double> _Cash_OMV;
        Nullable<double> _Cash_FSV;
        Nullable<double> _Cost_of_recovery;
        string _Facility_type;
        //Access End

        public int ObeId

        {
            get { return _ObeId; }
            set
            {
                if (_ObeId != value)
                {
                    _ObeId = value;
                    OnPropertyChanged(() => ObeId);
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

        public string CUSTOMER_NAME
        {
            get { return _CUSTOMER_NAME; }
            set
            {
                if (_CUSTOMER_NAME != value)
                {
                    _CUSTOMER_NAME = value;
                    OnPropertyChanged(() => CUSTOMER_NAME);
                }
            }
        }

        public string SUB_PORTFOLIO
        {
            get { return _SUB_PORTFOLIO; }
            set
            {
                if (_SUB_PORTFOLIO != value)
                {
                    _SUB_PORTFOLIO = value;
                    OnPropertyChanged(() => SUB_PORTFOLIO);
                }
            }
        }

        public string Maturity_profile
        {
            get { return _Maturity_profile; }
            set
            {
                if (_Maturity_profile != value)
                {
                    _Maturity_profile = value;
                    OnPropertyChanged(() => Maturity_profile);
                }
            }
        }
        public string RATING
        {
            get { return _RATING; }
            set
            {
                if (_RATING != value)
                {
                    _RATING = value;
                    OnPropertyChanged(() => RATING);
                }
            }
        }
        public int? Staging
        {
            get { return _Staging; }
            set
            {
                if (_Staging != value)
                {
                    _Staging = value;
                    OnPropertyChanged(() => Staging);
                }
            }
        }
        public string Type
        {
            get { return _Type; }
            set
            {
                if (Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        public string CUR
        {
            get { return _CUR; }
            set
            {
                if (_CUR != value)
                {
                    _CUR = value;
                    OnPropertyChanged(() => CUR);
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
        public DateTime TRNX_DATE
        {
            get { return _TRNX_DATE; }
            set
            {
                if (_TRNX_DATE != value)
                {
                    _TRNX_DATE = value;
                    OnPropertyChanged(() => TRNX_DATE);
                }
            }
        }

        public DateTime MATURITY_DATE
        {
            get { return _MATURITY_DATE; }
            set
            {
                if (_MATURITY_DATE != value)
                {
                    _MATURITY_DATE = value;
                    OnPropertyChanged(() => MATURITY_DATE);
                }
            }
        }

        public double Amount_FCY
        {
            get { return _Amount_FCY; }
            set
            {
                if (_Amount_FCY != value)
                {
                    _Amount_FCY = value;
                    OnPropertyChanged(() => Amount_FCY);
                }
            }
        }


        public double Amount_NGN
        {
            get { return _Amount_NGN; }
            set
            {
                if (_Amount_NGN != value)
                {
                    _Amount_NGN = value;
                    OnPropertyChanged(() => Amount_NGN);
                }
            }
        }

        public Nullable<decimal> Ex_Rate
        {
            get { return _Ex_Rate; }
            set
            {
                if (_Ex_Rate != value)
                {
                    _Ex_Rate = value;
                    OnPropertyChanged(() => Ex_Rate);
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

        public int? TenorMonths
        {
            get { return _TenorMonths; }
            set
            {
                if (_TenorMonths != value)
                {
                    _TenorMonths = value;
                    OnPropertyChanged(() => TenorMonths);
                }
            }
        }

        public string Portfolio
        {
            get { return _Portfolio; }
            set
            {
                if (_Portfolio != value)
                {
                    _Portfolio = value;
                    OnPropertyChanged(() => Portfolio);
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
        public Nullable<bool> CanCrystallize
        {
            get { return _CanCrystallize; }
            set
            {
                if (_CanCrystallize != value)
                {
                    _CanCrystallize = value;
                    OnPropertyChanged(() => CanCrystallize);
                }
            }
        }

        //Access Start

        public string ACCOUNT_NUMBER
        {
            get { return _ACCOUNT_NUMBER; }
            set
            {
                if (_ACCOUNT_NUMBER != value)
                {
                    _ACCOUNT_NUMBER = value;
                    OnPropertyChanged(() => ACCOUNT_NUMBER);
                }
            }
        }

        public string SETTLEMENT_ACCOUNT
        {
            get { return _SETTLEMENT_ACCOUNT; }
            set
            {
                if (_SETTLEMENT_ACCOUNT != value)
                {
                    _SETTLEMENT_ACCOUNT = value;
                    OnPropertyChanged(() => SETTLEMENT_ACCOUNT);
                }
            }
        }

        public string Forbearance_flag
        {
            get { return _Forbearance_flag; }
            set
            {
                if (_Forbearance_flag != value)
                {
                    _Forbearance_flag = value;
                    OnPropertyChanged(() => Forbearance_flag);
                }
            }
        }

        public string CBN_Sector
        {
            get { return _CBN_Sector; }
            set
            {
                if (_CBN_Sector != value)
                {
                    _CBN_Sector = value;
                    OnPropertyChanged(() => CBN_Sector);
                }
            }
        }

        public string Initial_Credit_Rating
        {
            get { return _Initial_Credit_Rating; }
            set
            {
                if (_Initial_Credit_Rating != value)
                {
                    _Initial_Credit_Rating = value;
                    OnPropertyChanged(() => Initial_Credit_Rating);
                }
            }
        }

        public int? Days_past_due
        {
            get { return _Days_past_due; }
            set
            {
                if (_Days_past_due != value)
                {
                    _Days_past_due = value;
                    OnPropertyChanged(() => Days_past_due);
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

        public string Principal_payment_structure
        {
            get { return _Principal_payment_structure; }
            set
            {
                if (_Principal_payment_structure != value)
                {
                    _Principal_payment_structure = value;
                    OnPropertyChanged(() => Principal_payment_structure);
                }
            }
        }

        public string Interest_payment_structure
        {
            get { return _Interest_payment_structure; }
            set
            {
                if (_Interest_payment_structure != value)
                {
                    _Interest_payment_structure = value;
                    OnPropertyChanged(() => Interest_payment_structure);
                }
            }
        }

        public int? CUST_ID
        {
            get { return _CUST_ID; }
            set
            {
                if (_CUST_ID != value)
                {
                    _CUST_ID = value;
                    OnPropertyChanged(() => CUST_ID);
                }
            }
        }

        public string Secured_Unsecured
        {
            get { return _Secured_Unsecured; }
            set
            {
                if (_Secured_Unsecured != value)
                {
                    _Secured_Unsecured = value;
                    OnPropertyChanged(() => Secured_Unsecured);
                }
            }
        }

        public Nullable<double> Forced_sale_value
        {
            get { return _Forced_sale_value; }
            set
            {
                if (_Forced_sale_value != value)
                {
                    _Forced_sale_value = value;
                    OnPropertyChanged(() => Forced_sale_value);
                }
            }
        }

        public Nullable<double> Cash_OMV
        {
            get { return _Cash_OMV; }
            set
            {
                if (_Cash_OMV != value)
                {
                    _Cash_OMV = value;
                    OnPropertyChanged(() => Cash_OMV);
                }
            }
        }

        public Nullable<double> Cash_FSV
        {
            get { return _Cash_FSV; }
            set
            {
                if (_Cash_FSV != value)
                {
                    _Cash_FSV = value;
                    OnPropertyChanged(() => Cash_FSV);
                }
            }
        }

        public Nullable<double> Cost_of_recovery
        {
            get { return _Cost_of_recovery; }
            set
            {
                if (_Cost_of_recovery != value)
                {
                    _Cost_of_recovery = value;
                    OnPropertyChanged(() => Cost_of_recovery);
                }
            }
        }

        public string Facility_type
        {
            get { return _Facility_type; }
            set
            {
                if (_Facility_type != value)
                {
                    _Facility_type = value;
                    OnPropertyChanged(() => Facility_type);
                }
            }
        }

        //Access End

        class OffBalanceSheetExposureValidator : AbstractValidator<OffBalanceSheetExposure>
        {
            public OffBalanceSheetExposureValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new OffBalanceSheetExposureValidator();
        }
    }
}
