using System;
using System.Linq;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.Basic.Entities
{
    public class Revenue : ObjectBase
    {
        int _RevenueId;
        string _TransId;
        DateTime _TransDate;
        string _Narrative;
        string _TeamCode;
        string _AccountOfficerCode;
        string _BranchCode;
        string _GLCode;
        string _GLAccount;
        string _GLDescription;
        string _Caption;
        string _RelatedAccount;
        string _AccountTitle;
        string _Indicator;
        decimal _Amount_LCY;
        DateTime _RunDate;
        string _CompanyCode;
        bool _Active;

        public int RevenueId
        {
            get { return _RevenueId; }
            set
            {
                if (_RevenueId != value)
                {
                    _RevenueId = value;
                    OnPropertyChanged(() => RevenueId);
                }
            }
        }

        public string TransId
        {
            get { return _TransId; }
            set
            {
                if (_TransId != value)
                {
                    _TransId = value;
                    OnPropertyChanged(() => TransId);
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

        public string Indicator
        {
            get { return _Indicator; }
            set
            {
                if (_Indicator != value)
                {
                    _Indicator = value;
                    OnPropertyChanged(() => Indicator);
                }
            }
        }

        public string AccountTitle
        {
            get { return _AccountTitle; }
            set
            {
                if (_AccountTitle != value)
                {
                    _AccountTitle = value;
                    OnPropertyChanged(() => AccountTitle);
                }
            }
        }

        public string GLDescription
        {
            get { return _GLDescription; }
            set
            {
                if (_GLDescription != value)
                {
                    _GLDescription = value;
                    OnPropertyChanged(() => GLDescription);
                }
            }
        }
        public string GLAccount
        {
            get { return _GLAccount; }
            set
            {
                if (_GLAccount != value)
                {
                    _GLAccount = value;
                    OnPropertyChanged(() => GLAccount);
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


        public string Narrative
        {
            get { return _Narrative; }
            set
            {
                if (_Narrative != value)
                {
                    _Narrative = value;
                    OnPropertyChanged(() => Narrative);
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


        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public string RelatedAccount
        {
            get { return _RelatedAccount; }
            set
            {
                if (_RelatedAccount != value)
                {
                    _RelatedAccount = value;
                    OnPropertyChanged(() => RelatedAccount);
                }
            }
        }

        public decimal Amount_LCY
        {
            get { return _Amount_LCY; }
            set
            {
                if (_Amount_LCY != value)
                {
                    _Amount_LCY = value;
                    OnPropertyChanged(() => Amount_LCY);
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


        class RevenueValidator : AbstractValidator<Revenue>
        {
            public RevenueValidator()
            {
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new RevenueValidator();
        }
    }
}