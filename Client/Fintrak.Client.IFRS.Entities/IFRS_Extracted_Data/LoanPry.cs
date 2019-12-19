using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanPry : ObjectBase
    {
        int _PryId;
        string _RefNo;
        string _AccountNo;
        string _ProductCategory;
        string _ProductCode;
        string _ProductName;
        String _ProductType;
        string _Currency;
        DateTime _ValueDate;
        DateTime _MaturityDate;
        String _Sector;
        double _Amount;
        Nullable<double> _PeriodicRepaymentAmount;
        decimal _ExchangeRate;
        decimal _Rate;
        int? _Tenor;
        int _InterestRepayFreq;
        int _PrincipalRepayFreq;
        string _Schedule_Type;
        Nullable<DateTime> _FirstRepaymentdate;
        Nullable<DateTime> _InterestFirstRepayDate;
        string _CompanyCode;
        bool _Active;

        //Access Start
        string _custid;
        Nullable<double> _OutstandingBal;
        double? _PastDue;
        string _InitialCreditRating;
        string _current_staging;
        string _rating;
        string _Classification;
        string _CollateralType;
        Nullable<double> _CollateralValue;
        string _ForbearanceFlag;
        Nullable<double> _ForcedSaleValue;
        Nullable<double> _CashOMV;
        Nullable<double> _CashFSV;
        Nullable<double> _CostRecovery;
        int? _MissedPaymentStage;
        int? _ClassificationStage;
        int? _ForbearanceStage;
        int? _CreditStaging;
        int? _ModelClassification;
        int? _ClassificationOverride;
        int? _ClassificationStageFinal;
        

        public int PryId

        {
            get { return _PryId; }
            set
            {
                if (_PryId != value)
                {
                    _PryId = value;
                    OnPropertyChanged(() => PryId);
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

        public Nullable<DateTime> InterestFirstRepayDate
        {
            get { return _InterestFirstRepayDate; }
            set
            {
                if (_InterestFirstRepayDate != value)
                {
                    _InterestFirstRepayDate = value;
                    OnPropertyChanged(() => InterestFirstRepayDate);
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

        public Nullable<double> PeriodicRepaymentAmount
        {
            get { return _PeriodicRepaymentAmount; }
            set
            {
                if (_PeriodicRepaymentAmount != value)
                {
                    _PeriodicRepaymentAmount = value;
                    OnPropertyChanged(() => PeriodicRepaymentAmount);
                }
            }
        }

        public decimal ExchangeRate
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

        public int? Tenor
        {
            get { return _Tenor; }
            set
            {
                if (_Tenor != value)
                {
                    _Tenor = value;
                    OnPropertyChanged(() => Tenor);
                }
            }
        }

        public int InterestRepayFreq
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

        public int PrincipalRepayFreq
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

        public string Schedule_Type
        {
            get { return _Schedule_Type; }
            set
            {
                if (_Schedule_Type != value)
                {
                    _Schedule_Type = value;
                    OnPropertyChanged(() => Schedule_Type);
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


        //Access Start

        public string custid
        {
            get { return _custid; }
            set
            {
                if (_custid != value)
                {
                    _custid = value;
                    OnPropertyChanged(() => custid);
                }
            }
        }

        public Nullable<double> OutstandingBal
        {
            get { return _OutstandingBal; }
            set
            {
                if (_OutstandingBal != value)
                {
                    _OutstandingBal = value;
                    OnPropertyChanged(() => OutstandingBal);
                }
            }
        }

        public double? PastDue
        {
            get { return _PastDue; }
            set
            {
                if (_PastDue != value)
                {
                    _PastDue = value;
                    OnPropertyChanged(() => PastDue);
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

        public string current_staging
        {
            get { return _current_staging; }
            set
            {
                if (_current_staging != value)
                {
                    _current_staging = value;
                    OnPropertyChanged(() => current_staging);
                }
            }
        }

        public string rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(() => rating);
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

        public Nullable<double> CashOMV
        {
            get { return _CashOMV; }
            set
            {
                if (_CashOMV != value)
                {
                    _CashOMV = value;
                    OnPropertyChanged(() => CashOMV);
                }
            }
        }

        public Nullable<double> CashFSV
        {
            get { return _CashFSV; }
            set
            {
                if (_CashFSV != value)
                {
                    _CashFSV = value;
                    OnPropertyChanged(() => CashFSV);
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

        public int? MissedPaymentStage
        {
            get { return _MissedPaymentStage; }
            set
            {
                if (_MissedPaymentStage != value)
                {
                    _MissedPaymentStage = value;
                    OnPropertyChanged(() => MissedPaymentStage);
                }
            }
        }

        public int? ClassificationStage
        {
            get { return _ClassificationStage; }
            set
            {
                if (_ClassificationStage != value)
                {
                    _ClassificationStage = value;
                    OnPropertyChanged(() => ClassificationStage);
                }
            }
        }

        public int? ForbearanceStage
        {
            get { return _ForbearanceStage; }
            set
            {
                if (_ForbearanceStage != value)
                {
                    _ForbearanceStage = value;
                    OnPropertyChanged(() => ForbearanceStage);
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

        public int? ClassificationStageFinal
        {
            get { return _ClassificationStageFinal; }
            set
            {
                if (_ClassificationStageFinal != value)
                {
                    _ClassificationStageFinal = value;
                    OnPropertyChanged(() => ClassificationStageFinal);
                }
            }
        }


        class LoanPryValidator : AbstractValidator<LoanPry>
        {
            public LoanPryValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanPryValidator();
        }
    }
}
