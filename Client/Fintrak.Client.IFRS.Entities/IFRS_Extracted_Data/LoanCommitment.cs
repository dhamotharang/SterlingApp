using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanCommitment : ObjectBase
    {
        int _LoanCommitmentId;
        string _RefNo;
        string _AccountNo;
        string _ProductCategory;
        string _ProductCode;
        string _ProductName;
        String _ProductType;
        DateTime _ValueDate;
        String _Sector;
        string _Currency;
        DateTime _MaturityDate;
        Nullable<DateTime> _FirstRepaymentdate;
        double _Amount;
        decimal _ExchangeRate;
        decimal _Rate;
        int? _TenorDays;
        int? _TenorMonth;
        string _MISCode;
        Nullable<double> _CollateralHaircut;
        Nullable<double> _CollateralRecoverableAmt;
        Nullable<double> _ODLimit;
        int? _Period;
        int? _Year;
        int? _InterestRepayFreq;
        int? _PrincipalRepayFreq;
        string _CompanyCode;
        Nullable<double> _PrincipalOutstandingBal;
        Nullable<double> _PastDueAmount;
        string _Rating;
        string _Classification;
        string _SubClassification;
        string _CollateralType;
        Nullable<double> _CollateralValue;
        //string _ForbearanceFlag;
        int? _Stage;
        DateTime _RunDate;
        bool _Active;
        

        public int LoanCommitmentId

        {
            get { return _LoanCommitmentId; }
            set
            {
                if (_LoanCommitmentId != value)
                {
                    _LoanCommitmentId = value;
                    OnPropertyChanged(() => LoanCommitmentId);
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

        public int? TenorDays
        {
            get { return _TenorDays; }
            set
            {
                if (_TenorDays != value)
                {
                    _TenorDays = value;
                    OnPropertyChanged(() => TenorDays);
                }
            }
        }

        public int? TenorMonth
        {
            get { return _TenorMonth; }
            set
            {
                if (_TenorMonth != value)
                {
                    _TenorMonth = value;
                    OnPropertyChanged(() => TenorMonth);
                }
            }
        }

        public string MISCode
        {
            get { return _MISCode; }
            set
            {
                if (_MISCode != value)
                {
                    _MISCode = value;
                    OnPropertyChanged(() => MISCode);
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

        public Nullable<double> CollateralRecoverableAmt
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

        public Nullable<double> ODLimit
        {
            get { return _ODLimit; }
            set
            {
                if (_ODLimit != value)
                {
                    _ODLimit = value;
                    OnPropertyChanged(() => ODLimit);
                }
            }
        }

        public int? Period
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

        public int? Year
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

        public string SubClassification
        {
            get { return _SubClassification; }
            set
            {
                if (_SubClassification != value)
                {
                    _SubClassification = value;
                    OnPropertyChanged(() => SubClassification);
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

        //public string ForbearanceFlag
        //{
        //    get { return _ForbearanceFlag; }
        //    set
        //    {
        //        if (_ForbearanceFlag != value)
        //        {
        //            _ForbearanceFlag = value;
        //            OnPropertyChanged(() => ForbearanceFlag);
        //        }
        //    }
        //}

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


        class LoanCommitmentValidator : AbstractValidator<LoanCommitment>
        {
            public LoanCommitmentValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanCommitmentValidator();
        }
    }
}
