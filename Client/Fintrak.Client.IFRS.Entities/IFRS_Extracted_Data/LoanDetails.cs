using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class RawLoanDetails : ObjectBase
    {
        int _LoanDetailId;
        string _RefNo;
        string _AccountNo;
        Nullable<double> _PastDueAmount;
        Nullable<double> _ODLimit;
        Nullable<double> _CollateralHaircut;
        Nullable<double> _CollateralRecoverableAmt;
        string _Segment;
        string _CollateralType;
        Nullable<double> _PrincipalOutstandingBal;
        Nullable<double> _Amount;
        Nullable<double> _Interest_Receiv_Pay_UnEarn;
        string _ProductCode;
        string _ProductName;
        string _ProductType;
        string _Sub_Type;
        string _Currency;
        Nullable<DateTime> _ValueDate;
        Nullable<DateTime> _MaturityDate;
        Nullable<DateTime> _FirstRepaymentdate;
        Nullable<DateTime> _PrincipalFirstRepaymentDate;
        int? _InterestRepayFreq;
        int? _PrincipalRepayFreq;
        Nullable<decimal> _ExchangeRate;
        string _Rating;
        Nullable<decimal> _Rate;
        int? _Stage;
        Nullable<double> _CollateralValue;
        string _CompanyCode;
        string _Sector;
        string _Classification;
        string _SubClassification;
        string _Custid;
        string _ForbearanceFlag;
        int? _MissedPayment;
        string _InitialCreditRating;
        string _InternalRating;
        int? _MissedPayment_Stage;
        int? _Classification_Stage;
        int? _Forbearance_Stage;
        int? _Classification_Stage_Final;
        int? _PastDueDays;
        bool _Active;

        //Access Start
        string _CUSTOMER_NAME;
        Nullable<double> _ForcedSaleValue;
        Nullable<double> _CostRecovery;
        int? _CreditStaging;
        int? _ModelClassification;
        int? _ClassificationOverride;

        public int LoanDetailId
        {
            get { return _LoanDetailId; }
            set
            {
                if (_LoanDetailId != value)
                {
                    _LoanDetailId = value;
                    OnPropertyChanged(() => LoanDetailId);
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
        public Nullable<double> PastDueAmount
        {
            get { return _PastDueAmount; }
            set
            {
                if (_PastDueAmount != value)
                {
                    _PastDueAmount = value;
                    OnPropertyChanged(() => PastDueAmount);
                }
            }
        }
        public Nullable<double> ODLimit 
        {
            get { return _ODLimit ; }
            set
            {
                if (_ODLimit  != value)
                {
                    _ODLimit  = value;
                    OnPropertyChanged(() => ODLimit );
                }
            }
        }
        public Nullable<double> CollateralHaircut 
        {
            get { return _CollateralHaircut ; }
            set
            {
                if (_CollateralHaircut  != value)
                {
                    _CollateralHaircut  = value;
                    OnPropertyChanged(() => CollateralHaircut );
                }
            }
        }
        public Nullable<double> CollateralRecoverableAmt 
        {
            get { return _CollateralRecoverableAmt ; }
            set
            {
                if (_CollateralRecoverableAmt  != value)
                {
                    _CollateralRecoverableAmt  = value;
                    OnPropertyChanged(() => CollateralRecoverableAmt );
                }
            }
        }
        public string Segment
        {
            get { return _Segment; }
            set
            {
                if (_Segment != value)
                {
                    _Segment = value;
                    OnPropertyChanged(() => Segment);
                }
            }
        }
        public string CollateralType 
        {
            get { return _CollateralType ; }
            set
            {
                if (_CollateralType  != value)
                {
                    _CollateralType  = value;
                    OnPropertyChanged(() => CollateralType );
                }
            }
        }
        public Nullable<double> PrincipalOutstandingBal
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
        public Nullable<double> Interest_Receiv_Pay_UnEarn
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

        public string Sub_Type
        {
            get { return _Sub_Type; }
            set
            {
                if (_Sub_Type != value)
                {
                    _Sub_Type = value;
                    OnPropertyChanged(() => Sub_Type);
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

        public Nullable<double> Amount
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
        public Nullable<DateTime> FirstRepaymentdate
        {
            get { return _FirstRepaymentdate; }
            set
            {
                if (_FirstRepaymentdate != value)
                {
                    _FirstRepaymentdate = value;
                    OnPropertyChanged(() => FirstRepaymentdate);
                }
            }
        }

        public Nullable<DateTime> PrincipalFirstRepaymentDate
        {
            get { return _PrincipalFirstRepaymentDate; }
            set
            {
                if (_PrincipalFirstRepaymentDate != value)
                {
                    _PrincipalFirstRepaymentDate = value;
                    OnPropertyChanged(() => PrincipalFirstRepaymentDate);
                }
            }
        }

        public int? InterestRepayFreq
        {
            get { return _InterestRepayFreq; }
            set
            {
                if (_InterestRepayFreq != value)
                {
                    _InterestRepayFreq = value;
                    OnPropertyChanged(() => InterestRepayFreq);
                }
            }
        }

        public int? PrincipalRepayFreq
        {
            get { return _PrincipalRepayFreq; }
            set
            {
                if (_PrincipalRepayFreq != value)
                {
                    _PrincipalRepayFreq = value;
                    OnPropertyChanged(() => PrincipalRepayFreq);
                }
            }
        }



        public Nullable<DateTime> MaturityDate
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

        public Nullable<decimal> ExchangeRate
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

        public Nullable<decimal> Rate
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

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }

        public string Classification
        {
            get { return _Classification.ToUpper(); }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
                }
            }
        }

        public string SubClassification
        {
            get { return _SubClassification.ToUpper(); }
            set
            {
                if (_SubClassification != value)
                {
                    _SubClassification = value;
                    OnPropertyChanged(() => SubClassification);
                }
            }
        }

        public string Custid
        {
            get { return _Custid; }
            set
            {
                if (_Custid != value)
                {
                    _Custid = value;
                    OnPropertyChanged(() => Custid);
                }
            }
        }

        public string ForbearanceFlag
        {
            get { return _ForbearanceFlag; }
            set
            {
                if (_ForbearanceFlag != value)
                {
                    _ForbearanceFlag = value;
                    OnPropertyChanged(() => ForbearanceFlag);
                }
            }
        }

        public int? MissedPayment
        {
            get { return _MissedPayment; }
            set
            {
                if (_MissedPayment != value)
                {
                    _MissedPayment = value;
                    OnPropertyChanged(() => MissedPayment);
                }
            }
        }

        public string InitialCreditRating
        {
            get { return _InitialCreditRating; }
            set
            {
                if (_InitialCreditRating != value)
                {
                    _InitialCreditRating = value;
                    OnPropertyChanged(() => InitialCreditRating);
                }
            }
        }

        public string InternalRating
        {
            get { return _InternalRating; }
            set
            {
                if (_InternalRating != value)
                {
                    _InternalRating = value;
                    OnPropertyChanged(() => InternalRating);
                }
            }
        }

        public int? MissedPayment_Stage
        {
            get { return _MissedPayment_Stage; }
            set
            {
                if (_MissedPayment_Stage != value)
                {
                    _MissedPayment_Stage = value;
                    OnPropertyChanged(() => MissedPayment_Stage);
                }
            }
        }

        public int? Classification_Stage
        {
            get { return _Classification_Stage; }
            set
            {
                if (_Classification_Stage != value)
                {
                    _Classification_Stage = value;
                    OnPropertyChanged(() => Classification_Stage);
                }
            }
        }

        public int? Forbearance_Stage
        {
            get { return _Forbearance_Stage; }
            set
            {
                if (_Forbearance_Stage != value)
                {
                    _Forbearance_Stage = value;
                    OnPropertyChanged(() => Forbearance_Stage);
                }
            }
        }

        public int? Classification_Stage_Final
        {
            get { return _Classification_Stage_Final; }
            set
            {
                if (_Classification_Stage_Final != value)
                {
                    _Classification_Stage_Final = value;
                    OnPropertyChanged(() => Classification_Stage_Final);
                }
            }
        }

        public int? PastDueDays
        {
            get { return _PastDueDays; }
            set
            {
                if (_PastDueDays != value)
                {
                    _PastDueDays = value;
                    OnPropertyChanged(() => PastDueDays);
                }
            }
        }

        //ACCESS START

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

        public Nullable<double> ForcedSaleValue
        {
            get { return _ForcedSaleValue; }
            set
            {
                if (_ForcedSaleValue != value)
                {
                    _ForcedSaleValue = value;
                    OnPropertyChanged(() => ForcedSaleValue);
                }
            }
        }

        public Nullable<double> CostRecovery
        {
            get { return _CostRecovery; }
            set
            {
                if (_CostRecovery != value)
                {
                    _CostRecovery = value;
                    OnPropertyChanged(() => CostRecovery);
                }
            }
        }

        public int? CreditStaging
        {
            get { return _CreditStaging; }
            set
            {
                if (_CreditStaging != value)
                {
                    _CreditStaging = value;
                    OnPropertyChanged(() => CreditStaging);
                }
            }
        }

        public int? ModelClassification
        {
            get { return _ModelClassification; }
            set
            {
                if (_ModelClassification != value)
                {
                    _ModelClassification = value;
                    OnPropertyChanged(() => ModelClassification);
                }
            }
        }

        public int? ClassificationOverride
        {
            get { return _ClassificationOverride; }
            set
            {
                if (_ClassificationOverride != value)
                {
                    _ClassificationOverride = value;
                    OnPropertyChanged(() => ClassificationOverride);
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
        class LoanDetailsValidator : AbstractValidator<RawLoanDetails>
        {
            public LoanDetailsValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanDetailsValidator();
        }
    }
}
