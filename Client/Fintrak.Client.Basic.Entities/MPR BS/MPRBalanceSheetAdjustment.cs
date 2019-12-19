using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class MPRBalanceSheetAdjustment : ObjectBase
    {
        int _BalancesheetAdjustmentId;
        string _AccountNo;
        string _AccountName;
        string _TeamCode;
        string _AccountOfficerCode;
        string _ProductCode;
        string _Category;
        string _CurrencyType;
        string _CompanyCode;
        double _ActualBal;
        double _AverageBal;
        decimal _Interest;
        DateTime _Rundate; 
        bool _Active;

        public int BalancesheetAdjustmentId
        {
            get { return _BalancesheetAdjustmentId; }
            set
            {
                if (_BalancesheetAdjustmentId != value)
                {
                    _BalancesheetAdjustmentId = value;
                    OnPropertyChanged(() => BalancesheetAdjustmentId);
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

        public double ActualBal
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

        public double AverageBal
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

        
        class MPRBalanceSheetAdjustmentValidator : AbstractValidator<MPRBalanceSheetAdjustment>
        {
            public MPRBalanceSheetAdjustmentValidator()
            {
                RuleFor(obj => obj.AccountNo).NotEmpty().WithMessage("AccountNo is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new MPRBalanceSheetAdjustmentValidator();
        }
    }
}
