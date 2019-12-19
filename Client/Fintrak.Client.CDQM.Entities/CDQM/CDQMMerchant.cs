using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.CDQM.Entities
{
    public class CDQMMerchant : ObjectBase
    {
        int _MerchantId;
        string _SN;
        string _BranchName;
        string _AccountOfficerCode;
        string _TeamCode;
        string _SBU;
        string _MerchantCode;
        string _MerchantName;
        string _ContactTitle;
        string _ContactName;
        string _MobilePhone;
        string _Email;
        string _PhysicalAddr;
        string _TerminalID;
        string _BankCode;
        string _BankAccNo;
        string _BankAccType;
        string _BusinessOccupationCode;
        string _MerchantCategoryCode;
        string _StateCode;
        string _VisaAcquireIDNumber;
        string _VerveAcquireIDNumber;
        string _MasterCardAcquireIDNumber;
        string _TerminalOwnerCode;
        string _LGALCDA;
        string _PTASP;
        bool _Active;

        public int MerchantId
        {
            get { return _MerchantId; }
            set
            {
                if (_MerchantId != value)
                {
                    _MerchantId = value;
                    OnPropertyChanged(() => MerchantId);
                }
            }
        }

        public string SN
        {
            get { return _SN; }
            set
            {
                if (_SN != value)
                {
                    _SN = value;
                    OnPropertyChanged(() => SN);
                }
            }
        }

        public string BranchName
        {
            get { return _BranchName; }
            set
            {
                if (_BranchName != value)
                {
                    _BranchName = value;
                    OnPropertyChanged(() => BranchName);
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

        public string SBU
        {
            get { return _SBU; }
            set
            {
                if (_SBU != value)
                {
                    _SBU = value;
                    OnPropertyChanged(() => SBU);
                }
            }
        }

        public string MerchantCode
        {
            get { return _MerchantCode; }
            set
            {
                if (_MerchantCode != value)
                {
                    _MerchantCode = value;
                    OnPropertyChanged(() => MerchantCode);
                }
            }
        }

        public string MerchantName
        {
            get { return _MerchantName; }
            set
            {
                if (_MerchantName != value)
                {
                    _MerchantName = value;
                    OnPropertyChanged(() => MerchantName);
                }
            }
        }

        public string ContactTitle
        {
            get { return _ContactTitle; }
            set
            {
                if (_ContactTitle != value)
                {
                    _ContactTitle = value;
                    OnPropertyChanged(() => ContactTitle);
                }
            }
        }

        public string ContactName
        {
            get { return _ContactName; }
            set
            {
                if (_ContactName != value)
                {
                    _ContactName = value;
                    OnPropertyChanged(() => ContactName);
                }
            }
        }

        public string MobilePhone
        {
            get { return _MobilePhone; }
            set
            {
                if (_MobilePhone != value)
                {
                    _MobilePhone = value;
                    OnPropertyChanged(() => MobilePhone);
                }
            }
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public string PhysicalAddr
        {
            get { return _PhysicalAddr; }
            set
            {
                if (_PhysicalAddr != value)
                {
                    _PhysicalAddr = value;
                    OnPropertyChanged(() => PhysicalAddr);
                }
            }
        }

        public string TerminalID
        {
            get { return _TerminalID; }
            set
            {
                if (_TerminalID != value)
                {
                    _TerminalID = value;
                    OnPropertyChanged(() => TerminalID);
                }
            }
        }

        public string BankCode
        {
            get { return _BankCode; }
            set
            {
                if (_BankCode != value)
                {
                    _BankCode = value;
                    OnPropertyChanged(() => BankCode);
                }
            }
        }

        public string BankAccNo
        {
            get { return _BankAccNo; }
            set
            {
                if (_BankAccNo != value)
                {
                    _BankAccNo = value;
                    OnPropertyChanged(() => BankAccNo);
                }
            }
        }

        public string BankAccType
        {
            get { return _BankAccType; }
            set
            {
                if (_BankAccType != value)
                {
                    _BankAccType = value;
                    OnPropertyChanged(() => BankAccType);
                }
            }
        }

        public string BusinessOccupationCode
        {
            get { return _BusinessOccupationCode; }
            set
            {
                if (_BusinessOccupationCode != value)
                {
                    _BusinessOccupationCode = value;
                    OnPropertyChanged(() => BusinessOccupationCode);
                }
            }
        }

        public string MerchantCategoryCode
        {
            get { return _MerchantCategoryCode; }
            set
            {
                if (_MerchantCategoryCode != value)
                {
                    _MerchantCategoryCode = value;
                    OnPropertyChanged(() => MerchantCategoryCode);
                }
            }
        }

        public string StateCode
        {
            get { return _StateCode; }
            set
            {
                if (_StateCode != value)
                {
                    _StateCode = value;
                    OnPropertyChanged(() => StateCode);
                }
            }
        }

        public string VisaAcquireIDNumber
        {
            get { return _VisaAcquireIDNumber; }
            set
            {
                if (_VisaAcquireIDNumber != value)
                {
                    _VisaAcquireIDNumber = value;
                    OnPropertyChanged(() => VisaAcquireIDNumber);
                }
            }
        }

        public string VerveAcquireIDNumber
        {
            get { return _VerveAcquireIDNumber; }
            set
            {
                if (_VerveAcquireIDNumber != value)
                {
                    _VerveAcquireIDNumber = value;
                    OnPropertyChanged(() => VerveAcquireIDNumber);
                }
            }
        }

        public string MasterCardAcquireIDNumber
        {
            get { return _MasterCardAcquireIDNumber; }
            set
            {
                if (_MasterCardAcquireIDNumber != value)
                {
                    _MasterCardAcquireIDNumber = value;
                    OnPropertyChanged(() => MasterCardAcquireIDNumber);
                }
            }
        }

        public string TerminalOwnerCode
        {
            get { return _TerminalOwnerCode; }
            set
            {
                if (_TerminalOwnerCode != value)
                {
                    _TerminalOwnerCode = value;
                    OnPropertyChanged(() => TerminalOwnerCode);
                }
            }
        }

        public string LGALCDA
        {
            get { return _LGALCDA; }
            set
            {
                if (_LGALCDA != value)
                {
                    _LGALCDA = value;
                    OnPropertyChanged(() => LGALCDA);
                }
            }
        }

        public string PTASP
        {
            get { return _PTASP; }
            set
            {
                if (_PTASP != value)
                {
                    _PTASP = value;
                    OnPropertyChanged(() => PTASP);
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


        class CDQMMerchantValidator : AbstractValidator<CDQMMerchant>
        {
            public CDQMMerchantValidator()
            {

            }
        }

        protected override IValidator GetValidator()
        {
            return new CDQMMerchantValidator();
        }
    }
}
