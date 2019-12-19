using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PLIncomeReport : ObjectBase
    {
        int _Id;
        string _TransId;
        DateTime _TransDate;
        string _Narrative;
        string _BranchCode;
        string _TeamCode;
        string _AccountOfficerCode;
        string _GLCode;
        string _Caption;
        string _GLAccount;
        string _CustCode;
        string _ProductCode;
        string _AccountTitle;
        string _RelatedAccount;
        decimal _Amount;
        int _Period;
        string _Year;
        DateTime _martdate;
        DateTime _Rundate;
        string _EntryStatus;
        string _StaffID;
        string _CompanyCode;
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

        public decimal Amount
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

        public string CustCode
        {
            get { return _CustCode; }
            set
            {
                if (_CustCode != value)
                {
                    _CustCode = value;
                    OnPropertyChanged(() => CustCode);
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

        public string StaffID
        {
            get { return _StaffID; }
            set
            {
                if (_StaffID != value)
                {
                    _StaffID = value;
                    OnPropertyChanged(() => StaffID);
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

        public int Period
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

        public string Year
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

        public DateTime martdate
        {
            get { return _martdate; }
            set
            {
                if (_martdate != value)
                {
                    _martdate = value;
                    OnPropertyChanged(() => martdate);
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

        
        class PLIncomeReportValidator : AbstractValidator<PLIncomeReport>
        {
            public PLIncomeReportValidator()
            {
                
             }
        }

        protected override IValidator GetValidator()
        {
            return new PLIncomeReportValidator();
        }
    }
}
