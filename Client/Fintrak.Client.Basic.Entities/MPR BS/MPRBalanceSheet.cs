using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRBalanceSheet : ObjectBase
    {
        int _BalanceSheetId;
        string _AccountNo;
        string _AccountName;
        string _TeamCode;
        string _AccountOfficerCode;
        string _CaptionName;
        string _BranchCode;
        string _ProductCode;
        string _Category;
        string _CurrencyType;
        string _Currency;    
        decimal _ActualBal;
        decimal _AverageBal;
        decimal _Interest;
        double _EffIntRate;
        decimal _Pool;
        double _PoolRate;
        double _ContractRate;
        string _VolumeGL;
        string _InterestGL;
        string _EntryStatus;
        double _MaxRate;
        decimal _PenalCharge;
        double _PenalRate;
        string _AcctStatus;
        string _CreditRating;
        DateTime _Rundate;
        string _CompanyCode;
        bool _Active;

        public int BalanceSheetId
        {
            get { return _BalanceSheetId; }
            set
            {
                if (_BalanceSheetId != value)
                {
                    _BalanceSheetId = value;
                    OnPropertyChanged(() => BalanceSheetId);
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

        public string AccountName
        {
            get { return _AccountName; }
            set
            {
                if (_AccountName != value)
                {
                    _AccountName = value;
                    OnPropertyChanged(() => AccountName);
                }
            }
        }

        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
                }
            }
        }

        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }

        public string CaptionName
        {
            get { return _CaptionName; }
            set
            {
                if (_CaptionName != value)
                {
                    _CaptionName = value;
                    OnPropertyChanged(() => CaptionName);
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


        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        public string CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => CurrencyType);
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

        public decimal ActualBal
        {
            get { return _ActualBal; }
            set
            {
                if (_ActualBal != value)
                {
                    _ActualBal = value;
                    OnPropertyChanged(() => ActualBal);
                }
            }
        }

        public decimal AverageBal
        {
            get { return _AverageBal; }
            set
            {
                if (_AverageBal != value)
                {
                    _AverageBal = value;
                    OnPropertyChanged(() => AverageBal);
                }
            }
        }


        public decimal Interest
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

        public double EffIntRate
        {
            get { return _EffIntRate; }
            set
            {
                if (_EffIntRate != value)
                {
                    _EffIntRate = value;
                    OnPropertyChanged(() => EffIntRate);
                }
            }
        }

        public decimal Pool
        {
            get { return _Pool; }
            set
            {
                if (_Pool != value)
                {
                    _Pool = value;
                    OnPropertyChanged(() => Pool);
                }
            }
        }


        public double PoolRate
        {
            get { return _PoolRate; }
            set
            {
                if (_PoolRate != value)
                {
                    _PoolRate = value;
                    OnPropertyChanged(() => PoolRate);
                }
            }
        }


        public double ContractRate
        {
            get { return _ContractRate; }
            set
            {
                if (_ContractRate != value)
                {
                    _ContractRate = value;
                    OnPropertyChanged(() => ContractRate);
                }
            }
        }


        public string VolumeGL
        {
            get { return _VolumeGL; }
            set
            {
                if (_VolumeGL != value)
                {
                    _VolumeGL = value;
                    OnPropertyChanged(() => VolumeGL);
                }
            }
        }

        public string InterestGL
        {
            get { return _InterestGL; }
            set
            {
                if (_InterestGL != value)
                {
                    _InterestGL = value;
                    OnPropertyChanged(() => InterestGL);
                }
            }
        }

        public string EntryStatus
        {
            get { return _EntryStatus; }
            set
            {
                if (_EntryStatus != value)
                {
                    _EntryStatus = value;
                    OnPropertyChanged(() => EntryStatus);
                }
            }
        }

        public double MaxRate
        {
            get { return _MaxRate; }
            set
            {
                if (_MaxRate != value)
                {
                    _MaxRate = value;
                    OnPropertyChanged(() => MaxRate);
                }
            }
        }


        public decimal PenalCharge
        {
            get { return _PenalCharge; }
            set
            {
                if (_PenalCharge != value)
                {
                    _PenalCharge = value;
                    OnPropertyChanged(() => PenalCharge);
                }
            }
        }


        public double PenalRate
        {
            get { return _PenalRate; }
            set
            {
                if (_PenalRate != value)
                {
                    _PenalRate = value;
                    OnPropertyChanged(() => PenalRate);
                }
            }
        }

        public string AcctStatus
        {
            get { return _AcctStatus; }
            set
            {
                if (_AcctStatus != value)
                {
                    _AcctStatus = value;
                    OnPropertyChanged(() => AcctStatus);
                }
            }
        }

        public string CreditRating
        {
            get { return _CreditRating; }
            set
            {
                if (_CreditRating != value)
                {
                    _CreditRating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }

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

        
        class MPRBalanceSheetValidator : AbstractValidator<MPRBalanceSheet>
        {
            public MPRBalanceSheetValidator()
            {
                
             }
        }

        protected override IValidator GetValidator()
        {
            return new MPRBalanceSheetValidator();
        }
    }
}
