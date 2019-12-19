using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class TrialBalanceConsolidated : ObjectBase
    {
        int _TrialBalanceId;
        string _BranchCode;
        string _GLCode;
        string _Description;
        string _GLSubHeadCode;
        string _Currency;
        double _ExchangeRate;
        decimal _Debit;
        decimal _Credit;
        decimal _LCY_Debit;
        decimal _LCY_Credit;
        decimal _LCY_Balance;
        decimal _Balance;
        string _GLType;
        decimal _RevaluationDiff;
        DateTime _TransDate;
        string _CompanyCode;
        string _SubGL;
        bool _Active;

        public int TrialBalanceId
        {
            get { return _TrialBalanceId; }
            set
            {
                if (_TrialBalanceId != value)
                {
                    _TrialBalanceId = value;
                    OnPropertyChanged(() => TrialBalanceId);
                }
            }
        }

        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
                }
            }
        }

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public string GLSubHeadCode
        {
            get { return _GLSubHeadCode; }
            set
            {
                if (_GLSubHeadCode != value)
                {
                    _GLSubHeadCode = value;
                    OnPropertyChanged(() => GLSubHeadCode);
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

        public decimal Debit
        {
            get { return _Debit; }
            set
            {
                if (_Debit != value)
                {
                    _Debit = value;
                    OnPropertyChanged(() => Debit);
                }
            }
        }

        public decimal Credit
        {
            get { return _Credit; }
            set
            {
                if (_Credit != value)
                {
                    _Credit = value;
                    OnPropertyChanged(() => Credit);
                }
            }
        }

        public decimal LCY_Debit
        {
            get { return _LCY_Debit; }
            set
            {
                if (_LCY_Debit != value)
                {
                    _LCY_Debit = value;
                    OnPropertyChanged(() => LCY_Debit);
                }
            }
        }

        public decimal LCY_Credit
        {
            get { return _LCY_Credit; }
            set
            {
                if (_LCY_Credit != value)
                {
                    _LCY_Credit = value;
                    OnPropertyChanged(() => LCY_Credit);
                }
            }
        }

        public decimal LCY_Balance
        {
            get { return _LCY_Balance; }
            set
            {
                if (_LCY_Balance != value)
                {
                    _LCY_Balance = value;
                    OnPropertyChanged(() => LCY_Balance);
                }
            }
        }

        public decimal Balance
        {
            get { return _Balance; }
            set
            {
                if (_Balance != value)
                {
                    _Balance = value;
                    OnPropertyChanged(() => Balance);
                }
            }
        }


        public string GLType
        {
            get { return _GLType; }
            set
            {
                if (_GLType != value)
                {
                    _GLType = value;
                    OnPropertyChanged(() => GLType);
                }
            }
        }


      


        public decimal RevaluationDiff
        {
            get { return _RevaluationDiff; }
            set
            {
                if (_RevaluationDiff != value)
                {
                    _RevaluationDiff = value;
                    OnPropertyChanged(() => RevaluationDiff);
                }
            }
        }

          

        public DateTime TransDate
        {
            get { return _TransDate; }
            set
            {
                if (_TransDate != value)
                {
                    _TransDate = value;
                    OnPropertyChanged(() => TransDate);
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

        public string SubGL
        {
            get { return _SubGL; }
            set
            {
                if (_SubGL != value)
                {
                    _SubGL = value;
                    OnPropertyChanged(() => SubGL);
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




        class TrialBalanceConsolidatedValidator : AbstractValidator<TrialBalanceConsolidated>
        {
            public TrialBalanceConsolidatedValidator()
            {
                
            }
        }

        protected override IValidator GetValidator()
        {
            return new TrialBalanceConsolidatedValidator();
        }
    }
}
