using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LoanPryMoratorium : ObjectBase
    {
        int _LoanPryMoratoriumId;
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
        double _FixedPrincipalAmount;      
        decimal _ExchangeRate;
        decimal _Rate;
        int _Tenor;
        int _InterestRepayFreq;
        int _PrincipalRepayFreq;
       // string _Schedule_Type;
        DateTime _FirstPrinRepaymentdate;
        DateTime _FirstIntrestRepaymentdate;
        string _CompanyCode;
        bool _Active;


        public int LoanPryMoratoriumId

        {
            get { return _LoanPryMoratoriumId; }
            set
            {
                if (_LoanPryMoratoriumId != value)
                {
                    _LoanPryMoratoriumId = value;
                    OnPropertyChanged(() => LoanPryMoratoriumId);
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
        public double FixedPrincipalAmount
        {
            get { return _FixedPrincipalAmount; }
            set
            {
                if (_FixedPrincipalAmount != value)
                {
                    _FixedPrincipalAmount = value;
                    OnPropertyChanged(() => FixedPrincipalAmount);
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

        public DateTime FirstPrinRepaymentdate
        {
            get { return _FirstPrinRepaymentdate; }
            set
            {
                if (_FirstPrinRepaymentdate != value)
                {
                    _FirstPrinRepaymentdate = value;
                    OnPropertyChanged(() => FirstPrinRepaymentdate);
                }
            }
        }

        public DateTime FirstIntrestRepaymentdate
        {
            get { return _FirstIntrestRepaymentdate; }
            set
            {
                if (_FirstIntrestRepaymentdate != value)
                {
                    _FirstIntrestRepaymentdate = value;
                    OnPropertyChanged(() => FirstIntrestRepaymentdate);
                }
            }
        }

        public int Tenor
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

        //public string Schedule_Type
        //{
        //    get { return _Schedule_Type; }
        //    set
        //    {
        //        if (_Schedule_Type != value)
        //        {
        //            _Schedule_Type = value;
        //            OnPropertyChanged(() => Schedule_Type);
        //        }
        //    }
        //}

  
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
        class LoanPryMoratoriumValidator : AbstractValidator<LoanPryMoratorium>
        {
            public LoanPryMoratoriumValidator()
            {
                RuleFor(obj => obj.RefNo).NotEmpty().WithMessage("RefNo is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new LoanPryMoratoriumValidator();
        }
    }
}
