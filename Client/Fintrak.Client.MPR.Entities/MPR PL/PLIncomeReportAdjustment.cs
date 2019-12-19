using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PLIncomeReportAdjustment : ObjectBase
    {
        int _Id;
        string _TeamCode;
        string _AccountOfficerCode;
        string _Narrative;
        string _BranchCode;
        string _GLCode;
        string _Caption;
        string _RelatedAccount;
        double _Amount;
        DateTime _RunDate;
        string _CompanyCode;
        string _Code;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
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


        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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

        
        class PLIncomeReportAdjustmentValidator : AbstractValidator<PLIncomeReportAdjustment>
        {
            public PLIncomeReportAdjustmentValidator()
            {
                RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new PLIncomeReportAdjustmentValidator();
        }
    }
}
